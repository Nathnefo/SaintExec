// dllmain.cpp : Définit le point d'entrée de l'application DLL.
#include "pch.h"
#include <iostream>
#include <fstream>
#include <string>
#include <vector>
#include "lua/lua.hpp"
#include "MinHook.h"
#include <Psapi.h>


void shutdown(FILE* fp, std::string reason);
int __cdecl printc(lua_State* L);


bool DUMP_LUA = false;
bool PATTERN_SCAN = true;
bool DEBUG = true;



// store all information for a function we need to hook
template <typename funcT>
struct functionh {
public:
    std::string name = "";

    // Hook informations
    LPVOID p_detour = nullptr;
    funcT original = nullptr;
    funcT target = nullptr;

    // Pointer (Epic games version)
    int module_offset = 0;

    // Patterscan informations
    const char* pattern = "";
    const char* mask = "";
    int pattern_offset = 0;

    void create(std::string nam, int module_offt, const char* pattrn, const char* msk, int pattrn_offset, LPVOID pdetour)
    {
        name = nam;
        module_offset = module_offt;
        pattern = pattrn;
        mask = msk;
        pattern_offset = pattrn_offset;
        p_detour = pdetour;
    }
};

#pragma region typedef
// function who need a hook
typedef __int64(__fastcall* luaL_loadbuffer_def)(lua_State* L, const char* buffer, size_t size, const char* name);
functionh<luaL_loadbuffer_def> floadbuffer;

//typedef __int64(__fastcall* lua_getfield_def)(lua_State* L, __int64 idx, const char* k);
//functionh<lua_getfield_def> flua_getfield;

typedef __int64(__fastcall* lua_setfield_def)(lua_State* L, int idx, const char* k);
functionh<lua_setfield_def> fsetfield;

typedef __int64(__fastcall* lua_pcall_def)(lua_State* L, int nargs, int nresults, int msgh);
functionh<lua_pcall_def> flua_pcall;

// function who just need to be call
typedef __int64(__fastcall* lua_pushcclosure_def)(lua_State* L, lua_CFunction fn, int n);
lua_pushcclosure_def  lua_pushcclosureOriginal = nullptr;

typedef lua_State* (__fastcall* sr_lua_get_state_by_name_def)(const char* id);
sr_lua_get_state_by_name_def sr_lua_get_state_by_nameOriginal = nullptr;

typedef __int64(__fastcall* lua_gettop_def)(lua_State* L);
lua_gettop_def  lua_gettopOriginal = nullptr;

typedef const char* (*lua_tolstring_def)(lua_State* L, int idx, size_t* len);
lua_tolstring_def  lua_tolstringOriginal = nullptr;
#pragma endregion

// array of functionh
std::vector<uintptr_t*> v_funcs;

HINSTANCE DllHandle;
intptr_t moduleBase;
DWORD sizemoduleBase;

lua_State* L_interface;
lua_State* L_gameplay;

std::vector<std::string> filenames;

#pragma region detour functions
__int64 loadbuffer_d(lua_State* L, const char* buffer, size_t size, const char* name)
{

    if (DUMP_LUA)
    {
        std::cout << "file dump  : " << name << std::endl;
        std::string sname = name;
        if (std::find(filenames.begin(), filenames.end(), sname) != filenames.end())
        {
            sname = sname + "_bis";
        }
        else {
            filenames.push_back(sname);
        }

        std::string filename = "./luadump/" + sname;
        std::ofstream MyFile(filename);

        MyFile << buffer;

        MyFile.close();
    }
    return floadbuffer.original(L, buffer, size, name);
}

/*
__int64 lua_getfield_d(lua_State* L, __int64 idx, const char* k)
{
    return flua_getfield.original(L, idx, k);
}*/


bool firstsetfield_i = true;
bool firstsetfield_g = true;
__int64 lua_setfield_d(lua_State* L, int idx, const char* k)
{
    if (firstsetfield_i)
    {
        L_interface = sr_lua_get_state_by_nameOriginal("interface");
        if (L_interface != 0)
        {
            std::cout << "Lua state Interface : " << L_interface << std::endl;
            firstsetfield_i = false;
            std::cout << "register printc ..." << std::endl;
            __int64 status = lua_pushcclosureOriginal(L_interface, &printc, 0);
            fsetfield.original(L_interface, LUA_GLOBALSINDEX, "printc");
            if (status != 0) {
                std::cout << "Error status lua_pushcclosure  : " << status << std::endl;
            }
        }
    }
    if (firstsetfield_g)
    {
        L_gameplay = sr_lua_get_state_by_nameOriginal("game play");

        if (L_gameplay != 0)
        {
            std::cout << "Lua state Gameplay : " << L_gameplay << std::endl;
            firstsetfield_g = false;
            std::cout << "register printc ..." << std::endl;
            __int64 status = lua_pushcclosureOriginal(L_gameplay, &printc, 0);
            fsetfield.original(L_gameplay, LUA_GLOBALSINDEX, "printc");
            if (status != 0) {
                std::cout << "Error status lua_pushcclosure  : " << status << std::endl;
            }

        }
    }

    return fsetfield.original(L, idx, k);
}

__int64 lua_pcall_d(lua_State* L, int nargs, int nresults, int msgh)
{
    return flua_pcall.original(L, nargs, nresults, msgh);
}
#pragma endregion


int __cdecl printc(lua_State* L)
{
    int n = lua_gettopOriginal(L);

    /*
    if (n <= 0)
    {
        std::cout << "Error : printc has 0 argument" << std::endl;
        return 0;
    }*/
    for (int i = 1; i <= n; i++)
    {
        std::cout << lua_tolstringOriginal(L, i, 0);
    }
    std::cout << std::endl;
    return 0;
}

bool execute_lua(lua_State* L, std::string L_name, const char* code, const char* code_name)
{
    if (L == NULL) {
        std::cout << "Error : luastate " + L_name + " is null" << std::endl;
        return false;
    }

    //reproduce the behavior of luaL_dostring
    __int64 status_b = floadbuffer.original(L, code, strlen(code), code_name);
    if (status_b != 0) {
        std::cout << "Error :" << std::endl;
        std::cout << "status load_buffer  : " << status_b << " (syntax)" << std::endl;
        return false;
    }
    __int64 status_p = flua_pcall.original(L, 0, LUA_MULTRET, 0);
    if (status_p != 0)
    {
        std::cout << "Error :" << std::endl;
        std::cout << "status lua_pcall  : " << status_p << " (runtime)" << std::endl;
        return false;
    }
    return true;
}
bool firstexecute_g = true;

void execute_lua_with_globals(lua_State* L, std::string L_name, const char* code, const char* code_name)
{
    bool can_be_executed = true;
    if (L_name == "interface") { // idk why but interface is locking itself everytime a script is executed
        can_be_executed = execute_lua(L, "interface", "if pcall(_PrepareForDynamicGlobals, '') then\n printc('PrepareForDynamicGlobals success')\nend", "preparefdg.lua");
    }
    if (firstexecute_g and L_name == "gameplay") {
        firstexecute_g = false;
        can_be_executed = execute_lua(L, "gameplay", "if pcall(_PrepareForDynamicGlobals, '') then\n printc('PrepareForDynamicGlobals success')\nend", "preparefdg.lua");
    }
    if (can_be_executed) {
        if (execute_lua(L, L_name, code, code_name)) {
            std::cout << "Execution Success !" << std::endl;
        }
    }

}

void execute_lua_file(lua_State* L, std::string L_name, std::string file_name)
{
    if (L == NULL) {
        std::cout << "Error : luastate " + L_name + " is null" << std::endl;
        return;
    }
    //reproduce luaL_dostring
    std::fstream myfile(file_name);

    if (myfile.is_open()) {
        std::string code;
        std::string line;
        while (getline(myfile, line))
        {
            code += line + "\n";
        }
        if (code == "") {
            std::cout << "Error: no code in " << file_name << std::endl;
            return;
        }
        // allow to create globals
        if (L_name == "interface") { // idk why but interface is locking itself everytime a script is executed
            execute_lua(L, "interface", "if pcall(_PrepareForDynamicGlobals, '') then\n printc('PrepareForDynamicGlobals success')\nend", "preparefdg.lua");
        }
        if (firstexecute_g and L_name == "gameplay") {
            firstexecute_g = false;
            execute_lua(L, "gameplay", "if pcall(_PrepareForDynamicGlobals, '') then\n printc('PrepareForDynamicGlobals success')\nend", "preparefdg.lua");
        }
        execute_lua(L, L_name, code.c_str(), file_name.c_str());
    }
    else
    {
        std::cout << "Error: failed to open " << file_name << std::endl;
    }
}

bool GetModuleInfo()
{
    MODULEINFO modInfo = { 0 };
    HMODULE hModulea = GetModuleHandle(L"sr_hv.exe");
    if (GetModuleInformation(GetCurrentProcess(), hModulea, &modInfo, sizeof(MODULEINFO)) == NULL)
    {
        return true;
    }
    moduleBase = (intptr_t)modInfo.lpBaseOfDll;
    sizemoduleBase = (DWORD)modInfo.SizeOfImage;
    if (moduleBase == 0 or sizemoduleBase == 0) {
        return true;
    }
    return false;
}


intptr_t FindPattern(const char* pattern, const char* mask, int offset)
{
    size_t patternLen = strlen(mask);
    for (int i = 0; i < sizemoduleBase - patternLen; i++)
    {

        bool found = true;
        for (int j = 0; j < patternLen; j++)
        {
            found &= mask[j] == '?' || pattern[j] == *(char*)(moduleBase + i + j);
            if (!found) {
                break;
            }
        }


        if (found) {
            return moduleBase + i + offset;
        }
    }
    return 0;
}


void InitializeFunctions()
{

    fsetfield.create("lua_setfield", 0xCF9860, "\x75\x00\x49\x8B\xD2\x48\x8B\xCB\xE8\x00\x00\x00\x00\x4C\x8B\x4B\x10\x4C\x8D\x44\x24\x20\x49", "x?xxxxxxx????xxxxxxxxxx", -40, lua_setfield_d);
    floadbuffer.create("loadbuffer", 0xCFADD0, "\x48\x83\xEC\x00\x48\x89\x54\x24\x20\x48\x8D", "xxx?xxxxxxx", 0, loadbuffer_d);
    flua_pcall.create("lua_pcall", 0xCF9080, "\x48\x89\x5C\x24\x08\x57\x48\x83\xEC\x00\x41\x8B\xF8\x44", "xxxxxxxxx?xxxx", 0, lua_pcall_d);
    //flua_getfield.create("lua_getfield", 0xCF8BB0, "\x48\x89\x5C\x24\x08\x57\x48\x83\xEC\x00\x4D\x8B", "xxxxxxxxx?xx", 0, lua_getfield_d);

    v_funcs.push_back(reinterpret_cast<uintptr_t*>(&fsetfield));
    v_funcs.push_back(reinterpret_cast<uintptr_t*>(&floadbuffer));
    v_funcs.push_back(reinterpret_cast<uintptr_t*>(&flua_pcall));
    //v_funcs.push_back(reinterpret_cast<uintptr_t*>(&flua_getfield));;
}
#define PipeName "\\\\.\\pipe\\SRLUAAA"

char buffer[999999];
int get_buffer_pipe() {

    HANDLE hPipe;
    DWORD dwRead;
    hPipe = CreateNamedPipe(TEXT(PipeName),
        PIPE_ACCESS_DUPLEX,
        PIPE_TYPE_MESSAGE | PIPE_READMODE_MESSAGE | PIPE_WAIT,
        1,
        100,
        100,
        NMPWAIT_USE_DEFAULT_WAIT,
        NULL);
    while (hPipe != INVALID_HANDLE_VALUE)
    {
        if (ConnectNamedPipe(hPipe, NULL) != FALSE)
        {
            while (ReadFile(hPipe, buffer, sizeof(buffer) - 1, &dwRead, NULL) != FALSE)
            {
                buffer[dwRead] = '\0';

                if (strlen(buffer) != NULL) {
                    DisconnectNamedPipe(hPipe);
                    CloseHandle(hPipe);
                    return 0;
                }
            }
        }

        DisconnectNamedPipe(hPipe);
    }
    CloseHandle(hPipe);
    return 1;
}

bool GetAddresses(FILE* fp)
{


    if (PATTERN_SCAN)
    {
        lua_tolstringOriginal = (lua_tolstring_def)(FindPattern("\x48\x89\x74\x24\x10\x57\x48\x83\xEC\x00\x49\x8B\xD8\x8B\xF2\x48\x8B\xF9\xE8\x00\x00\x00\x00\x4C", "xxxxxxxxx?xxxxxxxxx????x", -5));

        if (lua_tolstringOriginal == 0) {
            shutdown(fp, "Pattern Scan failed on the function : lua_tolstringOriginal");
            return true;
        }
        lua_gettopOriginal = (lua_gettop_def)(FindPattern("\x48\x8B\x41\x10\x48\x2B\x41", "xxxxxxx", 0));
        if (lua_gettopOriginal == 0) {
            shutdown(fp, "Pattern Scan failed on the function : lua_tolstringOriginal");
            return true;
        }
        lua_pushcclosureOriginal = (lua_pushcclosure_def)(FindPattern("\x48\x89\x5C\x24\x08\x48\x89\x74\x24\x10\x57\x48\x83\xEC\x00\x4C\x8B\x49\x20\x48\x8B\xF2", "xxxxxxxxxxxxxx?xxxxxxx", 0));
        if (lua_pushcclosureOriginal == 0) {
            shutdown(fp, "Pattern Scan failed on the function : lua_pushcclosureOriginal");
            return true;
        }
        sr_lua_get_state_by_nameOriginal = (sr_lua_get_state_by_name_def)(FindPattern("\x48\x89\x5C\x24\x08\x48\x89\x7C\x24\x10\x48\x8B\xD9", "xxxxxxxxxxxxx", 0));
        if (sr_lua_get_state_by_nameOriginal == 0) {
            shutdown(fp, "Pattern Scan failed on the function : sr_lua_get_state_by_nameOriginal");
            return true;
        }

        for (auto& i : v_funcs)
        {
            functionh<lua_setfield_def>* func = reinterpret_cast<functionh<lua_setfield_def>*>(i);
            intptr_t address = FindPattern(func->pattern, func->mask, func->pattern_offset);
            if (address == 0)
            {
                shutdown(fp, "Pattern Scan failed on the function : " + func->name);
                return true;
            }
            func->target = (lua_setfield_def)(address);
        }

    }
    else
    {
        lua_tolstringOriginal = (lua_tolstring_def)(moduleBase + 0xCF9BB0); //"sr_hv.exe"+0xCF9130
        lua_gettopOriginal = (lua_gettop_def)(moduleBase + 0xCF8CB0);
        lua_pushcclosureOriginal = (lua_pushcclosure_def)(moduleBase + 0xCF9130);
        sr_lua_get_state_by_nameOriginal = (sr_lua_get_state_by_name_def)(moduleBase + 0xCF5C50);
        for (auto& i : v_funcs)
        {
            functionh<lua_setfield_def>* func = reinterpret_cast<functionh<lua_setfield_def>*>(i);
            func->target = (lua_setfield_def)(moduleBase + func->module_offset);
        }

    }
    return false;
}

bool CreateHooks(FILE* fp)
{
    for (auto& i : v_funcs)
    {
        functionh<lua_setfield_def>* func = reinterpret_cast<functionh<lua_setfield_def>*>(i);
        if (MH_CreateHook(reinterpret_cast<void**>(func->target), func->p_detour, reinterpret_cast<void**>(&func->original)) != MH_OK) {
            shutdown(fp, func->name + " : CreateHook failed!");
            return true;
        }
    }
    return false;
}

bool EnableHooks(FILE* fp)
{
    for (auto& i : v_funcs)
    {
        functionh<lua_setfield_def>* func = reinterpret_cast<functionh<lua_setfield_def>*>(i);
        if (MH_EnableHook(reinterpret_cast<void**>(func->target)) != MH_OK) {
            shutdown(fp, func->name + " : EnableHook failed!");
            return true;
        }
    }
    return false;
}

#pragma region Dll Utility
DWORD __stdcall EjectThread(LPVOID lpParameter) {
    Sleep(100);
    FreeLibraryAndExitThread(DllHandle, 0);
    return 0;
}

void shutdown(FILE* fp, std::string reason) {

    MH_Uninitialize();
    std::cout << reason << std::endl;
    Sleep(1000);
    if (fp != nullptr)
        fclose(fp);
    FreeConsole();
    CreateThread(0, 0, EjectThread, 0, 0, 0);
    return;
}


#pragma endregion
char script[999999];
DWORD WINAPI Menue(HINSTANCE hModule) {

    FILE* fp = 0;

    if (DEBUG) {
        //setup console, cout and cin
        AllocConsole();
        freopen_s(&fp, "CONOUT$", "w", stdout);
        freopen_s(&fp, "CONIN$", "r", stdin);
    }

    MH_STATUS status = MH_Initialize();

    if (status != MH_OK)
    {
        std::string sStatus = MH_StatusToString(status);
        shutdown(fp, "Minhook init failed!");
        return 0;
    }

    InitializeFunctions();

    std::cout << "Getting functions addresses..." << std::endl;
    if (GetModuleInfo()) {
        shutdown(fp, "GetModuleInfo failed!");
        return 1;
    }

    if (GetAddresses(fp))
    {
        return 1;
    }

    std::cout << "Creating hooks..." << std::endl;
    if (CreateHooks(fp))
    {
        return 1;
    }

    std::cout << "Enabling hooks..." << std::endl;
    if (EnableHooks(fp))
    {
        return 1;
    }

    bool running = true;

    while (running)
    {

        if (get_buffer_pipe() == 0) {
            char header[10];
            memcpy(header, buffer, 9);
            header[9] = '\0';
            if (strcmp(header, ".HOOKSTOP") == 0)
            {
                running = false;
            }
            else if (strcmp(header, ".INTERFAC") == 0)
            {
                memcpy(script, buffer + 9, sizeof(buffer));
                execute_lua_with_globals(L_interface, "interface", script, "code_inj.lua"); //TODO : make a queue
            }
            else if (strcmp(header, ".GAMEPLAY") == 0) {
                memcpy(script, buffer + 9, sizeof(buffer));
                execute_lua_with_globals(L_gameplay, "gameplay", script, "code_inj.lua"); //TODO : make a queue
            }
        }
        else {
            std::cout << "Error : pipe" << std::endl;
        }
    }


    if (false)
    {
        bool running = true;
        while (running) {
            if (GetKeyState(VK_F5) & 0x8000)
            {
                execute_lua_file(L_interface, "interface", "code_injection.lua");
            }
            if (GetKeyState(VK_F6) & 0x8000)
            {
                execute_lua_file(L_interface, "interface", "code_injectionbis.lua");
            }
            if (GetKeyState(VK_F4) & 0x8000)
            {
                execute_lua_file(L_gameplay, "gameplay", "code_injection.lua");
            }
            if (GetKeyState(VK_END) & 0x8000)
            {
                running = false;
            }
            Sleep(100);
        }
    }



    shutdown(fp, "Stopping...");
    return 0;

}

BOOL APIENTRY DllMain(HMODULE hModule, DWORD  ul_reason_for_call, LPVOID lpReserved)
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
        DllHandle = hModule;

        CreateThread(NULL, 0, (LPTHREAD_START_ROUTINE)Menue, NULL, 0, NULL);
    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
    case DLL_PROCESS_DETACH:
        break;
    }
    return TRUE;
}

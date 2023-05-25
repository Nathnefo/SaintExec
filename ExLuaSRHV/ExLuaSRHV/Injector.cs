using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace ExLuaSRHV
{

    class Injector
    {
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out UIntPtr lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);
        [DllImport("kernel32.dll")]
        static extern bool CloseHandle(IntPtr hObject);

        const int PROCESS_CREATE_THREAD = 0x0002;
        const int PROCESS_QUERY_INFORMATION = 0x0400;
        const int PROCESS_VM_OPERATION = 0x0008;
        const int PROCESS_VM_WRITE = 0x0020;
        const int PROCESS_VM_READ = 0x0010;

        const uint MEM_COMMIT = 0x00001000;
        const uint MEM_RESERVE = 0x00002000;
        const uint PAGE_READWRITE = 4;


        public static bool DLLINJECTED = false;

        public static void Wait_for_game(Form1 form)
        {
            form.Set_UI_state("WaitingProcess");

            Process[] ProcessArray = Process.GetProcessesByName("sr_hv");
            while (ProcessArray.Length == 0)
            {
                Thread.Sleep(200);
                ProcessArray = Process.GetProcessesByName("sr_hv");
                if (form.Closing)
                {
                    return;
                }
            }
            Process targetProcess = ProcessArray[0];

            IntPtr procHandle = OpenProcess(PROCESS_CREATE_THREAD | PROCESS_QUERY_INFORMATION | PROCESS_VM_OPERATION | PROCESS_VM_WRITE | PROCESS_VM_READ, false,  targetProcess.Id);

            if(procHandle == IntPtr.Zero)
            {
                MessageBox.Show("Error OpenProcess");
                return;
            }


            IntPtr loadLibraryAddr = GetProcAddress(GetModuleHandle("kernel32.dll"), "LoadLibraryA");

            if (loadLibraryAddr == IntPtr.Zero)
            {
                MessageBox.Show("Error GetProcAddress");
                return;
            }

            string DllPath = System.IO.Directory.GetCurrentDirectory() + "\\HookLuaSr_hv.dll";
            if (!System.IO.File.Exists(DllPath))
            {
                MessageBox.Show("HookLuaSr_hv.dll doesn't exist try to desactivate your antivirus");
                return;
            }

            IntPtr allocMemAddress = VirtualAllocEx(procHandle, IntPtr.Zero, (uint)((DllPath.Length + 1) * Marshal.SizeOf(typeof(char))), MEM_COMMIT | MEM_RESERVE, PAGE_READWRITE);
            if (allocMemAddress == IntPtr.Zero)
            {
                MessageBox.Show("Error VirtualAllocEx");
                return;
            }

            UIntPtr bytesWritten;
            WriteProcessMemory(procHandle, allocMemAddress, Encoding.Default.GetBytes(DllPath), (uint)((DllPath.Length + 1) * Marshal.SizeOf(typeof(char))), out bytesWritten);


            IntPtr hThread = CreateRemoteThread(procHandle, IntPtr.Zero, 0, loadLibraryAddr, allocMemAddress, 0, IntPtr.Zero);


            if (hThread != IntPtr.Zero)
            {
                CloseHandle(hThread);
            }
            if (procHandle != IntPtr.Zero)
            {
                CloseHandle(procHandle);
            }

            DLLINJECTED = true;
            form.Set_UI_state("Attach");

            while (ProcessArray.Length > 0)
            {
                Thread.Sleep(200);
                ProcessArray = Process.GetProcessesByName("sr_hv");
                if (form.Closing)
                {
                    return;
                }
            }
            DLLINJECTED = false;
            form.Set_UI_state("Detach");


        }

    }
}

using MaterialSkin;
using MaterialSkin.Controls; 

namespace ExLuaSRHV
{
    public partial class Form1 : MaterialForm
    {
        public bool Closing = false;
        public Form1()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Purple800, Primary.Purple900, Primary.Purple800, Accent.Purple700, TextShade.WHITE);

            Waittext.ForeColor = Color.CornflowerBlue;
            Font Normal = new Font("Segoe UI", 14, FontStyle.Bold);
            Waittext.Font = Normal;


            Thread thread1 = new Thread(() => Injector.Wait_for_game(this));
            thread1.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void materialTextBox21_Click(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void materialMultiLineTextBox21_Click(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            var script = materialMultiLineTextBox1.Text;
            if(Gameplay_RadioButton.Checked)
            {
                NamedPipes.LuaPipe(script, 0);
            }
            else
            {
                NamedPipes.LuaPipe(script, 1);
            }


        }

        private void materialButton2_Click(object sender, EventArgs e)
        {

            NamedPipes.LuaPipe(".HOOKSTOP", 2);
            Injector.DLLINJECTED = false;
            Set_UI_state("Detach");

        }
        public void Set_UI_state(string state)
        {
            if (state == "WaitingProcess"){
                Waittext.Visible = true;
                Execute_Button.Enabled = false;
                Attach_Button.Enabled = false;
                Detach_Button.Enabled = false;
            }else if (state == "Attach")
            {
                Waittext.Visible = false;
                Execute_Button.Enabled = true;
                Attach_Button.Enabled = false;
                Detach_Button.Enabled = true;
            }else if (state == "Detach")
            {
                Waittext.Visible = false;
                Execute_Button.Enabled = false;
                Attach_Button.Enabled = true;
                Detach_Button.Enabled = false;
            }
            else
            {
                MessageBox.Show("Error SetUI");
            }
        }

        private void materialButton3_Click(object sender, EventArgs e)
        {
            Thread thread1 = new Thread(() => Injector.Wait_for_game(this));
            thread1.Start();
        }

        private void materialMultiLineTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void materialLabel1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Closing = true;
            if(Injector.DLLINJECTED)
            {
                NamedPipes.LuaPipe(".HOOKSTOP", 2);
                Injector.DLLINJECTED = false;
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            MessageBox.Show("SHESHHHHH");
            if (e.KeyCode == Keys.F1)
            {
                var script = materialMultiLineTextBox1.Text;
                if (Gameplay_RadioButton.Checked)
                {
                    NamedPipes.LuaPipe(script, 0);
                }
                else
                {
                    NamedPipes.LuaPipe(script, 1);
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyPressEventArgs e)
        {
            MessageBox.Show("SHESHHHHH");
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            MessageBox.Show("SHESHHHHH");
        }

        private void Form1_KeyUp(object sender, EventArgs e)
        {
            MessageBox.Show("SHESHHHHH");
        }
    }
}
namespace ExLuaSRHV
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.materialTabControl1 = new MaterialSkin.Controls.MaterialTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.materialCheckbox2 = new MaterialSkin.Controls.MaterialCheckbox();
            this.Interface_RadioButton = new MaterialSkin.Controls.MaterialRadioButton();
            this.Gameplay_RadioButton = new MaterialSkin.Controls.MaterialRadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.Waittext = new System.Windows.Forms.Label();
            this.Attach_Button = new MaterialSkin.Controls.MaterialButton();
            this.Detach_Button = new MaterialSkin.Controls.MaterialButton();
            this.Execute_Button = new MaterialSkin.Controls.MaterialButton();
            this.materialMultiLineTextBox1 = new MaterialSkin.Controls.MaterialMultiLineTextBox();
            this.materialTabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // materialTabControl1
            // 
            this.materialTabControl1.Controls.Add(this.tabPage1);
            this.materialTabControl1.Depth = 0;
            this.materialTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.materialTabControl1.Location = new System.Drawing.Point(3, 64);
            this.materialTabControl1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTabControl1.Multiline = true;
            this.materialTabControl1.Name = "materialTabControl1";
            this.materialTabControl1.SelectedIndex = 0;
            this.materialTabControl1.Size = new System.Drawing.Size(794, 433);
            this.materialTabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.materialCheckbox2);
            this.tabPage1.Controls.Add(this.Interface_RadioButton);
            this.tabPage1.Controls.Add(this.Gameplay_RadioButton);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.Waittext);
            this.tabPage1.Controls.Add(this.Attach_Button);
            this.tabPage1.Controls.Add(this.Detach_Button);
            this.tabPage1.Controls.Add(this.Execute_Button);
            this.tabPage1.Controls.Add(this.materialMultiLineTextBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(786, 405);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Home";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // materialCheckbox2
            // 
            this.materialCheckbox2.AutoSize = true;
            this.materialCheckbox2.Depth = 0;
            this.materialCheckbox2.Location = new System.Drawing.Point(589, 111);
            this.materialCheckbox2.Margin = new System.Windows.Forms.Padding(0);
            this.materialCheckbox2.MouseLocation = new System.Drawing.Point(-1, -1);
            this.materialCheckbox2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialCheckbox2.Name = "materialCheckbox2";
            this.materialCheckbox2.ReadOnly = false;
            this.materialCheckbox2.Ripple = true;
            this.materialCheckbox2.Size = new System.Drawing.Size(143, 37);
            this.materialCheckbox2.TabIndex = 10;
            this.materialCheckbox2.Text = "Dump Lua files";
            this.materialCheckbox2.UseVisualStyleBackColor = true;
            this.materialCheckbox2.CheckedChanged += new System.EventHandler(this.materialCheckbox2_CheckedChanged);
            // 
            // Interface_RadioButton
            // 
            this.Interface_RadioButton.AutoSize = true;
            this.Interface_RadioButton.Depth = 0;
            this.Interface_RadioButton.Location = new System.Drawing.Point(610, 65);
            this.Interface_RadioButton.Margin = new System.Windows.Forms.Padding(0);
            this.Interface_RadioButton.MouseLocation = new System.Drawing.Point(-1, -1);
            this.Interface_RadioButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.Interface_RadioButton.Name = "Interface_RadioButton";
            this.Interface_RadioButton.Ripple = true;
            this.Interface_RadioButton.Size = new System.Drawing.Size(97, 37);
            this.Interface_RadioButton.TabIndex = 9;
            this.Interface_RadioButton.TabStop = true;
            this.Interface_RadioButton.Text = "Interface";
            this.Interface_RadioButton.UseVisualStyleBackColor = true;
            // 
            // Gameplay_RadioButton
            // 
            this.Gameplay_RadioButton.AutoSize = true;
            this.Gameplay_RadioButton.Checked = true;
            this.Gameplay_RadioButton.Depth = 0;
            this.Gameplay_RadioButton.Location = new System.Drawing.Point(610, 28);
            this.Gameplay_RadioButton.Margin = new System.Windows.Forms.Padding(0);
            this.Gameplay_RadioButton.MouseLocation = new System.Drawing.Point(-1, -1);
            this.Gameplay_RadioButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.Gameplay_RadioButton.Name = "Gameplay_RadioButton";
            this.Gameplay_RadioButton.Ripple = true;
            this.Gameplay_RadioButton.Size = new System.Drawing.Size(107, 37);
            this.Gameplay_RadioButton.TabIndex = 8;
            this.Gameplay_RadioButton.TabStop = true;
            this.Gameplay_RadioButton.Text = "Gameplay";
            this.Gameplay_RadioButton.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(253, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Reminder : Always Open this before Saint Row ";
            // 
            // Waittext
            // 
            this.Waittext.AutoSize = true;
            this.Waittext.Font = new System.Drawing.Font("Segoe UI", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Waittext.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.Waittext.Location = new System.Drawing.Point(564, 314);
            this.Waittext.Name = "Waittext";
            this.Waittext.Size = new System.Drawing.Size(200, 23);
            this.Waittext.TabIndex = 4;
            this.Waittext.Text = "Waiting Saint Row IV ...";
            this.Waittext.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Waittext.Click += new System.EventHandler(this.label1_Click);
            // 
            // Attach_Button
            // 
            this.Attach_Button.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Attach_Button.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.Attach_Button.Depth = 0;
            this.Attach_Button.HighEmphasis = true;
            this.Attach_Button.Icon = null;
            this.Attach_Button.Location = new System.Drawing.Point(625, 259);
            this.Attach_Button.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.Attach_Button.MouseState = MaterialSkin.MouseState.HOVER;
            this.Attach_Button.Name = "Attach_Button";
            this.Attach_Button.NoAccentTextColor = System.Drawing.Color.Empty;
            this.Attach_Button.Size = new System.Drawing.Size(79, 36);
            this.Attach_Button.TabIndex = 3;
            this.Attach_Button.Text = "Attach";
            this.Attach_Button.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.Attach_Button.UseAccentColor = false;
            this.Attach_Button.UseVisualStyleBackColor = true;
            this.Attach_Button.Click += new System.EventHandler(this.materialButton3_Click);
            // 
            // Detach_Button
            // 
            this.Detach_Button.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Detach_Button.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.Detach_Button.Depth = 0;
            this.Detach_Button.HighEmphasis = true;
            this.Detach_Button.Icon = null;
            this.Detach_Button.Location = new System.Drawing.Point(625, 211);
            this.Detach_Button.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.Detach_Button.MouseState = MaterialSkin.MouseState.HOVER;
            this.Detach_Button.Name = "Detach_Button";
            this.Detach_Button.NoAccentTextColor = System.Drawing.Color.Empty;
            this.Detach_Button.Size = new System.Drawing.Size(78, 36);
            this.Detach_Button.TabIndex = 2;
            this.Detach_Button.Text = "Detach";
            this.Detach_Button.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.Detach_Button.UseAccentColor = false;
            this.Detach_Button.UseVisualStyleBackColor = true;
            this.Detach_Button.Click += new System.EventHandler(this.materialButton2_Click);
            // 
            // Execute_Button
            // 
            this.Execute_Button.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Execute_Button.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.Execute_Button.Depth = 0;
            this.Execute_Button.Font = new System.Drawing.Font("Segoe UI", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Execute_Button.HighEmphasis = true;
            this.Execute_Button.Icon = null;
            this.Execute_Button.Location = new System.Drawing.Point(621, 163);
            this.Execute_Button.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.Execute_Button.MouseState = MaterialSkin.MouseState.HOVER;
            this.Execute_Button.Name = "Execute_Button";
            this.Execute_Button.NoAccentTextColor = System.Drawing.Color.Empty;
            this.Execute_Button.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Execute_Button.Size = new System.Drawing.Size(84, 36);
            this.Execute_Button.TabIndex = 1;
            this.Execute_Button.Text = "Execute";
            this.Execute_Button.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.Execute_Button.UseAccentColor = false;
            this.Execute_Button.UseVisualStyleBackColor = true;
            this.Execute_Button.Click += new System.EventHandler(this.materialButton1_Click);
            // 
            // materialMultiLineTextBox1
            // 
            this.materialMultiLineTextBox1.AcceptsTab = true;
            this.materialMultiLineTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.materialMultiLineTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.materialMultiLineTextBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.materialMultiLineTextBox1.Depth = 0;
            this.materialMultiLineTextBox1.EnableAutoDragDrop = true;
            this.materialMultiLineTextBox1.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.materialMultiLineTextBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialMultiLineTextBox1.Location = new System.Drawing.Point(28, 37);
            this.materialMultiLineTextBox1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialMultiLineTextBox1.Name = "materialMultiLineTextBox1";
            this.materialMultiLineTextBox1.Size = new System.Drawing.Size(513, 367);
            this.materialMultiLineTextBox1.TabIndex = 0;
            this.materialMultiLineTextBox1.TabStop = false;
            this.materialMultiLineTextBox1.Text = "";
            this.materialMultiLineTextBox1.WordWrap = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 500);
            this.Controls.Add(this.materialTabControl1);
            this.DrawerTabControl = this.materialTabControl1;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(800, 500);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "Form1";
            this.Sizable = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lua Executor Saint Row IV - Re-Elected by Nathnéfo#4261";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.materialTabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MaterialSkin.Controls.MaterialTabControl materialTabControl1;
        private TabPage tabPage1;
        private MaterialSkin.Controls.MaterialMultiLineTextBox materialMultiLineTextBox1;
        private MaterialSkin.Controls.MaterialButton Execute_Button;
        private MaterialSkin.Controls.MaterialButton Detach_Button;
        private MaterialSkin.Controls.MaterialButton Attach_Button;
        private Label Waittext;
        private Label label2;
        private MaterialSkin.Controls.MaterialRadioButton Gameplay_RadioButton;
        private MaterialSkin.Controls.MaterialRadioButton Interface_RadioButton;
        private MaterialSkin.Controls.MaterialCheckbox materialCheckbox2;
    }
}
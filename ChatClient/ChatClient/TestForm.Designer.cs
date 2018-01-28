namespace ChatClient
{
    partial class TestForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.chatBox1 = new GuiDLL.ChatBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // chatBox1
            // 
            this.chatBox1.BackgroundColor = System.Drawing.Color.Empty;
            this.chatBox1.DateBoxColor = System.Drawing.Color.Empty;
            this.chatBox1.ForegroundColor = System.Drawing.Color.Black;
            this.chatBox1.FriendMessageColor = System.Drawing.Color.Empty;
            this.chatBox1.Location = new System.Drawing.Point(21, 12);
            this.chatBox1.Name = "chatBox1";
            this.chatBox1.Size = new System.Drawing.Size(706, 574);
            this.chatBox1.TabIndex = 0;
            this.chatBox1.YourMessageColor = System.Drawing.Color.Empty;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(799, 33);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(154, 79);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 672);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.chatBox1);
            this.Name = "TestForm";
            this.Text = "TestForm";
            this.ResumeLayout(false);

        }

        #endregion

        private GuiDLL.ChatBox chatBox1;
        private System.Windows.Forms.Button button1;
    }
}
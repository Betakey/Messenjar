namespace ChatClient
{
    partial class ChatClientForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.inputRichTextbox = new System.Windows.Forms.RichTextBox();
            this.sendButton = new System.Windows.Forms.Button();
            this.chatBox1 = new GuiDLL.ChatBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(86, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 1;
            // 
            // inputRichTextbox
            // 
            this.inputRichTextbox.Location = new System.Drawing.Point(12, 489);
            this.inputRichTextbox.Name = "inputRichTextbox";
            this.inputRichTextbox.Size = new System.Drawing.Size(390, 35);
            this.inputRichTextbox.TabIndex = 3;
            this.inputRichTextbox.Text = "";
            // 
            // sendButton
            // 
            this.sendButton.Location = new System.Drawing.Point(408, 487);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(130, 37);
            this.sendButton.TabIndex = 4;
            this.sendButton.Text = "Send";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // chatBox1
            // 
            this.chatBox1.AutoScroll = true;
            this.chatBox1.BackColor = System.Drawing.Color.FloralWhite;
            this.chatBox1.BackgroundColor = System.Drawing.Color.Empty;
            this.chatBox1.DateBoxColor = System.Drawing.Color.Empty;
            this.chatBox1.ForegroundColor = System.Drawing.Color.Empty;
            this.chatBox1.FriendMessageColor = System.Drawing.Color.Empty;
            this.chatBox1.Location = new System.Drawing.Point(12, 13);
            this.chatBox1.Name = "chatBox1";
            this.chatBox1.Size = new System.Drawing.Size(526, 468);
            this.chatBox1.TabIndex = 5;
            this.chatBox1.YourMessageColor = System.Drawing.Color.Empty;
            // 
            // ChatClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.ForestGreen;
            this.ClientSize = new System.Drawing.Size(551, 533);
            this.Controls.Add(this.chatBox1);
            this.Controls.Add(this.sendButton);
            this.Controls.Add(this.inputRichTextbox);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "ChatClientForm";
            this.Text = "ChatClient";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private GuiDLL.ChatBox chatBox;
        private System.Windows.Forms.RichTextBox inputRichTextbox;
        private System.Windows.Forms.Button sendButton;
        private GuiDLL.ChatBox chatBox1;
    }
}


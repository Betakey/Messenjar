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
            this.sendTextRichTextBox = new System.Windows.Forms.RichTextBox();
            this.chatRichTextBox = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.sendButton = new System.Windows.Forms.Button();
            this.friendsListbox = new System.Windows.Forms.ListBox();
            this.userNameTextBox = new System.Windows.Forms.TextBox();
            this.closeButton = new System.Windows.Forms.Button();
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
            // sendTextRichTextBox
            // 
            this.sendTextRichTextBox.BackColor = System.Drawing.Color.Chartreuse;
            this.sendTextRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.sendTextRichTextBox.Location = new System.Drawing.Point(12, 274);
            this.sendTextRichTextBox.Name = "sendTextRichTextBox";
            this.sendTextRichTextBox.Size = new System.Drawing.Size(220, 53);
            this.sendTextRichTextBox.TabIndex = 2;
            this.sendTextRichTextBox.Text = "";
            // 
            // chatRichTextBox
            // 
            this.chatRichTextBox.BackColor = System.Drawing.Color.Chartreuse;
            this.chatRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chatRichTextBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.chatRichTextBox.Location = new System.Drawing.Point(12, 25);
            this.chatRichTextBox.Name = "chatRichTextBox";
            this.chatRichTextBox.ReadOnly = true;
            this.chatRichTextBox.Size = new System.Drawing.Size(220, 239);
            this.chatRichTextBox.TabIndex = 3;
            this.chatRichTextBox.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Username:";
            // 
            // sendButton
            // 
            this.sendButton.BackColor = System.Drawing.Color.Chartreuse;
            this.sendButton.FlatAppearance.BorderSize = 0;
            this.sendButton.Location = new System.Drawing.Point(238, 275);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(89, 53);
            this.sendButton.TabIndex = 5;
            this.sendButton.Text = "Send";
            this.sendButton.UseVisualStyleBackColor = false;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // friendsListbox
            // 
            this.friendsListbox.BackColor = System.Drawing.Color.Chartreuse;
            this.friendsListbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.friendsListbox.ForeColor = System.Drawing.Color.Black;
            this.friendsListbox.FormattingEnabled = true;
            this.friendsListbox.Location = new System.Drawing.Point(238, 27);
            this.friendsListbox.Name = "friendsListbox";
            this.friendsListbox.Size = new System.Drawing.Size(89, 234);
            this.friendsListbox.TabIndex = 6;
            // 
            // userNameTextBox
            // 
            this.userNameTextBox.BackColor = System.Drawing.Color.Chartreuse;
            this.userNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.userNameTextBox.Location = new System.Drawing.Point(73, 6);
            this.userNameTextBox.Name = "userNameTextBox";
            this.userNameTextBox.ReadOnly = true;
            this.userNameTextBox.Size = new System.Drawing.Size(244, 13);
            this.userNameTextBox.TabIndex = 7;
            // 
            // closeButton
            // 
            this.closeButton.BackColor = System.Drawing.Color.Red;
            this.closeButton.ForeColor = System.Drawing.Color.White;
            this.closeButton.Location = new System.Drawing.Point(323, 1);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(21, 23);
            this.closeButton.TabIndex = 8;
            this.closeButton.Text = "X";
            this.closeButton.UseVisualStyleBackColor = false;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // ChatClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.ForestGreen;
            this.ClientSize = new System.Drawing.Size(345, 336);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.userNameTextBox);
            this.Controls.Add(this.friendsListbox);
            this.Controls.Add(this.sendButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chatRichTextBox);
            this.Controls.Add(this.sendTextRichTextBox);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ChatClientForm";
            this.Text = "ChatClient";
            this.Load += new System.EventHandler(this.ChatClient_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ChatClient_KeyDown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ChatClient_MouseDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox sendTextRichTextBox;
        private System.Windows.Forms.RichTextBox chatRichTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.ListBox friendsListbox;
        private System.Windows.Forms.TextBox userNameTextBox;
        private System.Windows.Forms.Button closeButton;
    }
}


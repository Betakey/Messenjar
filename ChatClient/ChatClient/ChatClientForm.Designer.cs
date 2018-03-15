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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChatClientForm));
            this.inputRichTextbox = new System.Windows.Forms.RichTextBox();
            this.sendButton = new System.Windows.Forms.Button();
            this.friendsList = new GuiDLL.FriendsList();
            this.chatBox = new GuiDLL.ChatBox();
            this.iconPictureBox = new System.Windows.Forms.PictureBox();
            this.statusPictureBox = new System.Windows.Forms.PictureBox();
            this.homePanel = new System.Windows.Forms.Panel();
            this.removeFriendTextBox = new System.Windows.Forms.Button();
            this.editImageButton = new System.Windows.Forms.Button();
            this.imagePictureBox = new System.Windows.Forms.PictureBox();
            this.addButton = new System.Windows.Forms.Button();
            this.searchNameTextBox = new System.Windows.Forms.TextBox();
            this.yourFriendsList = new GuiDLL.FriendsList();
            this.homeLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusPictureBox)).BeginInit();
            this.homePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imagePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // inputRichTextbox
            // 
            this.inputRichTextbox.Location = new System.Drawing.Point(254, 461);
            this.inputRichTextbox.Name = "inputRichTextbox";
            this.inputRichTextbox.Size = new System.Drawing.Size(227, 60);
            this.inputRichTextbox.TabIndex = 3;
            this.inputRichTextbox.Text = "";
            this.inputRichTextbox.Visible = false;
            // 
            // sendButton
            // 
            this.sendButton.Location = new System.Drawing.Point(487, 461);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(150, 60);
            this.sendButton.TabIndex = 4;
            this.sendButton.Text = "Send";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Visible = false;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // friendsList
            // 
            this.friendsList.AutoScroll = true;
            this.friendsList.BackColor = System.Drawing.Color.LightYellow;
            this.friendsList.Location = new System.Drawing.Point(12, 132);
            this.friendsList.Name = "friendsList";
            this.friendsList.Size = new System.Drawing.Size(236, 392);
            this.friendsList.TabIndex = 6;
            this.friendsList.BubbleClick += new System.Action<object, System.Windows.Forms.MouseEventArgs, GuiDLL.FriendEntry>(this.FriendsList_BubbleClick);
            // 
            // chatBox
            // 
            this.chatBox.AutoScroll = true;
            this.chatBox.BackColor = System.Drawing.Color.LightYellow;
            this.chatBox.BackgroundColor = System.Drawing.Color.Empty;
            this.chatBox.DateBoxColor = System.Drawing.Color.Empty;
            this.chatBox.ForegroundColor = System.Drawing.Color.Empty;
            this.chatBox.FriendMessageColor = System.Drawing.Color.Empty;
            this.chatBox.Location = new System.Drawing.Point(254, 12);
            this.chatBox.Name = "chatBox";
            this.chatBox.Size = new System.Drawing.Size(383, 443);
            this.chatBox.TabIndex = 5;
            this.chatBox.Visible = false;
            this.chatBox.YourMessageColor = System.Drawing.Color.Empty;
            // 
            // iconPictureBox
            // 
            this.iconPictureBox.BackColor = System.Drawing.Color.ForestGreen;
            this.iconPictureBox.Image = global::ChatClient.Properties.Resources.icon;
            this.iconPictureBox.Location = new System.Drawing.Point(12, 12);
            this.iconPictureBox.Name = "iconPictureBox";
            this.iconPictureBox.Size = new System.Drawing.Size(116, 114);
            this.iconPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.iconPictureBox.TabIndex = 7;
            this.iconPictureBox.TabStop = false;
            this.iconPictureBox.Click += new System.EventHandler(this.iconPictureBox_Click);
            // 
            // statusPictureBox
            // 
            this.statusPictureBox.Location = new System.Drawing.Point(134, 12);
            this.statusPictureBox.Name = "statusPictureBox";
            this.statusPictureBox.Size = new System.Drawing.Size(114, 114);
            this.statusPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.statusPictureBox.TabIndex = 8;
            this.statusPictureBox.TabStop = false;
            this.statusPictureBox.MouseHover += new System.EventHandler(this.statusPictureBox_MouseHover);
            // 
            // homePanel
            // 
            this.homePanel.Controls.Add(this.removeFriendTextBox);
            this.homePanel.Controls.Add(this.editImageButton);
            this.homePanel.Controls.Add(this.imagePictureBox);
            this.homePanel.Controls.Add(this.addButton);
            this.homePanel.Controls.Add(this.searchNameTextBox);
            this.homePanel.Controls.Add(this.yourFriendsList);
            this.homePanel.Controls.Add(this.homeLabel);
            this.homePanel.Location = new System.Drawing.Point(254, 12);
            this.homePanel.Name = "homePanel";
            this.homePanel.Size = new System.Drawing.Size(383, 512);
            this.homePanel.TabIndex = 9;
            // 
            // removeFriendTextBox
            // 
            this.removeFriendTextBox.Location = new System.Drawing.Point(122, 486);
            this.removeFriendTextBox.Name = "removeFriendTextBox";
            this.removeFriendTextBox.Size = new System.Drawing.Size(73, 23);
            this.removeFriendTextBox.TabIndex = 15;
            this.removeFriendTextBox.Text = "Entfernen";
            this.removeFriendTextBox.UseVisualStyleBackColor = true;
            this.removeFriendTextBox.Click += new System.EventHandler(this.removeFriendTextBox_Click);
            // 
            // editImageButton
            // 
            this.editImageButton.Location = new System.Drawing.Point(292, 104);
            this.editImageButton.Name = "editImageButton";
            this.editImageButton.Size = new System.Drawing.Size(75, 23);
            this.editImageButton.TabIndex = 14;
            this.editImageButton.Text = "Bearbeiten";
            this.editImageButton.UseVisualStyleBackColor = true;
            this.editImageButton.Click += new System.EventHandler(this.editImageButton_Click);
            // 
            // imagePictureBox
            // 
            this.imagePictureBox.Location = new System.Drawing.Point(280, 3);
            this.imagePictureBox.Name = "imagePictureBox";
            this.imagePictureBox.Size = new System.Drawing.Size(100, 100);
            this.imagePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imagePictureBox.TabIndex = 13;
            this.imagePictureBox.TabStop = false;
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(168, 104);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 23);
            this.addButton.TabIndex = 12;
            this.addButton.Text = "Hinzufügen";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // searchNameTextBox
            // 
            this.searchNameTextBox.Location = new System.Drawing.Point(49, 106);
            this.searchNameTextBox.Name = "searchNameTextBox";
            this.searchNameTextBox.Size = new System.Drawing.Size(113, 20);
            this.searchNameTextBox.TabIndex = 11;
            // 
            // yourFriendsList
            // 
            this.yourFriendsList.AutoScroll = true;
            this.yourFriendsList.BackColor = System.Drawing.Color.LightYellow;
            this.yourFriendsList.Location = new System.Drawing.Point(49, 133);
            this.yourFriendsList.Name = "yourFriendsList";
            this.yourFriendsList.Size = new System.Drawing.Size(240, 348);
            this.yourFriendsList.TabIndex = 10;
            // 
            // homeLabel
            // 
            this.homeLabel.AutoSize = true;
            this.homeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.homeLabel.Location = new System.Drawing.Point(42, 26);
            this.homeLabel.Name = "homeLabel";
            this.homeLabel.Size = new System.Drawing.Size(185, 42);
            this.homeLabel.TabIndex = 0;
            this.homeLabel.Text = "Startseite";
            // 
            // ChatClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.ForestGreen;
            this.ClientSize = new System.Drawing.Size(649, 533);
            this.Controls.Add(this.homePanel);
            this.Controls.Add(this.statusPictureBox);
            this.Controls.Add(this.iconPictureBox);
            this.Controls.Add(this.friendsList);
            this.Controls.Add(this.chatBox);
            this.Controls.Add(this.sendButton);
            this.Controls.Add(this.inputRichTextbox);
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(665, 572);
            this.MinimumSize = new System.Drawing.Size(665, 572);
            this.Name = "ChatClientForm";
            this.Text = "MessenJar";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChatClientForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusPictureBox)).EndInit();
            this.homePanel.ResumeLayout(false);
            this.homePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imagePictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.RichTextBox inputRichTextbox;
        private System.Windows.Forms.Button sendButton;
        private GuiDLL.ChatBox chatBox;
        private GuiDLL.FriendsList friendsList;
        private System.Windows.Forms.PictureBox iconPictureBox;
        private System.Windows.Forms.PictureBox statusPictureBox;
        private System.Windows.Forms.Panel homePanel;
        private System.Windows.Forms.Label homeLabel;
        private GuiDLL.FriendsList yourFriendsList;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.TextBox searchNameTextBox;
        private System.Windows.Forms.PictureBox imagePictureBox;
        private System.Windows.Forms.Button removeFriendTextBox;
        private System.Windows.Forms.Button editImageButton;
    }
}


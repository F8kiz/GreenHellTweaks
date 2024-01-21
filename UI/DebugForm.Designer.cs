namespace GHTweaks.UI
{
    partial class DebugForm
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
            this.RichTextBox_Log = new System.Windows.Forms.RichTextBox();
            this.Label_MessageQueueInfo = new System.Windows.Forms.Label();
            this.Label_MessageQueueCount = new System.Windows.Forms.Label();
            this.Button_ClearMessageQueue = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // RichTextBox_Log
            // 
            this.RichTextBox_Log.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RichTextBox_Log.Location = new System.Drawing.Point(0, 0);
            this.RichTextBox_Log.Name = "RichTextBox_Log";
            this.RichTextBox_Log.Size = new System.Drawing.Size(800, 403);
            this.RichTextBox_Log.TabIndex = 0;
            this.RichTextBox_Log.Text = "";
            // 
            // Label_MessageQueueInfo
            // 
            this.Label_MessageQueueInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Label_MessageQueueInfo.AutoSize = true;
            this.Label_MessageQueueInfo.Location = new System.Drawing.Point(12, 420);
            this.Label_MessageQueueInfo.Name = "Label_MessageQueueInfo";
            this.Label_MessageQueueInfo.Size = new System.Drawing.Size(116, 13);
            this.Label_MessageQueueInfo.TabIndex = 2;
            this.Label_MessageQueueInfo.Text = "Message queue count:";
            // 
            // Label_MessageQueueCount
            // 
            this.Label_MessageQueueCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Label_MessageQueueCount.AutoSize = true;
            this.Label_MessageQueueCount.Location = new System.Drawing.Point(126, 420);
            this.Label_MessageQueueCount.Name = "Label_MessageQueueCount";
            this.Label_MessageQueueCount.Size = new System.Drawing.Size(13, 13);
            this.Label_MessageQueueCount.TabIndex = 3;
            this.Label_MessageQueueCount.Text = "0";
            // 
            // Button_ClearMessageQueue
            // 
            this.Button_ClearMessageQueue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_ClearMessageQueue.Location = new System.Drawing.Point(703, 415);
            this.Button_ClearMessageQueue.Name = "Button_ClearMessageQueue";
            this.Button_ClearMessageQueue.Size = new System.Drawing.Size(85, 23);
            this.Button_ClearMessageQueue.TabIndex = 4;
            this.Button_ClearMessageQueue.Text = "Clear Queue";
            this.Button_ClearMessageQueue.UseVisualStyleBackColor = true;
            this.Button_ClearMessageQueue.Click += new System.EventHandler(this.Button_ClearMessageQueue_Click);
            // 
            // DebugForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Button_ClearMessageQueue);
            this.Controls.Add(this.Label_MessageQueueCount);
            this.Controls.Add(this.Label_MessageQueueInfo);
            this.Controls.Add(this.RichTextBox_Log);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "DebugForm";
            this.ShowIcon = false;
            this.Text = "GreenHell Tweaks debug";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DebugForm_FormClosing);
            this.Load += new System.EventHandler(this.DebugForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox RichTextBox_Log;
        private System.Windows.Forms.Label Label_MessageQueueInfo;
        private System.Windows.Forms.Label Label_MessageQueueCount;
        private System.Windows.Forms.Button Button_ClearMessageQueue;
    }
}
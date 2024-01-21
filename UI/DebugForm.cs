using GHTweaks.Models;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GHTweaks.UI
{
    public partial class DebugForm : Form
    {
        public event EventHandler<FormClosingEventArgs> OnDebugFormClosing;

        private readonly Dictionary<LogType, Color> dicLogTypeColor;

        private bool isFirstCloseRequest = true;

        private System.Timers.Timer timer;

        private readonly List<LogMessage> lstLogs = new List<LogMessage>();

        private readonly object lstLogsLock = new object();


        public DebugForm()
        {
            InitializeComponent();
            dicLogTypeColor = new Dictionary<LogType, Color>()
            {
                { LogType.Debug, Color.Purple },
                { LogType.Info, Color.Black },
                { LogType.Error, Color.Red },
                { LogType.Exception, Color.Red },
            };


            timer = new System.Timers.Timer();
            timer.Interval = 500;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void DebugForm_Load(object sender, EventArgs e)
        {

        }

        private async void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (lstLogs.Count < 1)
                return;

            timer.Stop();
            int count = lstLogs.Count;
            for(int i = 0; i < count; ++i)
            {
                if (lstLogs.Count < count)
                    break;

                var log = lstLogs[i];
                if ((RichTextBox_Log.TextLength - log.Message.Length) >= RichTextBox_Log.MaxLength)
                    RichTextBox_Log.Text = "";

                int selectionStart = RichTextBox_Log.Text.Length + log.Message.IndexOf("][") + 2;
                int selectionLength = log.Type.ToString().Length;
                Color selectionColor = dicLogTypeColor[log.Type];

                RichTextBox_Log.AppendText(log.Message);
                RichTextBox_Log.SelectionStart = selectionStart;
                RichTextBox_Log.SelectionLength = selectionLength;
                RichTextBox_Log.SelectionColor = selectionColor;
                RichTextBox_Log.SelectionLength = 0;
                RichTextBox_Log.AppendText(Environment.NewLine);

                await Task.Delay(100); // Prevent RichtextBox crash 
            }

            if (lstLogs.Count >= count)
            {
                lock (lstLogsLock)
                {
                    lstLogs.RemoveRange(0, count);
                }
            }
            Label_MessageQueueCount.Text = lstLogs.Count.ToString();
            timer?.Start();
        }

        public void AddMessage(LogType type, string message)
        {
            lstLogs.Add(new LogMessage(type, message));
            Label_MessageQueueCount.Text = lstLogs.Count.ToString();
        }

        private void DebugForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.ApplicationExitCall && isFirstCloseRequest)
            {
                isFirstCloseRequest = false;
                e.Cancel = true;
                return;
            }
            timer.Stop();
            timer.Dispose();
            timer = null;
            OnDebugFormClosing?.Invoke(this, e);
        }

        private void Button_ClearMessageQueue_Click(object sender, EventArgs e)
        {
            lock (lstLogsLock)
            {
                lstLogs.Clear();
            }
        }
    }
}

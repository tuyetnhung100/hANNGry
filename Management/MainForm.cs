using AccountLibrary;
using System.Windows.Forms;

namespace Management
{
    public class MainForm : Form
    {
        public MainForm(Account account)
        {
            Program.account = account;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(120, 0);
            this.Name = "MainForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {
            Hide();
            Program.story2.Show();
        }
    }
}

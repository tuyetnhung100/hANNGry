using AccountLibrary;
using System.Windows.Forms;

namespace Management
{
    public class MainForm : Form
    {
        public MainForm(Account account)
        {
            Program.account = account;

            Program.story2 = new Story2.Story2();
            Story2.Story2.LoginedEmployee = account;
            Program.story2.FormClosed += (sender, e) => Program.mainForm.Close();
            Program.story3 = new Story3.templateCreator();
            Story3.templateCreator.LoginedEmployee = account;
            Program.story3.FormClosed += (sender, e) => Program.mainForm.Close();
            Program.story4 = new Story4.Form1();
            Story4.Form1.LoginedEmployee = account;
            Program.story4.FormClosed += (sender, e) => Program.mainForm.Close();
            Program.InitializeButtonList(Program.story2);
            Program.InitializeButtonList(Program.story3);
            Program.InitializeButtonList(Program.story4);

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

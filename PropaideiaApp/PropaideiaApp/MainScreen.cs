using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PropaideiaApp
{
    public partial class MainScreen : Form
    {
        public MainScreen()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginScreen loginForm = new LoginScreen();
            loginForm.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void buttonTakeQuiz_Click(object sender, EventArgs e)
        {
            pictureBoxNext.Visible = true;
            buttonTakeQuiz.Visible = false;
            textBoxMain.Visible = false;
            labelTitle.Text = "Quiz για την προπαίδεια του: ";
            panelTF.Visible = true;
            panelTF.Enabled = true;
        }

        private void MainScreen_Load(object sender, EventArgs e)
        {

        }
    }
}

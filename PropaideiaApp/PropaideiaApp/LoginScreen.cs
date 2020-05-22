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
    public partial class LoginScreen : Form
    {
        public LoginScreen()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(!String.IsNullOrEmpty(textBoxUsername.Text))
            {
                labelPassword.Visible = true;
                textBoxPassword.Visible = true;
            }
            this.Hide();
            MainScreen mainForm = new MainScreen();
            mainForm.Show();
        }

        private void LoginScreen_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void buttonLogin_MouseLeave(object sender, EventArgs e)
        {
            buttonLogin.BackColor = Color.FromArgb(244, 211, 8);
        }

        private void buttonLogin_MouseEnter(object sender, EventArgs e)
        {
            buttonLogin.BackColor = Color.FromArgb(255, 222, 8);
        }

        private void buttonRegister_MouseEnter(object sender, EventArgs e)
        {
            buttonRegister.BackColor = Color.FromArgb(219, 75, 74);
        }

        private void buttonRegister_MouseLeave(object sender, EventArgs e)
        {
            buttonRegister.BackColor = Color.FromArgb(216, 40, 39);
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }
    }
}

using PropaideiaApp.DataMappers;
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
        public static string activeUser;
        public static string userType;

        public LoginScreen()
        {
            InitializeComponent();
        }

        private void LoginScreen_Load(object sender, EventArgs e)
        {

        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            MainScreen mainForm = new MainScreen();

            if (!String.IsNullOrEmpty(textBoxUsername.Text))
            {
                if(StudentMapper.Get(textBoxUsername.Text) != null)
                {
                    activeUser = StudentMapper.Get(textBoxUsername.Text).Username;
                    userType = "student";
                    this.Hide();
                    mainForm.Show();
                }
                else if(ProfessorMapper.Get(textBoxUsername.Text) != null)
                {
                    activeUser = ProfessorMapper.Get(textBoxUsername.Text).Username;
                    userType = "professor";
                    this.Hide();
                    mainForm.Show();
                }
                else
                {
                    MessageBox.Show("User not found! To create an account please click the Register button!", "Error", MessageBoxButtons.OK);
                }
            }
            else
            {
                MessageBox.Show("Please insert a username!", "Error", MessageBoxButtons.OK);
            }
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

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            
        }

    }
}

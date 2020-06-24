using PropaideiaApp.DataMappers;
using PropaideiaApp.Users;
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
            activeUser = "";
            userType = "";
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            MainScreen mainForm = new MainScreen();

            if (!String.IsNullOrEmpty(textBoxUsername.Text) && !String.IsNullOrEmpty(textBoxPassword.Text))
            {
                userType = Database.Login(textBoxUsername.Text, textBoxPassword.Text);
                if(userType == UserTypes.STUDENT)
                {
                    activeUser = StudentMapper.Get(textBoxUsername.Text).Username;
                    this.Hide();
                    mainForm.Show();
                }
                else if(userType == UserTypes.PROFESSOR)
                {
                    activeUser = ProfessorMapper.Get(textBoxUsername.Text).Username;
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
            if (!labelRegisterName.Visible) //First button click shows the rest of the fields to fill in
            {
                labelRegisterName.Visible = true;
                labelRegisterSurname.Visible = true;
                textBoxRegisterName.Visible = true;
                textBoxRegisterSurname.Visible = true;
            }
            else //After the rest of the fields are visible, the register button tries to register the user in the DB
            {
                if (!String.IsNullOrEmpty(textBoxUsername.Text) && !String.IsNullOrEmpty(textBoxPassword.Text) && !String.IsNullOrEmpty(textBoxRegisterName.Text) && !String.IsNullOrEmpty(textBoxRegisterSurname.Text))
                {
                    Student newStudent = new Student(textBoxUsername.Text, textBoxRegisterName.Text, textBoxRegisterSurname.Text);
                    if (StudentMapper.Get(newStudent.Username) == null)
                    {
                        if (StudentMapper.Insert(newStudent, textBoxPassword.Text))
                        {
                            if (MessageBox.Show("User registered successfully!", "Registered Successfully!", MessageBoxButtons.OK) == DialogResult.OK)
                            {
                                buttonLogin.PerformClick();
                            }
                        }
                        else
                        {
                            MessageBox.Show("There was an error while creating the user!", "Error", MessageBoxButtons.OK);
                        }
                    }
                    else
                    {
                        MessageBox.Show("A user with this username already exists!", "Error", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    MessageBox.Show("Please fill in all the above fields before registering!", "Error", MessageBoxButtons.OK);
                }
            }
        }

        private void textBoxUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter)
            {
                buttonLogin.PerformClick();
            }
        }

        private void textBoxPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                buttonLogin.PerformClick();
            }
        }

        private void LoginScreen_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void pictureBoxHelp_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"..\..\..\..\MainHelp.html");
        }
    }
}

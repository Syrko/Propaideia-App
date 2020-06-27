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
            //Reset user data, in case we return here from a logout
            activeUser = "";
            userType = "";
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            MainScreen mainForm = new MainScreen();

            if (!String.IsNullOrEmpty(textBoxUsername.Text) && !String.IsNullOrEmpty(textBoxPassword.Text))
            {
                userType = Database.Login(textBoxUsername.Text, textBoxPassword.Text); //Try to login, if it successed the user's type is returned
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
                    MessageBox.Show("Δεν βρέθηκε ο χρήστης! Για να δημιουργήσετε ένα λογαριασμό παρακαλούμε πατήστε το κουμπί Εγγραφή!", "Ειδοποίηση", MessageBoxButtons.OK);
                }
            }
            else
            {
                MessageBox.Show("Παρακαλούμε εισάγετε ένα όνομα χρήστη!", "Ειδοποίηση", MessageBoxButtons.OK);
            }
        }

        
        private void buttonRegister_Click(object sender, EventArgs e)
        {
            if (!labelRegisterName.Visible) //First button click shows the rest of the fields that need to be filled in
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
                    if (StudentMapper.Get(newStudent.Username) == null) //If the student doesn't already exist
                    {
                        if (StudentMapper.Insert(newStudent, textBoxPassword.Text))
                        {
                            if (MessageBox.Show("Ο χρήστης εγγράφηκε επιτυχώς!", "Επιτυχής Εγγραφή!", MessageBoxButtons.OK) == DialogResult.OK)
                            {
                                //auto login after register
                                buttonLogin.PerformClick();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Υπήρξε ένα σφάλμα κατά την δημιουργία του χρήστη!", "Σφάλμα", MessageBoxButtons.OK);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Υπάρχει ήδη ένας χρήστης με αυτό το όνομα χρήστη!", "Σφάλμα", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    MessageBox.Show("Παρακαλούμε συμπληρώστε όλα τα παραπάνω πεδιά πριν συνεχίσετε!", "Σφάλμα", MessageBoxButtons.OK);
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
            System.Diagnostics.Process.Start(@"OnlineHelp\MainHelp.html");
        }

        //Hover animations
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
    }
}

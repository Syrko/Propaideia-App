using PropaideiaApp.Properties;
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
        List<Button> buttonList = new List<Button>();
        public MainScreen()
        {
            InitializeComponent();
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


        private void MainScreen_Load(object sender, EventArgs e)
        {
            panelBlank.Visible = false;
            panelMult.Visible = false;
            panelTF.Visible = false;
            buttonList.Add(button1);
            buttonList.Add(button2);
            buttonList.Add(button3);
            buttonList.Add(button4);
            buttonList.Add(button5);
            buttonList.Add(button6);
            buttonList.Add(button7);
            buttonList.Add(button8);
            buttonList.Add(button9);
            buttonList.Add(button10);
            buttonList.Add(buttonFinalExam);
        }

        private void pictureBoxNext_Click(object sender, EventArgs e)
        {
            //TODO Call getNextQuest function
            panelBlank.Visible = false;
            panelTF.Visible = true;
            panelMult.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            labelTitleNumber.Text = "1";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            labelTitleNumber.Text = "2";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            labelTitleNumber.Text = "3";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            labelTitleNumber.Text = "4";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            labelTitleNumber.Text = "5";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            labelTitleNumber.Text = "6";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            labelTitleNumber.Text = "7";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            labelTitleNumber.Text = "8";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            labelTitleNumber.Text = "9";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            labelTitleNumber.Text = "10";
        }

        private void buttonFinalExam_Click(object sender, EventArgs e)
        {

        }

        private void buttonTakeQuiz_Click(object sender, EventArgs e)
        {
            textBoxMain.Visible = false;
            pictureBoxNext.Visible = true;
            buttonTakeQuiz.Visible = false;
            labelTitle.Text = "Quiz για την προπαίδεια του: ";
            panelBlank.Visible = true;
            panelTF.Visible = false;
            panelMult.Visible = false;
        }

        private void updateProgress(int chapter)
        {
            buttonList[chapter].Image = Resources.tick;
            buttonList[chapter + 1].Image = Resources.unlock;
            buttonList[chapter + 1].Enabled = true;
            //Update DB
        }
    }
}

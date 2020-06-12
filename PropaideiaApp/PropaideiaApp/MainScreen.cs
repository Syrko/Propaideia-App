using PropaideiaApp.Properties;
using PropaideiaApp.Quizes;
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
        List<Panel> questionPanelList = new List<Panel>();
        int currentNumber;
        Point questionPoint = new Point(214, 96);
        Point hideTF = new Point(794, 442);
        Point hideMult = new Point(798, 31);
        Point hideBlank = new Point(181, 445);
        Point hideResult = new Point(608, 446);
        int i = 0; //TO DELETE

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
            questionPanelList.Add(panelBlank);
            questionPanelList.Add(panelMult);
            questionPanelList.Add(panelTF);
            button1_Click(sender, e);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            labelTitleNumber.Text = "1";
            currentNumber = 1;
            changeLesson(currentNumber);
            pictureBoxNumber.Image = Resources._1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            labelTitleNumber.Text = "2";
            currentNumber = 2;
            changeLesson(currentNumber);
            pictureBoxNumber.Image = Resources._2;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            labelTitleNumber.Text = "3";
            currentNumber = 3;
            changeLesson(currentNumber);
            pictureBoxNumber.Image = Resources._3;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            labelTitleNumber.Text = "4";
            currentNumber = 4;
            changeLesson(currentNumber);
            pictureBoxNumber.Image = Resources._4;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            labelTitleNumber.Text = "5";
            currentNumber = 5;
            changeLesson(currentNumber);
            pictureBoxNumber.Image = Resources._5;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            labelTitleNumber.Text = "6";
            currentNumber = 6;
            changeLesson(currentNumber);
            pictureBoxNumber.Image = Resources._6;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            labelTitleNumber.Text = "7";
            currentNumber = 7;
            changeLesson(currentNumber);
            pictureBoxNumber.Image = Resources._7;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            labelTitleNumber.Text = "8";
            currentNumber = 8;
            changeLesson(currentNumber);
            pictureBoxNumber.Image = Resources._8;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            labelTitleNumber.Text = "9";
            currentNumber = 9;
            changeLesson(currentNumber);
            pictureBoxNumber.Image = Resources._9;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            labelTitleNumber.Text = "10";
            currentNumber = 10;
            changeLesson(currentNumber);
            pictureBoxNumber.Image = Resources._10;
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
            panelTip.Visible = false;
            showQuestion("TF");
        }

        private void takeQuiz()
        {

        }

        private void pictureBoxNext_Click(object sender, EventArgs e)
        {
            //TODO Call getNextQuest function
            if (i == 0)
            {
                showQuestion("Blank");
            }
            else if (i == 1)
            {
                showQuestion("Mult");
            }
            else
            {
                endQuiz();
                i = -1;
            }
            i++;
        }

        private void showQuestion(string questionType)
        {
            //List: 0-Blank, 1-Multiple Choice, 2-True/False
            //TODO get questions from db

            if(questionType == "Blank")
            {
                questionPanelList[0].Location = questionPoint;
                questionPanelList[0].Visible = true;
                questionPanelList[1].Visible = false;
                questionPanelList[2].Visible = false;
            }
            else if(questionType == "Mult")
            {
                questionPanelList[0].Visible = false;
                questionPanelList[1].Location = questionPoint;
                questionPanelList[1].Visible = true;
                questionPanelList[2].Visible = false;
            }
            else if(questionType == "TF")
            {
                questionPanelList[0].Visible = false;
                questionPanelList[1].Visible = false;
                questionPanelList[2].Location = questionPoint;
                questionPanelList[2].Visible = true;
            }
        }

        private void endQuiz()
        {
            questionPanelList[0].Visible = false;
            questionPanelList[0].Location = hideBlank;
            questionPanelList[1].Visible = false;
            questionPanelList[1].Location = hideMult;
            questionPanelList[2].Visible = false;
            questionPanelList[2].Location = hideTF;

            panelResult.Visible = true;
            panelResult.Location = questionPoint;

            //TODO May need numbering changes
            buttonList[currentNumber - 1].Image = Resources.tick;
            buttonList[currentNumber].Image = Resources.unlock;
            buttonList[currentNumber].Enabled = true;
            
            //Replace with grade funct
            progressBarResult.Value = 60;
            labelResultGrade.Text = 60.ToString() + "/100";
            //Save Results to DB

        }

        private void buttonResult_Click(object sender, EventArgs e)
        {
            panelResult.Visible = false;
            panelTip.Visible = true;
            panelMain.Visible = true;
            textBoxMain.Visible = true;
            pictureBoxNext.Visible = false;
            buttonTakeQuiz.Visible = true;
        }

        private void changeLesson(int current)
        {
            switch (current)
            {
                case 1:
                    textBoxMain.Text = "1 * 1 = 1\n1 * 2 = 2\n1 * 3 = 3\n1 * 4 = 4\n1 * 5 = 5\n1 * 6 = 6\n1 * 7 = 7\n1 * 8 = 8\n1 * 9 = 9\n1 * 10 = 10\n";
                    break;
                case 2:
                    textBoxMain.Text = "2 * 1 = 2\n2 * 2 = 4\n2 * 3 = 6\n2 * 4 = 8\n2 * 5 = 10\n2 * 6 = 12\n2 * 7 = 14\n2 * 8 = 16\n2 * 9 = 18\n2 * 10 = 20\n";
                    labelTip.Text = "Είναι όλοι οι\nζυγοί αριθμοί!";
                    break;
                case 3:
                    textBoxMain.Text = "3 * 1 = 3\n3 * 2 = 6\n3 * 3 = 9\n3 * 4 = 12\n3 * 5 = 15\n3 * 6 = 18\n3 * 7 = 21\n3 * 8 = 24\n3 * 9 = 27\n3 * 10 = 30\n";
                    break;
                case 4:
                    textBoxMain.Text = "4 * 1 = 4\n4 * 2 = 8\n4 * 3 = 12\n4 * 4 = 16\n4 * 5 = 20\n4 * 6 = 24\n4 * 7 = 28\n4 * 8 = 32\n4 * 9 = 36\n4 * 10 = 40\n";
                    break;
                case 5:
                    textBoxMain.Text = "5 * 1 = 5\n5 * 2 = 10\n5 * 3 = 15\n5 * 4 = 20\n5 * 5 = 25\n5 * 6 = 30\n5 * 7 = 35\n5 * 8 = 40\n5 * 9 = 45\n5 * 10 = 50\n";
                    break;
                case 6:
                    textBoxMain.Text = "6 * 1 = 6\n6 * 2 = 12\n6 * 3 = 18\n6 * 4 = 24\n6 * 5 = 30\n6 * 6 = 36\n6 * 7 = 42\n6 * 8 = 48\n6 * 9 = 54\n6 * 10 = 60\n";
                    break;
                case 7:
                    textBoxMain.Text = "7 * 1 = 7\n7 * 2 = 14\n7 * 3 = 21\n7 * 4 = 28\n7 * 5 = 35\n7 * 6 = 42\n7 * 7 = 49\n7 * 8 = 56\n7 * 9 = 63\n7 * 10 = 70\n";
                    break;
                case 8:
                    textBoxMain.Text = "8 * 1 = 8\n8 * 2 = 16\n8 * 3 = 24\n8 * 4 = 32\n8 * 5 = 40\n8 * 6 = 48\n8 * 7 = 56\n8 * 8 = 64\n8 * 9 = 72\n8 * 10 = 80\n";
                    break;
                case 9:
                    textBoxMain.Text = "9 * 1 = 9\n9 * 2 = 18\n9 * 3 = 27\n9 * 4 = 36\n9 * 5 = 45\n9 * 6 = 54\n9 * 7 = 63\n9 * 8 = 72\n9 * 9 = 81\n9 * 10 = 90\n";
                    break;
                case 10:
                    textBoxMain.Text = "10 * 1 = 10\n10 * 2 = 20\n10 * 3 = 30\n10 * 4 = 40\n10 * 5 = 50\n10 * 6 = 60\n10 * 7 = 70\n10 * 8 = 80\n10 * 9 = 90\n10 * 10 = 100\n";
                    labelTip.Text = "Ξαναγράφουμε τον\nαριθμό και γράφουμε\nδίπλα του ένα 0";
                    break;
            }
            
        }

        private void textBoxMain_Click(object sender, EventArgs e)
        {
            labelTitle.Focus();
        }
    }
}

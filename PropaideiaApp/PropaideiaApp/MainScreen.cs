using PropaideiaApp.DataMappers;
using PropaideiaApp.Properties;
using PropaideiaApp.Quizes;
using PropaideiaApp.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PropaideiaApp
{
    public partial class MainScreen : Form
    {
        List<Button> buttonList = new List<Button>();
        List<Panel> questionPanelList = new List<Panel>();
        List<NumericUpDown> gradesList = new List<NumericUpDown>();
        int currentNumber;
        Point questionPoint = new Point(214, 96);
        Point hideTF = new Point(794, 442);
        Point hideMult = new Point(798, 31);
        Point hideBlank = new Point(181, 445);
        Point hideResult = new Point(608, 446);
        Point hideSettings = new Point(797, 3);

        int QUIZQUESTIONSNUM = 2;
        static int questionCounter = 0;

        //Database
        Student activeStudent;
        Professor activeProfessor;
        


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

            if (LoginScreen.userType == "student")
            {
                //TODO Move Panel Visibility changes here
                activeStudent = StudentMapper.Get(LoginScreen.activeUser);

                for (int i = 0; i < activeStudent.Level; i++)
                {
                    buttonList[i].Enabled = true;
                    buttonList[i].Image = Resources.tick;
                }
                buttonList[activeStudent.Level - 1].Image = Resources.unlock;
                buttonList[activeStudent.Level - 1].PerformClick();
            }
            else
            {
                //TODO Move Panel Visibility changes here
                activeProfessor = ProfessorMapper.Get(LoginScreen.activeUser);
                for(int i = 0; i < 11; i++)
                {
                    buttonList[i].Visible = false;
                }
                buttonSave.Visible = true;
                panelProf.Visible = true;
                buttonTakeQuiz.Visible = false;
                //TODO Add tooltip to disabled settings button
                pictureBoxSettings.Enabled = false;

                gradesList.Add(numericUpDown1);
                gradesList.Add(numericUpDown2);
                gradesList.Add(numericUpDown3);
                gradesList.Add(numericUpDown4);
                gradesList.Add(numericUpDown5);
                gradesList.Add(numericUpDown6);
                gradesList.Add(numericUpDown7);
                gradesList.Add(numericUpDown8);
                gradesList.Add(numericUpDown9);
                gradesList.Add(numericUpDown10);
                gradesList.Add(numericUpDownFinal);
            }

        }


        private void button1_Click(object sender, EventArgs e)
        {
            labelTitleNumber.Text = "1";
            currentNumber = 1;
            changeLesson(currentNumber);
            pictureBoxNumber.Image = Resources._1;
            resetQuiz();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            labelTitleNumber.Text = "2";
            currentNumber = 2;
            changeLesson(currentNumber);
            pictureBoxNumber.Image = Resources._2;
            resetQuiz();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            labelTitleNumber.Text = "3";
            currentNumber = 3;
            changeLesson(currentNumber);
            pictureBoxNumber.Image = Resources._3;
            resetQuiz();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            labelTitleNumber.Text = "4";
            currentNumber = 4;
            changeLesson(currentNumber);
            pictureBoxNumber.Image = Resources._4;
            resetQuiz();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            labelTitleNumber.Text = "5";
            currentNumber = 5;
            changeLesson(currentNumber);
            pictureBoxNumber.Image = Resources._5;
            resetQuiz();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            labelTitleNumber.Text = "6";
            currentNumber = 6;
            changeLesson(currentNumber);
            pictureBoxNumber.Image = Resources._6;
            resetQuiz();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            labelTitleNumber.Text = "7";
            currentNumber = 7;
            changeLesson(currentNumber);
            pictureBoxNumber.Image = Resources._7;
            resetQuiz();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            labelTitleNumber.Text = "8";
            currentNumber = 8;
            changeLesson(currentNumber);
            pictureBoxNumber.Image = Resources._8;
            resetQuiz();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            labelTitleNumber.Text = "9";
            currentNumber = 9;
            changeLesson(currentNumber);
            pictureBoxNumber.Image = Resources._9;
            resetQuiz();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            labelTitleNumber.Text = "10";
            currentNumber = 10;
            changeLesson(currentNumber);
            pictureBoxNumber.Image = Resources._10;
            resetQuiz();
        }

        private void buttonFinalExam_Click(object sender, EventArgs e)
        {

        }

        private void resetQuiz()
        {
            questionPanelList[0].Visible = false;
            questionPanelList[0].Location = hideBlank;
            questionPanelList[1].Visible = false;
            questionPanelList[1].Location = hideMult;
            questionPanelList[2].Visible = false;
            questionPanelList[2].Location = hideTF;

            panelResult.Visible = false;
            panelResult.Location = hideResult;
            panelTip.Visible = true;
            panelMain.Visible = true;
            textBoxMain.Visible = true;
            pictureBoxNext.Visible = false;
            buttonTakeQuiz.Visible = true;

        }

        private QuizManager quizManager;
        Question question;

        private void buttonTakeQuiz_Click(object sender, EventArgs e)
        {
            textBoxMain.Visible = false;
            pictureBoxNext.Visible = true;
            buttonTakeQuiz.Visible = false;
            labelTitle.Text = "Quiz για την προπαίδεια του ";
            panelTip.Visible = false;

            questionCounter = 0;
            quizManager = new QuizManager((PropaideiaType)currentNumber, QUIZQUESTIONSNUM);
            takeQuiz();
        }

        private void pictureBoxNext_Click(object sender, EventArgs e)
        {
            if (question.GetType().ToString() == "PropaideiaApp.Quizes.QuestionMC")
            {
                if (radioButtonMult1.Checked)
                {
                    quizManager.AssignAnswer("0");
                }
                else if (radioButtonMult2.Checked)
                {
                    quizManager.AssignAnswer("1");
                }
                else if (radioButtonMult3.Checked)
                {
                    quizManager.AssignAnswer("2");
                }
                else
                {
                    MessageBox.Show("Please select an answer before continuing", "Select an Answer", MessageBoxButtons.OK);
                    return;
                }
            }
            else if(question.GetType().ToString() == "PropaideiaApp.Quizes.QuestionTF")
            {
                if (radioButtonTrue.Checked)
                {
                    quizManager.AssignAnswer("true");
                }
                else if(radioButtonFalse.Checked)
                {
                    quizManager.AssignAnswer("false");
                }
                else
                {
                    MessageBox.Show("Please select an answer to continue", "Select an Answer", MessageBoxButtons.OK);
                    return;
                }
            }
            else
            {
                quizManager.AssignAnswer(numericBlank.Value.ToString());
            }

            //Clear buttons
            radioButtonMult1.Checked = false;
            radioButtonMult2.Checked = false;
            radioButtonMult3.Checked = false;
            radioButtonTrue.Checked = false;
            radioButtonFalse.Checked = false;
            numericBlank.Value = 0;

            takeQuiz();
        }

        private void takeQuiz()
        {
            if (questionCounter < QUIZQUESTIONSNUM)
            {
                question = quizManager.GetQuestion(questionCounter);

                if (question.GetType().ToString() == "PropaideiaApp.Quizes.QuestionMC")
                {
                    showQuestionMult(question.Description, ((QuestionMC)question).PossibleAns);
                }
                else if (question.GetType().ToString() == "PropaideiaApp.Quizes.QuestionFG")
                {
                    showQuestion("FG", question.Description);
                }
                else
                {
                    showQuestion("TF", question.Description);
                }
                questionCounter++;
            }
            else
            {
                endQuiz();
            }
        }

        private void showQuestionMult(string questionDescr, int[] answersList)
        {
            //Panel List: 0-Blank, 1-Multiple Choice, 2-True/False

            questionPanelList[0].Visible = false;
            questionPanelList[1].Location = questionPoint;
            questionPanelList[1].Visible = true;
            questionPanelList[2].Visible = false;
            labelMultQuest.Text = questionDescr;
            radioButtonMult1.Text = answersList[0].ToString();
            radioButtonMult2.Text = answersList[1].ToString();
            radioButtonMult3.Text = answersList[2].ToString();
        }

        private void showQuestion(string questionType, string questionDescr)
        {
            //Panel List: 0-Blank, 1-Multiple Choice, 2-True/False

            if (questionType == "FG")
            {
                questionPanelList[0].Location = questionPoint;
                questionPanelList[0].Visible = true;
                questionPanelList[1].Visible = false;
                questionPanelList[2].Visible = false;
                labelBlankQuest.Text = questionDescr;
            }
            else if (questionType == "TF")
            {
                questionPanelList[0].Visible = false;
                questionPanelList[1].Visible = false;
                questionPanelList[2].Location = questionPoint;
                questionPanelList[2].Visible = true;
                labelTFQuest.Text = questionDescr;
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

            quizManager.GradeQuiz();

            progressBarResult.Value = quizManager.QuizGrade;
            labelResultGrade.Text = quizManager.QuizGrade.ToString() + "/100";

            if (quizManager.QuizGrade > 80)
            {
                buttonList[currentNumber - 1].Image = Resources.tick;
                buttonList[currentNumber].Image = Resources.unlock;
                buttonList[currentNumber].Enabled = true;
                labelResult.Text = "Συγχαρητήρια!!! Πέρασες το quiz με βαθμό: ";
            }
            
            activeStudent.StudentProgress.PropaideiaProgress[currentNumber - 1] = quizManager.QuizGrade;
            if (!StudentMapper.Update(activeStudent))
            {
                MessageBox.Show("Failed to update progress!", "Error", MessageBoxButtons.OK);
            }
        }

        private void buttonResult_Click(object sender, EventArgs e)
        {
            panelResult.Visible = false;
            panelResult.Location = hideResult;
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
                    labelTip.Text = "Ξαναγράφουμε τον αριθμό!";
                    labelTip.Font = new Font("Minion Pro", 12, FontStyle.Bold);
                    break;
                case 2:
                    textBoxMain.Text = "2 * 1 = 2\n2 * 2 = 4\n2 * 3 = 6\n2 * 4 = 8\n2 * 5 = 10\n2 * 6 = 12\n2 * 7 = 14\n2 * 8 = 16\n2 * 9 = 18\n2 * 10 = 20\n";
                    labelTip.Font = new Font("Minion Pro", 12, FontStyle.Bold);
                    labelTip.Text = "Είναι όλοι οι ζυγοί αριθμοί!";
                    break;
                case 3:
                    textBoxMain.Text = "3 * 1 = 3\n3 * 2 = 6\n3 * 3 = 9\n3 * 4 = 12\n3 * 5 = 15\n3 * 6 = 18\n3 * 7 = 21\n3 * 8 = 24\n3 * 9 = 27\n3 * 10 = 30\n";
                    labelTip.Font = new Font("Minion Pro", 12, FontStyle.Bold);
                    labelTip.Text = "Κάθε φορά προσθέτουμε 3!";
                    break;
                case 4:
                    textBoxMain.Text = "4 * 1 = 4\n4 * 2 = 8\n4 * 3 = 12\n4 * 4 = 16\n4 * 5 = 20\n4 * 6 = 24\n4 * 7 = 28\n4 * 8 = 32\n4 * 9 = 36\n4 * 10 = 40\n";
                    labelTip.Text = "Διπλασιάζουμε τον αριθμό και μετά ξαναδιπλασιάζουμε το αποτέλεσμα!";
                    labelTip.Font = new Font("Minion Pro", 10, FontStyle.Bold);
                    break;
                case 5:
                    textBoxMain.Text = "5 * 1 = 5\n5 * 2 = 10\n5 * 3 = 15\n5 * 4 = 20\n5 * 5 = 25\n5 * 6 = 30\n5 * 7 = 35\n5 * 8 = 40\n5 * 9 = 45\n5 * 10 = 50\n";
                    labelTip.Text = "Κόβουμε τον αριθμό στην μέση και βάζουμε ένα 0 δίπλα του";
                    labelTip.Font = new Font("Minion Pro", 10, FontStyle.Bold);
                    break;
                case 6:
                    textBoxMain.Text = "6 * 1 = 6\n6 * 2 = 12\n6 * 3 = 18\n6 * 4 = 24\n6 * 5 = 30\n6 * 6 = 36\n6 * 7 = 42\n6 * 8 = 48\n6 * 9 = 54\n6 * 10 = 60\n";
                    labelTip.Text = "Όταν πολλαπλασιάζουμε το 6 με ζυγό αριθμό το αποτέλεσμα θα τελειώνει με αυτόν τον αριθμό!";
                    labelTip.Font = new Font("Minion Pro", 8, FontStyle.Bold);
                    break;
                case 7:
                    textBoxMain.Text = "7 * 1 = 7\n7 * 2 = 14\n7 * 3 = 21\n7 * 4 = 28\n7 * 5 = 35\n7 * 6 = 42\n7 * 7 = 49\n7 * 8 = 56\n7 * 9 = 63\n7 * 10 = 70\n";
                    labelTip.Text = "Κάθε φορά προσθέτουμε 7!";
                    labelTip.Font = new Font("Minion Pro", 12, FontStyle.Bold);
                    break;
                case 8:
                    textBoxMain.Text = "8 * 1 = 8\n8 * 2 = 16\n8 * 3 = 24\n8 * 4 = 32\n8 * 5 = 40\n8 * 6 = 48\n8 * 7 = 56\n8 * 8 = 64\n8 * 9 = 72\n8 * 10 = 80\n";
                    labelTip.Text = "Διπλασιάζουμε τον αριθμό και μετά διπλασιάζουμε το αποτέλεσμα και ξαναδιπλασιάζουμε για το τελικό αποτέλεσμα!";
                    labelTip.Font = new Font("Minion Pro", 8, FontStyle.Bold);
                    break;
                case 9:
                    textBoxMain.Text = "9 * 1 = 9\n9 * 2 = 18\n9 * 3 = 27\n9 * 4 = 36\n9 * 5 = 45\n9 * 6 = 54\n9 * 7 = 63\n9 * 8 = 72\n9 * 9 = 81\n9 * 10 = 90\n";
                    labelTip.Text = "Το πρώτο ψηφίο του αποτελέσματος αυξάνεται ενώ το δεύτερο μειώνεται!";
                    labelTip.Font = new Font("Minion Pro", 10, FontStyle.Bold);
                    break;
                case 10:
                    textBoxMain.Text = "10 * 1 = 10\n10 * 2 = 20\n10 * 3 = 30\n10 * 4 = 40\n10 * 5 = 50\n10 * 6 = 60\n10 * 7 = 70\n10 * 8 = 80\n10 * 9 = 90\n10 * 10 = 100\n";
                    labelTip.Text = "Ξαναγράφουμε τον αριθμό και γράφουμε δίπλα του ένα 0";
                    labelTip.Font = new Font("Minion Pro", 10, FontStyle.Bold);
                    break;
            }
            
        }

        private void textBoxMain_Click(object sender, EventArgs e)
        {
            labelTitle.Focus();
        }

        private void pictureBoxSettings_Click(object sender, EventArgs e)
        {
            resetQuiz();

            panelMenu.Enabled = false;
            panelSettings.Visible = true;
            panelSettings.Location = questionPoint;
            panelTip.Visible = false;
            panelMain.Visible = false;
            textBoxChangeName.Text = "";
        }

        private void buttonChangeUsername_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBoxChangeName.Text) && !String.IsNullOrEmpty(textBoxChangeSurname.Text) && Regex.IsMatch(textBoxChangeName.Text, @"^[a-zA-Z]+$") && Regex.IsMatch(textBoxChangeSurname.Text, @"^[a-zA-Z]+$"))
            {
                Student tempStudent = activeStudent;
                tempStudent.Name = textBoxChangeName.Text;
                tempStudent.Surname = textBoxChangeSurname.Text;
                if (StudentMapper.Update(tempStudent))
                {
                    activeStudent = tempStudent;
                }
                else
                {
                    MessageBox.Show("Student details change failed!", "Error", MessageBoxButtons.OK);
                }
                
            }
            else
            {
               MessageBox.Show("Please type a valid name/surname!", "Error", MessageBoxButtons.OK);
            }
        }

        private void buttonResetAccount_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure you want to reset your account's progress?", "Reset Progress?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                activeStudent.Level = 1;
                activeStudent.StudentProgress.FinalExam = 0;
                for(int i = 0; i < 10; i++)
                {
                    activeStudent.StudentProgress.PropaideiaProgress[i] = 0;
                }
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            string searchUser;

            if (!String.IsNullOrEmpty(textBoxSearch.Text))
            {
                if (StudentMapper.Get(textBoxSearch.Text) != null)
                {
                    searchUser = StudentMapper.Get(textBoxSearch.Text).Username;
                    StudentProgress prog = StudentProgressMapper.Get(searchUser);
                    for(int i = 0; i < 10; i++)
                    {
                        gradesList[i].Visible = true;
                        gradesList[i].Value = prog.PropaideiaProgress[i];
                    }
                    gradesList[10].Value = prog.FinalExam;
                }
                else
                {
                    MessageBox.Show("Student not found! Please try again!", "Not found", MessageBoxButtons.OK);
                }
            }
            else
            {
                MessageBox.Show("Please type the username of the student first!", "Error", MessageBoxButtons.OK);
            }
        }

        private void textBoxSearchResults_Click(object sender, EventArgs e)
        {
            labelLogo.Focus();
        }

        private void pictureBoxSettingsBack_Click(object sender, EventArgs e)
        {
            panelMenu.Enabled = true;
            panelSettings.Visible = false;
            panelSettings.Location = hideSettings;
            panelTip.Visible = true;
            panelMain.Visible = true;
        }
    }
}

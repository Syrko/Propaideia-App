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
        List<Button> buttonList = new List<Button>(); //List with all the side-menu buttons
        List<Panel> questionPanelList = new List<Panel>(); //List with all 3 question panel types
        List<NumericUpDown> gradesList = new List<NumericUpDown>(); //Grades List for professor view
        List<Label> studentGrades = new List<Label>(); //Students Grades list
        int currentNumber; //Shows currently selected propaideia number

        //When hiding a panel, it is moved to the side. Each panel has a separate spot so they don't become nested
        Point questionPoint = new Point(214, 96);
        Point hideTF = new Point(794, 442);
        Point hideMult = new Point(798, 31);
        Point hideBlank = new Point(181, 445);
        Point hideResult = new Point(608, 446);
        Point hideSettings = new Point(797, 3);
        Point hideGrades = new Point(795, 323);

        static int QUIZQUESTIONSNUM = 10; //Number of questions of each quiz
        static int EXAMQUESTIONSNUM = 20; //Number of questions of the final exam
        static int questionCounter = 0;
        int grade;

        //Quiz-Users object declarations
        private QuizManager quizManager;
        Question question;

        Student activeStudent;
        Professor activeProfessor;
        Student searchUser;


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

            if (LoginScreen.userType == "student") //User is a Student
            {
                activeStudent = StudentMapper.Get(LoginScreen.activeUser);
                if(activeStudent.Level < 12) //Update buttons based on quiz completion (Level)
                {
                    for (int i = 0; i < activeStudent.Level; i++)
                    {
                        buttonList[i].Enabled = true;
                        buttonList[i].Image = Resources.tick;
                        toolTipMain.SetToolTip(buttonList[i], "Έχει ολοκληρωθεί η προπαίδεια του " + (i + 1).ToString());
                    }
                    buttonList[activeStudent.Level - 1].Image = Resources.unlock;
                    toolTipMain.SetToolTip(buttonList[activeStudent.Level - 1], "Διαβάστε την προπαίδεια και όταν είστε έτοιμοι δοκιμάστε το τεστ!");
                    buttonList[activeStudent.Level - 1].PerformClick();
                }
                else //Update all buttons as complete
                {
                    for (int i = 0; i <= 10; i++)
                    {
                        buttonList[i].Enabled = true;
                        buttonList[i].Image = Resources.tick;
                        toolTipMain.SetToolTip(buttonList[i], "Έχει ολοκληρωθεί η προπαίδεια του " + (i + 1).ToString());
                    }
                    buttonList[10].PerformClick();
                }
                
                pictureBoxGrades.Visible = true;

                studentGrades.Add(labelGradeShow1);
                studentGrades.Add(labelGradeShow2);
                studentGrades.Add(labelGradeShow3);
                studentGrades.Add(labelGradeShow4);
                studentGrades.Add(labelGradeShow5);
                studentGrades.Add(labelGradeShow6);
                studentGrades.Add(labelGradeShow7);
                studentGrades.Add(labelGradeShow8);
                studentGrades.Add(labelGradeShow9);
                studentGrades.Add(labelGradeShow10);
                studentGrades.Add(labelGradeShowFinal);
            }
            else //User is a professor
            {
                activeProfessor = ProfessorMapper.Get(LoginScreen.activeUser);
                for(int i = 0; i < 11; i++)
                {
                    buttonList[i].Visible = false;
                }
                buttonSave.Visible = true;
                panelProf.Visible = true;
                buttonTakeQuiz.Visible = false;
                pictureBoxSettings.Enabled = false;
                labelTitle.Text = "Διαχείριση Μαθητών";
                labelTitleNumber.Visible = false;

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


        private void pictureBoxHelp_Click(object sender, EventArgs e)
        {
            if(LoginScreen.userType == "student")
            {
                Process.Start(@"OnlineHelp\MainHelp.html");
            }
            else
            {
                Process.Start(@"OnlineHelp\MainProfHelp.html");
            }
            
        }

        //Student's Grades List
        private void pictureBoxGrades_Click(object sender, EventArgs e)
        {
            //Show student grades
            resetQuiz();
            panelGrades.Location = new Point(193,65);
            panelGrades.Visible = true;
            panelMenu.Enabled = false;
            panelMain.Visible = false;
            panelSettings.Visible = false;
            panelTip.Visible = false;
            panelSettings.Visible = false;
            buttonTakeQuiz.Visible = false;

            //Fill the numbericUpDowns with the student's progress
            for(int i = 0; i < 10; i++)
            {
                studentGrades[i].Text = activeStudent.StudentProgress.PropaideiaProgress[i].ToString();
            }
            studentGrades[10].Text = activeStudent.StudentProgress.FinalExam.ToString();
        }

        private void pictureBoxGradesBack_Click(object sender, EventArgs e)
        {
            //Return to main menu
            panelGrades.Location = hideGrades;
            panelGrades.Visible = false;
            panelSettings.Visible = false;
            panelMenu.Enabled = true;
            panelMain.Visible = true;
            panelTip.Visible = true;
            buttonTakeQuiz.Visible = true;
        }

        //Clicking a sidemenu button updates the current propaideia number and changes the text in titles and textboxes
        //If the user is in a quiz and clicks a sidemenu button, he leaves the quiz and all progress is lost
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
            //Show different text based on if the student has passed the final exam
            currentNumber = 0;
            if (activeStudent.Level == 12)
            {
                labelTitle.Text = "Συγχαρητήρια!";
                labelTitleNumber.Visible = false;
                labelTip.Text = "Μπράβο! Τώρα πια ξέρεις την προπαίδεια μέχρι και το 10!";
                textBoxMain.Text = "Πολύ καλή προσπάθεια! Έχετε ολοκληρώσει την εκπαίδευση,\nμπορείτε να συνεχίσετε να διαβάζετε\nή να βελτιώσετε τους βαθμούς σας στα τεστ!";
                toolTipMain.SetToolTip(buttonList[10], "Έχετε ολοκληρώσει και την τελική εξέταση!");
                labelTip.Font = new Font("Minion Pro", 10, FontStyle.Bold);
            }
            else
            {
                toolTipMain.SetToolTip(buttonList[10], "Κάντε μια επανάληψη και δοκιμάστε την τελική εξέταση!");
                labelTitleNumber.Text = "";
                changeLesson(currentNumber);
            }
            resetQuiz();
        }

        //Quiz Classes
        private void buttonTakeQuiz_Click(object sender, EventArgs e)
        {
            //Start Quiz
            textBoxMain.Visible = false;
            pictureBoxNext.Visible = true;
            buttonTakeQuiz.Visible = false;
            panelTip.Visible = false;
            questionCounter = 0;
            if (currentNumber != 0)
            {
                labelTitle.Text = "Quiz για την προπαίδεια του ";
                quizManager = new QuizManager((PropaideiaType)currentNumber, QUIZQUESTIONSNUM);
            }
            else
            {
                labelTitle.Text = "Τελική Εξέταση!";
                quizManager = new QuizManager((PropaideiaType)currentNumber, EXAMQUESTIONSNUM);
            }

            takeQuiz();
        }

        private void pictureBoxNext_Click(object sender, EventArgs e)
        {
            //Save answer and calls takeQuiz to go to the next question
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
                    MessageBox.Show("Παρακαλούμε επιλέξτε μια απάντηση πριν συνεχίσετε!", "Επιλέξτε μια απάντηση", MessageBoxButtons.OK);
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
                    MessageBox.Show("Παρακαλούμε επιλέξτε μια απάντηση", "Επιλέξτε μια απάντηση", MessageBoxButtons.OK);
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

        private void takeQuiz() //show question based on it's type until we reach QUIZQUESTIONNUM number of questions
        {
            if(currentNumber != 0) //Quiz
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
            else //Final Exam
            {
                if (questionCounter < EXAMQUESTIONSNUM)
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
            
        }

        //Quiz UI visibility updates
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
            //hide every question
            questionPanelList[0].Visible = false;
            questionPanelList[0].Location = hideBlank;
            questionPanelList[1].Visible = false;
            questionPanelList[1].Location = hideMult;
            questionPanelList[2].Visible = false;
            questionPanelList[2].Location = hideTF;

            pictureBoxNext.Visible = false;

            panelResult.Visible = true;
            panelResult.Location = questionPoint;

            //Get results-grade
            quizManager.GradeQuiz();
            grade = quizManager.QuizGrade;

            //Update UI based on results
            progressBarResult.Value = grade;
            labelResultGrade.Text = grade.ToString() + "/100";

            if (currentNumber != 0) //Quiz
            {
                if (activeStudent.StudentProgress.PropaideiaProgress[currentNumber - 1] < grade)
                {
                     activeStudent.StudentProgress.PropaideiaProgress[currentNumber - 1] = grade;
                }
                if (grade > 80)
                {
                    buttonList[currentNumber - 1].Image = Resources.tick;
                    toolTipMain.SetToolTip(buttonList[currentNumber - 1], "Έχει ολοκληρωθεί η προπαίδεια του " + currentNumber);
                    if (!buttonList[currentNumber].Enabled)
                    {
                        buttonList[currentNumber].Image = Resources.unlock;
                        toolTipMain.SetToolTip(buttonList[currentNumber], "Διαβάστε την προπαίδεια και όταν είστε έτοιμοι δοκιμάστε το τεστ!");
                        buttonList[currentNumber].Enabled = true;
                    }
                    labelResult.Text = "Συγχαρητήρια!!!\nΠέρασες το quiz με βαθμό: ";
                    if (activeStudent.Level <= currentNumber)
                    {
                        activeStudent.Level = currentNumber + 1;
                    }
                }
                else
                {
                    labelResult.Text = "Δεν πέρασες το τεστ!\nΔιάβασε ξανά την προπαίδεια\nκαι ξαναδοκίμασε!";
                }
            }
            else //Final Exam
            {
                activeStudent.StudentProgress.FinalExam = grade;
                if (grade > 80)
                {
                    buttonList[10].Image = Resources.tick;
                    buttonList[10].Enabled = true;
                    labelResult.Text = "Συγχαρητήρια!!!\nΠέρασες την εξέταση με βαθμό: ";
                    if (activeStudent.Level < 12)
                    {
                        activeStudent.Level = 12;
                    }
                }
            }

            StudentMapper.Update(activeStudent);
        }


        private void buttonResult_Click(object sender, EventArgs e)
        {
            //Continue button after quiz returns the user to the main screen
            panelResult.Visible = false;
            panelResult.Location = hideResult;
            panelTip.Visible = true;
            panelMain.Visible = true;
            textBoxMain.Visible = true;
            buttonTakeQuiz.Visible = true;
            buttonList[currentNumber].PerformClick();
        }
        private void resetQuiz()
        {
            //hide every question and return to main screen
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

        //Change the main screen text content, based on the current active propaideia
        private void changeLesson(int current)
        {
            switch (current)
            {
                case 0:
                    labelTitle.Text = "Τελική Εξέταση";
                    textBoxMain.Text = "Συγχαρητήρια!\n\nΈχετε περάσει όλα τα quiz!\n\nΤο μόνο που μένει είναι\nνα απαντήσετε στις ερωτήσεις\nτου τελικού διαγωνίσματος!\n\nΠατήστε το παράκατω\nκουμπί όταν είστε έτοιμοι";
                    labelTip.Text = "Κάνε μια επανάληψη πριν δώσεις το διαγώνισμα!";
                    labelTip.Font = new Font("Minion Pro", 10, FontStyle.Bold);
                    break;
                case 1:
                    labelTitle.Text = "Μαθαίνω την προπαίδεια του ";
                    textBoxMain.Text = "1 x 1 = 1\n1 x 2 = 2\n1 x 3 = 3\n1 x 4 = 4\n1 x 5 = 5\n1 x 6 = 6\n1 x 7 = 7\n1 x 8 = 8\n1 x 9 = 9\n1 x 10 = 10\n";
                    labelTip.Text = "Ξαναγράφουμε τον δεύτερο αριθμό!";
                    labelTip.Font = new Font("Minion Pro", 12, FontStyle.Bold);
                    break;
                case 2:
                    labelTitle.Text = "Μαθαίνω την προπαίδεια του ";
                    textBoxMain.Text = "2 x 1 = 2\n2 x 2 = 4\n2 x 3 = 6\n2 x 4 = 8\n2 x 5 = 10\n2 x 6 = 12\n2 x 7 = 14\n2 x 8 = 16\n2 x 9 = 18\n2 x 10 = 20\n";
                    labelTip.Font = new Font("Minion Pro", 12, FontStyle.Bold);
                    labelTip.Text = "Διπλασιάζουμε τον δεύτερο αριθμό!";
                    break;
                case 3:
                    labelTitle.Text = "Μαθαίνω την προπαίδεια του ";
                    textBoxMain.Text = "3 x 1 = 3\n3 x 2 = 6\n3 x 3 = 9\n3 x 4 = 12\n3 x 5 = 15\n3 x 6 = 18\n3 x 7 = 21\n3 x 8 = 24\n3 x 9 = 27\n3 x 10 = 30\n";
                    labelTip.Font = new Font("Minion Pro", 12, FontStyle.Bold);
                    labelTip.Text = "Κάθε φορά προσθέτουμε 3!";
                    break;
                case 4:
                    labelTitle.Text = "Μαθαίνω την προπαίδεια του ";
                    textBoxMain.Text = "4 x 1 = 4\n4 x 2 = 8\n4 x 3 = 12\n4 x 4 = 16\n4 x 5 = 20\n4 x 6 = 24\n4 x 7 = 28\n4 x 8 = 32\n4 x 9 = 36\n4 x 10 = 40\n";
                    labelTip.Text = "Διπλασιάζουμε τον δεύτερο αριθμό και ξαναδιπλασιάζουμε το αποτέλεσμα!";
                    labelTip.Font = new Font("Minion Pro", 10, FontStyle.Bold);
                    break;
                case 5:
                    labelTitle.Text = "Μαθαίνω την προπαίδεια του ";
                    textBoxMain.Text = "5 x 1 = 5\n5 x 2 = 10\n5 x 3 = 15\n5 x 4 = 20\n5 x 5 = 25\n5 x 6 = 30\n5 x 7 = 35\n5 x 8 = 40\n5 x 9 = 45\n5 x 10 = 50\n";
                    labelTip.Text = "Όλα  τα αποτελέσματα τελειώνουν με 0 ή 5!";
                    labelTip.Font = new Font("Minion Pro", 10, FontStyle.Bold);
                    break;
                case 6:
                    labelTitle.Text = "Μαθαίνω την προπαίδεια του ";
                    textBoxMain.Text = "6 x 1 = 6\n6 x 2 = 12\n6 x 3 = 18\n6 x 4 = 24\n6 x 5 = 30\n6 x 6 = 36\n6 x 7 = 42\n6 x 8 = 48\n6 x 9 = 54\n6 x 10 = 60\n";
                    labelTip.Text = "Όταν πολλαπλασιάζουμε το 6 με ζυγό αριθμό το αποτέλεσμα θα τελειώνει με αυτόν τον αριθμό!";
                    labelTip.Font = new Font("Minion Pro", 8, FontStyle.Bold);
                    break;
                case 7:
                    labelTitle.Text = "Μαθαίνω την προπαίδεια του ";
                    textBoxMain.Text = "7 x 1 = 7\n7 x 2 = 14\n7 x 3 = 21\n7 x 4 = 28\n7 x 5 = 35\n7 x 6 = 42\n7 x 7 = 49\n7 x 8 = 56\n7 x 9 = 63\n7 x 10 = 70\n";
                    labelTip.Text = "Κάθε φορά προσθέτουμε 7!";
                    labelTip.Font = new Font("Minion Pro", 12, FontStyle.Bold);
                    break;
                case 8:
                    labelTitle.Text = "Μαθαίνω την προπαίδεια του ";
                    textBoxMain.Text = "8 x 1 = 8\n8 x 2 = 16\n8 x 3 = 24\n8 x 4 = 32\n8 x 5 = 40\n8 x 6 = 48\n8 x 7 = 56\n8 x 8 = 64\n8 x 9 = 72\n8 x 10 = 80\n";
                    labelTip.Text = "Διπλασιάζουμε τον δεύτερο αριθμό και μετά διπλασιάζουμε το αποτέλεσμα και ξαναδιπλασιάζουμε για το τελικό αποτέλεσμα!";
                    labelTip.Font = new Font("Minion Pro", 8, FontStyle.Bold);
                    break;
                case 9:
                    labelTitle.Text = "Μαθαίνω την προπαίδεια του ";
                    textBoxMain.Text = "9 x 1 = 9\n9 x 2 = 18\n9 x 3 = 27\n9 x 4 = 36\n9 x 5 = 45\n9 x 6 = 54\n9 x 7 = 63\n9 x 8 = 72\n9 x 9 = 81\n9 x 10 = 90\n";
                    labelTip.Text = "Το πρώτο ψηφίο του αποτελέσματος αυξάνεται κατά ένα ενώ το δεύτερο μειώνεται κατά ένα!";
                    labelTip.Font = new Font("Minion Pro", 10, FontStyle.Bold);
                    break;
                case 10:
                    labelTitle.Text = "Μαθαίνω την προπαίδεια του ";
                    textBoxMain.Text = "10 x 1 = 10\n10 x 2 = 20\n10 x 3 = 30\n10 x 4 = 40\n10 x 5 = 50\n10 x 6 = 60\n10 x 7 = 70\n10 x 8 = 80\n10 x 9 = 90\n10 x 10 = 100\n";
                    labelTip.Text = "Ξαναγράφουμε τον δεύτερο αριθμό και γράφουμε δίπλα του ένα 0";
                    labelTip.Font = new Font("Minion Pro", 10, FontStyle.Bold);
                    break;
            }
            
        }
        private void textBoxMain_Click(object sender, EventArgs e)
        {
            //Avoiding cursor on textbox
            labelTitle.Focus();
        }

        //Settings classes
        private void pictureBoxSettings_Click(object sender, EventArgs e)
        {
            //Show settings menu
            resetQuiz();

            panelMenu.Enabled = false;
            panelSettings.Visible = true;
            panelSettings.Location = questionPoint;
            panelTip.Visible = false;
            panelMain.Visible = false;
            panelGrades.Visible = false;
            buttonTakeQuiz.Visible = false;
            buttonUpdateName.Visible = true;
            textBoxChangeName.Text = activeStudent.Name;
            textBoxChangeSurname.Text = activeStudent.Surname;

        }

        private void buttonUpdateName_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBoxChangeName.Text) && !String.IsNullOrEmpty(textBoxChangeSurname.Text) && Regex.IsMatch(textBoxChangeName.Text, @"^[a-zA-Z]+$") && Regex.IsMatch(textBoxChangeSurname.Text, @"^[a-zA-Z]+$"))
            {
                //Change name and surname of a temp user and if StudentMapper. Update succeeds we replace the activeStudent with it
                Student tempStudent = activeStudent;
                tempStudent.Name = textBoxChangeName.Text;
                tempStudent.Surname = textBoxChangeSurname.Text;
                if (StudentMapper.Update(tempStudent))
                {
                    activeStudent = tempStudent;
                    MessageBox.Show("Τα στοιχεία του λογαριασμού σας ενημερώθηκαν επιτυχώς!", "Ο λογαριασμός ενημερώθηκε επιτυχώς", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("Η αλλαγή των στοιχείων του μαθητή απέτυχε!", "Σφάλμα", MessageBoxButtons.OK);
                }
            }
            else
            {
                MessageBox.Show("Παρακαλούμε πληκτρολογήστε ένα έγκυρο όνομα/επώνυμο!", "Ειδοποίηση", MessageBoxButtons.OK);
            }
        }

        private void buttonResetAccount_Click(object sender, EventArgs e)
        {
            //After a warning, the student's progress is reset to 0
            if (MessageBox.Show("Είστε βέβαιοι ότι θέλετε να επαναφέτε την πρόοδο του λογαριασμού σας?", "Επαναφορά προόδου;", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                activeStudent.Level = 1;
                activeStudent.StudentProgress.FinalExam = 0;
                for (int i = 0; i < 10; i++)
                {
                    activeStudent.StudentProgress.PropaideiaProgress[i] = 0;
                }
                StudentMapper.Update(activeStudent);
                MessageBox.Show("Ο λογαριασμός σας έχει επαναφερθεί! Θα μεταφερθείτε στην οθόνη σύνδεσης!", "Επαναφορά Λογαριασμού", MessageBoxButtons.OK);
                this.Hide();
                LoginScreen loginForm = new LoginScreen();
                loginForm.Show();
            }
        }

        private void pictureBoxSettingsBack_Click(object sender, EventArgs e)
        {
            //Return to main screen
            panelMenu.Enabled = true;
            panelSettings.Visible = false;
            panelSettings.Location = hideSettings;
            panelTip.Visible = true;
            panelMain.Visible = true;
            buttonUpdateName.Visible = false;
            buttonTakeQuiz.Visible = true;
        }

        //Professor Classes
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBoxSearch.Text))
            {
                if (StudentMapper.Get(textBoxSearch.Text) != null) //If the student exists
                {
                    //Display student's grades on the NumericUpDowns
                    searchUser = StudentMapper.Get(textBoxSearch.Text);
                    for (int i = 0; i < 10; i++)
                    {
                        gradesList[i].Visible = true;
                        gradesList[i].Value = searchUser.StudentProgress.PropaideiaProgress[i];
                    }
                    gradesList[10].Visible = true;
                    gradesList[10].Value = searchUser.StudentProgress.FinalExam;
                }
                else
                {
                    MessageBox.Show("Δεν βρέθηκε ο μαθητής!", "Δεν βρέθηκε", MessageBoxButtons.OK);
                }
            }
            else
            {
                MessageBox.Show("Παρακαλούμε πληκτρολογήστε το όνομα χρήστη του μαθητή πρώτα!", "Ειδοποίηση", MessageBoxButtons.OK);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            //
            int tempLevel = 0;
            if(searchUser != null)
            {
                for(int i = 0; i < 10; i++)
                {
                    if(gradesList[i].Value > 80)
                    {
                        tempLevel = i + 2; //Find the highest passed propaideia i.e. if propaideia 6 quiz is passed the Level should be 7 and because "i" starts from zero, we increase it by 2
                        searchUser.StudentProgress.PropaideiaProgress[i] = Convert.ToInt32(gradesList[i].Value);
                    }
                }
                if(gradesList[10].Value > 80)
                {
                    searchUser.Level = 12;
                }
                else
                {
                    searchUser.Level = tempLevel;
                }
                searchUser.StudentProgress.FinalExam = Convert.ToInt32(gradesList[10].Value);
                StudentMapper.Update(searchUser);
                MessageBox.Show("Οι βαθμοί του " + searchUser.Username + " έχουν ενημερωθεί!", "Επιτυχής ενημέρωση!", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("Παρακαλούμε αναζητήστε πρώτα έναν μαθητή!", "Ειδοποίηση", MessageBoxButtons.OK);
            }
        }
        private void textBoxSearchResults_Click(object sender, EventArgs e)
        {
            //Avoiding cursor on textbox
            labelLogo.Focus();
        }

        private void textBoxSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                buttonSearch.PerformClick();
            }
        }

        private void MainScreen_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}

﻿using PropaideiaApp.Properties;
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
        int currentNumber;
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
            labelTip.Text = "Ξαναγράφουμε τον\nαριθμό και γράφουμε\nδίπλα το ένα 0";
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

        private void changeLesson(int current)
        {
            switch (current)
            {
                case 1:
                    textBoxMain.Text = "1 * 1 = 1\n1 * 2 = 2\n1 * 3 = 3\n1 * 4 = 4\n1 * 5 = 5\n1 * 6 = 6\n1 * 7 = 7\n1 * 8 = 8\n1 * 9 = 9\n1 * 10 = 10\n";
                    break;
                case 2:
                    textBoxMain.Text = "2 * 1 = 2\n2 * 2 = 4\n2 * 3 = 6\n2 * 4 = 8\n2 * 5 = 10\n2 * 6 = 12\n2 * 7 = 14\n2 * 8 = 16\n2 * 9 = 18\n2 * 10 = 20\n";
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
                    break;
            }
            
        }
    }
}

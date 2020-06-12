using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropaideiaApp.Quizes
{
	class QuizManager
	{
		private QuestionGenerator questionGenerator;
		private int questionsNum;
		private PropaideiaType propaideiaType;
		private List<string> studentAnswers;

		private int quizGrade;

		public int QuizGrade { get => quizGrade; }

		internal QuizManager(PropaideiaType propaideiaType, int questionsNum)
		{
			this.propaideiaType = propaideiaType;
			this.questionsNum = questionsNum;
			this.questionGenerator = new QuestionGenerator(this.propaideiaType, questionsNum);
			this.studentAnswers = new List<string>(questionsNum);

			GenerateQuestions();
		}

		private void GenerateQuestions()
		{
			for(int i = 0; i < questionsNum; i++)
			{
				questionGenerator.CreateNextQuestion();
			}
		}

		internal void AssignAnswer(string answer)
		{
			studentAnswers.Add(answer);
		}

		internal Question GetQuestion(int questionNumber)
		{
			return questionGenerator.Questions[questionNumber];
		}

		internal void GradeQuiz()
		{
			List<Question> questions = questionGenerator.Questions;
			for(int i = 0; i < questionsNum; i++)
			{
				questions[i].StudentAnswer = questions[i].CorrectAnswer == studentAnswers[i];
			}
			foreach(Question q in questions)
			{
				if (q.StudentAnswer)
				{
					quizGrade++;
				}
			}
			quizGrade = quizGrade / questionsNum * 100;
		}
	}

	enum PropaideiaType
	{
		PROPAIDEIA1 = 1,
		PROPAIDEIA2 = 2,
		PROPAIDEIA3 = 3,
		PROPAIDEIA4 = 4,
		PROPAIDEIA5 = 5,
		PROPAIDEIA6 = 6,
		PROPAIDEIA7 = 7,
		PROPAIDEIA8 = 8,
		PROPAIDEIA9 = 9,
		PROPAIDEIA10 = 10,
		FINAL_EXAM = 0
	}

	enum QuestionFormat // TODO change diagram
	{
		FILL_GAPS = 0,
		MULTIPLE_CHOICE = 1,
		TRUE_FALSE = 2
	}
}

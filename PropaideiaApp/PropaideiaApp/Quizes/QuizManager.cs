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
		private int propaideiaType;
		private List<string> answers;

		private int quizGrade;

		public int QuizGrade { get => quizGrade; }

		internal QuizManager(PropaideiaType propaideiaType, int questionsNum)
		{
			this.propaideiaType = (int)propaideiaType;
			this.questionsNum = questionsNum;
			this.questionGenerator = new QuestionGenerator(this.propaideiaType, questionsNum);
			this.answers = new List<string>(questionsNum);

			GenerateQuestions();
		}

		private void GenerateQuestions()
		{
			for(int i = 0; i < questionsNum; i++)
			{
				questionGenerator.CreateNextQuestion();
			}
		}

		internal void AssignAnswer(string answer, int questionNumber)
		{
			answers[questionNumber] = answer;
		}

		internal void GradeQuiz()
		{
			List<Question> questions = questionGenerator.Questions;
			for(int i = 0; i < questionsNum; i++)
			{
				questions[i].Result = questions[i].Answer == answers[i];
			}
			foreach(Question q in questions)
			{
				if (q.Result)
				{
					QuizGrade++;
				}
			}
			QuizGrade = QuizGrade / questionsNum * 100;
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

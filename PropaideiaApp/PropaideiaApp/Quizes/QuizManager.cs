using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropaideiaApp.Quizes
{
	/// <summary>
	/// The central class for the quiz creation and management.
	/// The forms of this program need only to create an object of this class to access the functionality of quizzes.
	/// </summary>
	class QuizManager
	{
		private QuestionGenerator questionGenerator;
		private int questionsNum;
		private PropaideiaType propaideiaType;
		private List<string> studentAnswers;

		private int quizGrade;

		public int QuizGrade { get => quizGrade; }

		/// <summary>
		/// Creates a new quiz of the given type and number of questions.
		/// </summary>
		/// <param name="propaideiaType">The type of the quiz.</param>
		/// <param name="questionsNum">The number of questions the quiz will have.</param>
		internal QuizManager(PropaideiaType propaideiaType, int questionsNum)
		{
			this.propaideiaType = propaideiaType;
			this.questionsNum = questionsNum;
			this.questionGenerator = new QuestionGenerator(this.propaideiaType, questionsNum);
			this.studentAnswers = new List<string>(questionsNum);

			GenerateQuestions();
		}

		/// <summary>
		/// Generates the necessary number of questions.
		/// </summary>
		private void GenerateQuestions()
		{
			for(int i = 0; i < questionsNum; i++)
			{
				questionGenerator.CreateNextQuestion();
			}
		}

		/// <summary>
		/// Assigns the user's answer to its respective question.
		/// </summary>
		/// <param name="answer">The user's answer.</param>
		internal void AssignAnswer(string answer)
		{
			studentAnswers.Add(answer);
		}

		/// <summary>
		/// A simple question getter based on the question's position in the quiz.
		/// </summary>
		/// <param name="questionNumber">The question's position in the quiz (0-based index).</param>
		/// <returns>The question needed in that position.</returns>
		internal Question GetQuestion(int questionNumber)
		{
			return questionGenerator.Questions[questionNumber];
		}

		/// <summary>
		/// Grades the quiz beased on the user's assigned answers.
		/// </summary>
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

			quizGrade = (quizGrade * 100) / questionsNum;
		}
	}

	/// <summary>
	/// Simple enumeration for the types of quiz available.
	/// </summary>
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

	/// <summary>
	/// Simple enumeration for the question types available.
	/// </summary>
	enum QuestionFormat // TODO change diagram
	{
		FILL_GAPS = 0,
		MULTIPLE_CHOICE = 1,
		TRUE_FALSE = 2
	}
}

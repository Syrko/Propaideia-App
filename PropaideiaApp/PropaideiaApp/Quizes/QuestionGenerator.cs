using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropaideiaApp.Quizes
{
	/// <summary>
	/// The generator responsible for creating questions of the various types needed to create a new quiz.
	/// </summary>
	class QuestionGenerator // TODO change diagram
	{
		private PropaideiaType propaideiaType;
		private List<Question> questions;

		public QuestionGenerator(PropaideiaType propaideiaType, int questionNum)
		{
			this.propaideiaType = propaideiaType;
			this.questions = new List<Question>(questionNum);
		}

		internal List<Question> Questions { get => questions; }

		/// <summary>
		/// Creates the next question of the quiz and selects the question type randomly.
		/// </summary>
		internal void CreateNextQuestion()
		{
			int questionType = Randomizer.RollZeroBasedD3();

			switch (questionType)
			{
				case (int)QuestionFormat.FILL_GAPS:
					Questions.Add(new QuestionFG(propaideiaType));
					break;
				case (int)QuestionFormat.TRUE_FALSE:
					Questions.Add(new QuestionTF(propaideiaType));
					break;
				case (int)QuestionFormat.MULTIPLE_CHOICE:
					Questions.Add(new QuestionMC(propaideiaType));
					break;
			}
		}
	}
}

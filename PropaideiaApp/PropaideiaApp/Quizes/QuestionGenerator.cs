using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropaideiaApp.Quizes
{
	class QuestionGenerator // TODO change diagram
	{
		private int propaideiaType;
		private int questionNum;		// TODO change diagram
		private List<Question> questions;
		private int counter;

		public QuestionGenerator(int propaideiaType, int questionNum)
		{
			this.propaideiaType = propaideiaType;
			this.questionNum = questionNum;
			this.Questions = new List<Question>(questionNum);
			this.counter = 0;
		}

		internal List<Question> Questions { get => questions; set => questions = value; }

		internal void CreateNextQuestion()
		{
			Random rand = new Random();
			int questionType = rand.Next(0, 3);

			switch (questionType)
			{
				case (int)QuestionFormat.FILL_GAPS:
					Questions[counter] = new QuestionFG(propaideiaType);
					break;
				case (int)QuestionFormat.TRUE_FALSE:
					Questions[counter] = new QuestionTF(propaideiaType);
					break;
				case (int)QuestionFormat.MULTIPLE_CHOICE:
					Questions[counter] = new QuestionMC(propaideiaType);
					break;
			}
			counter++;
		}
	}
}

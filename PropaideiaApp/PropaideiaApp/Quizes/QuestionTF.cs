using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropaideiaApp.Quizes
{
	class QuestionTF : Question
	{
		public QuestionTF(int propaideia)
		{
			this.Propaideia = propaideia;

			GenerateQuestion();
		}

		private void GenerateQuestion()
		{
			int[] tempAsk = new int[3];

			bool chance = (Rand.Next(100) - 50) < 0;
			if (chance)
			{
				tempAsk[0] = this.Propaideia;
				tempAsk[1] = Rand.Next(1, this.Propaideia + 1);
			}
			else
			{
				tempAsk[1] = this.Propaideia;
				tempAsk[0] = Rand.Next(1, this.Propaideia + 1);
			}

			chance = (Rand.Next(100) - 50) < 0;
			if (chance)
			{
				tempAsk[2] = tempAsk[0] * tempAsk[1];
				this.Answer = "True";
			}
			else
			{
				chance = (Rand.Next(100) - 50) < 0;
				if (chance)
				{
					tempAsk[2] = tempAsk[0] * tempAsk[1] + this.Propaideia;
				}
				else
				{
					tempAsk[2] = tempAsk[0] * tempAsk[1] - this.Propaideia;
				}
				this.Answer = "False";
			}
			this.Ask = tempAsk[0] + " x " + tempAsk[1] + " = " + tempAsk[2];
		}
	}
}

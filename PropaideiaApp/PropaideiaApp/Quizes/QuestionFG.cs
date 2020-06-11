using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropaideiaApp.Quizes
{
	class QuestionFG : Question
	{
		public QuestionFG(PropaideiaType propaideia)
		{
			this.Propaideia = propaideia;

			GenerateQuestion();
		}

		private void GenerateQuestion()
		{
			// Determine which part of the question will be the gap
			int gap = Rand.Next(0, 3);
			int[] tempAsk = new int[3];
			tempAsk[gap] = 0;

			if (tempAsk[2] == 0)
			{
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
				this.CorrectAnswer = (tempAsk[0] * tempAsk[1]).ToString();
				this.Description = tempAsk[0].ToString() + " x " + tempAsk[1].ToString() + " = _";
			}
			else
			{
				bool chance = (Rand.Next(100) - 50) < 0;
				if (tempAsk[0] == 0)
				{
					if (chance)
					{
						tempAsk[1] = Rand.Next(1, this.Propaideia + 1);
					}
					else
					{
						tempAsk[1] = this.Propaideia;
					}
					tempAsk[2] = tempAsk[0] * tempAsk[1];
					this.CorrectAnswer = tempAsk[2].ToString();
					this.Description = "_ x " + tempAsk[1].ToString() + " = " + tempAsk[2].ToString();
				}
				else
				{
					if (chance)
					{
						tempAsk[0] = Rand.Next(1, this.Propaideia + 1);
					}
					else
					{
						tempAsk[0] = this.Propaideia;
					}
					tempAsk[2] = tempAsk[0] * tempAsk[1];
					this.CorrectAnswer = tempAsk[2].ToString();
					this.Description = tempAsk[0].ToString() + " x _ = " + tempAsk[2].ToString();
				}
			}
		}
	}
}

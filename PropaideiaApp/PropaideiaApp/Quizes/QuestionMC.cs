using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropaideiaApp.Quizes
{
	class QuestionMC : Question
	{
		private int[] possibleAns = new int[3];
		public QuestionMC(PropaideiaType propaideia)
		{
			this.Propaideia = propaideia;

			GenerateQuestion();
		}

		public int[] PossibleAns { get => possibleAns; }

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

			tempAsk[2] = tempAsk[0] * tempAsk[1];

			int ans = Rand.Next(0, 3);
			possibleAns[ans] = tempAsk[2];
			this.CorrectAnswer = possibleAns[ans].ToString();
			chance = (Rand.Next(100) - 50) < 0;
			switch (ans)
			{
				case 0:
					if (chance)
					{
						possibleAns[1] = tempAsk[2] - this.Propaideia;
						possibleAns[2] = tempAsk[2] + this.Propaideia;
					}
					else
					{
						possibleAns[1] = tempAsk[2] + this.Propaideia;
						possibleAns[2] = tempAsk[2] - this.Propaideia;
					}
					break;
				case 1:
					if (chance)
					{
						possibleAns[0] = tempAsk[2] - this.Propaideia;
						possibleAns[2] = tempAsk[2] + this.Propaideia;
					}
					else
					{
						possibleAns[2] = tempAsk[2] + this.Propaideia;
						possibleAns[0] = tempAsk[2] - this.Propaideia;
					}
					break;
				case 2:
					if (chance)
					{
						possibleAns[0] = tempAsk[2] - this.Propaideia;
						possibleAns[1] = tempAsk[2] + this.Propaideia;
					}
					else
					{
						possibleAns[1] = tempAsk[2] + this.Propaideia;
						possibleAns[0] = tempAsk[2] - this.Propaideia;
					}
					break;
			}
		}
	}
}

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
		private int[] descriptionComponents = new int[3];

		public QuestionMC(PropaideiaType propaideia)
		{
			this.Propaideia = propaideia;

			GenerateQuestion();
		}

		public int[] PossibleAns { get => possibleAns; }

		private void GenerateQuestion()
		{
			if (Propaideia == PropaideiaType.FINAL_EXAM)
			{
				// For the final exam

				// Randomize and construct description
				descriptionComponents[0] = Randomizer.RollDX(10);
				descriptionComponents[1] = Randomizer.RollDX(10);

				descriptionComponents[2] = descriptionComponents[0] * descriptionComponents[1];

				Description = descriptionComponents[0] + " x " + descriptionComponents[1] + " = _";

				// Randomize the position of the correct answer among the possible answers
				int pos = Randomizer.RollZeroBasedD3();
				CorrectAnswer = pos.ToString();

				switch (pos)
				{
					case 0:
						possibleAns[0] = descriptionComponents[2];
						if (Randomizer.FlipCoin())
						{
							possibleAns[1] = descriptionComponents[2] + Randomizer.RollDX(10);
							possibleAns[2] = descriptionComponents[2] - Randomizer.RollDX(10);
						}
						else
						{
							possibleAns[1] = descriptionComponents[2] - Randomizer.RollDX(10);
							possibleAns[2] = descriptionComponents[2] + Randomizer.RollDX(10);
						}
						break;
					case 1:
						possibleAns[1] = descriptionComponents[2];
						if (Randomizer.FlipCoin())
						{
							possibleAns[0] = descriptionComponents[2] + Randomizer.RollDX(10);
							possibleAns[2] = descriptionComponents[2] - Randomizer.RollDX(10);
						}
						else
						{
							possibleAns[0] = descriptionComponents[2] - Randomizer.RollDX(10);
							possibleAns[2] = descriptionComponents[2] + Randomizer.RollDX(10);
						}
						break;
					case 2:
						possibleAns[2] = descriptionComponents[2];
						if (Randomizer.FlipCoin())
						{
							possibleAns[0] = descriptionComponents[2] + Randomizer.RollDX(10);
							possibleAns[1] = descriptionComponents[2] - Randomizer.RollDX(10);
						}
						else
						{
							possibleAns[0] = descriptionComponents[2] - Randomizer.RollDX(10);
							possibleAns[1] = descriptionComponents[2] + Randomizer.RollDX(10);
						}
						break;
				}
			}
			else
			{
				// For regular quizes

				// Randomize and construct description
				if (Randomizer.FlipCoin())
				{
					descriptionComponents[0] = (int)Propaideia;
					descriptionComponents[1] = Randomizer.RollDX(10);
				}
				else
				{
					descriptionComponents[0] = Randomizer.RollDX(10);
					descriptionComponents[1] = (int)Propaideia;
				}

				descriptionComponents[2] = descriptionComponents[0] * descriptionComponents[1];

				Description = descriptionComponents[0] + " x " + descriptionComponents[1] + " = _";

				// Randomize the position of the correct answer among the possible answers
				int pos = Randomizer.RollZeroBasedD3();
				CorrectAnswer = pos.ToString();

				switch (pos)
				{
					case 0:
						possibleAns[0] = descriptionComponents[2];
						if (Randomizer.FlipCoin())
						{
							possibleAns[1] = descriptionComponents[2] + (int)Propaideia;
							possibleAns[2] = descriptionComponents[2] - (int)Propaideia;
						}
						else
						{
							possibleAns[1] = descriptionComponents[2] - (int)Propaideia;
							possibleAns[2] = descriptionComponents[2] + (int)Propaideia;
						}
						break;
					case 1:
						possibleAns[1] = descriptionComponents[2];
						if (Randomizer.FlipCoin())
						{
							possibleAns[0] = descriptionComponents[2] + (int)Propaideia;
							possibleAns[2] = descriptionComponents[2] - (int)Propaideia;
						}
						else
						{
							possibleAns[0] = descriptionComponents[2] - (int)Propaideia;
							possibleAns[2] = descriptionComponents[2] + (int)Propaideia;
						}
						break;
					case 2:
						possibleAns[2] = descriptionComponents[2];
						if (Randomizer.FlipCoin())
						{
							possibleAns[0] = descriptionComponents[2] + (int)Propaideia;
							possibleAns[1] = descriptionComponents[2] - (int)Propaideia;
						}
						else
						{
							possibleAns[0] = descriptionComponents[2] - (int)Propaideia;
							possibleAns[1] = descriptionComponents[2] + (int)Propaideia;
						}
						break;
				}
			}
		}
	}
}

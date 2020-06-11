using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropaideiaApp.Quizes
{
	class QuestionTF : Question
	{
		private int[] descriptionComponents = new int[3];

		public QuestionTF(PropaideiaType propaideia)
		{
			this.Propaideia = propaideia;

			GenerateQuestion();
		}

		void GenerateQuestion()
		{
			if(Propaideia == (int)PropaideiaType.FINAL_EXAM)
			{
				// For the final exam

				// Randomize and construct description
				descriptionComponents[0] = Randomizer.RollDX(10);
				descriptionComponents[1] = Randomizer.RollDX(10);

				descriptionComponents[2] = descriptionComponents[0] * descriptionComponents[1];

				if (Randomizer.FlipCoin())
				{
					// Finish generation of question after setting answer
					CorrectAnswer = "true";
				}
				else
				{
					// Falsifying a component
					CorrectAnswer = "false";

					if (Randomizer.FlipCoin())
					{
						// Falsify the left part of the equation
						if (Randomizer.FlipCoin())
						{
							if (Randomizer.FlipCoin())
								descriptionComponents[1] += 1;
							else
								descriptionComponents[1] -= 1;
						}
						else
						{
							if (Randomizer.FlipCoin())
								descriptionComponents[0] += 1;
							else
								descriptionComponents[0] -= 1;
						}
					}
					else
					{
						// Falsify the right part of the equation
						if (Randomizer.FlipCoin())
							descriptionComponents[2] += Randomizer.RollDX(10);
						else
							descriptionComponents[2] -= Randomizer.RollDX(10);
					}
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

				if (Randomizer.FlipCoin())
				{
					// Finish generation of question after setting answer
					CorrectAnswer = "true";
				}
				else
				{
					// Falsifying a component
					CorrectAnswer = "false";

					if (Randomizer.FlipCoin())
					{
						// Falsify the left part of the equation
						if (descriptionComponents[0] == (int)Propaideia)
						{
							if (Randomizer.FlipCoin())
								descriptionComponents[1] += 1;
							else
								descriptionComponents[1] -= 1;
						}
						else
						{
							if (Randomizer.FlipCoin())
								descriptionComponents[0] += 1;
							else
								descriptionComponents[0] -= 1;
						}
					}
					else
					{
						// Falsify the right part of the equation
						if (Randomizer.FlipCoin())
							descriptionComponents[2] += (int)Propaideia;
						else
							descriptionComponents[2] -= (int)Propaideia;
					}
				}
			}
		}
	}
}

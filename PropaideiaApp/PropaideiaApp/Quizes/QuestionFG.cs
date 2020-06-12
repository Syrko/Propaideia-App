using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropaideiaApp.Quizes
{
	class QuestionFG : Question
	{
		private int[] descriptionComponents = new int[3];

		public QuestionFG(PropaideiaType propaideia)
		{
			this.Propaideia = propaideia;

			GenerateQuestion();
		}

		private void GenerateQuestion()
		{
			if (Propaideia == PropaideiaType.FINAL_EXAM)
			{
				// For the final exam

				// Randomize and construct description
				descriptionComponents[0] = Randomizer.RollDX(10);
				descriptionComponents[1] = Randomizer.RollDX(10);

				descriptionComponents[2] = descriptionComponents[0] * descriptionComponents[1];

				if (Randomizer.FlipCoin())
				{
					// The gap will be on the left side of the equation
					if (Randomizer.FlipCoin())
					{
						CorrectAnswer = descriptionComponents[0].ToString();
						Description = "_ x " + descriptionComponents[1] + " = " + descriptionComponents[2];
					}
					else
					{
						CorrectAnswer = descriptionComponents[1].ToString();
						Description = descriptionComponents[0] + " x _ = " + descriptionComponents[2];
					}
				}
				else
				{
					// The gap will be on the right side of the equation
					CorrectAnswer = descriptionComponents[2].ToString();
					Description = descriptionComponents[0] + " x " + descriptionComponents[1] + " = _";
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
					// The gap will be on the left side of the equation
					if (descriptionComponents[1] == (int)Propaideia)
					{
						CorrectAnswer = descriptionComponents[0].ToString();
						Description = "_ x " + descriptionComponents[1] + " = " + descriptionComponents[2];
					}
					else
					{
						CorrectAnswer = descriptionComponents[1].ToString();
						Description = descriptionComponents[0] + " x _ = " + descriptionComponents[2];
					}
				}
				else
				{
					// The gap will be on the right side of the equation
					CorrectAnswer = descriptionComponents[2].ToString();
					Description = descriptionComponents[0] + " x " + descriptionComponents[1] + " = _";
				}
			}
		}
	}
}

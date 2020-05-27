using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropaideiaApp.Quizes
{
	class QuizManager
	{
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
		FILL_GAPS,
		MULTIPLE_CHOICE,
		TRUE_FALSE
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropaideiaApp.Quizes
{
	abstract class Question
	{
		private bool studentAnswer;
		private string description;
		private string correctAnswer;
		private PropaideiaType propaideia;

		public bool StudentAnswer { get => studentAnswer; set => studentAnswer = value; }
		public string Description { get => description; set => description = value; }
		public string CorrectAnswer { get => correctAnswer; set => correctAnswer = value; }
		public PropaideiaType Propaideia { get => propaideia; set => propaideia = value; }
	}
}

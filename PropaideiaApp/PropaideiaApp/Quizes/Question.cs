using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropaideiaApp.Quizes
{
	abstract class Question
	{
		private bool result;
		private string ask;
		private string answer;
		private int propaideia;
		private Random rand = new Random();

		public bool Result { get => result; set => result = value; }
		public string Ask { get => ask; set => ask = value; }
		public string Answer { get => answer; set => answer = value; }
		public int Propaideia { get => propaideia; set => propaideia = value; }
		public Random Rand { get => rand; }
	}
}

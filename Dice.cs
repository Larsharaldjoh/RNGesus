using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace DiscordBot
{
	public class Dice
	{
		public static string SimpleDice(string input)
		{
			if (Regex.Match(input, "/roll/i").Success)
			{
				return "Dice will be rolled";
			}
			else
			{
				return null;
			}
		}
	}
}

using System.Globalization;
using System.Text.RegularExpressions;

namespace Maxim_Technology_Practice;

internal static class Program {
	public static void Main(string[] args) {
		var inputLine = Console.ReadLine();
		if (inputLine is null) return;

		var wrongSymbols = Regex.Matches(inputLine, "[^a-z]");
		if (wrongSymbols.Count > 0) {
			var wrongSymbolsString = string.Join(", ", wrongSymbols.Select(m => m.Value));
			Console.WriteLine("Вы неправильно ввели следующие символы: " + wrongSymbolsString);
		}
		else {
			var changedLine = ChangeLine(inputLine);
			Console.WriteLine($"Обработанная строка: {changedLine}");
			Console.WriteLine($"Символы в обработанной строке:\n{CountCharactersIndividually(changedLine)}");
		}
	}

	private static string ChangeLine(string line) {
		string resultLine;

		if (line.Length % 2 == 0) {
			var middleIndex = line.Length / 2;

			var reversedHalfOfLine1 = ReverseString(line[..middleIndex]);
			var reversedHalfOfLine2 = ReverseString(line[middleIndex..]);

			resultLine = reversedHalfOfLine1 + reversedHalfOfLine2;
		}
		else {
			var reversedLine = ReverseString(line);


			resultLine = reversedLine + line;
		}

		return resultLine;
	}

	private static string ReverseString(string s) => new(s.Reverse().ToArray());

	private static string CountCharactersIndividually(string line) {
		var orderedCharactersGroups = line
			.GroupBy(ch => ch)
			.Select(g => new { Char = g.Key, Сount = g.Count() })
			.OrderBy(g => g.Char);
		
		return string.Join("\n", orderedCharactersGroups);
	}
}
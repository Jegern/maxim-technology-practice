using System.Text.RegularExpressions;

namespace Maxim_Technology_Practice;

internal static class Program {
	public static void Main(string[] args) {
		Console.Write("Введите строку: ");
		var inputLine = Console.ReadLine();
		if (inputLine is null || inputLine.Length == 0) {
			Console.WriteLine("Вы ввели пустую строку");
			return;
		}

		var wrongSymbols = Regex.Matches(inputLine, "[^a-z]");
		if (wrongSymbols.Count > 0) {
			var wrongSymbolsString = string.Join(", ", wrongSymbols.Select(m => m.Value));
			Console.WriteLine("Вы неправильно ввели следующие символы: " + wrongSymbolsString);
		}
		else {
			var changedLine = ChangeLine(inputLine);
			Console.WriteLine($"Обработанная строка: {changedLine}");

			var characterCounts = GetStringWithCharacterCounts(changedLine);
			Console.WriteLine($"Количество каждого из символов в обработанной строке:\n" +
			                  $"{characterCounts}");

			var substring = GetSubstringWithSideVowels(changedLine);
			Console.WriteLine(substring.Length > 0
				? $"Подстрока, начинающаяся и заканчивающаяся на гласную: {substring}"
				: $"Подстрока, начинающаяся и заканчивающаяся на гласную, не найдена");
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

	private static string ReverseString(string s) =>
		new(s.Reverse().ToArray());

	private static string GetStringWithCharacterCounts(string line) {
		var orderedCharactersGroups = line
			.GroupBy(ch => ch)
			.Select(g => new { Char = g.Key, Сount = g.Count() })
			.OrderBy(g => g.Char);

		return string.Join("\n", orderedCharactersGroups);
	}

	private static string GetSubstringWithSideVowels(string line) =>
		Regex.Match(line, "[aeiouy]\\w*[aeiouy]").Value;
}
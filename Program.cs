using System.Text.RegularExpressions;

namespace Maxim_Technology_Practice;

internal static class Program {
	public static async Task Main(string[] args) {
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
				: "Подстрока, начинающаяся и заканчивающаяся на гласную, не найдена");
		
			switch (GetChoiceOfSort()) {
				case 1:
					Console.WriteLine($"Отсортированная (быстрым) строка: {Sort.QuickSort(changedLine)}");
					break;
				case 2:
					Console.WriteLine($"Отсортированная (деревом) строка: {Sort.TreeSort(changedLine)}");
					break;
				default:
					Console.WriteLine("Строка не была отсортирована");
					break;
			}

			var randomIndex = await GetRandomNumber(changedLine.Length - 1);
			var truncatedChangedLine = changedLine.Remove(randomIndex, 1);
			Console.WriteLine($"Обработанная строка без одного символа [{randomIndex}]: {truncatedChangedLine}");
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

	private static int GetChoiceOfSort() {
		Console.Write("Доступны следующие алгоритмы сортировок" +
		              "\n1. Быстрая сортировка" +
		              "\n2. Сортировка деревом" +
		              "\nВыберите один: ");
		var choice = Console.ReadLine();
		if (choice is null || choice.Length == 0) {
			Console.WriteLine("Вы ввели пустую строку");
			return -1;
		}

		if (choice is not ("1" or "2")) {
			Console.WriteLine("Неправильный ввод");
			return -1;
		}

		return int.Parse(choice);
	}

	private static string GetSubstringWithSideVowels(string line) =>
		Regex.Match(line, "[aeiouy]\\w*[aeiouy]").Value;

	private static async Task<int> GetRandomNumber(int max) {
		var baseUrl = $"http://www.randomnumberapi.com/api/v1.0/random?min={0}&max={max}&count=1";
		try {
			using var client = new HttpClient();
			using var responce = await client.GetAsync(baseUrl);
			using var content = responce.Content;
			var data = await content.ReadAsStringAsync();
			return int.Parse(data[1..^1]);
		}
		catch (HttpRequestException) {
			Console.WriteLine("Ошибка: невозможно обратиться к ресурсу");
		}
		catch (TaskCanceledException) {
			Console.WriteLine("Ошибка: превышено время ожидание ответа");
		}
		
		var random = new Random();
		return random.Next(max);
	}
}
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using Web_Maxim_Technology_Practice.Models;

namespace Web_Maxim_Technology_Practice.Controllers;

[ApiController]
[Route("[controller]")]
public class StringChangeController : ControllerBase {
	private static readonly StringChangeModel StringChangeModel = new();

	[HttpGet("{inputLine}")]
	public async Task<IActionResult> Input(string inputLine, [FromQuery] string sortNumber) {
		var wrongSymbols = Regex.Matches(inputLine, "[^a-z]");
		if (wrongSymbols.Count > 0) {
			var wrongSymbolsString = string.Join(", ", wrongSymbols.Select(m => m.Value));
			return BadRequest($"Вы неправильно ввели следующие символы: {wrongSymbolsString}");
		}

		StringChangeModel.ChangedLine = ChangeLine(inputLine);
		StringChangeModel.CharacterCounts = GetStringWithCharacterCounts(StringChangeModel.ChangedLine);
		StringChangeModel.VowelSubstring = GetSubstringWithSideVowels(StringChangeModel.ChangedLine);
		StringChangeModel.SortedChangedLine = sortNumber switch {
			"1" => Sort.QuickSort(StringChangeModel.ChangedLine),
			"2" => Sort.TreeSort(StringChangeModel.ChangedLine),
			_ => null
		};
		var randomIndex = await GetRandomNumber(StringChangeModel.ChangedLine.Length - 1);
		var truncatedChangedLine = StringChangeModel.ChangedLine.Remove(randomIndex, 1);
		StringChangeModel.TruncatedChangedLine = truncatedChangedLine;

		return Ok(StringChangeModel);
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

	private static IDictionary<char, int> GetStringWithCharacterCounts(string line) {
		var counts = new Dictionary<char, int>();

		foreach (var symbol in line) {
			if (counts.ContainsKey(symbol)) {
				counts[symbol]++;
			}
			else {
				counts.Add(symbol, 1);
			}
		}

		return counts;
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
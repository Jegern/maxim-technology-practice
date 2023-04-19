namespace Maxim_Technology_Practice;

internal static class Program {
	public static void Main(string[] args) {
		var inputLine = Console.ReadLine();

		if (inputLine is null) return;

		if (inputLine.Length % 2 == 0) {
			var middleIndex = inputLine.Length / 2;

			var reversedHalfOfLine1 = ReverseString(inputLine[..middleIndex]);
			var reversedHalfOfLine2 = ReverseString(inputLine[middleIndex..]);

			Console.WriteLine(reversedHalfOfLine1 + reversedHalfOfLine2);
		}
		else {
			var reversedLine = ReverseString(inputLine);

			Console.WriteLine(reversedLine + inputLine);
		}
	}

	private static string ReverseString(string s) => new(s.Reverse().ToArray());
}
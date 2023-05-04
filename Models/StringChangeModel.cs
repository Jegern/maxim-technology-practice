namespace Web_Maxim_Technology_Practice.Models; 

public class StringChangeModel {
	public string? ChangedLine { get; set; }
	public IDictionary<char, int>? CharacterCounts { get; set; }
	public string? VowelSubstring { get; set; }
	public string? SortedChangedLine { get; set; }
	public string? TruncatedChangedLine { get; set; }
}
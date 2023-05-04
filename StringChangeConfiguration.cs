namespace Web_Maxim_Technology_Practice; 

public class StringChangeConfiguration {
	public string? RandomApi { get; init; }
	public StringChangeSettings? Settings { get; init; }
}

public class StringChangeSettings {
	public List<string>? BlackList { get; init; }
}
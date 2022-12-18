using System;

public class ScoreEntry
{
    public string PlayerGuid { get; set; }
    public string PlayerName { get; set; }
    public long Span { get; set; }

    public override string ToString()
    {
        return "{\"PlayerGuid\":\"" + PlayerGuid + "\",\"PlayerName\":\"" + PlayerName + "\",\"Span\":" + Span + "}";
    }
}

public class ScoreRecord
{
    public int ID { get; set; }
    public string PlayerGuid { get; set; }
    public string PlayerName { get; set; }
    public TimeSpan Span { get; set; }
}
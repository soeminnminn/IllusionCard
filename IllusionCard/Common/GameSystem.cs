using System;

public class GameSystem
{
    public Language language { get; private set; } = Language.Japanese;

    public string UserUUID { get; private set; } = string.Empty;

    public static GameSystem Instance
    {
        get => new GameSystem();
    }

    public enum Language
    {
        Japanese,
        English,
        SimplifiedChinese,
        TraditionalChinese,
    }
}

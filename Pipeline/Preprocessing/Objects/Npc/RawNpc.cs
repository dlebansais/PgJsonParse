﻿namespace Preprocessor;

public class RawNpc
{
    public string? AreaName { get; set; }
    public string? Desc { get; set; }
    public object? Services { get; set; }
    public string? Pos { get; set; }
    public string? AreaFriendlyName { get; set; }
    public string[]? ItemGifts { get; set; }
    public RawNpcPreference[]? Preferences { get; set; }
    public string? Name { get; set; }
}

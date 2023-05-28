namespace Translator;

using System.Collections.Generic;

public abstract class Tag
{
    public Tag()
    {
    }

    public Tag(string parameters, string content, List<Tag> nestedTagList)
    {
        Parameters = parameters;
        Content = content;
        NestedTagList = nestedTagList;
    }

    public string Parameters { get; } = string.Empty;
    public string Content { get; } = string.Empty;
    public List<Tag> NestedTagList { get; } = new List<Tag>();

    public abstract string OpeningTag { get; }
    public abstract string ClosingTag { get; }
}

namespace Translator;

using System.Collections.Generic;

public class TagItalic : Tag
{
    public TagItalic()
    {
    }

    public TagItalic(string parameters, string content, List<Tag> nestedTagList)
        : base(parameters, content, nestedTagList)
    {
    }

    public override string OpeningTag { get { return HtmlParser.OpeningTagTable[typeof(TagItalic)]; } }
    public override string ClosingTag { get { return HtmlParser.ClosingTagTable[typeof(TagItalic)]; } }
}

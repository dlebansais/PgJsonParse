namespace Translator;

using System.Collections.Generic;

public class TagSpan : Tag
{
    public TagSpan()
    {
    }

    public TagSpan(string parameters, string content, List<Tag> nestedTagList)
        : base(parameters, content, nestedTagList)
    {
    }

    public override string OpeningTag { get { return HtmlParser.OpeningTagTable[typeof(TagSpan)]; } }
    public override string ClosingTag { get { return HtmlParser.ClosingTagTable[typeof(TagSpan)]; } }
}

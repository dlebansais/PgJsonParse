namespace Translator;

using System.Collections.Generic;

public class TagHyperlink : Tag
{
    public TagHyperlink()
    {
    }

    public TagHyperlink(string parameters, string content, List<Tag> nestedTagList)
        : base(parameters, content, nestedTagList)
    {
    }

    public override string OpeningTag { get { return HtmlParser.OpeningTagTable[typeof(TagHyperlink)]; } }
    public override string ClosingTag { get { return HtmlParser.ClosingTagTable[typeof(TagHyperlink)]; } }
}

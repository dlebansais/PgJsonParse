namespace Translator;

using System.Collections.Generic;

public class TagSection : Tag
{
    public TagSection()
    {
    }

    public TagSection(string parameters, string content, List<Tag> nestedTagList)
        : base(parameters, content, nestedTagList)
    {
    }

    public override string OpeningTag { get { return HtmlParser.OpeningTagTable[typeof(TagSection)]; } }
    public override string ClosingTag { get { return HtmlParser.ClosingTagTable[typeof(TagSection)]; } }
}

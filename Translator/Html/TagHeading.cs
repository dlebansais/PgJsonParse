namespace Translator;

using System.Collections.Generic;

public class TagHeading : Tag
{
    public TagHeading()
    {
    }

    public TagHeading(string parameters, string content, List<Tag> nestedTagList)
        : base(parameters, content, nestedTagList)
    {
    }

    public override string OpeningTag { get { return HtmlParser.OpeningTagTable[typeof(TagHeading)]; } }
    public override string ClosingTag { get { return HtmlParser.ClosingTagTable[typeof(TagHeading)]; } }
}

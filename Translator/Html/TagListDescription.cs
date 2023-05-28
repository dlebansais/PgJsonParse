namespace Translator;

using System.Collections.Generic;

public class TagListDescription : Tag
{
    public TagListDescription()
    {
    }

    public TagListDescription(string parameters, string content, List<Tag> nestedTagList)
        : base(parameters, content, nestedTagList)
    {
    }

    public override string OpeningTag { get { return HtmlParser.OpeningTagTable[typeof(TagListDescription)]; } }
    public override string ClosingTag { get { return HtmlParser.ClosingTagTable[typeof(TagListDescription)]; } }
}

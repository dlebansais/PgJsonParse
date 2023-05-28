namespace Translator;

using System.Collections.Generic;

public class TagListUnordered : Tag
{
    public TagListUnordered()
    {
    }

    public TagListUnordered(string parameters, string content, List<Tag> nestedTagList)
        : base(parameters, content, nestedTagList)
    {
    }

    public override string OpeningTag { get { return HtmlParser.OpeningTagTable[typeof(TagListUnordered)]; } }
    public override string ClosingTag { get { return HtmlParser.ClosingTagTable[typeof(TagListUnordered)]; } }
}

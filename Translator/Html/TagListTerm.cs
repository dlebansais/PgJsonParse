namespace Translator;

using System.Collections.Generic;

public class TagListTerm : Tag
{
    public TagListTerm()
    {
    }

    public TagListTerm(string parameters, string content, List<Tag> nestedTagList)
        : base(parameters, content, nestedTagList)
    {
    }

    public override string OpeningTag { get { return HtmlParser.OpeningTagTable[typeof(TagListTerm)]; } }
    public override string ClosingTag { get { return HtmlParser.ClosingTagTable[typeof(TagListTerm)]; } }
}

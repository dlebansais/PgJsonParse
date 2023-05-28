namespace Translator;

using System.Collections.Generic;

public class TagImage : Tag
{
    public TagImage()
    {
    }

    public TagImage(string parameters, string content, List<Tag> nestedTagList)
        : base(parameters, content, nestedTagList)
    {
    }

    public override string OpeningTag { get { return HtmlParser.OpeningTagTable[typeof(TagImage)]; } }
    public override string ClosingTag { get { return HtmlParser.ClosingTagTable[typeof(TagImage)]; } }
}

namespace Translator
{
    using System.Collections.Generic;

    public class TagParagraph : Tag
    {
        public TagParagraph()
        {
        }

        public TagParagraph(string parameters, string content, List<Tag> nestedTagList)
            : base(parameters, content, nestedTagList)
        {
        }

        public override string OpeningTag { get { return HtmlParser.OpeningTagTable[typeof(TagParagraph)]; } }
        public override string ClosingTag { get { return HtmlParser.ClosingTagTable[typeof(TagParagraph)]; } }
    }
}

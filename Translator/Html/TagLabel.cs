namespace Translator
{
    using System.Collections.Generic;

    public class TagLabel : Tag
    {
        public TagLabel()
        {
        }

        public TagLabel(string parameters, string content, List<Tag> nestedTagList)
            : base(parameters, content, nestedTagList)
        {
        }

        public override string OpeningTag { get { return HtmlParser.OpeningTagTable[typeof(TagLabel)]; } }
        public override string ClosingTag { get { return HtmlParser.ClosingTagTable[typeof(TagLabel)]; } }
    }
}

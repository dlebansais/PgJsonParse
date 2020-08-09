namespace Translator
{
    using System.Collections.Generic;

    public class TagBold : Tag
    {
        public TagBold()
        {
        }

        public TagBold(string parameters, string content, List<Tag> nestedTagList)
            : base(parameters, content, nestedTagList)
        {
        }

        public override string OpeningTag { get { return HtmlParser.OpeningTagTable[typeof(TagBold)]; } }
        public override string ClosingTag { get { return HtmlParser.ClosingTagTable[typeof(TagBold)]; } }
    }
}

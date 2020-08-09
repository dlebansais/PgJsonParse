namespace Translator
{
    using System.Collections.Generic;

    public class TagBlockQuote : Tag
    {
        public TagBlockQuote()
        {
        }

        public TagBlockQuote(string parameters, string content, List<Tag> nestedTagList)
            : base(parameters, content, nestedTagList)
        {
        }

        public override string OpeningTag { get { return HtmlParser.OpeningTagTable[typeof(TagBlockQuote)]; } }
        public override string ClosingTag { get { return HtmlParser.ClosingTagTable[typeof(TagBlockQuote)]; } }
    }
}

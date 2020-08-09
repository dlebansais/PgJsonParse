namespace Translator
{
    using System.Collections.Generic;

    public class TagSubscript : Tag
    {
        public TagSubscript()
        {
        }

        public TagSubscript(string parameters, string content, List<Tag> nestedTagList)
            : base(parameters, content, nestedTagList)
        {
        }

        public override string OpeningTag { get { return HtmlParser.OpeningTagTable[typeof(TagSubscript)]; } }
        public override string ClosingTag { get { return HtmlParser.ClosingTagTable[typeof(TagSubscript)]; } }
    }
}

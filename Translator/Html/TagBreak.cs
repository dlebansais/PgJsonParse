namespace Translator
{
    using System.Collections.Generic;

    public class TagBreak : Tag
    {
        public TagBreak()
        {
        }

        public TagBreak(string parameters, string content, List<Tag> nestedTagList)
            : base(parameters, content, nestedTagList)
        {
        }

        public override string OpeningTag { get { return HtmlParser.OpeningTagTable[typeof(TagBreak)]; } }
        public override string ClosingTag { get { return HtmlParser.ClosingTagTable[typeof(TagBreak)]; } }
    }
}

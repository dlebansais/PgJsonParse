namespace Translator
{
    using System.Collections.Generic;

    public class TagListValue : Tag
    {
        public TagListValue()
        {
        }

        public TagListValue(string parameters, string content, List<Tag> nestedTagList)
            : base(parameters, content, nestedTagList)
        {
        }

        public override string OpeningTag { get { return HtmlParser.OpeningTagTable[typeof(TagListValue)]; } }
        public override string ClosingTag { get { return HtmlParser.ClosingTagTable[typeof(TagListValue)]; } }
    }
}

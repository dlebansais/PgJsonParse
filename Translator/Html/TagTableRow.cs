namespace Translator
{
    using System.Collections.Generic;

    public class TagTableRow : Tag
    {
        public TagTableRow()
        {
        }

        public TagTableRow(string parameters, string content, List<Tag> nestedTagList)
            : base(parameters, content, nestedTagList)
        {
        }

        public override string OpeningTag { get { return HtmlParser.OpeningTagTable[typeof(TagTableRow)]; } }
        public override string ClosingTag { get { return HtmlParser.ClosingTagTable[typeof(TagTableRow)]; } }
    }
}

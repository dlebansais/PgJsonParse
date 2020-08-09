namespace Translator
{
    using System.Collections.Generic;

    public class TagTableCell : Tag
    {
        public TagTableCell()
        {
        }

        public TagTableCell(string parameters, string content, List<Tag> nestedTagList)
            : base(parameters, content, nestedTagList)
        {
        }

        public override string OpeningTag { get { return HtmlParser.OpeningTagTable[typeof(TagTableCell)]; } }
        public override string ClosingTag { get { return HtmlParser.ClosingTagTable[typeof(TagTableCell)]; } }
    }
}

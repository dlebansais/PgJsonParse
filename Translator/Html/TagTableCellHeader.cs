namespace Translator
{
    using System.Collections.Generic;

    public class TagTableCellHeader : Tag
    {
        public TagTableCellHeader()
        {
        }

        public TagTableCellHeader(string parameters, string content, List<Tag> nestedTagList)
            : base(parameters, content, nestedTagList)
        {
        }

        public override string OpeningTag { get { return HtmlParser.OpeningTagTable[typeof(TagTableCellHeader)]; } }
        public override string ClosingTag { get { return HtmlParser.ClosingTagTable[typeof(TagTableCellHeader)]; } }
    }
}

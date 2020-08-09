namespace Translator
{
    using System.Collections.Generic;

    public class TagTable : Tag
    {
        public TagTable()
        {
        }

        public TagTable(string parameters, string content, List<Tag> nestedTagList)
            : base(parameters, content, nestedTagList)
        {
        }

        public override string OpeningTag { get { return HtmlParser.OpeningTagTable[typeof(TagTable)]; } }
        public override string ClosingTag { get { return HtmlParser.ClosingTagTable[typeof(TagTable)]; } }
    }
}

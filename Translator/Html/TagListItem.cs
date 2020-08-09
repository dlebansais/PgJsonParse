namespace Translator
{
    using System.Collections.Generic;

    public class TagListItem : Tag
    {
        public TagListItem()
        {
        }

        public TagListItem(string parameters, string content, List<Tag> nestedTagList)
            : base(parameters, content, nestedTagList)
        {
        }

        public override string OpeningTag { get { return HtmlParser.OpeningTagTable[typeof(TagListItem)]; } }
        public override string ClosingTag { get { return HtmlParser.ClosingTagTable[typeof(TagListItem)]; } }
    }
}

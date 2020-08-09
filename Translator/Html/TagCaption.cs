namespace Translator
{
    using System.Collections.Generic;

    public class TagCaption : Tag
    {
        public TagCaption()
        {
        }

        public TagCaption(string parameters, string content, List<Tag> nestedTagList)
            : base(parameters, content, nestedTagList)
        {
        }

        public override string OpeningTag { get { return HtmlParser.OpeningTagTable[typeof(TagCaption)]; } }
        public override string ClosingTag { get { return HtmlParser.ClosingTagTable[typeof(TagCaption)]; } }
    }
}

namespace Translator
{
    using System.Collections.Generic;

    public class TagSuperscript : Tag
    {
        public TagSuperscript()
        {
        }

        public TagSuperscript(string parameters, string content, List<Tag> nestedTagList)
            : base(parameters, content, nestedTagList)
        {
        }

        public override string OpeningTag { get { return HtmlParser.OpeningTagTable[typeof(TagSuperscript)]; } }
        public override string ClosingTag { get { return HtmlParser.ClosingTagTable[typeof(TagSuperscript)]; } }
    }
}

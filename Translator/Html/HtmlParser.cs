namespace Translator
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    public static class HtmlParser
    {
        public static Dictionary<Type, string> OpeningTagTable = new Dictionary<Type, string>()
        {
            { typeof(TagHyperlink), "<a" },
            { typeof(TagBlockQuote), "<blockquote" },
            { typeof(TagBreak), "<br" },
            { typeof(TagBold), "<b" },
            { typeof(TagCaption), "<caption" },
            { typeof(TagListValue), "<dd" },
            { typeof(TagSection), "<div" },
            { typeof(TagListDescription), "<dl" },
            { typeof(TagListTerm), "<dt" },
            { typeof(TagHeading), "<h#" },
            { typeof(TagImage), "<img" },
            { typeof(TagItalic), "<i" },
            { typeof(TagLabel), "<label" },
            { typeof(TagListItem), "<li" },
            { typeof(TagParagraph), "<p" },
            { typeof(TagSpan), "<span" },
            { typeof(TagSubscript), "<sub" },
            { typeof(TagSuperscript), "<sup" },
            { typeof(TagTable), "<table" },
            { typeof(TagTableCell), "<td" },
            { typeof(TagTableCellHeader), "<th" },
            { typeof(TagTableRow), "<tr" },
            { typeof(TagListUnordered), "<ul" },
        };

        public static Dictionary<Type, string> ClosingTagTable = new Dictionary<Type, string>()
        {
            { typeof(TagHyperlink), "</a>" },
            { typeof(TagBlockQuote), "</blockquote>" },
            { typeof(TagBreak), "</br>" },
            { typeof(TagBold), "</b>" },
            { typeof(TagCaption), "</caption>" },
            { typeof(TagListValue), "</dd>" },
            { typeof(TagSection), "</div>" },
            { typeof(TagListDescription), "</dl>" },
            { typeof(TagListTerm), "</dt>" },
            { typeof(TagHeading), "</h#>" },
            { typeof(TagImage), "</img>" },
            { typeof(TagItalic), "</i>" },
            { typeof(TagLabel), "</label>" },
            { typeof(TagListItem), "</li>" },
            { typeof(TagParagraph), "</p>" },
            { typeof(TagSpan), "</span>" },
            { typeof(TagSubscript), "</sub>" },
            { typeof(TagSuperscript), "</sup>" },
            { typeof(TagTable), "</table>" },
            { typeof(TagTableCell), "</td>" },
            { typeof(TagTableCellHeader), "</th>" },
            { typeof(TagTableRow), "</tr>" },
            { typeof(TagListUnordered), "</ul>" },
        };

        public static string GetDebugString(string text, int index)
        {
            if (index + 100 < text.Length)
                return text.Substring(index, 100);
            else
                return text.Substring(index);
        }

        public static bool Parse(string htmltext, int startIndex, out Tag tag, out int endIndex)
        {
            tag = null;
            endIndex = -1;
            string TagDebug;

            int Index = startIndex;
            while (Index < htmltext.Length && htmltext[Index] != '<')
                Index++;

            int OpeningTagIndex = Index;
            TagDebug = GetDebugString(htmltext, Index);

            Type SelectedType = null;
            int SelectedLevel = 0;
            string OpeningTag = null;

            foreach (KeyValuePair<Type, string> Entry in OpeningTagTable)
            {
                OpeningTag = Entry.Value;

                if (OpeningTag.Contains("#"))
                {
                    for (int i = 1; i < 6; i++)
                    {
                        string OpeningTagWithLevel = OpeningTag.Replace("#", i.ToString());

                        if (Index + OpeningTagWithLevel.Length < htmltext.Length && htmltext.Substring(Index, OpeningTagWithLevel.Length) == OpeningTagWithLevel)
                        {
                            SelectedType = Entry.Key;
                            OpeningTagIndex += OpeningTagWithLevel.Length;
                            OpeningTag = OpeningTagWithLevel;
                            SelectedLevel = i;
                            break;
                        }
                    }
                }
                else
                {
                    if (Index + OpeningTag.Length < htmltext.Length && htmltext.Substring(Index, OpeningTag.Length) == OpeningTag)
                    {
                        SelectedType = Entry.Key;
                        OpeningTagIndex += OpeningTag.Length;
                    }
                }

                if (SelectedType != null)
                    break;
            }

            if (SelectedType == null)
            {
                Debug.WriteLine($"Tag not found: {TagDebug}");
                return false;
            }

            while (Index < htmltext.Length && htmltext[Index] != '>')
                Index++;

            if (Index >= htmltext.Length)
                return false;

            string TagParameters;
            string TagContent;
            List<Tag> NestedTageList = new List<Tag>();

            if (htmltext[Index - 1] == '/')
            {
                TagParameters = htmltext.Substring(OpeningTagIndex, Index - 1 - OpeningTagIndex);
                TagContent = string.Empty;
                endIndex = Index + 1;
            }
            else
            {
                TagParameters = htmltext.Substring(OpeningTagIndex, Index - OpeningTagIndex);
                Index++;

                string ClosingTag = ClosingTagTable[SelectedType];
                if (SelectedLevel > 0)
                    ClosingTag = ClosingTag.Replace("#", SelectedLevel.ToString());

                string NonTagContent = string.Empty;

                for (; ; )
                {
                    while (Index < htmltext.Length && htmltext[Index] != '<')
                    {
                        NonTagContent += htmltext[Index];
                        Index++;
                    }

                    if (Index >= htmltext.Length)
                        return false;

                    TagDebug = GetDebugString(htmltext, Index);

                    if (Index + ClosingTag.Length < htmltext.Length && htmltext.Substring(Index, ClosingTag.Length) == ClosingTag)
                    {
                        endIndex = Index + ClosingTag.Length;
                        break;
                    }
                    else if (SelectedType != typeof(TagSection) && SelectedType != typeof(TagTable) && Index + OpeningTag.Length < htmltext.Length && htmltext.Substring(Index, OpeningTag.Length) == OpeningTag)
                    {
                        endIndex = Index;
                        break;
                    }
                    else if (Index + 2 < htmltext.Length && htmltext.Substring(Index, 2) == "</")
                    {
                        endIndex = Index;
                        break;
                    }

                    if (!Parse(htmltext, Index, out Tag NestedTag, out int NestedEndIndex))
                        return false;

                    NestedTageList.Add(NestedTag);
                    Index = NestedEndIndex;

                    if (NonTagContent.Trim().Length > 0)
                        NonTagContent += "|";
                }

                TagContent = NonTagContent;
            }

            tag = (Tag)Activator.CreateInstance(SelectedType, TagParameters, TagContent, NestedTageList);

            return true;
        }

        public static bool FindNextTag<T>(string htmltext, ref int startIndex, out T tag)
            where T : Tag, new()
        {
            tag = null;

            int TagIndex;

            string OpeningTag = OpeningTagTable[typeof(T)];

            if (OpeningTag.Contains("#"))
            {
                TagIndex = htmltext.Length;

                for (int i = 1; i < 6; i++)
                {
                    string OpeningTagWithLevel = OpeningTag.Replace("#", i.ToString());
                    int TagIndexWithLevel = htmltext.IndexOf(OpeningTagWithLevel, startIndex);

                    if (TagIndexWithLevel >= 0 && TagIndex > TagIndexWithLevel)
                        TagIndex = TagIndexWithLevel;
                }

                if (TagIndex == htmltext.Length)
                    TagIndex = -1;
            }
            else
                TagIndex = htmltext.IndexOf(OpeningTag, startIndex);

            if (TagIndex >= startIndex)
            {
                if (Parse(htmltext, TagIndex, out Tag NewTag, out int EndIndex))
                {
                    startIndex = EndIndex;
                    tag = NewTag as T;
                    return true;
                }
            }

            return false;
        }
    }
}

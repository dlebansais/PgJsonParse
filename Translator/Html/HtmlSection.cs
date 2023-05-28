namespace Translator;

using System.Collections.Generic;
using System.Diagnostics;

public class HtmlSection
{
    public HtmlSection(string content, List<Tag> nestedTagList)
    {
        Content = content;
        NestedTagList = nestedTagList;
    }

    public string Content { get; }
    public List<Tag> NestedTagList { get; }

    public static HtmlSection? FromPage(string pageContent, string headerText1, string headerText2)
    {
        int Index = 0;
        TagTable Table;
        List<Tag> NestedTagList = null!;

        while (HtmlParser.FindNextTag(pageContent, ref Index, out Table) && Table != null)
        {
            NestedTagList = Table.NestedTagList;
            if (NestedTagList.Count == 0)
                continue;

            TagTableRow? FirstRow = NestedTagList[0] as TagTableRow;
            if (FirstRow == null)
                continue;

            List<Tag> RowNestedTagList = FirstRow.NestedTagList;
            if (RowNestedTagList.Count < 2)
                continue;

            TagTableCellHeader? Header1 = RowNestedTagList[0] as TagTableCellHeader;
            TagTableCellHeader? Header2 = RowNestedTagList[1] as TagTableCellHeader;
            if (Header1 == null || Header2 == null)
                continue;

            string Content1 = Header1.Content.Trim();
            string Content2 = Header2.Content.Trim();

            if (Content1 == headerText1 && Content2 == headerText2)
                break;
        }

        return Table != null ? new HtmlSection(Table.Content, NestedTagList) : null;
    }
}

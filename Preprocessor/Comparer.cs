namespace Preprocessor;

using System.Diagnostics;

internal class Comparer
{
    public void Compare(string readContent, string writeContent)
    {
        int ReadOffset = 0;
        int WriteOffset = 0;
        string PreviousReadLine = string.Empty;
        string PreviousWriteLine = string.Empty;

        while (GetNextLine(readContent, ref ReadOffset, out string ReadLine) && GetNextLine(writeContent, ref WriteOffset, out string WriteLine))
        {
            if (ReadLine != WriteLine)
            {
              bool HasNextReadLine = true;
                bool HasNextWriteLine = true;
                int NextReadOffset = ReadOffset;
                int NextWriteOffset = WriteOffset;

                while (HasNextReadLine || HasNextWriteLine)
                {
                    int EndReadOffset = NextReadOffset;
                    int EndWriteOffset = NextWriteOffset;
                    HasNextReadLine = GetNextLine(readContent, ref NextReadOffset, out string NextReadLine);
                    HasNextWriteLine = GetNextLine(writeContent, ref NextWriteOffset, out string NextWriteLine);

                    if (HasNextReadLine && NextReadLine == WriteLine)
                    {
                        AddDifference(readContent, ReadOffset, EndReadOffset, isRead: true, PreviousReadLine, ReadLine);
                        ReadOffset = NextReadOffset;
                        return;
                    }
                    else if (HasNextWriteLine && NextWriteLine == ReadLine)
                    {
                        AddDifference(writeContent, WriteOffset, EndWriteOffset, isRead: false, PreviousWriteLine, WriteLine);
                        WriteOffset = NextWriteOffset;
                        return;
                    }
                }
            }

            PreviousReadLine = ReadLine;
            PreviousWriteLine = WriteLine;
        }
    }

    private bool GetNextLine(string content, ref int offset, out string line)
    {
        int StartOffset = offset;
        while (offset < content.Length && content[offset] != '\r' && content[offset] != '\n')
            offset++;

        if (offset < content.Length)
        {
            int EndOffset = offset;
            while (offset < content.Length && (content[offset] == '\r' || content[offset] == '\n'))
                offset++;

            line = content.Substring(StartOffset, EndOffset - StartOffset);
            return true;
        }
        else if (offset > StartOffset)
        {
            line = content.Substring(StartOffset, content.Length - StartOffset);
            return true;
        }
        else
        {
            line = string.Empty;
            return false;
        }
    }

    private void AddDifference(string content, int offset, int endOffset, bool isRead, string previousLine, string startLine)
    {
        string Difference = $"  {previousLine}\r\n";
        Difference += $"{(isRead ? "+" : "-")} {startLine}\r\n";

        string Line;
        int DiffOffset = offset;

        while (DiffOffset < endOffset)
        {
            GetNextLine(content, ref DiffOffset, out Line);
            Difference += $"{(isRead ? "+" : "-")} {Line}\r\n";

            if (Difference.Length >= 1000)
                break;
        }

        GetNextLine(content, ref DiffOffset, out Line);
        Difference += $"  {Line}\r\n";

        Debug.WriteLine(Difference);
    }
}

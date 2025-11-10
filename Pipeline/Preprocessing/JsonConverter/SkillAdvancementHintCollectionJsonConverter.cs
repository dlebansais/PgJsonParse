namespace Preprocessor;

using System;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

public class SkillAdvancementHintCollectionJsonConverter : JsonConverter<SkillAdvancementHintCollection>
{
    public override SkillAdvancementHintCollection? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        SkillAdvancementHintCollection Result = new();

        ReadTableDictionary(Result, ref reader, options);

        return Result;
    }

    private void ReadTableDictionary(SkillAdvancementHintCollection collection, ref Utf8JsonReader reader, JsonSerializerOptions options)
    {
        while (reader.Read() && reader.TokenType == JsonTokenType.PropertyName)
        {
            string Key = reader.GetString() ?? throw new NullReferenceException();
            reader.Read();
            string Hint = reader.GetString() ?? throw new NullReferenceException();

            if (int.TryParse(Key, out int Level))
            {
                SkillAdvancementHint AdvancementHint = new();
                AdvancementHint.Level = Level;
                AdvancementHint.Hint = Hint;
                AdvancementHint.Npcs = ParseNpcs(Hint);

                collection.Add(AdvancementHint);
            }
            else
            {
                Debug.WriteLine($"Invalid advancement hint key: {Key}");
                throw new PreprocessorException(this);
            }
        }
    }

    private static string[]? ParseNpcs(string hint)
    {
        if (hint.EndsWith("learn a new dance move."))
            return null;

        if (hint.EndsWith("defeat a fabled dragon of yore."))
            return null;

        hint = hint.Replace(" during a Full Moon,", string.Empty);

        string Pattern;
        int StartIndex;

        Pattern = "gain favor with ";
        StartIndex = hint.IndexOf(Pattern);

        if (StartIndex < 0)
        {
            Pattern = "speak with ";
            StartIndex = hint.IndexOf(Pattern);
        }

        if (StartIndex < 0)
        {
            Pattern = "seek out ";
            StartIndex = hint.IndexOf(Pattern);
        }

        if (StartIndex < 0)
        {
            Pattern = "befriend ";
            StartIndex = hint.IndexOf(Pattern);
        }

        if (StartIndex < 0)
        {
            Pattern = " ...";
            StartIndex = hint.IndexOf(Pattern);

            if (StartIndex > 0)
                return null;
        }

        if (StartIndex < 0)
        {
            Pattern = " ??";
            StartIndex = hint.IndexOf(Pattern);

            if (StartIndex > 0)
                return null;
        }

        if (StartIndex < 0)
        {
            if (hint.Contains(" equip "))
                return null;

            Debug.WriteLine($"Advancement trigger not found in: {hint}");
            throw new PreprocessorException();
        }

        StartIndex += Pattern.Length;

        int EndIndex;

        EndIndex = hint.IndexOf(" in ", StartIndex);
        if (EndIndex < 0)
            EndIndex = hint.IndexOf(" outside of ", StartIndex);
        if (EndIndex < 0)
            EndIndex = hint.IndexOf(" deep beneath ", StartIndex);

        if (EndIndex <= StartIndex)
        {
            if (hint.Contains(" ??"))
                return null;

            Debug.WriteLine(string.Empty);
            Debug.WriteLine($"Bad advancement hint: {hint}");
            throw new PreprocessorException();
        }

        string NpcNameString = hint.Substring(StartIndex, EndIndex - StartIndex);
        string[] NpcNames = NpcNameString.Split(new string[] { " or " }, StringSplitOptions.None);

        if (NpcNames.Length == 0)
        {
            Debug.WriteLine($"No NPC name in advancement hint: {hint}");
            throw new PreprocessorException();
        }

        string[] Result = new string[NpcNames.Length];
        for (int i = 0; i < Result.Length; i++)
        {
            string NpcName = NpcNames[i];
            NpcName = NpcName.Replace(" ", string.Empty);
            Result[i] = $"NPC_{NpcName}";
        }

        return Result;
    }

    public override void Write(Utf8JsonWriter writer, SkillAdvancementHintCollection value, JsonSerializerOptions options)
    {
        WriteTableDictionary(writer, value, options);
    }

    private void WriteTableDictionary(Utf8JsonWriter writer, SkillAdvancementHintCollection value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        foreach (SkillAdvancementHint AdvancementHint in value)
        {
            string Key = AdvancementHint.Level.ToString()!;
            string? Hint = AdvancementHint.Hint;

            writer.WriteString(Key, Hint);
        }

        writer.WriteEndObject();
    }
}

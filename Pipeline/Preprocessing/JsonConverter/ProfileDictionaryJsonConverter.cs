namespace Preprocessor;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

public class ProfileDictionaryJsonConverter : JsonConverter<ProfileDictionary>
{
    public override ProfileDictionary? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        ProfileDictionary Result = new();

        ReadDictionary(Result, ref reader, options);

        return Result;
    }

    private void ReadDictionary(ProfileDictionary dictionary, ref Utf8JsonReader reader, JsonSerializerOptions options)
    {
        while (reader.Read() && reader.TokenType == JsonTokenType.PropertyName)
        {
            string Key = reader.GetString() ?? throw new NullReferenceException();
            reader.Read();

            string[]? RawProfile = null;
            Exception? Exception1 = null;

            try
            {
                RawProfile = JsonSerializer.Deserialize<string[]>(ref reader, options) ?? throw new NullReferenceException();
            }
            catch (Exception Exception)
            {
                Exception1 = Exception;
            }

            if (RawProfile is not null)
            {
                dictionary.Add(Key, new Profile(Key, RawProfile));
            }
            else
            {
                Debug.WriteLine($"\r\nKey: ");
                Debug.WriteLine(Exception1?.Message);
                throw new PreprocessorException(this);
            }
        }
    }

    public override void Write(Utf8JsonWriter writer, ProfileDictionary value, JsonSerializerOptions options)
    {
        WriteDictionary(writer, value, options);
    }

    private void WriteDictionary(Utf8JsonWriter writer, ProfileDictionary value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        foreach (KeyValuePair<string, Profile> Entry in value)
        {
            string Key = Entry.Key;
            Profile Profile = Entry.Value;

            writer.WritePropertyName(Key);
            JsonSerializer.Serialize(writer, Profile.ToRawProfile(Key), options);
        }

        writer.WriteEndObject();
    }
}

namespace Preprocessor;

internal record ColorFormat(string RGB, string? Name, bool HasSharp, bool HasAlpha, bool IsLowerCase)
{
    public string NormalizedRGB => RGB.ToUpper();

    public override string ToString()
    {
        if (Name is not null)
            return Name;

        string Result = RGB;

        if (HasAlpha)
            Result = Result + "FF";

        if (HasSharp)
            Result = "#" + Result;

        if (IsLowerCase)
            Result = Result.ToLower();

        return Result;
    }
}

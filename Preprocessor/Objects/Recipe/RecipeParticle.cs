namespace Preprocessor;

using System;
using System.Text.RegularExpressions;

internal class RecipeParticle
{
    public string? LightColor { get; set; }
    public string? ParticleName { get; set; }
    public string? PrimaryColor { get; set; }
    public string? SecondaryColor { get; set; }

    private const string ColorHeader = "Color=";
    private const string LightColorHeader = "LightColor=";

    public static RecipeParticle? Parse(string? content)
    {
        if (content is null)
            return null;

        // Search for an expression between parentheses.
        string ParameterPattern = @"\(([^)]+)\)";
        Match ParameterMatch = Regex.Match(content, ParameterPattern, RegexOptions.IgnoreCase);
        if (!ParameterMatch.Success)
            return new RecipeParticle { ParticleName = content };

        RecipeParticle Result = new();
        Result.ParticleName = content.Substring(0, ParameterMatch.Index);

        string InsideParameterString = content.Substring(ParameterMatch.Index + 1, content.Length - ParameterMatch.Index - 2);

        string[] LightSplit = InsideParameterString.Split(';');
        if (LightSplit.Length < 1 || LightSplit.Length > 2)
            throw new InvalidCastException();

        string ParticleColorString = LightSplit[0];
        string[] ColorSplit = ParticleColorString.Split(',');

        if (ColorSplit.Length == 1)
        {
            string FirstColor = ColorSplit[0];

            if (FirstColor.StartsWith(LightColorHeader))
                Result.LightColor = RgbColor.Parse(FirstColor, LightColorHeader, out _);
            else
                Result.PrimaryColor = RgbColor.Parse(FirstColor, ColorHeader, out _);
        }
        else if (ColorSplit.Length == 2)
        {
            Result.PrimaryColor = RgbColor.Parse(ColorSplit[0], ColorHeader, out _);
            Result.SecondaryColor = RgbColor.Parse(ColorSplit[1], string.Empty, out _);
        }
        else
            throw new InvalidCastException();

        if (LightSplit.Length == 2)
            Result.LightColor = RgbColor.Parse(LightSplit[1], LightColorHeader, out _);

        return Result;
    }

    public static string? ToString(RecipeParticle? particle)
    {
        if (particle is null)
            return null;

        if (particle.LightColor is null)
        {
            if (particle.PrimaryColor is null)
                return particle.ParticleName;

            if (particle.SecondaryColor is null)
                return $"{particle.ParticleName}({ColorHeader}#{particle.PrimaryColor})";

            return $"{particle.ParticleName}({ColorHeader}#{particle.PrimaryColor},#{particle.SecondaryColor})";
        }
        else
        {
            if (particle.PrimaryColor is null)
                return $"{particle.ParticleName}({LightColorHeader}#{particle.LightColor})";
            else
                return $"{particle.ParticleName}({ColorHeader}#{particle.PrimaryColor},#{particle.SecondaryColor};{LightColorHeader}#{particle.LightColor})";
        }
    }
}

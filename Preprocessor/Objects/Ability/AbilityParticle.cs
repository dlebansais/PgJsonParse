namespace Preprocessor;

using System;
using System.Text.RegularExpressions;

internal class AbilityParticle : Particle
{
    public string? AoEColor { get; set; }
    public int? AoERange { get; set; }
    public string? ParticleName { get; set; }
    public string? PrimaryColor { get; set; }
    public string? SecondaryColor { get; set; }

    private const string ColorHeader = "Color=";
    private const string AoeColorHeader = "AoeColor=";
    private const string AoeRangeHeader = "AoeRange=";

    public static AbilityParticle? Parse(string? content)
    {
        if (content is null)
            return null;

        // Search for an expression between parentheses.
        string ParameterPattern = @"\(([^)]+)\)";
        Match ParameterMatch = Regex.Match(content, ParameterPattern, RegexOptions.IgnoreCase);
        if (!ParameterMatch.Success)
            return new AbilityParticle { ParticleName = content };

        AbilityParticle Result = new();
        Result.ParticleName = content.Substring(0, ParameterMatch.Index);

        string InsideParameterString = content.Substring(ParameterMatch.Index + 1, content.Length - ParameterMatch.Index - 2);

        string[] AoESplit = InsideParameterString.Split(';');
        if (AoESplit.Length < 1 || AoESplit.Length > 2)
            throw new InvalidCastException();

        string ParticleAoEString = AoESplit[0];

        if (ParticleAoEString.StartsWith(AoeRangeHeader))
            Result.AoERange = int.Parse(ParticleAoEString.Substring(AoeRangeHeader.Length));

        string ParticleColorString = AoESplit[AoESplit.Length - 1];

        if (ParticleColorString.StartsWith(AoeColorHeader))
            Result.AoEColor = ParseColor(ParticleColorString, AoeColorHeader, out _);
        else if (!ParticleColorString.StartsWith(AoeRangeHeader))
        {
            string[] ColorSplit = ParticleColorString.Split(',');

            if (ColorSplit.Length == 1)
            {
                Result.PrimaryColor = ParseColor(ColorSplit[0], ColorHeader, out _);
            }
            else if (ColorSplit.Length == 2)
            {
                Result.PrimaryColor = ParseColor(ColorSplit[0], ColorHeader, out _);
                Result.SecondaryColor = ParseColor(ColorSplit[1], string.Empty, out _);
            }
            else
                throw new InvalidCastException();
        }

        return Result;
    }

    public static string? ToString(AbilityParticle? particle)
    {
        if (particle is null)
            return null;

        if (particle.AoERange is null)
        {
            if (particle.PrimaryColor is null)
                return particle.ParticleName;

            if (particle.SecondaryColor is null)
                return $"{particle.ParticleName}({ColorHeader}#{particle.PrimaryColor})";

            return $"{particle.ParticleName}({ColorHeader}#{particle.PrimaryColor},#{particle.SecondaryColor})";
        }
        else
        {
            if (particle.AoEColor is null)
                return $"{particle.ParticleName}({AoeRangeHeader}{particle.AoERange})";
            else
                return $"{particle.ParticleName}({AoeRangeHeader}{particle.AoERange};{AoeColorHeader}#{particle.AoEColor})";
        }
    }
}

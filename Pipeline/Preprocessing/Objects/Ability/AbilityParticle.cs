namespace Preprocessor;

using System.Diagnostics;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using FreeSql.DataAnnotations;

public class AbilityParticle : IHasKey<int>, IHasParentKey<int>
{
    private const string ColorHeader = "Color=";
    private const string AoeColorHeader = "AoeColor=";
    private const string AoeRangeHeader = "AoeRange=";

    [JsonIgnore]
    [Column(IsPrimary = true)]
    public int Key { get; set; }

    [JsonIgnore]
    public int ParentKey { get; set; }

    [JsonIgnore]
    public string? ParentProperty { get; set; }

    public string? AoEColor { get; set; }

    public int? AoERange { get; set; }

    public string? ParticleName { get; set; }

    public string? PrimaryColor { get; set; }

    public string? SecondaryColor { get; set; }

    public static AbilityParticle? Parse(string? content)
    {
        if (content is null)
            return null;

        // Search for an expression between parentheses.
        string ParameterPattern = @"\(([^)]+)\)";
        Match ParameterMatch = Regex.Match(content, ParameterPattern, RegexOptions.IgnoreCase);
        if (!ParameterMatch.Success)
        {
            string ParticleName = content == "Wolf-SeeRed" ? "WolfSeeRed" : content;
            return new AbilityParticle { ParticleName = ParticleName };
        }

        AbilityParticle Result = new();
        Result.ParticleName = content.Substring(0, ParameterMatch.Index);

        string InsideParameterString = content.Substring(ParameterMatch.Index + 1, content.Length - ParameterMatch.Index - 2);

        string[] AoESplit = InsideParameterString.Split(';');
        Debug.Assert(AoESplit.Length >= 1);
        
        if (AoESplit.Length > 2)
            throw new PreprocessorException();

        string ParticleAoEString = AoESplit[0];

        if (ParticleAoEString.StartsWith(AoeRangeHeader))
            Result.AoERange = int.Parse(ParticleAoEString.Substring(AoeRangeHeader.Length));

        string ParticleColorString = AoESplit[AoESplit.Length - 1];

        if (ParticleColorString.StartsWith(AoeColorHeader))
            Result.AoEColor = RgbColor.Parse(ParticleColorString, AoeColorHeader, out _);
        else if (!ParticleColorString.StartsWith(AoeRangeHeader))
        {
            string[] ColorSplit = ParticleColorString.Split(',');

            if (ColorSplit.Length == 1)
            {
                Result.PrimaryColor = RgbColor.Parse(ColorSplit[0], ColorHeader, out _);
            }
            else if (ColorSplit.Length == 2)
            {
                Result.PrimaryColor = RgbColor.Parse(ColorSplit[0], ColorHeader, out _);
                Result.SecondaryColor = RgbColor.Parse(ColorSplit[1], string.Empty, out _);
            }
            else
                throw new PreprocessorException();
        }

        return Result;
    }

    public static string? ToString(AbilityParticle? particle)
    {
        if (particle is null)
            return null;

        string? ParticleName = particle.ParticleName == "WolfSeeRed" ? "Wolf-SeeRed" : particle.ParticleName;

        if (particle.AoERange is null)
        {
            if (particle.PrimaryColor is null)
                return ParticleName;

            if (particle.SecondaryColor is null)
                return $"{ParticleName}({ColorHeader}#{particle.PrimaryColor})";

            return $"{ParticleName}({ColorHeader}#{particle.PrimaryColor},#{particle.SecondaryColor})";
        }
        else
        {
            if (particle.AoEColor is null)
                return $"{ParticleName}({AoeRangeHeader}{particle.AoERange})";
            else
                return $"{ParticleName}({AoeRangeHeader}{particle.AoERange};{AoeColorHeader}#{particle.AoEColor})";
        }
    }
}

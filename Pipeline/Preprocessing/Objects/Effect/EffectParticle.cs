namespace Preprocessor;

using System.Text.RegularExpressions;

public class EffectParticle
{
    public string? AoEColor { get; set; }
    public int? AoERange { get; set; }
    public string? ParticleName { get; set; }

    private const string AoeColorHeader = "AoeColor=#";
    private const string AoeRangeHeader = "AoeRange=";

    public static EffectParticle? Parse(string? content)
    {
        if (content is null)
            return null;

        // Search for an expression between parentheses.
        string ParameterPattern = @"\(([^)]+)\)";
        Match ParameterMatch = Regex.Match(content, ParameterPattern, RegexOptions.IgnoreCase);
        if (!ParameterMatch.Success)
        {
            string ParticleName = content == "OnFire-Green" ? "OnFireGreen" : content;
            return new EffectParticle { ParticleName = ParticleName };
        }

        EffectParticle Result = new();
        Result.ParticleName = content.Substring(0, ParameterMatch.Index);

        string InsideParameterString = content.Substring(ParameterMatch.Index + 1, content.Length - ParameterMatch.Index - 2);

        string[] AoESplit = InsideParameterString.Split(';');
        string ParticleAoEString = AoESplit[0];
        if (ParticleAoEString.StartsWith(AoeRangeHeader))
            Result.AoERange = int.Parse(ParticleAoEString.Substring(AoeRangeHeader.Length));
        else
            throw new PreprocessorException();

        if (AoESplit.Length == 2)
        {
            string ParticleColorString = AoESplit[1];

            if (ParticleColorString.StartsWith(AoeColorHeader))
                Result.AoEColor = RgbColor.Parse(ParticleColorString, AoeColorHeader, out _);
        }
        else
            throw new PreprocessorException();

        return Result;
    }

    public static string? ToString(EffectParticle? particle)
    {
        if (particle is null)
            return null;

        if (particle.AoERange is null)
        {
            if (particle.ParticleName == "OnFireGreen")
                return "OnFire-Green";
            else
                return particle.ParticleName;
        }

        if (particle.AoEColor is null)
            return $"{particle.ParticleName}({AoeRangeHeader}{particle.AoERange})";
        else
            return $"{particle.ParticleName}({AoeRangeHeader}{particle.AoERange};{AoeColorHeader}{particle.AoEColor})";
    }
}

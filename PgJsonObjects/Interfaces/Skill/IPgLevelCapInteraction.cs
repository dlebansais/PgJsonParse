﻿namespace PgJsonObjects
{
    public interface IPgLevelCapInteraction
    {
        int OtherLevel { get; }
        int? RawOtherLevel { get; }
        int Level { get; }
        int? RawLevel { get; }
        IPgSkill Link { get; }
    }
}

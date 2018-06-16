﻿using System.Collections.Generic;

namespace PgJsonObjects
{
    public interface IPgAttribute
    {
        string Label { get; }
        List<int> IconIdList { get; }
        string Tooltip { get; }
        double DefaultValue { get; }
        double? RawDefaultValue { get; }
        DisplayType DisplayType { get; }
        DisplayRule DisplayRule { get; }
        bool IsHidden { get; }
        bool? RawIsHidden { get; }
    }
}
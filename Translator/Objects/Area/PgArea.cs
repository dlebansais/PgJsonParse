﻿namespace PgObjects
{
    public class PgArea
    {
        public string FriendlyName { get; set; } = string.Empty;
        public string ShortFriendlyName { get; set; } = string.Empty;
        public MapAreaName KeyArea { get; set; }
    }
}

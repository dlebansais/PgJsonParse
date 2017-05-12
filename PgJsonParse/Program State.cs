namespace PgJsonParse
{
    public enum ProgramState
    {
        StartupScreen,
        LocatingLastVersion,
        Downloading,
        Parsing,
        ConnectingTables,
        CreatingIndex,
        LoadingIcons,
        Ready,
    }
}

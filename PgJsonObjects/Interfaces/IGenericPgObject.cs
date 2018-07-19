namespace PgJsonObjects
{
    public interface IGenericPgObject
    {
        string Key { get; }
        void Init();
        void AddLinkBackCollection(IPgBackLinkableCollection LinkBackCollection);
        void AddLinkBack(IBackLinkable LinkBack);
    }
}

namespace PgObjects
{
    using System.Collections.Generic;

    public class PgProfile : PgObject
    {
        public List<string> EffectList_Keys { get; set; } = new List<string>();

        public void SetLink(PgPower link) => Link = link;
        private PgPower? Link;

        public int IconId { get; set; }

        public override int ObjectIconId { get { return IconId; } }
        public override string ObjectName { get { return Key; } }
        public override string ToString() { return Key; }
    }
}

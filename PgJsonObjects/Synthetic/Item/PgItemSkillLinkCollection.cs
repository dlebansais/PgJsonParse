using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgItemSkillLinkCollection : List<IPgItemSkillLink>, IPgItemSkillLinkCollection
    {
        public static PgItemSkillLink CreateItem(byte[] data, ref int offset)
        {
            PgItemSkillLink Result = new PgItemSkillLink(data, ref offset);
            Result.Init();
            return Result;
        }
    }
}

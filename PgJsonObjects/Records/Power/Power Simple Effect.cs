using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PowerSimpleEffect : PowerEffect, IPgPowerSimpleEffect
    {
        private static readonly string IconIdPattern = "<icon=";

        public PowerSimpleEffect(string Description)
        {
            Description = Description.Trim();
            IconIdList = new List<int>();

            for(;;)
            {
                if (IconIdList.Count > 0 && Description.Contains(IconIdPattern))
                    Description = Description.Trim();

                if (Description.Length < IconIdPattern.Length)
                    break;

                if (!Description.StartsWith(IconIdPattern))
                    break;

                int EndIndex = Description.IndexOf('>');
                if (EndIndex < IconIdPattern.Length + 1)
                    break;

                string IdString = Description.Substring(IconIdPattern.Length, EndIndex - IconIdPattern.Length);

                int Id;
                if (!int.TryParse(IdString, out Id))
                    break;

                if (!IconIdList.Contains(Id))
                    IconIdList.Add(Id);

                Description = Description.Substring(EndIndex + 1);
            }

            this.Description = Description;
        }

        public string Description { get; private set; }
        public List<int> IconIdList { get; private set; }

        public override string AsEffectString()
        {
            string Result = "";

            foreach (int Id in IconIdList)
                Result += IconIdPattern + Id.ToString() + ">";

            Result += Description;

            return Result;
        }
    }
}

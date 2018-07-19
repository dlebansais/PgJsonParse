using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgItemAttributeLink : PgItemEffect<PgItemAttributeLink>, IPgItemAttributeLink
    {
        public PgItemAttributeLink(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgItemAttributeLink CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgItemAttributeLink CreateNew(byte[] data, ref int offset)
        {
            PgItemAttributeLink Result = new PgItemAttributeLink(data, ref offset);
            float AttributeEffect = Result.AttributeEffect;
            return Result;
        }

        public override string Key { get { return null; } }
        public string AttributeName { get { return GetString(4); } }
        public float AttributeEffect { get { return (float)GetDouble(8).Value; } }
        public IPgAttribute Link { get { return GetObject(12, ref _Link, PgAttribute.CreateNew); } } private IPgAttribute _Link;
        protected override List<string> FieldTableOrder { get { return GetStringList(16, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;
        public FloatFormat AttributeEffectFormat { get { return GetEnum<FloatFormat>(20); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser>(); } }

        public override string AsEffectString()
        {
            return "{" + AttributeName + "}{" + Tools.FloatToString(AttributeEffect, FloatFormat.Standard) + "}";
        }

        public string FriendlyNameAndEffect
        {
            get { return FriendlyName + " " + FriendlyEffect; }
        }

        public string FriendlyName
        {
            get { return Link.LabelRippedOfPercent; }
        }

        public string FriendlyEffect
        {
            get
            {
                string AttributeEffectString;

                if (Link.IsLabelWithPercent)
                {
                    AttributeEffectString = Tools.FloatToString(AttributeEffect * 100, AttributeEffectFormat);

                    if (AttributeEffect > 0)
                        AttributeEffectString = "+" + AttributeEffectString;

                    AttributeEffectString += "%";
                }
                else
                {
                    AttributeEffectString = AttributeEffect.ToString();

                    if (AttributeEffect > 0)
                        AttributeEffectString = "+" + AttributeEffectString;
                }

                return AttributeEffectString;
            }
        }

        public override string SortingName { get { return AttributeName; } }

        public override IList<IBackLinkable> GetLinkBack()
        {
            return null;
        }
    }
}

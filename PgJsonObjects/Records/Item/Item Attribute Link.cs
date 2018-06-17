using System.Collections.Generic;

namespace PgJsonObjects
{
    public class ItemAttributeLink : ItemEffect, IPgItemAttributeLink
    {
        private static WeightProfile SelectedProfile;

        public static void SetSelectedProfile(WeightProfile NewSelectedProfile)
        {
            SelectedProfile = NewSelectedProfile;
        }

        public ItemAttributeLink(string AttributeName, float AttributeEffect, FloatFormat AttributeEffectFormat)
        {
            this.AttributeName = AttributeName;
            this.AttributeEffect = AttributeEffect;
            this.AttributeEffectFormat = AttributeEffectFormat;

            Link = null;
            IsParsed = false;
        }

        public string AttributeName { get; private set; }
        public float AttributeEffect { get; private set; }
        public FloatFormat AttributeEffectFormat { get; private set; }
        public Attribute Link { get; private set; }
        public bool IsParsed { get; private set; }
        
        public override string AsEffectString()
        {
            return "{" + AttributeName + "}{" + Tools.FloatToString(AttributeEffect, FloatFormat.Standard) + "}";
        }

        public void SetLink(Attribute Link)
        {
            this.Link = Link;
            IsParsed = true;
        }

        public override bool Equals(object obj)
        {
            ItemAttributeLink AsItemAttributeLink;
            if ((AsItemAttributeLink = obj as ItemAttributeLink) != null)
                return AsItemAttributeLink.Link == Link;
            else
                return false;
        }

        public override int GetHashCode()
        {
            return Link.GetHashCode();
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

        public string FriendlyScore
        {
            get
            {
                if (SelectedProfile == null)
                    return "Error";

                WeightProfile WeightProfile = SelectedProfile;

                foreach (AttributeWeight AttributeWeight in WeightProfile.AttributeWeightList)
                    if (Link == AttributeWeight.Attribute)
                        return (AttributeEffect * AttributeWeight.Weight).ToString();

                return "Ignored";
            }
        }

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BaseOffset = offset;
            Dictionary<int, ISerializableJsonObject> StoredObjectTable = new Dictionary<int, ISerializableJsonObject>();

            AddInt(1, data, ref offset, BaseOffset, 0);
            AddDouble(AttributeEffect, data, ref offset, BaseOffset, 4);
            AddObject(Link, data, ref offset, BaseOffset, 8, StoredObjectTable);

            FinishSerializing(data, ref offset, BaseOffset, 12, null, StoredObjectTable, null, null, null, null, null, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}

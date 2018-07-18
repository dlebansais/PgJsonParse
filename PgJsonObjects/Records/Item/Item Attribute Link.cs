﻿using System.Collections.Generic;

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
        public IPgAttribute Link { get; private set; }
        public bool IsParsed { get; private set; }
        
        public override string AsEffectString()
        {
            return "{" + AttributeName + "}{" + Tools.FloatToString(AttributeEffect, FloatFormat.Standard) + "}";
        }

        public void SetLink(IPgAttribute Link)
        {
            this.Link = Link;
            IsParsed = true;
        }

        /*
        public override bool Equals(object obj)
        {
            IPgItemAttributeLink AsItemAttributeLink;
            if ((AsItemAttributeLink = obj as IPgItemAttributeLink) != null)
                return AsItemAttributeLink.Link == Link;
            else
                return false;
        }*/

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
            Dictionary<int, string> StoredStringtable = new Dictionary<int, string>();
            Dictionary<int, ISerializableJsonObject> StoredObjectTable = new Dictionary<int, ISerializableJsonObject>();
            Dictionary<int, List<string>> StoredStringListTable = new Dictionary<int, List<string>>();

            AddInt(1, data, ref offset, BaseOffset, 0);
            AddString(AttributeName, data, ref offset, BaseOffset, 4, StoredStringtable);
            AddDouble(AttributeEffect, data, ref offset, BaseOffset, 8);
            AddObject(Link as ISerializableJsonObject, data, ref offset, BaseOffset, 12, StoredObjectTable);
            AddStringList(new List<string>(), data, ref offset, BaseOffset, 16, StoredStringListTable);
            AddEnum(AttributeEffectFormat, data, ref offset, BaseOffset, 20);
            
            FinishSerializing(data, ref offset, BaseOffset, 22, StoredStringtable, StoredObjectTable, null, null, null, null, StoredStringListTable, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}

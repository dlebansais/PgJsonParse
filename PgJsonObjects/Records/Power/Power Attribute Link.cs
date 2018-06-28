using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PowerAttributeLink : PowerEffect, IPgPowerAttributeLink
    {
        public PowerAttributeLink(string AttributeName, float AttributeEffect, FloatFormat AttributeEffectFormat)
        {
            this.AttributeName = AttributeName;
            this.RawAttributeEffect = AttributeEffect;
            this.AttributeEffectFormat = AttributeEffectFormat;
            this.AttributeSkill = PowerSkill.Internal_None;
            AttributeLink = null;
            SkillLink = null;
            IsParsed = false;
        }

        public PowerAttributeLink(string AttributeName, float AttributeEffect, FloatFormat AttributeEffectFormat, PowerSkill AttributeSkill)
        {
            this.AttributeName = AttributeName;
            this.RawAttributeEffect = AttributeEffect;
            this.AttributeEffectFormat = AttributeEffectFormat;
            this.AttributeSkill = AttributeSkill;
            AttributeLink = null;
            SkillLink = null;
            IsParsed = false;
        }

        public string AttributeName { get; private set; }
        public float AttributeEffect { get { return RawAttributeEffect.HasValue ? RawAttributeEffect.Value : 0; } }
        public float? RawAttributeEffect { get; private set; }
        public IPgAttribute AttributeLink { get; private set; }
        public IPgSkill SkillLink { get; private set; }
        public FloatFormat AttributeEffectFormat { get; private set; }

        public PowerSkill AttributeSkill { get; private set; }
        public bool IsParsed { get; private set; }

        public override string AsEffectString()
        {
            if (AttributeSkill == PowerSkill.Internal_None)
                return "{" + AttributeName + "}{" + Tools.FloatToString(AttributeEffect, FloatFormat.Standard) + "}";
            else
                return "{" + AttributeName + "}{" + Tools.FloatToString(AttributeEffect, FloatFormat.Standard) + "}{" + AttributeSkill.ToString() + "}";
        }

        public void SetLinks(IPgAttribute AttributeLink, IPgSkill SkillLink)
        {
            this.AttributeLink = AttributeLink;
            this.SkillLink = SkillLink;
            IsParsed = true;
        }

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BaseOffset = offset;
            Dictionary<int, string> StoredStringtable = new Dictionary<int, string>();
            Dictionary<int, ISerializableJsonObject> StoredObjectTable = new Dictionary<int, ISerializableJsonObject>();

            AddInt(1, data, ref offset, BaseOffset, 0);
            AddString(AttributeName, data, ref offset, BaseOffset, 4, StoredStringtable);
            AddDouble((double)RawAttributeEffect, data, ref offset, BaseOffset, 8);
            AddObject(AttributeLink as ISerializableJsonObject, data, ref offset, BaseOffset, 12, StoredObjectTable);
            AddObject(SkillLink as ISerializableJsonObject, data, ref offset, BaseOffset, 16, StoredObjectTable);
            AddEnum(AttributeEffectFormat, data, ref offset, BaseOffset, 20);

            FinishSerializing(data, ref offset, BaseOffset, 22, StoredStringtable, StoredObjectTable, null, null, null, null, null, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}

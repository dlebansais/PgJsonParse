namespace PgJsonObjects
{
    public class PowerAttributeLink : PowerEffect
    {
        public PowerAttributeLink(string AttributeName, float AttributeEffect, FloatFormat AttributeEffectFormat)
        {
            this.AttributeName = AttributeName;
            this.AttributeEffect = AttributeEffect;
            this.AttributeEffectFormat = AttributeEffectFormat;
            this.AttributeSkill = PowerSkill.Internal_None;
            AttributeLink = null;
            SkillLink = null;
            IsParsed = false;
        }

        public PowerAttributeLink(string AttributeName, float AttributeEffect, FloatFormat AttributeEffectFormat, PowerSkill AttributeSkill)
        {
            this.AttributeName = AttributeName;
            this.AttributeEffect = AttributeEffect;
            this.AttributeEffectFormat = AttributeEffectFormat;
            this.AttributeSkill = AttributeSkill;
            AttributeLink = null;
            SkillLink = null;
            IsParsed = false;
        }

        public string AttributeName { get; private set; }
        public float AttributeEffect { get; private set; }
        public FloatFormat AttributeEffectFormat { get; private set; }
        public PowerSkill AttributeSkill { get; private set; }
        public Attribute AttributeLink { get; private set; }
        public Skill SkillLink { get; private set; }
        public bool IsParsed { get; private set; }

        public override string AsEffectString()
        {
            if (AttributeSkill == PowerSkill.Internal_None)
                return "{" + AttributeName + "}{" + Tools.FloatToString(AttributeEffect, FloatFormat.Standard) + "}";
            else
                return "{" + AttributeName + "}{" + Tools.FloatToString(AttributeEffect, FloatFormat.Standard) + "}{" + AttributeSkill.ToString() + "}";
        }

        public void SetLinks(Attribute AttributeLink, Skill SkillLink)
        {
            this.AttributeLink = AttributeLink;
            this.SkillLink = SkillLink;
            IsParsed = true;
        }
    }
}

using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class ServerInfoEffectCollection : List<ServerInfoEffect>, ISerializableJsonObjectCollection
    {
        public ISerializableJsonObject GetAt(int index)
        {
            return this[index] as ISerializableJsonObject;
        }

        public static IGenericPgObject CreateItem(byte[] data, int offset)
        {
            ServerInfoEffectType ServerInfoEffectType = (ServerInfoEffectType)BitConverter.ToInt32(data, offset);
            offset += 4;

            switch (ServerInfoEffectType)
            {
                case ServerInfoEffectType.RaiseSkillToLevel:
                case ServerInfoEffectType.GiveXP:
                case ServerInfoEffectType.GiveXpVerbose:
                    return new PgSkillAndLevelServerInfoEffect(data, offset);

                case ServerInfoEffectType.BoostHydration:
                case ServerInfoEffectType.BoostBodyHeat:
                case ServerInfoEffectType.IocainePoison:
                case ServerInfoEffectType.UseGeologySurvey_Serbule:
                case ServerInfoEffectType.UseGeologySurvey_SouthSerbule:
                case ServerInfoEffectType.UseGeologySurvey_Eltibule:
                case ServerInfoEffectType.UseGeologySurvey_Kur:
                case ServerInfoEffectType.UseMiningSurveyX_KurMountains:
                case ServerInfoEffectType.UseMiningSurvey_SouthSerbule:
                case ServerInfoEffectType.UseMiningSurvey_Eltibule:
                case ServerInfoEffectType.UseMiningSurveyX_Ilmari:
                case ServerInfoEffectType.UseEltibuleTreasureMap:
                case ServerInfoEffectType.UseIlmariTreasureMap:
                case ServerInfoEffectType.Armor:
                    return new PgValueServerInfoEffect(data, offset);

                case ServerInfoEffectType.HouseholdPoison:
                    return new PgValueServerInfoEffect(data, offset);
                    //return new PgSimpleServerInfoEffect(data, offset);

                case ServerInfoEffectType.LearnAbility:
                    return new PgAbilityServerInfoEffect(data, offset);

                case ServerInfoEffectType.GiveCouncilCoins:
                    return new PgIntervalServerInfoEffect(data, offset);
                    //return new PgValueServerInfoEffect(data, offset);

                case ServerInfoEffectType.GivePoetryAppreciationXp:
                    return new PgPoetryServerInfoEffect(data, offset);

                case ServerInfoEffectType.Drinking_Beer:
                case ServerInfoEffectType.Drinking_HardLiquor:
                case ServerInfoEffectType.Drinking_Wine:
                    return new PgDrinkEffectServerInfoEffect(data, offset);

                case ServerInfoEffectType.ArmorPotion:
                case ServerInfoEffectType.HealthPotion:
                case ServerInfoEffectType.PowerPotion:
                    return new PgPotionServerInfoEffect(data, offset);

                case ServerInfoEffectType.HealthHOTPotion:
                case ServerInfoEffectType.PowerHOTPotion:
                    return new PgPotionServerInfoEffect(data, offset);

                case ServerInfoEffectType.SummonFlower:
                case ServerInfoEffectType.SummonFlowerDisplay:
                case ServerInfoEffectType.SpawnMushroomFarmBox:
                case ServerInfoEffectType.SpawnItemDispenser:
                case ServerInfoEffectType.BestowGolemConditional:
                case ServerInfoEffectType.BestowGolemAbility:
                case ServerInfoEffectType.PulseEvent:
                case ServerInfoEffectType.SetPrimaryCombatSkill:
                case ServerInfoEffectType.SetInteractionFlag:
                case ServerInfoEffectType.SummonGruesomeSpookyPunch:
                    return new PgSimpleServerInfoEffect(data, offset);

                case ServerInfoEffectType.EquipmentBoost:
                    return new PgTemporaryServerInfoEffect(data, offset);
                    //return new PgEquipmentBoostServerInfoEffect(data, offset);

                default:
                    throw new InvalidOperationException();
            }
        }
    }
}

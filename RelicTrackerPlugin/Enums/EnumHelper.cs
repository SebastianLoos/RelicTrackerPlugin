using Lumina.Excel.GeneratedSheets;
using RelicTrackerPlugin.Enums.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RelicTrackerPlugin.Enums;

internal class EnumHelper
{
    public static T? GetAttribute<T>(Enum enumType) where T : Attribute
    {
        Type type = enumType.GetType();
        MemberInfo[] memberInfos = type.GetMember(enumType.ToString());
        object[] attributes = memberInfos[0].GetCustomAttributes(typeof(T), false);

        return attributes.Length > 0 ? (T)attributes[0] : null;
    }

    public static void ForEach<T>(Action<T> action)
    {
        foreach (object value in Enum.GetValues(typeof(T)))
        {
            action((T)value);
        }
    }

    public static IEnumerable<T> GetAllValues<T>() where T : Enum
    {
        return Enum.GetValues(typeof(T)).Cast<T>();
    }

    public static WeaponJob GetWeaponJob(ClassJob? classJob)
    {
        return classJob?.JobIndex switch
        {
            0 => WeaponJob.UNKNOWN,
            1 => WeaponJob.PLD,
            2 => WeaponJob.MNK,
            3 => WeaponJob.WAR,
            4 => WeaponJob.DRG,
            5 => WeaponJob.BRD,
            6 => WeaponJob.WHM,
            7 => WeaponJob.BLM,
            8 => WeaponJob.SMN,
            9 => WeaponJob.SCH,
            10 => WeaponJob.NIN,
            11 => WeaponJob.MCH,
            12 => WeaponJob.DRK,
            13 => WeaponJob.AST,
            14 => WeaponJob.SAM,
            15 => WeaponJob.RDM,
            16 => WeaponJob.BLU,
            17 => WeaponJob.GNB,
            18 => WeaponJob.DNC,
            19 => WeaponJob.RPR,
            20 => WeaponJob.SGE,
            _ => WeaponJob.UNKNOWN
        };
    }

    public static Tuple<uint,int>[] GetWeaponItemNeededQuantities()
    {
        return GetAllValues<WeaponItem>().Select(x => new Tuple<uint, int>(GetAttribute<WeaponItemIdAttribute>(x)?.Value ?? 0, GetWeaponItemNeededQuantity(x))).ToArray();
    }

    private static int GetWeaponItemNeededQuantity(WeaponItem weaponItem)
    {
        return GetAllValues<WeaponCategory>().Select(weaponCategory => new { WeaponCategory = weaponCategory, Jobs = GetAttribute<WeaponCategoryJobsAttribute>(weaponCategory)?.Values })
            .Sum(x => x.Jobs?.Sum(job => GetAttribute<WeaponCategoryStepsAttribute>(x.WeaponCategory)?.Values.Sum(weaponStep => GetWeaponItemNeededQuantity(weaponItem, job, weaponStep)))) ?? 0;
    }

    private static int GetWeaponItemNeededQuantity(WeaponItem weaponItem, WeaponJob weaponJob, WeaponStep weaponStep)
    {
        return GetAttribute<WeaponSubStepsAttribute>(weaponStep)?.Values.Sum(x => GetWeaponItemNeededQuantity(weaponItem, weaponJob, x)) ?? 0;
    }

    private static int GetWeaponItemNeededQuantity(WeaponItem weaponItem, WeaponJob weaponJob, WeaponQuestSet weaponQuestSet)
    {
        WeaponQuestSetAttribute? attribute = GetAttribute<WeaponQuestSetAttribute>(weaponQuestSet);
        if (attribute == null)
        {
            return 0;
        }
        else
        {
            return attribute.QuestType switch
            {
                WeaponQuestType.OneTime => GetWeaponItemNeededQuantity(weaponItem, attribute.Values.ElementAtOrDefault(0)),
                WeaponQuestType.JobSpecific => GetWeaponItemNeededQuantity(weaponItem, attribute.Values.ElementAtOrDefault((int)weaponJob)),
                _ => 0
            };
        }
    }

    private static int GetWeaponItemNeededQuantity(WeaponItem weaponItem, WeaponQuest weaponQuest)
    {
        return GetAttribute<WeaponQuestAttribute>(weaponQuest)?.Steps.Sum(x => GetWeaponItemNeededQuantity(weaponItem, x)) ?? 0;
    }

    private static int GetWeaponItemNeededQuantity(WeaponItem weaponItem, WeaponQuestStep weaponQuestStep)
    {
        int index = Array.IndexOf(GetAttribute<WeaponQuestItemsAttribute>(weaponQuestStep)?.Values ?? Array.Empty<WeaponItem>(), weaponItem);
        if (index < 0)
        {
            return 0;
        }
        else
        {
            return GetAttribute<WeaponQuestItemsAttribute>(weaponQuestStep)?.Amounts.ElementAtOrDefault(index) ?? 0;
        }
    }

    private static int GetWeaponItemNeededQuantity(WeaponItem weaponItem, WeaponJob weaponJob, WeaponSubStep weaponSubStep)
    {
        int quantity = 0;
        int index = Array.IndexOf(GetAttribute<WeaponSubStepItemsAttribute>(weaponSubStep)?.Values ?? Array.Empty<WeaponItem>(), weaponItem);
        if (index > 0)
        {
            quantity += GetAttribute<WeaponSubStepItemsAttribute>(weaponSubStep)?.Amounts.ElementAtOrDefault(index) ?? 0;
        }

        quantity += GetWeaponItemNeededQuantity(weaponItem, weaponJob, GetAttribute<WeaponSubStepQuestAttribute>(weaponSubStep)?.Value ?? WeaponQuestSet.Unknown);

        return quantity;
    }
}

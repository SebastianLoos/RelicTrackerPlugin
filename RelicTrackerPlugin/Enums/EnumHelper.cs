using Lumina.Excel.GeneratedSheets;
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
}

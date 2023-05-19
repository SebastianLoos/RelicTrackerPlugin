using System;

namespace RelicTrackerPlugin.Enums.Attributes;

[AttributeUsage(AttributeTargets.Field)]
internal class WeaponQuestItemsAttribute : Attribute
{
    public WeaponItem[] Values { get; }

    public int[] Amounts { get; }

    public WeaponQuestItemsAttribute(WeaponItem[] values, int[] amounts)
    {
        Values = values;
        Amounts = amounts;
    }
}

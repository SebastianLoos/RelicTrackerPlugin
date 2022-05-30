using System;

namespace RelicTrackerPlugin.Enums.Attributes;

[AttributeUsage(AttributeTargets.Field)]
internal class WeaponSubStepItemsAttribute : Attribute
{
    public WeaponItem[] Values { get; }

    public int[] Amounts { get; }

    public WeaponSubStepItemsAttribute(WeaponItem[] values , int[] amounts)
    {
        Values = values;
        Amounts = amounts;
    }
}

using System;

namespace RelicTrackerPlugin.Enums.Attributes;

[AttributeUsage(AttributeTargets.Field)]
internal class WeaponStepQuestsAttribute : Attribute
{
    public WeaponQuest[] Values { get; }

    public WeaponStepQuestsAttribute(WeaponQuest[] values)
    {
        Values = values;
    }
}

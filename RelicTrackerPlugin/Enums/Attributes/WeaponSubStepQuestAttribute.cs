using System;

namespace RelicTrackerPlugin.Enums.Attributes;

[AttributeUsage(AttributeTargets.Field)]
internal class WeaponSubStepQuestAttribute : Attribute
{
    public WeaponQuest Value { get; }

    public WeaponSubStepQuestAttribute(WeaponQuest value)
    {
        Value = value;
    }
}

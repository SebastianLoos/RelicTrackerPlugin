using System;

namespace RelicTrackerPlugin.Enums.Attributes;

[AttributeUsage(AttributeTargets.Field)]
internal class WeaponSubStepQuestAttribute : Attribute
{
    public WeaponQuestSet Value { get; }


    public WeaponSubStepQuestAttribute(WeaponQuestSet value)
    {
        Value = value;
    }
}

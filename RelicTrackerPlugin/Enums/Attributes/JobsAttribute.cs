using System;

namespace RelicTrackerPlugin.Enums.Attributes;

[AttributeUsage(AttributeTargets.Field)]
internal class JobsAttribute : Attribute
{
    public WeaponJob[] Values;

    public JobsAttribute(WeaponJob[] values)
    {
        Values = values;
    }

    public override string ToString()
    {
        return string.Join(',', Values);
    }
}

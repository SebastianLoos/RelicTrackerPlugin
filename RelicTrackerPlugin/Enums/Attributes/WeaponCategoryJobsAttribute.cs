using System;

namespace RelicTrackerPlugin.Enums.Attributes;

[AttributeUsage(AttributeTargets.Field)]
internal class WeaponCategoryJobsAttribute : Attribute
{
    public WeaponJob[] Values { get; }

    public WeaponCategoryJobsAttribute(WeaponJob[] values)
    {
        Values = values;
    }

    public override string ToString()
    {
        return string.Join(',', Values);
    }
}

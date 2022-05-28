using System;

namespace RelicTrackerPlugin.Enums.Attributes;

[AttributeUsage(AttributeTargets.Field)]
internal class WeaponCategoryNameAttribute : Attribute
{
    public string Value { get; }

    public WeaponCategoryNameAttribute(string value)
    {
        Value = value;
    }

    public override string ToString()
    {
        return Value;
    }
}

using System;

namespace RelicTrackerPlugin.Enums.Attributes;

[AttributeUsage(AttributeTargets.Field)]
internal class WeaponItemIdAttribute : Attribute
{
    public uint Value { get; }

    public WeaponItemIdAttribute(uint value)
    {
        Value = value;
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}

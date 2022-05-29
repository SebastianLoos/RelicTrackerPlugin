using System;

namespace RelicTrackerPlugin.Enums.Attributes;

[AttributeUsage(AttributeTargets.Field)]
internal class WeaponIdAttribute : Attribute
{
    public uint Value { get; }

    public WeaponIdAttribute(uint value)
    {
        Value = value;
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}

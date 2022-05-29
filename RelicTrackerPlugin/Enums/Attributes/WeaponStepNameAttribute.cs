using System;

namespace RelicTrackerPlugin.Enums.Attributes;

[AttributeUsage(AttributeTargets.Field)]
internal class WeaponStepNameAttribute : Attribute
{
    public string Value { get; }

    public WeaponStepNameAttribute(string value) 
    { 
        Value = value; 
    }
}

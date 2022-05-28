using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelicTrackerPlugin.Enums.Attributes;

[AttributeUsage(AttributeTargets.Field)]
internal class WeaponJobAbbreviationAttribute : Attribute
{
    public string Value { get; }

    public WeaponJobAbbreviationAttribute(string value)
    {
        Value = value;
    }

    public override string ToString()
    {
        return Value;
    }
}

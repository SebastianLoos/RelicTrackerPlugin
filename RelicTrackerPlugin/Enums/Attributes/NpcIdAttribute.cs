using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelicTrackerPlugin.Enums.Attributes;

[AttributeUsage(AttributeTargets.Field)]
internal class NpcIdAttribute : Attribute
{
    public uint Value { get; }

    public NpcIdAttribute(uint value)
    {
        Value = value;
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}

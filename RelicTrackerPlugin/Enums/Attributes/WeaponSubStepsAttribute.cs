using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelicTrackerPlugin.Enums.Attributes;

[AttributeUsage(AttributeTargets.Field)]
internal class WeaponSubStepsAttribute : Attribute
{
    public WeaponSubStep[] Values { get; }

    public WeaponSubStepsAttribute(WeaponSubStep[] values)
    {
        Values = values;
    }
}

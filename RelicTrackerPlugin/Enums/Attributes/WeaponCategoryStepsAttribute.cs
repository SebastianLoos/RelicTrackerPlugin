using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelicTrackerPlugin.Enums.Attributes;

[AttributeUsage(AttributeTargets.Field)]
internal class WeaponCategoryStepsAttribute : Attribute
{
    public WeaponStep[] Values { get; }

    public WeaponCategoryStepsAttribute(WeaponStep[] values)
    {
        Values = values;
    }
}

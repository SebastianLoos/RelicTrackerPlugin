﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelicTrackerPlugin.Enums.Attributes;

[AttributeUsage(AttributeTargets.Field)]
internal class WeaponQuestAttribute : Attribute
{
    public uint Value { get; }

    public WeaponQuestStep[] Steps { get; }

    public WeaponQuestAttribute(uint value, WeaponQuestStep[] steps)
    {
        Value = value;
        Steps = steps;
    }
}

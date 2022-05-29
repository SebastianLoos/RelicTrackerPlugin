using System;

namespace RelicTrackerPlugin.Enums.Attributes;

[AttributeUsage(AttributeTargets.Field)]
internal class WeaponQuestAttribute : Attribute
{
    public uint[] Values { get; }

    public WeaponQuestType QuestType { get; }

    public WeaponQuestAttribute(uint[] values, WeaponQuestType questType)
    {
        Values = values;
        QuestType = questType;
    }
}

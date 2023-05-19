using System;

namespace RelicTrackerPlugin.Enums.Attributes;

[AttributeUsage(AttributeTargets.Field)]
internal class WeaponQuestSetAttribute : Attribute
{
    public WeaponQuest[] Values { get; }

    public WeaponQuestType QuestType { get; }

    public WeaponQuestSetAttribute(WeaponQuest[] values, WeaponQuestType questType)
    {
        Values = values;
        QuestType = questType;
    }
}

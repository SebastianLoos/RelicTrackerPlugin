using System;

namespace RelicTrackerPlugin.Enums.Attributes;

[AttributeUsage(AttributeTargets.Field)]
internal class WeaponQuestLocationAttribute : Attribute
{
    public uint Value { get; }

    public float PositionX { get; }

    public float PositionY { get; }

    public WeaponQuestLocationAttribute(uint value, float positionX = 0f, float positionY = 0f)
    {
        Value = value;
        PositionX = positionX;
        PositionY = positionY;
    }
}
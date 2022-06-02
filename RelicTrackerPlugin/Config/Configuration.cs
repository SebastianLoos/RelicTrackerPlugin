using Dalamud.Configuration;
using Dalamud.Plugin;
using System;
using System.Collections.Generic;

namespace RelicTrackerPlugin.Config;

[Serializable]
public class Configuration : IPluginConfiguration
{
    public int Version { get; set; } = 0;

    public List<ConfigurationItem> Items { get; set; } = new();

    public List<uint> CompletedQuestIds { get; set; } = new();

    public bool SomePropertyToBeSavedAndWithADefault { get; set; } = true;

    // the below exist just to make saving less cumbersome

    [NonSerialized]
    private DalamudPluginInterface? pluginInterface;

    public void Initialize(DalamudPluginInterface pluginInterface)
    {
        this.pluginInterface = pluginInterface;
    }

    public void Save()
    {
        pluginInterface!.SavePluginConfig(this);
    }

    public bool IsQuestCompleted(uint? questId)
    {
        if (questId == null)
        {
            return false;
        }
        else
        {
            return CompletedQuestIds.Contains((uint)questId);
        }
    }
}

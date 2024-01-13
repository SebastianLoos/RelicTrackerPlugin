using Dalamud.Configuration;
using Dalamud.Plugin;
using RelicTrackerPlugin.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RelicTrackerPlugin.Config;

[Serializable]
public class Configuration : IPluginConfiguration
{
    public int Version { get; set; } = 0;

    public List<ConfigurationItem> Items { get; set; } = new();

    public List<uint> CompletedQuestIds { get; set; } = new();

    public List<ConfigurationSubStep> CompletedWeaponSubSteps { get; set; } = new();

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

    public bool IsSubStepComplete(uint weaponSubStep, uint weaponJob)
    {
        return CompletedWeaponSubSteps.Any(x => x.WeaponSubStep == weaponSubStep & x.WeaponJob == weaponJob);
    }

    public void AddCompletedSubStep(uint weaponSubStep, uint weaponJob)
    {
        ConfigurationSubStep? completedWeaponSubStep = CompletedWeaponSubSteps.FirstOrDefault(x => x.WeaponSubStep  == weaponSubStep & x.WeaponJob == weaponJob);
        if (completedWeaponSubStep == null)
        {
            CompletedWeaponSubSteps.Add(new()
            {
                WeaponSubStep = weaponSubStep,
                WeaponJob = weaponJob
            });
        }
    }

    public void RemoveCompletedSubStep(uint weaponSubStep, uint weaponJob)
    {
        ConfigurationSubStep? completedWeaponSubStep = CompletedWeaponSubSteps.FirstOrDefault(x => x.WeaponSubStep == weaponSubStep & x.WeaponJob == weaponJob);
        if (completedWeaponSubStep != null)
        {
            CompletedWeaponSubSteps.Remove((ConfigurationSubStep)completedWeaponSubStep);
        }
    }
}

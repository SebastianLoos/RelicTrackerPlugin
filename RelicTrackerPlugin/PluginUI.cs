﻿using Dalamud.Plugin;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using ImGuiNET;
using Lumina.Excel.GeneratedSheets;
using RelicTrackerPlugin.Enums;
using RelicTrackerPlugin.Enums.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using XivCommon;
using static Dalamud.Interface.Utility.Raii.ImRaii;
using static FFXIVClientStructs.FFXIV.Client.Game.RetainerManager;

namespace RelicTrackerPlugin;

// It is good to have this be disposable in general, in case you ever need it
// to do any cleanup
class PluginUI : IDisposable
{
    // this extra bool exists for ImGui, since you can't ref a property
    private bool visible = false;
    public bool Visible
    {
        get { return visible; }
        set { visible = value; }
    }

    private bool settingsVisible = false;
    public bool SettingsVisible
    {
        get { return settingsVisible; }
        set { settingsVisible = value; }
    }

    private bool retainerScanActive = false;
    public bool RetainerScanActive
    {
        get { return retainerScanActive; }
        set { retainerScanActive = value; }
    }
    
    private readonly Plugin plugin;
    
    private string[] inventoryIDs = Array.Empty<string>();
    private Models.Item[] items = Array.Empty<Models.Item>();

    private readonly XivCommonBase commonBase;

    private WeaponCategory selectedWeaponCategory;
    private WeaponStep selectedStep;
    private WeaponJob selectedJob;

    private ItemInventory selectedRetainer;
    
    public PluginUI(Plugin plugin, DalamudPluginInterface dalamudPluginInterface)
    {
        this.plugin = plugin;
        
        commonBase = new(dalamudPluginInterface);
        selectedJob = EnumHelper.GetWeaponJob(plugin.ClientState.LocalPlayer?.ClassJob.GetWithLanguage(plugin.ClientState.ClientLanguage));
        selectedRetainer = ItemInventory.Retainer1;
    }

    public void Dispose()
    {
    }

    public void Draw()
    {
        // This is our only draw handler attached to UIBuilder, so it needs to be
        // able to draw any windows we might have open.
        // Each method checks its own visibility/state to ensure it only draws when
        // it actually makes sense.
        // There are other ways to do this, but it is generally best to keep the number of
        // draw delegates as low as possible.

        DrawMainWindow();
        DrawSettingsWindow();
        DrawRetainerScanWindow();
    }

    public void DrawMainWindow()
    {
        if (!Visible)
        {
            return;
        }

        ImGui.SetNextWindowSize(new Vector2(800, 600), ImGuiCond.FirstUseEver);
        ImGui.SetNextWindowSizeConstraints(new Vector2(800, 600), new Vector2(float.MaxValue, float.MaxValue));
        if (ImGui.Begin("Relic Tracker Plugin", ref visible, ImGuiWindowFlags.HorizontalScrollbar | ImGuiWindowFlags.NoScrollWithMouse))
        {
            
            if (ImGui.BeginMenuBar())
            {
                ImGui.MenuItem("Items");
                ImGui.EndMenuBar();
            }
            if (ImGui.BeginTabBar("weaponGeneration"))
            {
                IEnumerable<WeaponCategory> weaponCategories = EnumHelper.GetAllValues<WeaponCategory>();
                foreach (WeaponCategory weaponCategory in weaponCategories)
                {
                    if (ImGui.BeginTabItem($"{EnumHelper.GetAttribute<WeaponCategoryNameAttribute>(weaponCategory)}(0/{EnumHelper.GetAttribute<WeaponCategoryJobsAttribute>(weaponCategory)?.Values.Length})"))
                    {
                        selectedWeaponCategory = weaponCategory;
                        ImGui.EndTabItem();
                    }
                }
                ImGui.EndTabBar();
            }

            WeaponJob currentJob = EnumHelper.GetWeaponJob(plugin.ClientState.LocalPlayer?.ClassJob.GetWithLanguage(plugin.ClientState.ClientLanguage));

            ImGui.Text($"Current Job: {currentJob} (0/11)");
            ImGui.Text($"Selected Job: {selectedJob} (0/11)");

            DrawTabContent(selectedWeaponCategory);

            ImGui.Separator();

            if (ImGui.Button("Settings"))
            {
                SettingsVisible = true;
            }

            foreach (string inventory1Name in inventoryIDs)
            {
                ImGui.Text(inventory1Name);
            }
            
        }
        ImGui.End();
    }
    
    public void DrawSettingsWindow()
    {
        if (!SettingsVisible)
        {
            return;
        }

        ImGui.SetNextWindowSize(new Vector2(232, 75), ImGuiCond.Always);
        if (ImGui.Begin("A Wonderful Configuration Window", ref this.settingsVisible,
            ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse))
        {
            // can't ref a property, so use a local copy
            var configValue = plugin.Configuration.SomePropertyToBeSavedAndWithADefault;
            if (ImGui.Checkbox("Random Config Bool", ref configValue))
            {
                plugin.Configuration.SomePropertyToBeSavedAndWithADefault = configValue;
                // can save immediately on change, if you don't want to provide a "Save and Close" button
                plugin.Configuration.Save();
            }
        }
        ImGui.End();
    }

    public void DrawRetainerScanWindow()
    {
        if (!RetainerScanActive)
        {
            return;
        }

        ImGui.SetNextWindowSize(new Vector2(350, 150), ImGuiCond.Always);
        if (ImGui.Begin("Scan retainer inventories", ref this.retainerScanActive,
            ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse))
        {
            if (ImGui.BeginCombo("retainerselect", "Select Retainer"))
            {
                ImGui.Text("Test");
            }
            ImGui.EndCombo();
        }
        ImGui.End();
    }

    private void DrawTabContent(WeaponCategory weaponCategory)
    {
        switch (weaponCategory)
        {
            case WeaponCategory.ZodiacWeapons:
                DrawZodiacWeaponUI();
                break;
            case WeaponCategory.ItemOverview:
                DrawItemOverviewUI();
                break;
            default:
                break;
        }
    }

    private void DrawItemOverviewUI()
    {
        ImGui.BeginChild("scrolling", new Vector2(0, 400), true, ImGuiWindowFlags.HorizontalScrollbar);
        ImGui.PushStyleVar(ImGuiStyleVar.ItemSpacing, new Vector2(0, 5));

        foreach (var item in items)
        {
            ImGui.Text($"{item.Name} x{item.Quantity} of {item.NeededQuantity}");
        }
        ImGui.EndChild();

        if (ImGui.Button("Scan Inventory"))
        {
            items = plugin.ItemFinder.ScanPlayerInventory();
        }

        if (ImGui.Button("Scan retainers"))
        {
            selectedRetainer = ItemInventory.Retainer1;
            //RetainerScanActive = true;
            items = plugin.ItemFinder.ScanRetainer();
        }
    }

    private void DrawZodiacWeaponUI()
    {
        if (ImGui.BeginTable("zodiacJobs", 11))
        {
            ImGui.TableNextRow();
            WeaponJob[] zodiacWeaponJobs = EnumHelper.GetAttribute<WeaponCategoryJobsAttribute>(selectedWeaponCategory)?.Values ?? Array.Empty<WeaponJob>();
            for (int i = 0; i < zodiacWeaponJobs.Length; i++)
            {
                ImGui.TableSetColumnIndex(i);
                if (selectedJob == zodiacWeaponJobs[i])
                {
                    ImGui.PushStyleColor(ImGuiCol.Button, 0xFFFF0000);
                    if (ImGui.Button(zodiacWeaponJobs[i].ToString()))
                    {
                        selectedJob = zodiacWeaponJobs[i];
                    }
                    ImGui.PopStyleColor(1);
                }
                else
                {
                    if (ImGui.Button(zodiacWeaponJobs[i].ToString()))
                    {
                        selectedJob = zodiacWeaponJobs[i];
                    }
                }
            }
            ImGui.EndTable();
        }
        ImGui.Separator();
        ImGui.PushStyleVar(ImGuiStyleVar.ItemSpacing, new Vector2(0, 5));

        WeaponStep[] weaponSteps = EnumHelper.GetAttribute<WeaponCategoryStepsAttribute>(selectedWeaponCategory)?.Values ?? Array.Empty<WeaponStep>();
        
        if (ImGui.BeginChild("stepSelection", new Vector2(150,200), true, ImGuiWindowFlags.HorizontalScrollbar))
        {
            for (int i = 0; i < weaponSteps.Length; i++)
            {
                if (ImGui.Selectable(EnumHelper.GetAttribute<WeaponStepNameAttribute>(weaponSteps[i])?.Value ?? "Unknown", selectedStep == weaponSteps[i]))
                {
                    selectedStep = weaponSteps[i];
                }
            }
        }
        ImGui.EndChild();

        ImGui.SameLine();
        if (ImGui.BeginChild("subSteps", new Vector2(600, 400), true, ImGuiWindowFlags.HorizontalScrollbar))
        {
            WeaponSubStep[] subSteps = EnumHelper.GetAttribute<WeaponSubStepsAttribute>(selectedStep)?.Values ?? Array.Empty<WeaponSubStep>();

            for (int j = 0; j < subSteps.Length; j++)
            {
                bool stepCompleted = IsSubStepComplete(subSteps[j], selectedJob);
                if (stepCompleted)
                {
                    ImGui.PushStyleColor(ImGuiCol.Header, 0xFF1DB000);
                    ImGui.PushStyleColor(ImGuiCol.HeaderHovered, 0xFF1CD000);
                }
                
                WeaponSubStepItemsAttribute? attribute = EnumHelper.GetAttribute<WeaponSubStepItemsAttribute>(subSteps[j]);
                WeaponItem[] weaponItems = attribute?.Values ?? Array.Empty<WeaponItem>();
                int[] weaponItemsAmounts = attribute?.Amounts ?? Array.Empty<int>();

                if (weaponItems.Any())
                {
                    if (ImGui.CollapsingHeader($"{weaponItems.Length} required items"))
                    {
                        for (int k = 0; k < weaponItems.Length; k++)
                        {
                            ImGui.Text($"{plugin.GameDataFinder.GetItemName(EnumHelper.GetAttribute<WeaponItemIdAttribute>(weaponItems[k])?.Value ?? 0)} x{(weaponItemsAmounts.Length-1 >= k ? weaponItemsAmounts[k] : 0)}");
                        }

                        if (ImGui.Button(stepCompleted ? $"Mark Step {j} incomplete" : $"Mark Step {j} complete"))
                        {
                            if (stepCompleted)
                            {
                                plugin.Configuration.RemoveCompletedSubStep((uint)subSteps[j], (uint)selectedJob);
                            }
                            else
                            {
                                plugin.Configuration.AddCompletedSubStep((uint)subSteps[j], (uint)selectedJob);
                            }
                            plugin.Configuration.Save();
                        }
                        ImGui.PopStyleVar(1);

                    }
                }

                WeaponQuestSet weaponQuestSetEnum = EnumHelper.GetAttribute<WeaponSubStepQuestAttribute>(subSteps[j])?.Value ?? WeaponQuestSet.Unknown;
                WeaponQuestSetAttribute? weaponQuestSet = EnumHelper.GetAttribute<WeaponQuestSetAttribute>(weaponQuestSetEnum);
                WeaponQuest? weaponQuestEnum = GetWeaponQuest(weaponQuestSet, selectedJob);
                WeaponQuestAttribute? weaponQuest = EnumHelper.GetAttribute<WeaponQuestAttribute>(weaponQuestEnum ?? WeaponQuest.Unknown);

                if (weaponQuestSetEnum != WeaponQuestSet.Unknown && ImGui.CollapsingHeader($"{GetWeaponQuestName(weaponQuest)}"))
                {
                    ImGui.PushStyleVar(ImGuiStyleVar.ItemSpacing, new Vector2(5, 15));
                    uint? questId = GetQuestId(weaponQuest);
                    if (ImGui.Button("Open Quest (" + questId + ")"))
                    {
                        Quest? quest = GetQuest(weaponQuest);
                        if (quest != null)
                        {
                            if (questId != null)
                            {
                                commonBase.Functions.Journal.OpenQuest((uint)questId);
                            }
                        }
                    }
                    ImGui.SameLine();
                    if (ImGui.Button("Locate Quest (" + questId + ")"))
                    {
                        unsafe
                        {
                            Quest? quest = GetQuest(weaponQuest);
                            Level? questLocation = quest?.IssuerLocation.Value;
                            if (questLocation != null)
                            {
                                AgentMap* map = AgentMap.Instance();
                                map->SetFlagMapMarker(questLocation.Territory.Row, questLocation.Map.Row, new Vector3(questLocation.X, questLocation.Y, questLocation.Z));
                                map->OpenMapByMapId(questLocation.Map.Row);
                            }
                        }
                    }
                    ImGui.SameLine();
                    if (ImGui.Button(stepCompleted ? $"Mark Step {j} incomplete" : $"Mark Step {j} complete"))
                    {
                        uint? id = GetQuestId(weaponQuest);
                        if (id != null)
                        {
                            if (stepCompleted)
                            {
                                plugin.Configuration.CompletedQuestIds.Remove((uint)id);
                            }
                            else
                            {
                                plugin.Configuration.CompletedQuestIds.Add((uint)id);
                            }
                            plugin.Configuration.Save();
                        }
                    }
                    ImGui.PopStyleVar(1);
                    
                    if (weaponQuest != null)
                    {
                        for (int i = 0; i < weaponQuest?.Steps.Length; i++)
                        {
                            WeaponQuestLocationAttribute? weaponQuestLocation = EnumHelper.GetAttribute<WeaponQuestLocationAttribute>(weaponQuest.Steps[i]);
                            if (weaponQuestLocation != null)
                            {
                                ImGui.Text($"Step {i+1}: Go to location {weaponQuestLocation.Value}");
                                if (ImGui.Button("Open Map"))
                                {
                                    unsafe
                                    {
                                        Map? location = plugin.GameDataFinder.GetMap(weaponQuestLocation.Value);
                                        AgentMap* map = AgentMap.Instance();
                                        map->SetFlagMapMarker(location?.TerritoryType.Value?.RowId ?? 0, location?.RowId ?? 0, weaponQuestLocation.PositionX, weaponQuestLocation.PositionY);
                                        map->OpenMapByMapId(location?.RowId ?? 0);
                                    }
                                }
                            }

                            WeaponQuestItemsAttribute? weaponQuestItems = EnumHelper.GetAttribute<WeaponQuestItemsAttribute>(weaponQuest.Steps[i]);
                            if (weaponQuestItems != null)
                            {
                                ImGui.Text($"Step {i + 1}: Get the following items");
                                if (ImGui.CollapsingHeader($"{weaponQuestItems.Values.Length} required items"))
                                {
                                    for (int k = 0; k < weaponQuestItems.Values.Length; k++)
                                    {
                                        ImGui.Text($"{plugin.GameDataFinder.GetItemName(EnumHelper.GetAttribute<WeaponItemIdAttribute>(weaponQuestItems.Values[k])?.Value ?? 0)} x{(weaponQuestItems.Amounts.Length - 1 >= k ? weaponQuestItems.Amounts[k] : 0)}");
                                    }
                                }
                            }
                        }
                    }
                    
                }
                if (stepCompleted)
                {
                    ImGui.PopStyleColor(2);
                }
            }

            ImGui.Separator();

            if (ImGui.Button($"Set {EnumHelper.GetAttribute<WeaponStepNameAttribute>(selectedStep)?.Value ?? "Unknown"} complete"))
            {

            }
        }
        ImGui.EndChild();
    }

    private static WeaponQuest? GetWeaponQuest(WeaponQuestSetAttribute? weaponQuestSetAttribute, WeaponJob weaponJob)
    {
        if (weaponQuestSetAttribute == null)
        {
            return null;
        }
        return weaponQuestSetAttribute.QuestType switch
        {
            WeaponQuestType.OneTime => weaponQuestSetAttribute.Values[0],
            WeaponQuestType.JobSpecific => weaponQuestSetAttribute.Values[(int)weaponJob],
            _ => null
        };
    }

    private static uint? GetQuestId(WeaponQuestAttribute? weaponQuestAttribute)
    {
        if (weaponQuestAttribute == null)
        {
            return null;
        }
        return weaponQuestAttribute.Value;
    }

    private string GetWeaponQuestName(WeaponQuestAttribute? weaponQuestAttribute)
    {
        if (weaponQuestAttribute == null)
        {
            return string.Empty;
        }
        return plugin.QuestFinder.GetQuest(weaponQuestAttribute.Value)?.Name ?? string.Empty;
    }

    private Quest? GetQuest(WeaponQuestAttribute? weaponQuestAttribute)
    {
        if (weaponQuestAttribute == null)
        {
            return null;
        }
        return plugin.QuestFinder.GetRawQuest(weaponQuestAttribute.Value);
    }

    private bool IsSubStepComplete(WeaponSubStep subStep, WeaponJob job)
    {
        WeaponQuestSet? weaponQuestSetEnum = EnumHelper.GetAttribute<WeaponSubStepQuestAttribute>(subStep)?.Value;
        WeaponSubStepItemsAttribute? weaponSubStepItemsAttribute = EnumHelper.GetAttribute<WeaponSubStepItemsAttribute>(subStep);
        if (weaponQuestSetEnum != null) 
        {
            WeaponQuestSetAttribute? weaponQuestSet = EnumHelper.GetAttribute<WeaponQuestSetAttribute>(weaponQuestSetEnum);
            WeaponQuest? weaponQuestEnum = GetWeaponQuest(weaponQuestSet, selectedJob);
            WeaponQuestAttribute? weaponQuest = EnumHelper.GetAttribute<WeaponQuestAttribute>(weaponQuestEnum ?? WeaponQuest.Unknown);

            return plugin.Configuration.IsQuestCompleted(GetQuestId(weaponQuest));
        }
        else if (weaponSubStepItemsAttribute != null)
        {
            return plugin.Configuration.IsSubStepComplete((uint)subStep, (uint)job);
        }
        else
        {
            return false;
        }
    }
}

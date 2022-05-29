using Dalamud.Game.ClientState;
using ImGuiNET;
using Lumina.Excel.GeneratedSheets;
using RelicTrackerPlugin.Config;
using RelicTrackerPlugin.Core;
using RelicTrackerPlugin.Enums;
using RelicTrackerPlugin.Enums.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace RelicTrackerPlugin;

// It is good to have this be disposable in general, in case you ever need it
// to do any cleanup
class PluginUI : IDisposable
{
    private Configuration configuration;

    // this extra bool exists for ImGui, since you can't ref a property
    private bool visible = false;
    public bool Visible
    {
        get { return this.visible; }
        set { this.visible = value; }
    }

    private bool settingsVisible = false;
    public bool SettingsVisible
    {
        get { return this.settingsVisible; }
        set { this.settingsVisible = value; }
    }

    private readonly ItemFinder itemFinder;
    private readonly QuestFinder questFinder;
    private readonly ClientState clientState;

    private string[] inventoryIDs = Array.Empty<string>();
    private Models.Item[] items = Array.Empty<Models.Item>();

    private WeaponCategory selectedWeaponCategory;
    private WeaponJob selectedJob;

    public PluginUI(Configuration configuration, ItemFinder itemFinder, QuestFinder questFinder, ClientState clientState)
    {
        this.configuration = configuration;
        this.itemFinder = itemFinder;
        this.questFinder = questFinder;
        this.clientState = clientState;
        selectedJob = EnumHelper.GetWeaponJob(clientState.LocalPlayer?.ClassJob.GetWithLanguage(clientState.ClientLanguage));
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
    }

    public void DrawMainWindow()
    {
        if (!Visible)
        {
            return;
        }

        ImGui.SetNextWindowSize(new Vector2(375, 330), ImGuiCond.FirstUseEver);
        ImGui.SetNextWindowSizeConstraints(new Vector2(375, 330), new Vector2(float.MaxValue, float.MaxValue));
        if (ImGui.Begin("Relic Tracker Plugin", ref this.visible, ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse))
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

            WeaponJob currentJob = EnumHelper.GetWeaponJob(clientState.LocalPlayer?.ClassJob.GetWithLanguage(clientState.ClientLanguage));

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
            var configValue = this.configuration.SomePropertyToBeSavedAndWithADefault;
            if (ImGui.Checkbox("Random Config Bool", ref configValue))
            {
                this.configuration.SomePropertyToBeSavedAndWithADefault = configValue;
                // can save immediately on change, if you don't want to provide a "Save and Close" button
                this.configuration.Save();
            }
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

        if (ImGui.CollapsingHeader("Zodiac Weapon"))
        {
            ImGui.Text("Bla");
        }
        foreach (var item in items)
        {
            ImGui.Text($"{item.Name} x{item.Quantity}");
        }
        ImGui.EndChild();

        if (ImGui.Button("Scan Inventory"))
        {
            items = itemFinder.ScanInventory();
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
                if (ImGui.Button(zodiacWeaponJobs[i].ToString()))
                {
                    selectedJob = zodiacWeaponJobs[i];
                }
            }
            ImGui.EndTable();
        }
        ImGui.Separator();
        ImGui.BeginChild("scrolling", new Vector2(0, 400), true, ImGuiWindowFlags.HorizontalScrollbar);
        ImGui.PushStyleVar(ImGuiStyleVar.ItemSpacing, new Vector2(0, 5));

        WeaponStep[] weaponSteps = EnumHelper.GetAttribute<WeaponCategoryStepsAttribute>(selectedWeaponCategory)?.Values ?? Array.Empty<WeaponStep>();
        for (int i = 0; i < weaponSteps.Length; i++)
        {
            if (ImGui.CollapsingHeader($"Step {i + 1}: {EnumHelper.GetAttribute<WeaponStepNameAttribute>(weaponSteps[i])?.Value ?? "Unknown"}"))
            {
                ImGui.BeginChild("relicQuests", new Vector2(0, 200), true, ImGuiWindowFlags.None);

                WeaponQuest[] relicQuests = EnumHelper.GetAttribute<WeaponStepQuestsAttribute>(WeaponStep.Relic)?.Values ?? Array.Empty<WeaponQuest>();

                for (int j = 0; j < relicQuests.Length; j++)
                {
                    WeaponQuestAttribute? weaponQuest = EnumHelper.GetAttribute<WeaponQuestAttribute>(relicQuests[j]);
                    ImGui.PushStyleColor(ImGuiCol.Header, 0xFFFF0000);
                    if (ImGui.CollapsingHeader($"{GetWeaponQuest(weaponQuest, selectedJob)}"))
                    {

                    }
                    ImGui.PopStyleColor(1);
                }

                ImGui.EndChild();
            }
        }
        ImGui.EndChild();
    }

    private string GetWeaponQuest(WeaponQuestAttribute? weaponQuestAttribute, WeaponJob weaponJob)
    {
        if (weaponQuestAttribute == null)
        {
            return string.Empty;
        }
        return weaponQuestAttribute.QuestType switch
        {
            WeaponQuestType.OneTime => questFinder.GetQuest(weaponQuestAttribute.Values[0])?.Name ?? string.Empty,
            WeaponQuestType.JobSpecific => questFinder.GetQuest(weaponQuestAttribute.Values[(int)weaponJob])?.Name ?? string.Empty,
            _ => string.Empty
        };
    }
}

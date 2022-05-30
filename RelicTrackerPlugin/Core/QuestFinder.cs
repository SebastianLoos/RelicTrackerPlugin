using Dalamud.Data;
using Dalamud.Game.ClientState;
using RelicTrackerPlugin.Core.Scanners;
using RelicTrackerPlugin.Models;
using System.Linq;

namespace RelicTrackerPlugin.Core;
internal class QuestFinder
{
    private readonly GameDataFinder gameDataFinder;
    private readonly QuestScanner questScanner;

    public QuestFinder(DataManager dataManager, ClientState clientState)
    {
        gameDataFinder = new(dataManager, clientState.ClientLanguage);
        questScanner = new();
    }

    public Quest[] ScanQuest()
    {
        return questScanner.GetActiveQuest().Select(x => new Quest()
        {
            Id = x.QuestID,
            Name = gameDataFinder.GetQuestName(x.QuestID)
        }).ToArray();
    }

    public Quest GetQuest(uint id)
    {
        return new Quest()
        {
            Id = id,
            Name = gameDataFinder.GetQuestName(id)
        };
    }

    public Lumina.Excel.GeneratedSheets.Quest? GetRawQuest(uint id)
    {
        return gameDataFinder.GetQuest(id);
    }
}

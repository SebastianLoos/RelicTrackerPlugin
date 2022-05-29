using Dalamud;
using Dalamud.Data;
using Lumina.Excel.GeneratedSheets;
using System.Linq;

namespace RelicTrackerPlugin.Core;

internal class GameDataFinder
{
    private readonly ClientLanguage clientLanguage;
    private readonly DataManager dataManager;

    public GameDataFinder(DataManager dataManager, ClientLanguage clientLanguage)
    {
        this.dataManager = dataManager;
        this.clientLanguage = clientLanguage;
    }

    public string GetItemName(uint itemId)
    {
        return dataManager.GetExcelSheet<Item>(clientLanguage)?.FirstOrDefault(x => x.RowId == itemId)?.Name.RawString ?? string.Empty;
    }

    public string GetQuestName(uint questId)
    {
        return dataManager.GetExcelSheet<Quest>(clientLanguage)?.FirstOrDefault(x => x.RowId == questId)?.Name.RawString ?? string.Empty;
    }

    public Level? GetQuestLocation(uint questId)
    {
        return dataManager.GetExcelSheet<Quest>(clientLanguage)?.FirstOrDefault(x => x.RowId == questId)?.IssuerLocation.Value;
    }
}

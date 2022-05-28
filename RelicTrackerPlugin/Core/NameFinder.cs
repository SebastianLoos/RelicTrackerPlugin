using Dalamud;
using Dalamud.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelicTrackerPlugin.Core;

internal class NameFinder
{
    private readonly ClientLanguage clientLanguage;
    private readonly DataManager dataManager;

    public NameFinder(DataManager dataManager, ClientLanguage clientLanguage)
    {
        this.dataManager = dataManager;
        this.clientLanguage = clientLanguage;
    }

    public string GetItemName(uint itemId)
    {
        return dataManager.GetExcelSheet<Lumina.Excel.GeneratedSheets.Item>(clientLanguage)?.FirstOrDefault(x => x.RowId == itemId)?.Name.RawString ?? string.Empty;
    }
}

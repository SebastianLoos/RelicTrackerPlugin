using Dalamud.Data;
using Dalamud.Plugin.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AchievementSheet = Lumina.Excel.GeneratedSheets.Achievement;

namespace RelicTrackerPlugin.Core.Scanners;

internal class AchievementScanner
{
    private IDataManager dataManager;

    public AchievementScanner(IDataManager dataManager)
    {
        this.dataManager = dataManager;
    }
}

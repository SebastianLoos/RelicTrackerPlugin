using Dalamud.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AchievementSheet = Lumina.Excel.GeneratedSheets.Achievement;

namespace RelicTrackerPlugin.Core.Scanners;

internal class AchievementScanner
{
    private DataManager dataManager;

    public AchievementScanner(DataManager dataManager)
    {
        this.dataManager = dataManager;
    }
}

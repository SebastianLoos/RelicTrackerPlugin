using FFXIVClientStructs.FFXIV.Client.Game;
using System;
using System.Linq;
using static FFXIVClientStructs.FFXIV.Client.Game.QuestManager;
using static FFXIVClientStructs.FFXIV.Client.Game.QuestManager.QuestListArray;

namespace RelicTrackerPlugin.Core.Scanners;

internal unsafe class QuestScanner : IDisposable
{
    private const byte questLogSize = 30;

    private readonly QuestManager* questManager;

    public QuestScanner()
    {
        questManager = Instance();
    }

    public Quest[] GetActiveQuest()
    {
        QuestListArray questsArray = questManager->Quest;

        Quest[] quests = new Quest[questLogSize];

        for(int i = 0; i<questLogSize; i++)
        {
            quests[i] = *questsArray[i];
        }

        return quests.ToArray();
    }

    public void Dispose()
    {
    }
}

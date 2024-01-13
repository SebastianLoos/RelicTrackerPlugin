using FFXIVClientStructs.FFXIV.Application.Network.WorkDefinitions;
using FFXIVClientStructs.FFXIV.Client.Game;
using RelicTrackerPlugin.Models;
using System;
using System.Linq;
using static FFXIVClientStructs.FFXIV.Client.Game.QuestManager;

namespace RelicTrackerPlugin.Core.Scanners;

internal unsafe class QuestScanner : IDisposable
{
    private const byte questLogSize = 30;

    private readonly QuestManager* questManager;

    public QuestScanner()
    {
        questManager = Instance();
    }

    public ushort[] GetActiveQuest()
    {
        int questLogLength = questManager->NormalQuestsSpan.Length;

        ushort[] quests = new ushort[questLogSize];

        for(int i = 0; i<questLogLength; i++)
        {
            QuestWork qw = questManager->NormalQuestsSpan[i];
            quests[i] = qw.QuestId;
        }

        return quests.ToArray();
    }

    public void Dispose()
    {
    }
}

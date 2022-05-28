using FFXIVClientStructs.FFXIV.Client.Game;
using System;
using System.Collections.Generic;

namespace RelicTrackerPlugin.Core.Scanners;

internal unsafe class InventoryItemScanner : IDisposable
{
    private const byte inventorySize = 35;

    private readonly InventoryManager* inventoryManager;

    public InventoryItemScanner()
    {
        inventoryManager = InventoryManager.Instance();
    }

    public uint[] GetItemIds(InventoryType inventoryType)
    {
        InventoryContainer* inventory1Container = inventoryManager->GetInventoryContainer(inventoryType);
        InventoryItem* inventoryitem = inventory1Container->Items;

        uint[] itemIds = new uint[inventorySize];

        for (int i = 0; i < inventorySize; i++)
        {
            itemIds[i] = inventoryitem->ItemID;
            inventoryitem++;
        }

        return itemIds;
    }

    public void Dispose()
    {
    }
}

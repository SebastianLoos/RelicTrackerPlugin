using Dalamud.Data;
using Dalamud.Game.ClientState;
using FFXIVClientStructs.FFXIV.Client.Game;
using RelicTrackerPlugin.Core.Scanners;
using RelicTrackerPlugin.Models;
using System.Collections.Generic;
using System.Linq;

namespace RelicTrackerPlugin.Core;
internal class ItemFinder
{
    private readonly NameFinder nameFinder;
    private readonly InventoryItemScanner inventoryItemScanner;

    public ItemFinder(DataManager dataManager, ClientState clientState)
    {
        nameFinder = new(dataManager, clientState.ClientLanguage);
        inventoryItemScanner = new();
    }

    public Item[] ScanInventory()
    {
        InventoryType[] invetoriesToScan = new InventoryType[] {InventoryType.Inventory1, InventoryType.Inventory2, InventoryType.Inventory3,
            InventoryType.Inventory4, InventoryType.Currency, InventoryType.SaddleBag1, InventoryType.SaddleBag2};

        List<Item> items = new();

        foreach (InventoryType inventory in invetoriesToScan)
        {
            items.AddRange(inventoryItemScanner.GetItemIds(inventory).Select(x => new Item()
            {
                Id = x
            }));
        }

        return items.ToArray();
    }
}

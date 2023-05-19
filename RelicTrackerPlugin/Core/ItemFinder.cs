using Dalamud.Data;
using Dalamud.Game.ClientState;
using FFXIVClientStructs.FFXIV.Client.Game;
using RelicTrackerPlugin.Core.Scanners;
using RelicTrackerPlugin.Enums;
using RelicTrackerPlugin.Enums.Attributes;
using RelicTrackerPlugin.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RelicTrackerPlugin.Core;
internal class ItemFinder
{
    private readonly GameDataFinder nameFinder;
    private readonly InventoryItemScanner inventoryItemScanner;

    private readonly InventoryType[] playerInventory = new InventoryType[] {InventoryType.Inventory1, InventoryType.Inventory2, InventoryType.Inventory3,
            InventoryType.Inventory4, InventoryType.Currency };

    private readonly InventoryType[] retainerInventory = new InventoryType[] { InventoryType.RetainerPage1, InventoryType.RetainerPage2, InventoryType.RetainerPage3, 
        InventoryType.RetainerPage4, InventoryType.RetainerPage5, InventoryType.RetainerPage6, InventoryType.RetainerPage7 };

    private readonly InventoryType[] saddleBags = new InventoryType[] { InventoryType.SaddleBag1, InventoryType.SaddleBag2 };

    private readonly uint[] weaponItemIds = EnumHelper.GetAllValues<WeaponItem>().Select(x => EnumHelper.GetAttribute<WeaponItemIdAttribute>(x)?.Value ?? 0).ToArray();

    private readonly Tuple<uint, int>[] weaponItemQuantities = EnumHelper.GetWeaponItemNeededQuantities();

    public ItemFinder(DataManager dataManager, ClientState clientState)
    {
        nameFinder = new(dataManager, clientState.ClientLanguage);
        inventoryItemScanner = new();
    }

    public Item[] ScanPlayerInventory()
    {
        return ScanInventory(playerInventory);
    }

    public Item[] ScanRetainer()
    {
        return ScanInventory(retainerInventory);
    }

    private Item[] ScanInventory(InventoryType[] inventoryTypes)
    {
        List<Item> items = new();

        foreach (InventoryType inventory in inventoryTypes)
        {
            items.AddRange(inventoryItemScanner.GetItems(inventory).Where(x => weaponItemIds.Contains(x.ItemID)).GroupBy(x => x.ItemID).Select(x => new Item()
            {
                Id = x.Key,
                InventoryType = inventory,
                Name = nameFinder.GetItemName(x.Key),
                Quantity = x.Sum(y => y.Quantity),
                NeededQuantity = weaponItemQuantities.FirstOrDefault(x => )
            }));
        }

        return items.ToArray();
    }
}

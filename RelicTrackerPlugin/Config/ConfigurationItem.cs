using FFXIVClientStructs.FFXIV.Client.Game;
using RelicTrackerPlugin.Enums;

namespace RelicTrackerPlugin.Config;
public class ConfigurationItem
{
    public uint Id { get; set; }

    public InventoryType InventoryType { get; set; }

    public ItemInventory Inventory { get; set; }

    public short Amount { get; set; }
}

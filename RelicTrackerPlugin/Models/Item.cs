using FFXIVClientStructs.FFXIV.Client.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelicTrackerPlugin.Models;
internal class Item
{
    public uint Id { get; set; }

    public InventoryType inventoryType { get; set; }
}

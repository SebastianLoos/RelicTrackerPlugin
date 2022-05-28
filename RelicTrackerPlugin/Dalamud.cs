using Dalamud.Data;
using Dalamud.Game.ClientState;
using Dalamud.Game.Command;

namespace RelicTrackerPlugin;
public class Dalamud
{
    public CommandManager CommandManager { get; set; }

    public DataManager DataManager { get; set; }
    public ClientState ClientState { get; set; }
}

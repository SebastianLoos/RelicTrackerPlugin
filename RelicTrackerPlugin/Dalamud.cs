using Dalamud.Plugin.Services;

namespace RelicTrackerPlugin;
public class Dalamud
{
    public ICommandManager CommandManager { get; set; }

    public IDataManager DataManager { get; set; }
    public IClientState ClientState { get; set; }
}

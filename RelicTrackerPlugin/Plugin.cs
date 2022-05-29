using Dalamud.Data;
using Dalamud.Game.ClientState;
using Dalamud.Game.Command;
using Dalamud.IoC;
using Dalamud.Plugin;
using RelicTrackerPlugin.Config;
using RelicTrackerPlugin.Core;

namespace RelicTrackerPlugin;

public sealed class Plugin : IDalamudPlugin
{
    public string Name => "Relic Tracker Plugin";

    private const string commandName = "/relictracker";

    private DalamudPluginInterface PluginInterface { get; init; }
    private CommandManager CommandManager { get; init; }
    private Configuration Configuration { get; init; }
    private DataManager DataManager { get; init; }
    private ClientState ClientState { get; init; }
    private PluginUI PluginUi { get; init; }

    private ItemFinder ItemFinder { get; init; }
    private QuestFinder QuestFinder { get; init; }

    public Plugin(
        [RequiredVersion("1.0")] DalamudPluginInterface pluginInterface,
        [RequiredVersion("1.0")] CommandManager commandManager,
        [RequiredVersion("1.0")] DataManager dataManager,
        [RequiredVersion("1.0")] ClientState clientState)
    {
        PluginInterface = pluginInterface;
        CommandManager = commandManager;
        DataManager = dataManager;
        ClientState = clientState;

        ItemFinder = new ItemFinder(dataManager, clientState);
        QuestFinder = new QuestFinder(dataManager, clientState);

        Configuration = PluginInterface.GetPluginConfig() as Configuration ?? new Configuration();
        Configuration.Initialize(PluginInterface);

        PluginUi = new PluginUI(Configuration, ItemFinder, QuestFinder, ClientState);

        CommandManager.AddHandler(commandName, new CommandInfo(OnCommand)
        {
            HelpMessage = "A useful message to display in /xlhelp"
        });

        PluginInterface.UiBuilder.Draw += DrawUI;
        PluginInterface.UiBuilder.OpenConfigUi += DrawConfigUI;
        
    }

    public void Dispose()
    {
        PluginUi.Dispose();
        CommandManager.RemoveHandler(commandName);
    }

    private void OnCommand(string command, string args)
    {
        // in response to the slash command, just display our main ui
        PluginUi.Visible = true;
    }

    private void DrawUI()
    {
        PluginUi.Draw();
    }

    private void DrawConfigUI()
    {
        PluginUi.SettingsVisible = true;
    }
}

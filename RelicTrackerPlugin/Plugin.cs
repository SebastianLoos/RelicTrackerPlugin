using Dalamud.Data;
using Dalamud.Game.ClientState;
using Dalamud.Game.Command;
using Dalamud.Game.Gui;
using Dalamud.IoC;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;
using RelicTrackerPlugin.Config;
using RelicTrackerPlugin.Core;

namespace RelicTrackerPlugin;

public sealed class Plugin : IDalamudPlugin
{
    public string Name => "Relic Tracker Plugin";

    private const string commandName = "/relictracker";

    private DalamudPluginInterface PluginInterface { get; init; }
    private ICommandManager CommandManager { get; init; }

    public Configuration Configuration { get; init; }
    public IDataManager DataManager { get; init; }
    public IClientState ClientState { get; init; }
    private PluginUI PluginUi { get; init; }

    public IChatGui ChatGui { get; init; }

    internal GameDataFinder GameDataFinder { get; init; }
    internal ItemFinder ItemFinder { get; init; }
    internal QuestFinder QuestFinder { get; init; }

    public Plugin(
        [RequiredVersion("1.0")] DalamudPluginInterface pluginInterface,
        [RequiredVersion("1.0")] ICommandManager commandManager,
        [RequiredVersion("1.0")] IDataManager dataManager,
        [RequiredVersion("1.0")] IChatGui chatGui,
        [RequiredVersion("1.0")] IClientState clientState)
    {
        PluginInterface = pluginInterface;
        CommandManager = commandManager;
        DataManager = dataManager;
        ClientState = clientState;
        ChatGui = chatGui;

        ItemFinder = new ItemFinder(dataManager, clientState);
        QuestFinder = new QuestFinder(dataManager, clientState);
        GameDataFinder = new GameDataFinder(dataManager, clientState.ClientLanguage);

        Configuration = PluginInterface.GetPluginConfig() as Configuration ?? new Configuration();
        Configuration.Initialize(PluginInterface);

        PluginUi = new PluginUI(this, pluginInterface);

        CommandManager.AddHandler(commandName, new CommandInfo(OnCommand)
        {
            HelpMessage = "Test!"
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

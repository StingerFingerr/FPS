using UI;
using UI.Warning_panel;
using UnityEngine;
using Zenject;

public class MainMenuUiInstaller: MonoInstaller
{
    public GameObject mainMenuPrefab;
    public GameObject warningPanelPrefab;
    
    public override void InstallBindings()
    {
        BindMainMenu();
        BindWarningPanel();
    }

    private void BindMainMenu() => 
        Container.Bind<MainMenu>().FromComponentInNewPrefab(mainMenuPrefab).AsSingle().NonLazy();

    private void BindWarningPanel() => 
        Container.Bind<IWarningPanel>().FromComponentInNewPrefab(warningPanelPrefab).AsSingle();
}
using UI;
using UI.Game;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "SoServicesInstaller", menuName = "Installers/GlobalSoServicesInstaller")]
public class ScriptableObjectServicesInstaller : ScriptableObjectInstaller<ScriptableObjectServicesInstaller>
{
    public PrefabService prefabService;
    public InventoryIcons inventoryIcons;

    public override void InstallBindings()
    {
        BindPrefabService();

        BindInventoryIcons();
    }

    private void BindInventoryIcons() => 
        Container.Bind<IInventoryIcons>().FromInstance(inventoryIcons);

    private void BindPrefabService() => 
        Container.Bind<IPrefabService>().FromInstance(prefabService);

}
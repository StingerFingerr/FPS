using UI;
using UI.Game;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "SoServicesInstaller", menuName = "Installers/GlobalSoServicesInstaller")]
public class ScriptableObjectServicesInstaller : ScriptableObjectInstaller<ScriptableObjectServicesInstaller>
{
    public PrefabService prefabService;
    public InventoryIcons inventoryIcons;
    public EnemyArmorsProvider enemyArmors;
    public InventoryInitialState inventoryInitialState;

    public override void InstallBindings()
    {
        BindPrefabService();
        BindInventoryIcons();
        BindEnemyArmorsProvider();
        BindInventoryInitialState();
    }

    private void BindInventoryInitialState() => 
        Container.Bind<InventoryInitialState>().FromInstance(inventoryInitialState).AsSingle();

    private void BindEnemyArmorsProvider() => 
        Container.Bind<EnemyArmorsProvider>().FromInstance(enemyArmors).AsSingle();

    private void BindInventoryIcons() => 
        Container.Bind<IInventoryIcons>().FromInstance(inventoryIcons);

    private void BindPrefabService() => 
        Container.Bind<IPrefabService>().FromInstance(prefabService);

}
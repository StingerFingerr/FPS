using Infrastructure;
using Prefab_service;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "PrefabServiceInstaller", menuName = "Installers/PrefabServiceInstaller")]
public class PrefabServiceInstaller : ScriptableObjectInstaller<PrefabServiceInstaller>
{
    public PrefabService prefabService;

    public override void InstallBindings()
    {
        BindPrefabService();
    }

    private void BindPrefabService() => 
        Container.Bind<IPrefabService>().FromInstance(prefabService);
}
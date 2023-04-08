using System.Collections.Generic;
using Infrastructure;
using Zenject;

public class CrosshairCachedFactory: IFactory<CrosshairType, DynamicCrosshairBase>
{
    private Dictionary<CrosshairType, DynamicCrosshairBase> _crosshairs = new();

    private DiContainer _container;
    private IPrefabService _prefabService;
    
    
    public CrosshairCachedFactory (
        DiContainer container,
        IPrefabService prefabService)
    {
        _container = container;
        _prefabService = prefabService;
    }
    
    public DynamicCrosshairBase Create(CrosshairType type)
    {

        if (_crosshairs.TryGetValue(type, out DynamicCrosshairBase crosshair))
            return crosshair;
        else
        {
            crosshair = _container.InstantiatePrefabForComponent<DynamicCrosshairBase>(
                _prefabService.GetCrosshairPrefabByType(type));

            _crosshairs.Add(type, crosshair);
            
            return crosshair;
        }
    }
}
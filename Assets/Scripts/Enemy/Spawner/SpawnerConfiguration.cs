using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Create spawner configuration", fileName = "Enemy spawner configuration")]
public class SpawnerConfiguration: ScriptableObject
{
    public uint zombieAmount = 10;
    public uint maxAmountAtTime = 5;
    
    public bool hasBoos;
    public uint spawnBossAfterKills = 7;
    public byte bossWeight = 5;
    
    private void OnValidate()
    {
        if (maxAmountAtTime > zombieAmount)
            maxAmountAtTime = zombieAmount;
        if (spawnBossAfterKills > zombieAmount)
            spawnBossAfterKills = zombieAmount;
    }
}
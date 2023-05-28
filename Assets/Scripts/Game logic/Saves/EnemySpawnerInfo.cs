using System;

namespace Game_logic
{
    [Serializable]
    public class EnemySpawnerInfo
    {
        public int enemiesToSpawn;
        public int killedEnemies;
        public bool bossIsKilled;
        public bool stopSpawn;
        public bool cleared;
    }
}
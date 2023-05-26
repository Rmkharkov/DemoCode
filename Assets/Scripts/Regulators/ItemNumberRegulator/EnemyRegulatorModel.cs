namespace Regulators.ItemNumberRegulator
{
    [System.Serializable]
    public class EnemyRegulatorModel : BaseModel
    {
        public int MaxEnemiesNumber = 5;
        public int MaxMinesNumber = 5;
        public float EnemiesSpawnDelayInSec = 10f;
        public float MinesSpawnDelayInSec = 6f;
        public float MinimalSpawnDistance = 1f;
    }
}
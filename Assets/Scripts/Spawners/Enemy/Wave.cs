public struct Wave
{
    public Wave(int enemyAmount, float spawnDelay)
    {
        EnemyAmount = enemyAmount;
        SpawnDelay = spawnDelay;
    }

    public int EnemyAmount { get; private set; }
    public float SpawnDelay { get; private set; }
}
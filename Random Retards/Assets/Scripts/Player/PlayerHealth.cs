using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Destrucable
{
    [SerializeField] SpawnPoint[] spawnPoints;

    void spawnAtNewSpawnPoint()
    {
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        transform.position = spawnPoints[spawnIndex].transform.position;
        transform.rotation = spawnPoints[spawnIndex].transform.rotation;
    }
    public override void Die()
    {
        base.Die();
        spawnAtNewSpawnPoint();
    }

    [ContextMenu("Test Die!")]
    void TestDie()
    {
        Die();
    }

}

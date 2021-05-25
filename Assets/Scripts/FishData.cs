using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Scriptable Objects/Fish", order = 1)]
public class FishData : ScriptableObject {
    public float minSpawnDepth;
    public float depthSpawnMultiplier; // x% more spawn every 100m after minSpawnDepth
    public float burstChance; // x% chance to spawn an extra amount between min and max burst spawn
    public int minBurstSpawn, maxBurstSpawn;
    public GameObject prefab;
    public int score;
}

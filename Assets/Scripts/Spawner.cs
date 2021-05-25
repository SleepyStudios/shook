using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Spawner : MonoBehaviour {
    public List<FishData> fish = new List<FishData>();
    public float spawnInterval = 2f;
    public float spawnAheadDistance = 100f;
    public float randomYOffset = 10f;

    float tmrSpawn;

    void Update() {
        tmrSpawn += Time.deltaTime;
        if (tmrSpawn >= spawnInterval) {
            Spawn();
            tmrSpawn = 0;
        }
    }

    void Spawn() {
        float xOffset = 50f;

        Rope r = FindObjectOfType<Rope>();
        List<FishData> spawnables = fish.Where((f) => r.transform.position.y <= -f.minSpawnDepth).ToList();
        foreach (FishData f in spawnables) {
            bool burst = Random.value <= f.burstChance;
            int burstAmount = burst ? Random.Range(f.minBurstSpawn, f.maxBurstSpawn + 1) : 0;

            for (int i = 0; i < burstAmount + 1; i++) {
                float spawnX = Random.Range(
                    Camera.main.ScreenToWorldPoint(new Vector2(xOffset, 0)).x,
                    Camera.main.ScreenToWorldPoint(new Vector2(Screen.width - xOffset, 0)).x
                );
                Vector2 pos = new Vector2(spawnX, r.transform.position.y - spawnAheadDistance + Random.Range(-randomYOffset, randomYOffset));

                StartCoroutine(DelayedInstantiate(f.prefab, pos, f));
            } 
        }
    }

    IEnumerator DelayedInstantiate(GameObject prefab, Vector2 pos, FishData data) {
        yield return new WaitForSeconds(Random.Range(0f, 1f));
        GameObject fish = Instantiate(prefab, pos, Quaternion.identity);
        fish.GetComponent<FishBase>().data = data;
        yield return null;
    }
}

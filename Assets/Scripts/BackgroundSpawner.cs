using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BackgroundSpawner : MonoBehaviour
{
    public List<GameObject> objects = new List<GameObject>();
    public List<float> weights = new List<float>();

    public float spawnInterval = 2f;
    public float spawnAheadDistance = 100f;
    public float randomYOffset = 30f;
    public float minScale = 0.5f, maxScale = 1f;
    public float minAlpha = 0.1f, maxAlpha = 0.6f;

    float tmrSpawn;

    void Update()
    {
        tmrSpawn += Time.deltaTime;
        if (tmrSpawn >= spawnInterval) {
            Spawn();
            tmrSpawn = 0;
        }
    }

    void Spawn() {
        float xOffset = 10f;

        List<float> weightedSpawns = weights.Select((o) => o * Random.value).ToList();
        int highestIdx = weightedSpawns.IndexOf(weightedSpawns.Max());

        GameObject o = objects[highestIdx];
        Rope r = FindObjectOfType<Rope>();

        float spawnX = Random.Range(
            Camera.main.ScreenToWorldPoint(new Vector2(xOffset, 0)).x,
            Camera.main.ScreenToWorldPoint(new Vector2(Screen.width - xOffset, 0)).x
        );
        Vector2 pos = new Vector2(spawnX, r.transform.position.y - spawnAheadDistance + Random.Range(-randomYOffset, randomYOffset));

        StartCoroutine(DelayedInstantiate(o, pos));
    }

    IEnumerator DelayedInstantiate(GameObject obj, Vector2 pos) {
        yield return new WaitForSeconds(Random.Range(5f, 10f));
        GameObject go = Instantiate(obj, pos, Quaternion.identity);
        go.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, Random.Range(minAlpha, maxAlpha));
        float randomScale = Random.Range(minScale, maxScale);
        go.transform.localScale = new Vector2(randomScale, randomScale);
        yield return null;
    }
}

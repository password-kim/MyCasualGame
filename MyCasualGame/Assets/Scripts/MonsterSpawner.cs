using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public Transform centerTransform = null;
    public GameObject monsterPrefab = null;
    public float distance = 10.0f;
    public Vector3 randomDir = Vector3.zero;

    public float spawnDelay = 1.0f;
    public float lastSwpanTime = 0.0f;

    public List<GameObject> spawnMonsterList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            centerTransform = player.transform;
        }

        lastSwpanTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > lastSwpanTime + spawnDelay && spawnMonsterList.Count < 100)
        {
            lastSwpanTime = Time.time;

            randomDir = new Vector3(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 0.0f);
            Vector3 spawnPoint = (randomDir * distance) + centerTransform.position;
            GameObject newMonster = Instantiate(monsterPrefab, spawnPoint, Quaternion.identity);
            spawnMonsterList.Add(newMonster);
            spawnDelay = Random.Range(1.0f, 5.0f);
        }
    }
}

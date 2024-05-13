using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public float destrucTime = 0.5f;
    [Range(0f, 1f)]
    public float itemSpawnChance = 0.2f;
    public GameObject[] spawnItem;

    void Start()
    {
        Destroy(gameObject, destrucTime);
    }

    private void OnDestroy()
    {
        if (spawnItem.Length > 0 && Random.value < itemSpawnChance)
        {
            int randomIndex = Random.Range(0, spawnItem.Length);
            Instantiate(spawnItem[randomIndex], transform.position, Quaternion.identity);
        }
    }

}

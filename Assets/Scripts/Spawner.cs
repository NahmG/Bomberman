using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Spawner : MonoBehaviour
{
    public int squidNumber = 0;
    public GameObject squidPrefab;
    public List<Vector2> squidPositions = new()
    {
        new(5,3), new(3,7), new(9,9), new(11,5)
    }; 


    private void Start()
    {
        SpawnBot(squidPrefab, squidPositions[0]);
        SpawnBot(squidPrefab, squidPositions[1]);
        SpawnBot(squidPrefab, squidPositions[2]);
        SpawnBot(squidPrefab, squidPositions[3]);
    }

    private void SpawnBot(GameObject botPrefab, Vector2 position)
    {
        Instantiate(botPrefab, position, Quaternion.identity);

        squidNumber++;
    }
}

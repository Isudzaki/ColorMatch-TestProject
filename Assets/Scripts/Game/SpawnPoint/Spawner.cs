using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    #region Variables
    // Array to store all figures
    [SerializeField]
    private GameObject[] figures;

    // Array to store all spawnPoints
    [SerializeField]
    private Transform[] spawnPoints;

    // Variables to crate timer for spawnew
    [SerializeField]
    private float timeBetweenSpawn;

    private float nextSpawnTime;
    #endregion

    #region MonoBehaviourFunctions
    private void Update()
    {
        // Function start
        RandomSpawn();
    }
    #endregion

    #region Functions
    //Create a function for Random spawn
    private void RandomSpawn()
    {
        if (Time.time > nextSpawnTime)
        {
            Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject randomFigures = figures[Random.Range(0, figures.Length)];

            Instantiate(randomFigures, randomSpawnPoint.position, randomSpawnPoint.rotation);
            nextSpawnTime = Time.time + timeBetweenSpawn;
        }
    }
    #endregion
}

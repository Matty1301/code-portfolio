using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.LEGO.Behaviours.Triggers;   //For PickupTrigger type

public class MinionSpawnManager : MonoBehaviour
{
    public int minionsToSpawn;

    public GameObject minionPrefab;
    public PickupTrigger loseTrigger;

    private List<Transform> spawnPoints = new List<Transform>();
    private GameObject[] aliveMinions;

    void Start()
    {
        //Populate spawnPoints list with the transforms of the spawn point game objects
        spawnPoints.AddRange(GetComponentsInChildren<Transform>());

        //Remove the transform of the parent game object from the list
        spawnPoints.Remove(transform);

        aliveMinions = new GameObject[minionsToSpawn];
    }

    private void Update()
    {
        CheckAliveMinions();
    }

    //For each element in aliveMinions check if it is empty. If yes then spawn a new minion
    void CheckAliveMinions()
    {
        for (int i = 0; i < minionsToSpawn; i++)
        {
            if (aliveMinions[i] == null)
            {
                aliveMinions[i] = SpawnMinion();
            }
        }
    }

    GameObject SpawnMinion()
    {
        //If there are no available spawn points you lose the game
        if (spawnPoints.Count <= 0)
            loseTrigger.Progress = 1;

        int rand = Random.Range(0, spawnPoints.Count);  // Select a random spawn point
        GameObject minionSpawned = Instantiate(minionPrefab, spawnPoints[rand]);
        spawnPoints.Remove(spawnPoints[rand]);  //Remove the selected spawn point from the list of available spawn points
        return minionSpawned;
    }

}

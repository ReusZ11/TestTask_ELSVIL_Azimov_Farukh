using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawn : MonoBehaviour
{
    [SerializeField] private ObjectPool objectPool;

    [Header("Cube Spawn Postions")]
    [SerializeField] private Transform[] blueCubeSpawnPositions;
    [SerializeField] private Transform[] redCubeSpawnPosition;


    private List<GameObject> activesCubes = new List<GameObject>();


    private void Start()
    {
        SpawnAllCubes();
    }

    void SpawnAllCubes()
    {


        foreach (Transform blueCubePostion in blueCubeSpawnPositions)
        {
            SpawnBlueCubes(blueCubePostion);
        }

        foreach(Transform redCubePostion in redCubeSpawnPosition)
        {
            SpawnRedCubes(redCubePostion);
        }
    }


    public void ReturnAllToPool()
    {
        foreach(GameObject cubes in activesCubes)
        {
            cubes.SetActive(false);
        }
        activesCubes.Clear();
    }


    public GameObject SpawnBlueCubes(Transform postition)
    {
        GameObject blueCube = objectPool.GetBlueCube();
        blueCube.transform.position = postition.transform.position;

        CubeDeathSystem deathSystem = blueCube.GetComponent<CubeDeathSystem>();
        if(deathSystem != null )
        {
            deathSystem.cubeType = CubeType.Blue;
        }

        blueCube.SetActive(true);
        activesCubes.Add(blueCube);
        return blueCube;
    }

    public GameObject SpawnRedCubes(Transform postition)
    {
        GameObject redCube = objectPool.GetRedCube();
        redCube.transform.position = postition.transform.position;

        CubeDeathSystem deathSystem = redCube.GetComponent<CubeDeathSystem>();
        if (deathSystem != null)
        {
            deathSystem.cubeType = CubeType.Red;
        }

        redCube.SetActive(true);
        activesCubes.Add(redCube);
        return redCube;
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private Transform poolParent;
    [Header("Cube Prefabs")]
    [SerializeField] private GameObject redCubePrefab;
    [SerializeField] private GameObject blueCubePrefab;

    [Header("Initional Quantity Prefabs")]
    [SerializeField] private int initialRedCubes = 10;
    [SerializeField] private int initialBlueCubes = 10;

    private List<GameObject> redCubesPool = new List<GameObject>();
    private List<GameObject> blueCubesPool = new List<GameObject>();


    private void Awake()
    {

        for(int i = 0; i < initialRedCubes; i++)
        {
            GameObject redCube = Instantiate(redCubePrefab, poolParent);
            redCube.SetActive(false);
            redCubesPool.Add(redCube);
        }

        for(int i = 0;i < initialBlueCubes; i++)
        {
            GameObject blueCube = Instantiate (blueCubePrefab, poolParent);
            blueCube.SetActive(false);
            blueCubesPool.Add(blueCube);
        }
    }

    public GameObject GetRedCube()
    {
        foreach(GameObject redCube in redCubesPool)
        {
            if(!redCube.activeInHierarchy) return redCube;
        }

        GameObject newRedCube = Instantiate(redCubePrefab);
        newRedCube.SetActive(false);
        redCubesPool.Add(newRedCube);
        return newRedCube;
    }

    public GameObject GetBlueCube()
    {
        foreach (GameObject blueCube in blueCubesPool)
        {
            if (!blueCube.activeInHierarchy) return blueCube;
        }

        GameObject newBlueCube = Instantiate(blueCubePrefab);
        newBlueCube.SetActive(false);
        blueCubesPool.Add(newBlueCube);
        return newBlueCube;
    }
}

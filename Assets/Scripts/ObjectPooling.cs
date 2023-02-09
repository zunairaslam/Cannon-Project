using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling instance;
    public List<GameObject> pooledCube;
    public GameObject cubeToPool;
    public int amountTopool;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        pooledCube = new List<GameObject>();
        for (int i = 0; i < amountTopool; i++)
        {
            AddObjectToPool();

        }
    }

    public GameObject AddObjectToPool()
    {
        GameObject cube = (GameObject)Instantiate(cubeToPool);
        cube.SetActive(false);
        pooledCube.Add(cube);

        return cube;
    }


    public GameObject GetObjectFromPool()
    {
        for (int i = 0; i < pooledCube.Count; i++)
        {

            if (!pooledCube[i].activeInHierarchy)
            {
                return pooledCube[i];
            }

        }

        return AddObjectToPool();


    }
}

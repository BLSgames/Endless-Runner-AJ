using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public GameObject[] pooledObject;
    
    public static ObjectPooler instance;

    private List<GameObject>pooledObjectList;
    private float spawnZ ;
    private float platform_Width = 40f;
    [SerializeField] private Transform playerTransform;
    private int amnTilesOnScreen = 5;
    private int lastPrefabIndex =0;
    private void Awake() {
        instance = this;
    }

    void Start()
    {
        spawnZ = 40f;
        pooledObjectList = new List<GameObject>();

        for( int i=0; i<pooledObject.Length; i++)
        {
            GameObject obj= (GameObject)Instantiate(pooledObject[i]);
            obj.SetActive(false);
            pooledObjectList.Add(obj);
        }

    }

    private void Update() {
        if(playerTransform.position.z - 50f > (spawnZ - amnTilesOnScreen*platform_Width))
        {
            GameObject newPlatform = GetPooledObject();
            newPlatform.transform.position = new Vector3(0, 0, spawnZ);
            newPlatform.SetActive(true);
            spawnZ += platform_Width;            
        }
    }

    public GameObject GetPooledObject () 
    {
        
        int a = RandomPrefabIndex();
       
        if(!pooledObjectList[a].activeInHierarchy)
        {
            return pooledObjectList[a];
        }
        else
        {
            for(int i=0; i<pooledObjectList.Count; i++)
            {
                if(!pooledObjectList[i].activeInHierarchy)
                {
                    return pooledObjectList[i];
                }
            }

            GameObject obj= (GameObject)Instantiate(pooledObject[1]);
            obj.SetActive(false);
            pooledObjectList.Add(obj);
            return obj;
        }

        
    }
    private int RandomPrefabIndex()
    {
        if(pooledObjectList.Count <= 1)
            return 0;

        int randomIndex = lastPrefabIndex;
        while(randomIndex == lastPrefabIndex)
        {
            randomIndex = Random.Range(0, pooledObjectList.Count);
        }

        lastPrefabIndex = randomIndex;
        return randomIndex;
    }  
}

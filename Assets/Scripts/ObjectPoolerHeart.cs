using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolerHeart : MonoBehaviour
{
    public GameObject pooledObject;
    private int pooledAmount = 2;
    private List<GameObject>pooledObjectList;
    private float spawnZ;
    [SerializeField] private Transform playerTransform;

    public static ObjectPoolerHeart instance;
    private void Awake() {
        instance = this;
    }
    void Start()
    {
        spawnZ = 300f;
        pooledObjectList = new List<GameObject>();

        for( int i=0; i<pooledAmount; i++)
        {
            GameObject obj= (GameObject)Instantiate(pooledObject);
            obj.SetActive(false);
            pooledObjectList.Add(obj);
        }

        SpawnHeart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnHeart()
    {
        GameObject newHeart = GetPooledObject();
        // set random xpos
        float[] Xpos = new float[3];
        Xpos[0] = 0;
        Xpos[1] = 3f;
        Xpos[2] = -3f;
        int RandomXpos = Random.Range(0, Xpos.Length);
        newHeart.transform.position = new Vector3(Xpos[RandomXpos], 2.3f, spawnZ);
        newHeart.SetActive(true);
        spawnZ += Random.Range(300, 400f);
    }

    public GameObject GetPooledObject () 
    {
        
        for(int i=0; i<pooledObjectList.Count; i++)
        {
            if(!pooledObjectList[i].activeInHierarchy)
            {
                return pooledObjectList[i];
            }
        }

        GameObject obj= (GameObject)Instantiate(pooledObject);
        obj.SetActive(false);
        pooledObjectList.Add(obj);
        return obj;
        
    }
}

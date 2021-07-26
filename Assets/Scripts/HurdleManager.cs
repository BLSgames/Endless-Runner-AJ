using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurdleManager : MonoBehaviour
{

    public GameObject[] hurdlePrefabs;
    private Transform playerTransform;
    private float spawnZ = 25f;
    private float safeZone = 25f;
    private float hurdleLength = 10f;
    private int amnHurdleOnScreen = 10;
    private int lastPrefabIndex = 0;

    public List<GameObject> activeHurdle;

    // Start is called before the first frame update
    private void Start()
    {
        activeHurdle = new List<GameObject>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        
        for (int i=0; i<amnHurdleOnScreen; i++)
        {
            SpawnHurdle();
        }
        
           
    }

    // Update is called once per frame
    private void Update()
    {
        // if(playerTransform.position.z - activeHurdle[0].transform.position.z >= 20f)
        if(playerTransform.position.z - safeZone > spawnZ - amnHurdleOnScreen*hurdleLength)
        {
            DeleteHurdle();
            SpawnHurdle();
        }

        if(activeHurdle.Count <= 7)
        {
            SpawnHurdle();
            SpawnHurdle();
            SpawnHurdle();
        }

        // if(transform.position.z > 40f)
        //     SpawnHurdle();
    }
    public void SpawnHurdle(int prefabIndex = -1)
    {
        GameObject go;
        if(prefabIndex == -1)
            go = Instantiate(hurdlePrefabs[ RandomPrefabIndex() ]) as GameObject;
        else
            go = Instantiate(hurdlePrefabs[prefabIndex]) as GameObject;

        go.transform.SetParent(transform);
        // go.transform.position = Vector3.forward*spawnZ;

        float[] Xpos = new float[3];
        Xpos[0] = 0;
        Xpos[1] = 3f;
        Xpos[2] = -3f;
        int RandomXpos = Random.Range(0, Xpos.Length);

        go.transform.position = new Vector3( Xpos[RandomXpos], 2f ,spawnZ);
        spawnZ += Random.Range( 10f,15f);
        activeHurdle.Add(go);
    }
    public void DeleteHurdle()
    {
        Destroy(activeHurdle[0]);
        activeHurdle.RemoveAt(0);
    }
    private int RandomPrefabIndex()
    {
        if(hurdlePrefabs.Length <= 1)
            return 0;
        
        int randomIndex = lastPrefabIndex;
        while(randomIndex == lastPrefabIndex)
        {
            randomIndex = Random.Range(0, hurdlePrefabs.Length);
        }

        lastPrefabIndex = randomIndex;
        return randomIndex;
    }


   
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurdleManagerSc : MonoBehaviour
{

    public GameObject[] hurdlePrefabs;
    private Transform playerTransform;
    private float spawnZ = 0f;
    private float hurdleLength = 3f; //
    private int amnHurdleOnScreen = 10;
    private float safeZone = 25f;
    private int lastPrefabIndex = 0;

    private List<GameObject> activeHurdle;

    // Start is called before the first frame update
    private void Start()
    {
        activeHurdle = new List<GameObject>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        
        
        
           
    }

    // Update is called once per frame
    private void Update()
    {
        if(playerTransform.position.z - safeZone > (spawnZ - amnHurdleOnScreen*hurdleLength))
        {
            SpawnHurdle();
            DeleteHurdle();
        }

        if(transform.position.z > 40f)
            SpawnHurdle();
    }
    private void SpawnHurdle(int prefabIndex = -1)
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

        go.transform.position = new Vector3(Xpos[RandomXpos], 1.7f ,spawnZ);
        spawnZ += Random.Range(15,40);
        activeHurdle.Add(go);
    }
    private void DeleteHurdle()
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


    // public GameObject[] hurdle;
    // private float hurdleTime;
    // // Start is called before the first frame update
    // void Start()
    // {
    //     StartCoroutine(SpawnHerdle());
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
    // IEnumerator SpawnHerdle()
    // {
    //     yield return new WaitForSeconds(hurdleTime);

    //     Spawn();
    // }
    // private void Spawn()
    // {
    //     int randomHerdle = Random.Range(0, hurdle.Length -1 );
    //     StartCoroutine(SpawnHerdle());
    // }
}

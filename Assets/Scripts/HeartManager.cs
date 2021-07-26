using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartManager : MonoBehaviour
{
    private Transform playerTransform;
    private List<GameObject> activeHeart;
    private float safeZone = 25f;

    // Start is called before the first frame update
    private void Start()
    {
        activeHeart = new List<GameObject>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        
        for (int i=0; i<amnHeartOnScreen; i++)
        {
            SpawnHeart();
        }   
    }
    private void Update() {
        // if(playerTransform.position.z - activeCoins[0].transform.position.z >= 20f)
        if (playerTransform.position.z - safeZone > (spawnZ - amnHeartOnScreen * tileLength))
        {
            DeleteHeart();
            SpawnHeart();
        }
    }
    private int amnHeartOnScreen = 3;
    private float tileLength = 165f;
    public GameObject heartPrefab;
    private float spawnZ = 500f;
    public void SpawnHeart()
    {
       
        GameObject go;
        go = Instantiate(heartPrefab) as GameObject;
        go.transform.SetParent(transform);
        // go.transform.position = Vector3.forward*spawnZ;

        float[] Xpos = new float[3];
        Xpos[0] = 0;
        Xpos[1] = 3f;
        Xpos[2] = -3f;
        int RandomXpos = Random.Range(0, Xpos.Length);

        go.transform.position = new Vector3(Xpos[RandomXpos], 2.3f, spawnZ);
        spawnZ += Random.Range(400, 500f);
        activeHeart.Add(go);
    }
    private void DeleteHeart()
    {
        Destroy(activeHeart[0]);
        activeHeart.RemoveAt(0);
    }
}

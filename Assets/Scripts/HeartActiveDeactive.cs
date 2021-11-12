using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartActiveDeactive : MonoBehaviour
{
    private GameObject player;
    private void Start() {
        player = GameObject.FindWithTag("Player");
    }
    
    private void Update()
    {
        if(gameObject.activeInHierarchy && transform.position.z <= player.transform.position.z - 50f)
        {
            ObjectPoolerHeart.instance.SpawnHeart();
            gameObject.SetActive(false);
        }
    }
}

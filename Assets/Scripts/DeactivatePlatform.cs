using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivatePlatform : MonoBehaviour
{
    private GameObject player;
    private void Start() {
        player = GameObject.FindWithTag("Player");
    }
    void Update()
    {
      
        if(transform.position.z <= player.transform.position.z - 50f)
        {
            gameObject.SetActive(false);
        }
    }
}

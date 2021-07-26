using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    private void Update()
    {
            Vector3 playerPos = player.position;
            Vector3 cameraPos = transform.position;
            cameraPos.z = playerPos.z - 14f;
            transform.position = cameraPos;
    }
}

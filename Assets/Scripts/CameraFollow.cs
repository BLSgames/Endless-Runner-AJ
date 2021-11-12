using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Vector3 offset;
    private void Start()
    {
        offset = transform.position - player.position;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        Vector3 newPos = new Vector3(transform.position.x, transform.position.y, offset.z + player.position.z);
        transform.position = Vector3.Lerp(transform.position, newPos, 10*Time.deltaTime);
    }
}

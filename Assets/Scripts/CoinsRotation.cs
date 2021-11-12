using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsRotation : MonoBehaviour
{

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.gameObject.tag == "Obstacles")
    //     {
    //         // Destroy(other.gameObject);
    //         // GameObject.FindObjectOfType<HurdleManager>().SpawnHurdle();
    //         Debug.Log("Obstacles destroy by coins");
    //     }
    // }
    private void Update()
    {
        transform.Rotate(0, 100 * Time.deltaTime, 0);
    }

}

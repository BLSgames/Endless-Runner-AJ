using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartRotating : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Obstacles")
        {
            Destroy(other.gameObject);
            
        }
        if(other.gameObject.tag == "Coins")
        {
            Destroy(other.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 100*Time.deltaTime, 0);
    }
}

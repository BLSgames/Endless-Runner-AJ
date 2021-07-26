using UnityEngine;
using UnityEngine.UI;

public class ExpandMenuItem : MonoBehaviour
{
    [HideInInspector] public Image img;
    [HideInInspector] public Transform trans;
    
    void Awake()
    {
        img = GetComponent<Image>();
        trans = transform;
    }
}

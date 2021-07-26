using UnityEngine.UI;
using UnityEngine;
using System.Linq;

public class RadioBtn : MonoBehaviour
{
    ToggleGroup toggleGroup;
    private void Start() {
        toggleGroup = GetComponent<ToggleGroup>();
        Toggle toggle =  toggleGroup.ActiveToggles().FirstOrDefault();
        
        
    }
    public void On() {
        AudioManager.instance.Play("Theme");
        
    }
    public void Off()
    {
        AudioManager.instance.BgSoundPause();
    }
}
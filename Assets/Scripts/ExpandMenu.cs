
using UnityEngine;
using UnityEngine.UI;

public class ExpandMenu : MonoBehaviour
{
    [Header("space between menu items")]
    [SerializeField] Vector2 spacing;

    public Sprite DefaultSprite;
    public Sprite CloseIconSprite;

    Button mainButton;
    ExpandMenuItem[] menuItems;
    bool isExpanded = false;

    Vector2 mainButtonPosition;
    private int itemsCount;
    private void Start()
    {
        itemsCount = transform.childCount - 1;
        menuItems = new ExpandMenuItem[itemsCount];
        for (int i = 0; i < itemsCount; i++)
        {
            menuItems[i] = transform.GetChild(i + 1).GetComponent<ExpandMenuItem>();
        }
        mainButton = transform.GetChild(0).GetComponent<Button>();
        mainButton.onClick.AddListener(ToggleMenu);
        mainButton.transform.SetAsLastSibling();
        mainButtonPosition =mainButton.transform.position;

        //reset all button position to mainbutton
        ResetPosition();
    }
    private void ResetPosition()
    {
       for(int i=0; i<itemsCount; i++)
       {
           menuItems[i].trans.position = mainButtonPosition;
       }
    }
    private void ToggleMenu()
    {
        isExpanded = !isExpanded;
        if(isExpanded)
        {
            //menu opened
            for(int i=0; i<itemsCount; i++)
            {
                menuItems[i].trans.position = mainButtonPosition + spacing*(i+1);
            }
            mainButton.image.sprite = CloseIconSprite;
            AudioManager.instance.Play("Menu");
        }
        else
        {
            // menu closed
            for(int i=0; i<itemsCount; i++ )
            {
                menuItems[i].trans.position = mainButtonPosition;
            }
            mainButton.image.sprite = DefaultSprite;
            AudioManager.instance.Play("Menu");
        }
    }
    private void OnDestroy() {
        mainButton.onClick.RemoveListener(ToggleMenu);
    }
}

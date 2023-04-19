using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuHelper : MonoBehaviour
{
    public List<GameObject> listOfMenus;
    public GameObject mainMenu;
    [Header("Load Game Button")]
    public Button loadGameButton;

    // Start is called before the first frame update
    void Start()
    {
        DisableAllMenus();
        mainMenu.SetActive(true);
        DisableLoadGameButton();
    }
    private void DisableAllMenus()
    {
        listOfMenus.ForEach(menu => menu.SetActive(false));
    }
    public void ChangeMenu(GameObject menu)
    {
        DisableAllMenus();
        menu.SetActive(true);
    }
    public void DisableLoadGameButton()
    {
        if (!SaveManager.instance.hasPreviousSave)
        {
            loadGameButton.interactable = false;
        }
    }
}

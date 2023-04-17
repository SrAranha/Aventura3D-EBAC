using System.Collections.Generic;
using UnityEngine;

public class MenuHelper : MonoBehaviour
{
    public List<GameObject> listOfMenus;
    public GameObject mainMenu;

    // Start is called before the first frame update
    void Start()
    {
        DisableAllMenus();
        mainMenu.SetActive(true);
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
}

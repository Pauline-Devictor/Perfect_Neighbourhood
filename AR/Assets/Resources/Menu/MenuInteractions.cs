using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MenuInteractions : MonoBehaviour
{
    public GameObject menuPanel;

    public void ShowHideMenu()
    {
        menuPanel.SetActive(menuPanel.activeSelf ? false : true);
        GameObject.Find("City").GetComponent<CityInteractions>().enabled = menuPanel.activeSelf ? false : true;
    }
}

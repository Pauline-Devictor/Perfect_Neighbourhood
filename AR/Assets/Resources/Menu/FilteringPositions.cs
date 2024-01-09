using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FilteringPositions : MonoBehaviour
{
    //TODO: once a time-slot button is clicked, filter the positions of the objects and suspects in the scene

    public void PickedTimeSlot(GameObject button)
    {
        if(button.GetComponent<Image>().color == Color.cyan)
        {
            button.GetComponent<Image>().color = Color.white;
        }
        else
        {
            button.GetComponent<Image>().color = Color.cyan;
        }
    }
}
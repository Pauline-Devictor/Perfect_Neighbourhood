using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FilteringPositions : MonoBehaviour
{
    private GameObject slider;
    private Dictionary<string, Vector3> locations;
    public GameObject suspectMarker;

    public void Start(){
        slider = GameObject.Find("Slider");
        locations = GetComponent<Locations>().locations;
        setupMap(null, 0);
    }

    void setupMap(List<SuspectsList.Suspect> suspects, int timeSlot){
        if(suspects == null) return;
        GameObject map = GameObject.Find("MapMenu");
        foreach (Transform child in map.transform) {
            if(child.gameObject.name != "Image"){
                Destroy(child.gameObject);
            }
        }
        foreach(SuspectsList.Suspect suspect in suspects){
            if(locations.ContainsKey(suspect.positions[timeSlot])){
                GameObject marker = Instantiate(suspectMarker);
                marker.transform.SetParent(map.transform);
                marker.transform.localPosition = locations[suspect.positions[timeSlot]];
                marker.transform.localScale = new Vector3(1, 1, 1);
                marker.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
                marker.GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);
                marker.GetComponent<RectTransform>().anchoredPosition = new Vector2(locations[suspect.positions[timeSlot]].x, locations[suspect.positions[timeSlot]].y);
            }
        }
    }

    public void Filtering(List<SuspectsList.Suspect> suspects){
        int value = (int)slider.GetComponent<Slider>().value;
        setupMap(suspects, value);
    }
}


            
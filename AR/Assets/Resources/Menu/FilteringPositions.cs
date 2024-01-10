using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FilteringPositions : MonoBehaviour
{
    private GameObject slider;
    private Dictionary<string, Vector3> locations;
    public GameObject suspectMarker;
    private string[] timeSlots = {"onePM", "twoPM", "threePM", "fourPM", "fivePM"};

    public void Start(){
        slider = GameObject.Find("Slider");
        locations = GetComponent<Locations>().locations;
        setupMap(null);
    }

    void setupMap(List<SuspectsList.Suspect> suspects){
        if(suspects == null) return;
        GameObject map = GameObject.Find("MapMenu");
        foreach (Transform child in map.transform) {
            if(child.gameObject.name != "Image"){
                Destroy(child.gameObject);
            }
        }
        foreach(SuspectsList.Suspect suspect in suspects){
            if(locations.ContainsKey(suspect.position)){
                //instantiate with default position
                GameObject marker = Instantiate(suspectMarker);
                marker.transform.SetParent(map.transform);
                //convert vector 3 to world position
                Vector3 position3D = locations[suspect.position];
                Vector3 position2D = Camera.main.WorldToScreenPoint(position3D);
                //set the position
                marker.transform.position = position2D;
                marker.transform.localScale = new Vector3(1, 1, 1);
                //log the parent
                Debug.Log(marker.transform.position);
            }
        }
    }

    public void Filtering(List<SuspectsList.Suspect> suspects){
        int value = (int)slider.GetComponent<Slider>().value;
        string timeSlot = timeSlots[value];
        //TODO
        setupMap(suspects);
    }
}


            
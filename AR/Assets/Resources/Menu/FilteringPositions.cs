using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FilteringPositions : MonoBehaviour
{
    private GameObject slider;
    private Dictionary<string, Vector3> locations;
    public GameObject suspectMarker;
    public List<SuspectsList.Suspect> suspects;

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
        Dictionary<string, int> count = new Dictionary<string, int>();
        foreach(string location in locations.Keys){
            count.Add(location, 0);
        }
        foreach(SuspectsList.Suspect suspect in suspects){
            if(locations.ContainsKey(suspect.positions[timeSlot])){
                count[suspect.positions[timeSlot]] += 1;
                Vector3 position = locations[suspect.positions[timeSlot]];
                GameObject marker = Instantiate(suspectMarker);
                marker.name = suspect.name;
                marker.transform.SetParent(map.transform);
                if(count[suspect.positions[timeSlot]] > 1){
                    position.x += Random.Range(-10f, 10f);
                    position.y += Random.Range(-10f, 10f);
                }
                marker.transform.localScale = new Vector3(1, 1, 1);
                marker.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
                marker.GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);
                marker.GetComponent<RectTransform>().anchoredPosition = new Vector2(position.x, position.y);                
            }
        }
        
    }

    public void FilteringPositionsByTime(){
        if(suspects == null) return;
        int value = (int)slider.GetComponent<Slider>().value;
        setupMap(suspects, value);
    }
}


            
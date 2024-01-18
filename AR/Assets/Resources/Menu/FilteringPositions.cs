using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FilteringPositions : MonoBehaviour
{
    private GameObject slider;
    private Dictionary<string, Vector3> locations;
    private GameObject placeholder;
    public GameObject suspectMarker;
    public GameObject objMarker;
    public List<SuspectsList.Suspect> suspects;

    public void Start(){
        slider = GameObject.Find("Slider");
        locations = GetComponent<Locations>().locations;
        placeholder = GameObject.Find("ScrollObjects/Viewport/Content");
        setupMap(null, 0);
    }

    void setupMap(List<SuspectsList.Suspect> suspects, int timeSlot){
        if(suspects == null) return;
        GameObject map = GameObject.Find("MapMenu");
        List<ObjectsList.ObjectSus> objs = GetComponent<ObjectsList>().objectSus;
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
                    position.y += Random.Range(-20f, 20f);
                }
                marker.transform.localScale = new Vector3(1, 1, 1);
                marker.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
                marker.GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);
                marker.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(position.x, position.y, -2f);
                }
        }
        placeholder.GetComponent<TextMeshProUGUI>().text = "pan\n";
        Debug.Log(objs);
        foreach(ObjectsList.ObjectSus obj in objs){
            Debug.Log(obj.name + " " + obj.position + " " + obj.time);
            if(locations.ContainsKey(obj.position) && obj.time == timeSlot){
                count[obj.position] += 1;
                Vector3 position = locations[obj.position];
                GameObject marker = Instantiate(objMarker);
                marker.name = obj.name;
                marker.transform.SetParent(map.transform);
                if(count[obj.position] > 1){
                    position.x += Random.Range(-10f, 10f);
                    position.y += Random.Range(-20f, 20f);
                }
                marker.transform.localScale = new Vector3(1, 1, 1);
                marker.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
                marker.GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);
                marker.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(position.x, position.y, -2f);               
                placeholder.GetComponent<TMPro.TextMeshProUGUI>().text += obj.name + "\n";
            }
        }
    }

    public void FilteringPositionsByTime(){
        if(suspects == null) return;
        int value = (int)slider.GetComponent<Slider>().value;
        setupMap(suspects, value);
    }
}


            
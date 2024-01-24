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
    public List<Suspect> suspects;

    public void Start(){
        slider = GameObject.Find("Slider");
        locations = GetComponent<Locations>().locations;
        placeholder = GameObject.Find("ScrollObjects/Viewport/Objects");
        setupMap(null, 0);
    }

    void setupMap(List<Suspect> suspects, int timeSlot){
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
        foreach(Suspect suspect in suspects){
            if(locations.ContainsKey(suspect.positions[timeSlot])){
                count[suspect.positions[timeSlot]] += 1;
                Vector3 position = locations[suspect.positions[timeSlot]];
                if(count[suspect.positions[timeSlot]] > 1){
                    position.x += Random.Range(-10f, 10f);
                    position.y += Random.Range(-5f, 5f);
                }
                createMarker(suspectMarker, map, position, suspect.name);
            }
        }
        placeholder.GetComponent<TextMeshProUGUI>().text = "pan\n";
        foreach(ObjectsList.ObjectSus obj in objs){
            if(locations.ContainsKey(obj.position) && obj.time == timeSlot){
                count[obj.position] += 1;
                Vector3 position = locations[obj.position];
                if(count[obj.position] > 1){
                    position.x += Random.Range(-10f, 10f);
                    position.y += Random.Range(-5f, 5f);
                }
                createMarker(objMarker, map, position, obj.name);
                placeholder.GetComponent<TMPro.TextMeshProUGUI>().text += obj.name + "\n";
            }
        }
    }

    private void createMarker(GameObject marker, GameObject map, Vector3 position, string name){
        GameObject newMarker = Instantiate(marker);
        newMarker.name = name;
        newMarker.transform.SetParent(map.transform);
        newMarker.GetComponent<RectTransform>().localScale = new Vector3(1.224543f, 3.16787f, 0.4166666f);
        newMarker.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
        newMarker.GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);
        newMarker.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(position.x, position.y, -2f);
        newMarker.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, 0);
    }

    public void FilteringPositionsByTime(){
        if(suspects == null) return;
        int value = (int)slider.GetComponent<Slider>().value;
        setupMap(suspects, value);
    }
}


            
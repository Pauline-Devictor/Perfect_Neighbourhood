using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuildingSelectionController : MonoBehaviour
{
    public TextAsset suspectCsvFile;
    public TextAsset objectCsvFile;
    public TextMeshPro BuildingName;
    public GameObject BuildingDetails;
    public TextMeshPro SuspectListContainer;
    private SelectableBuilding selectedBuilding;
    public Camera ARCamera;
    private bool isInfoMoving = false;
    float t = 0f;
    public float Speed = 100.0f;
    private List<Suspect> suspects;

    // Start is called before the first frame update
    void Start()
    {
        DataStore dataStore = GameObject.Find("DataStore").GetComponent<DataStore>();
        suspects = dataStore.Suspects;
        BuildingDetails.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Selecting a building
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = ARCamera.ScreenPointToRay(touch.position);
                RaycastHit hitObject;
                if (Physics.Raycast(ray, out hitObject))
                {
                    if (hitObject.transform.GetComponent<SelectableBuilding>() != null)
                    {
                        SelectableBuilding building = hitObject.transform.GetComponent<SelectableBuilding>();
                        if (selectedBuilding != null && selectedBuilding.BuildingName == building.BuildingName)
                        {
                            BuildingDetails.SetActive(false);
                            selectedBuilding.IsSelected = false;
                            selectedBuilding = null;
                        }
                        else
                        {
                            if (selectedBuilding != null)
                            {
                                selectedBuilding.IsSelected = false;
                                isInfoMoving = true;
                                SelectBuilding(building);
                            }
                            else
                            {
                                SelectBuilding(building, true);
                            }
                        }
                    }
                }
            }
        }
        else if (Input.touchCount == 2)
        {
            Touch[] touches = Input.touches;
            if (touches[0].phase == TouchPhase.Began || touches[1].phase == TouchPhase.Began)
            {
                SelectableBuilding building1 = GetHitBuilding(touches[0]);
                SelectableBuilding building2 = GetHitBuilding(touches[1]);
                if (building1 != null && building2 != null)
                {
                    if (building1.BuildingName != building2.BuildingName)
                    {
                        selectedBuilding = building1;
                        TravelTimeInfo travelTimeInfo = GetTravelTimesForBuildings(building1.BuildingName, building2.BuildingName);
                        string travelTimes = building1.BuildingName + " to " + building2.BuildingName + "\n";
                        travelTimes += "Bus: " + travelTimeInfo.bus.ToString() + " min\n";
                        travelTimes += "Car: " + travelTimeInfo.car.ToString() + " min\n";
                        travelTimes += "Walk: " + travelTimeInfo.walk.ToString() + " min\n";
                        BuildingName.text = travelTimes;
                        SuspectListContainer.text = "";
                    }
                }
            }
        }


        // Repositioning the building info
        if (selectedBuilding != null && !isInfoMoving)
        {
            RepositionBuildingInfo();
        }

        // Rotating the building info to face the camera
        if (selectedBuilding != null)
        {
            var newRotation = BuildingDetails.transform.rotation;
            newRotation.y = -ARCamera.transform.rotation.y;
            BuildingDetails.transform.rotation = newRotation;
        }

        // Animation for moving the building info
        if (isInfoMoving)
        {
            t += Time.deltaTime * Speed / 100;
            if (t < 1f)
            {
                float interpolationRatio = 1 - Mathf.Pow(1 - t, 3);
                Vector3 interpolatedPosition = Vector3.Lerp(BuildingDetails.transform.position, selectedBuilding.transform.position + new Vector3(0, selectedBuilding.GetComponent<Collider>().bounds.size.y, 0), interpolationRatio);
                BuildingDetails.transform.position = interpolatedPosition;
            }
            else
            {
                isInfoMoving = false;
                t = 0f;
            }
        }
    }

    void SelectBuilding(SelectableBuilding building, bool placeDirectly = false)
    {
        selectedBuilding = building;
        selectedBuilding.IsSelected = true;
        BuildingName.text = selectedBuilding.BuildingName;
        ShowSuspectsForBuilding(selectedBuilding);
        if (placeDirectly) RepositionBuildingInfo();
        BuildingDetails.SetActive(true);
        t = 0f;
    }

    void RepositionBuildingInfo()
    {
        BuildingDetails.transform.position = selectedBuilding.transform.position + new Vector3(0, selectedBuilding.transform.GetComponent<Collider>().bounds.size.y, 0);
    }

    SelectableBuilding GetHitBuilding(Touch touch)
    {
        Ray ray = ARCamera.ScreenPointToRay(touch.position);
        RaycastHit hitObject;
        if (Physics.Raycast(ray, out hitObject))
        {
            return hitObject.transform.GetComponent<SelectableBuilding>();
        }
        else
        {
            return null;
        }
    }

    List<Suspect> GetSuspectsForBuilding(string buildingName)
    {
        SuspectListContainer.text = suspects.Count.ToString();
        List<Suspect> suspectsForBuilding = new List<Suspect>();
        foreach (Suspect suspect in suspects)
        {
            foreach (string position in suspect.positions)
            {
                SuspectListContainer.text = position;

                if (position == buildingName)
                {
                    suspectsForBuilding.Add(suspect);
                }
                break;
            }
        }
        return suspectsForBuilding;
    }

    int indexToHour(int index)
    {
        return index + 13;
    }

    string rangesToString(List<int[]> ranges)
    {
        string result = "";
        foreach (int[] range in ranges)
        {
            result += range[0].ToString() + "h - " + range[1].ToString() + "h, ";
        }
        return result;
    }

    string GetPresenceRangeForSuspectInBuilding(Suspect suspect, string buildingName)
    {
        var ranges = new List<int[]>();
        int[] currentRange = new int[2];
        for (int i = 0; i < suspect.positions.Length; i++)
        {
            if (suspect.positions[i] == buildingName)
            {
                if (currentRange[0] == 0)
                {
                    currentRange[0] = indexToHour(i);
                }
                currentRange[1] = indexToHour(i);
            }
            else
            {
                if (currentRange[0] != 0)
                {
                    ranges.Add(currentRange);
                    currentRange = new int[2];
                }
            }
        }
        if (currentRange[0] != 0)
        {
            ranges.Add(currentRange);
        }

        return rangesToString(ranges);
    }

    void ShowSuspectsForBuilding(SelectableBuilding building)
    {
        List<Suspect> suspectsForBuilding = GetSuspectsForBuilding(building.BuildingName);
        string suspectList = "";
        foreach (Suspect suspect in suspectsForBuilding)
        {
            suspectList += suspect.name + ": " + GetPresenceRangeForSuspectInBuilding(suspect, building.BuildingName) + "\n";
        }
        SuspectListContainer.text = suspectList;
    }

    TravelTimeInfo GetTravelTimesForBuildings(string building1, string building2)
    {
        float distance = Vector3.Distance(GameObject.Find(building1).transform.position, GameObject.Find(building2).transform.position) * 10;
        TravelTimeInfo travelTimeInfo = new TravelTimeInfo();
        // the idea is to get fake but credible travel times by bus, car and walking
        travelTimeInfo.bus = (int)(distance * 2);
        travelTimeInfo.car = (int)(distance * 1.1);
        travelTimeInfo.walk = (int)(distance * 5);
        return travelTimeInfo;
    }
}

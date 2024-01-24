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

    public List<Suspect> GetSuspectsForBuilding(string buildingName)
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

    public void ShowSuspectsForBuilding(SelectableBuilding building)
    {
        List<Suspect> suspectsForBuilding = GetSuspectsForBuilding(building.BuildingName);
        string suspectList = "";
        foreach (Suspect suspect in suspectsForBuilding)
        {
            suspectList += suspect.name + "\n";
        }
        SuspectListContainer.text = suspectList;
    }
}

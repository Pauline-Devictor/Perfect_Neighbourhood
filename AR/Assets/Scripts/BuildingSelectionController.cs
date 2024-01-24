using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuildingSelectionController : MonoBehaviour
{
    public TextMeshPro BuildingName;
    public GameObject BuildingDetails;
    private SelectableBuilding selectedBuilding;
    public Camera ARCamera;
    private bool isInfoMoving = false;
    float t = 0f;
    public float Speed = 100.0f;

    // Start is called before the first frame update
    void Start()
    {
        selectedBuilding = null;
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
        if (placeDirectly) RepositionBuildingInfo();
        BuildingDetails.SetActive(true);
        t = 0f;
    }

    void RepositionBuildingInfo()
    {
        BuildingDetails.transform.position = selectedBuilding.transform.position + new Vector3(0, selectedBuilding.transform.GetComponent<Collider>().bounds.size.y, 0);
    }
}

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
    public int interpolationFramesCount = 45; // Number of frames to completely interpolate between the 2 positions
    int elapsedFrames = 0;

    // Start is called before the first frame update
    void Start()
    {
        selectedBuilding = null;
        BuildingDetails.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isInfoMoving)
        {
            // function easeOutCubic(x: number): number {
            //     return 1 - Math.pow(1 - x, 3);
            // }
            float interpolationRatio = 1 - Mathf.Pow(1 - (float)elapsedFrames / interpolationFramesCount, 3);
            Vector3 interpolatedPosition = Vector3.Lerp(BuildingDetails.transform.position, selectedBuilding.transform.position + new Vector3(0, selectedBuilding.GetComponent<Collider>().bounds.size.y, 0), interpolationRatio);
            BuildingDetails.transform.position = interpolatedPosition;
            elapsedFrames = (elapsedFrames + 1) % (interpolationFramesCount + 1);  // reset elapsedFrames to zero after it reached (interpolationFramesCount + 1)
            if (elapsedFrames == 0)
            {
                isInfoMoving = false;
            }
        }
        if (Input.touchCount > 0)
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
                            selectedBuilding = null;
                        }
                        else
                        {
                            Collider collider = hitObject.transform.GetComponent<Collider>();
                            if (selectedBuilding != null)
                            {
                                selectedBuilding.IsSelected = false;
                                isInfoMoving = true;
                            }
                            else
                            {
                                BuildingDetails.SetActive(true);
                                BuildingDetails.transform.position = hitObject.transform.position + new Vector3(0, collider.bounds.size.y, 0);
                            }
                            selectedBuilding = building;
                            selectedBuilding.IsSelected = true;
                            BuildingName.text = selectedBuilding.BuildingName;
                        }
                    }
                }
            }
        }
    }
}

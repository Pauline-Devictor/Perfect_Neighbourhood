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

    // Start is called before the first frame update
    void Start()
    {
        selectedBuilding = null;
        BuildingName.text = "Bonjour";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                BuildingName.text = "Touch";
                Ray ray = ARCamera.ScreenPointToRay(touch.position);
                Debug.Log("Raycast");
                Debug.Log(ray);
                RaycastHit hitObject;
                if (Physics.Raycast(ray, out hitObject))
                {
                    Debug.Log("Hit " + hitObject.transform.name);
                    if (hitObject.transform.GetComponent<SelectableBuilding>() != null)
                    {
                        Debug.Log("Selecting " + hitObject.transform.name);
                        if (selectedBuilding != null)
                        {
                            selectedBuilding.IsSelected = false;
                        }
                        else
                        {
                            BuildingDetails.SetActive(true);
                        }
                        selectedBuilding = hitObject.transform.GetComponent<SelectableBuilding>();
                        selectedBuilding.IsSelected = true;
                        BuildingName.text = selectedBuilding.BuildingName;
                    }
                }
            }
        }
    }
}

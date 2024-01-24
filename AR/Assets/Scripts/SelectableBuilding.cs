using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableBuilding : MonoBehaviour
{
    private string buildingName;
    private Outline outline;
    public string BuildingName
    {
        get { return buildingName; }
        set { buildingName = value; }
    }
    private bool isSelected = false;
    public bool IsSelected
    {
        get { return isSelected; }
        set { isSelected = value; }
    }

    // private Canvas buildingInfo;
    // Start is called before the first frame update
    void Start()
    {
        // buildingInfo = GameObject.FindWithTag("BuildingInfo").GetComponent<Canvas>();
        outline = gameObject.AddComponent<Outline>();

        outline.OutlineMode = Outline.Mode.OutlineAll;
        outline.OutlineColor = Color.magenta;
        outline.OutlineWidth = 5f;
        outline.enabled = false;
        buildingName = gameObject.name;
    }

    // Update is called once per frame
    void Update()
    {
        outline.enabled = isSelected;
    }
}

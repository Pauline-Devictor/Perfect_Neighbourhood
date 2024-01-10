using System.Collections.Generic;
using UnityEngine;

public class PropsLooking : MonoBehaviour
{
    public Transform vrCamera; // Assign your VR camera to this field in the Inspector
    private string propTag = "Prop"; // The tag to identify the objects
    private List<GameObject> propObjects = new List<GameObject>();

    void Start()
    {
        // Find all GameObjects with the specified tag and convert them to a List
        GameObject[] propsArray = GameObject.FindGameObjectsWithTag(propTag);
        propObjects.AddRange(propsArray);
    }

    private void Update()
    {
        // Check if the VR camera is assigned
        if (vrCamera == null)
        {
            Debug.LogWarning("VR Camera not assigned!");
            return;
        }

        RaycastHit hit;
        // Cast a ray from the VR camera forward
        if (Physics.Raycast(vrCamera.position, vrCamera.forward, out hit))
        {
            // Check if the hit object has the specified tag
            if (hit.collider.CompareTag(propTag))
            {
                // If the object is tagged as "Prop", activate its light component
                Light propLight = hit.collider.GetComponentInChildren<Light>();
                if (propLight != null)
                {
                    propLight.enabled = true; // Activate the light
                }
            }
            else
            {
                foreach (GameObject prop in propObjects)
                {
                    prop.GetComponentInChildren<Light>().enabled = false;
                }
            }
        }
    }
}

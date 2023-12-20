using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    [SerializeField] Transform lightContainer;
    public List<Light> directLights = new List<Light>();
    public List<Light> childLights = new List<Light>();

    void Start()
    {
        InitializeLightLists(lightContainer);
    }

    void InitializeLightLists(Transform parent)
    {
        foreach (Transform child in parent)
        {
            if (child.GetComponent<Light>() != null && child.parent.transform == lightContainer)
            {
                directLights.Add(child.GetComponent<Light>());
            }
            else
            {
                childLights.Add(child.GetComponentInChildren<Light>());
            }
        }
    }


    public void OutsideLights(bool turnOn, int hour)
    {
        float intensity = 0;
        if (turnOn) {
            switch (hour)
            {
                case 13:
                    intensity = 3;
                    break;
                case 14:
                    intensity = 2.25f;
                    break;
                case 15:
                    intensity = 1.5f;
                    break;
                case 16:
                    intensity = 1;
                    break;
                case 17:
                    intensity = 0.5f;
                    break;
                default:
                    intensity = 1;
                    break;
            }
            foreach (Light light in childLights)
            {
                light.intensity = intensity;
            }
        }
        foreach (Light light in directLights)
        {
            light.intensity = intensity;
        }
    }

    public void CeillingLights(bool turnOn)
    {
        float intensity;
        if (turnOn) { intensity = 0.5f; }
        else intensity = 0;
        foreach (Light light in childLights)
        {
            light.intensity = intensity;
        }
    }
}

using System.Collections.Generic;
using UnityEngine;
using Meta.WitAi;
using Meta.WitAi.Json;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    [SerializeField] Transform lightContainer;
    public List<Light> directLights = new List<Light>();
    public List<Light> childLights = new List<Light>();

    public const string TRAIT_ID = "wit$on_off";
    public const string TRAIT_ON_VALUE = "on";

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
                    intensity = 1.6f;
                    break;
                case 14:
                    intensity = 2f;
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



    // On response callback
    public void OnResponse(WitResponseNode commandResult)
    {
        // Check for trait value
        var traitValue = commandResult.GetTraitValue(TRAIT_ID);
        if (string.IsNullOrEmpty(traitValue))
        {
            Debug.LogWarning($"No value found for trait: {TRAIT_ID}");
            return;
        }

        // Get value
        bool isOn = string.Equals(traitValue, TRAIT_ON_VALUE);
        Debug.Log("isOn : " + isOn);
        CeillingLights(isOn);
        OutsideLights(isOn, 13);
        
    }
}

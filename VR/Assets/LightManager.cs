using System.Collections.Generic;
using UnityEngine;
using Meta.WitAi;
using Meta.WitAi.Json;
using Meta.WitAi.Data.Entities;

public class LightManager : MonoBehaviour
{
    [SerializeField] Transform lightContainer;
    public List<Light> directLights = new();
    public List<Light> childLights = new();

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

    private void OutsideLights(bool turnOn, int hour)
    {
        float intensity = 0;
        Color color = Color.white;
        if (turnOn) {
            switch (hour)
            {
                case -1:
                    return;
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
                    color = new Color(1, (float) 0.55, 0);
                    intensity = 1f;
                    break;
                default:
                    intensity = 1;
                    break;
            }
        }
        foreach (Light light in directLights)
        {
            light.color = color;
            light.intensity = intensity;
        }
    }

    private void CeillingLights(bool turnOn)
    {
        float intensity;
        if (turnOn) { intensity = 0.5f; }
        else intensity = 0;
        foreach (Light light in childLights)
        {
            light.intensity = intensity;
        }
    }

    private void OutsideLights(bool turnOn)
    {
        float intensity = 0;
        if (turnOn)
            intensity = 1;
        foreach (Light light in directLights)
        {
            light.intensity = intensity;
        }
    }

    // On response callback
    public void OnResponse(WitResponseNode commandResult)
    {

        IsLightOnCommand(commandResult);
        IsLightOffCommand(commandResult);
        IsWindowOnCommand(commandResult);
        IsWindowOffCommand(commandResult);
        IsHourCommand(commandResult);

    }


    private void IsLightOnCommand(WitResponseNode commandResult)
    {
        WitEntityData[] action = commandResult.GetEntities("lightStatus:lightStatus");
        if (action == null) return;

        if (action.Length > 0)
            {
                if (action[0].name != null)
                {
                    if (action[0].name == "lightStatus")
                        CeillingLights(true);
                }
            }
        
    }
    private void IsLightOffCommand(WitResponseNode commandResult)
    {

        WitEntityData[] action = commandResult.GetEntities("lightOffStatus:lightOffStatus");
        if (action == null) return;
        if (action.Length > 0)
        {
            if (action[0].name != null)
            {
                if (action[0].name == "lightOffStatus")
                    CeillingLights(false);
            }
        }
    }
    private void IsWindowOnCommand(WitResponseNode commandResult)
    {
        WitEntityData[] action = commandResult.GetEntities("WindowOpenStatus:WindowOpenStatus");
        if (action == null) return;

        if (action.Length > 0)
        {
            if (action[0].name != null)
            {
                if (action[0].name == "WindowOpenStatus")
                    OutsideLights(true);
            }
        }
    }
    private void IsWindowOffCommand(WitResponseNode commandResult)
    {
        WitEntityData[] action = commandResult.GetEntities("WindowCloseStatus:WindowCloseStatus");
        if (action == null) return;

        if (action.Length > 0)
        {
            if (action[0].name != null)
            {
                if (action[0].name == "WindowCloseStatus")
                    OutsideLights(false);
            }
        }
    }
    private void IsHourCommand(WitResponseNode commandResult)
    {
        WitEntityData[] action = commandResult.GetEntities("hour_number:hour_number");
        if (action == null) return;

        if (action.Length > 0)
        {
            if (action[0].name != null)
            {
                if (action[0].name == "hour_number")
                {
                    int hour = -1;
                    try
                    {
                        hour = int.Parse(action[0].value);
                    }
                    catch
                    {
                        return;
                    }
                    finally 
                    {
                        OutsideLights(true, hour);
                    }
                    
                }     
            }
        }
    }
}

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
                if (action[0].value != null)
                {
                    if (action[0].value == "Allume les lumières")
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
            if (action[0].value != null)
            {
                if (action[0].value == "éteint la lumière" || action[0].value == "éteins la lumière")
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
            if (action[0].value != null)
            {
                if (action[0].value == "Ouvre les rideaux" || action[0].value == "ouvre les rideaux")
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
            if (action[0].value != null)
            {
                if (action[0].value == "Ferme les rideaux" || action[0].value == "ferme les rideaux")
                    OutsideLights(false);
            }
        }
    }
    private void IsHourCommand(WitResponseNode commandResult)
    {
        WitEntityData[] action = commandResult.GetEntities("hour_number:hour_number");
        int hour = 13;
        if (action == null) return;
        Debug.Log(action);

        if (action.Length > 0)
        {
            if (action[0].name != null)
            {
                Debug.Log(action[0].name);
                if (action[0].name == "hour_number")
                {
                    Debug.Log(action[0].value);
                    hour = int.Parse(action[0].value);
                    Debug.Log(hour);
                    OutsideLights(true, hour);
                }     
            }
        }
    }
}

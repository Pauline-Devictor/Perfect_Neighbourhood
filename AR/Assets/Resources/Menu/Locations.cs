using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locations : MonoBehaviour
{
    public Dictionary<string, Vector3> locations = new Dictionary<string, Vector3>();
    void Start()
    {
        locations.Add("JosBugers", new Vector3(-80.6f, 60f, 0));
        locations.Add("Gym", new Vector3(-38.5f, 60f, 0));
        locations.Add("Casino", new Vector3(3.4f, 60f, 0));
        locations.Add("Pizzeria", new Vector3(45.3f, 60f, 0));
        locations.Add("HistoryMuseum", new Vector3(86.6f, 60f, 0));
        locations.Add("GardenShop", new Vector3(128.2f, 60f, 0));
        locations.Add("Market", new Vector3(-79.2f, -66.3f, 0));
        locations.Add("Bank", new Vector3(-38.5f, -27.1f, 0));
        locations.Add("MusicStore", new Vector3(3.4f, -27.1f, 0));
        locations.Add("Doctor", new Vector3(45.3f, -27.1f, 0));
        locations.Add("Theater", new Vector3(86.6f, -27.1f, 0));
        locations.Add("Statue", new Vector3(128.2f, -27.1f, 0));
        locations.Add("PostOffice", new Vector3(-38.5f, -100f, 0));
        locations.Add("Park", new Vector3(23.3f, -100f, 0));
        locations.Add("DimSumResto", new Vector3(86.6f, -100f, 0));
        locations.Add("ShootingRange", new Vector3(128.2f, -100f, 0));
        locations.Add("Fromagerie", new Vector3(-80.6f, -183.3f, 0));
        locations.Add("HappyBar", new Vector3(-38.5f, -183.3f, 0));
        locations.Add("School", new Vector3(3.4f, -183.3f, 0));
        locations.Add("Spa", new Vector3(45.3f, -183.3f, 0));
        locations.Add("Police", new Vector3(86.6f, -183.3f, 0));
        locations.Add("Bakery", new Vector3(128.2f, -183.3f, 0));
        locations.Add("Butchery", new Vector3(-80.6f, -280.8f, 0));
        locations.Add("Telecom", new Vector3(-38.5f, -280.8f, 0));
        locations.Add("Library", new Vector3(3.4f, -280.8f, 0));
        locations.Add("Neighborhood", new Vector3(86.6f, -280.8f, 0));
    }
}

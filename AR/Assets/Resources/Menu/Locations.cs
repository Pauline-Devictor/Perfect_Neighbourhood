using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locations : MonoBehaviour
{
    public Dictionary<string, Vector3> locations = new Dictionary<string, Vector3>();
    void Start()
    {
        float x = 10f;
        float y = 0;
        //correct these locations: add 13f to x, and -25f to y
        locations.Add("JosBugers", new Vector3(-80.6f + x, 60f + y, 0));
        locations.Add("Gym", new Vector3(-38.5f + x, 60f + y, 0));
        locations.Add("Casino", new Vector3(3.4f + x, 60f + y, 0));
        locations.Add("Pizzeria", new Vector3(45.3f + x, 60f + y, 0));
        locations.Add("HistoryMuseum", new Vector3(86.6f + x, 60f + y, 0));
        locations.Add("GardenShop", new Vector3(128.2f + x, 60f + y, 0));

        locations.Add("Market", new Vector3(-80.6f + x, -60.1f + y, 0));

        locations.Add("Bank", new Vector3(-38.5f + x, -27.1f + y, 0));
        locations.Add("MusicStore", new Vector3(3.4f + x, -27.1f + y, 0));
        locations.Add("Doctor", new Vector3(45.3f + x, -27.1f + y, 0));
        locations.Add("Theater", new Vector3(86.6f + x, -27.1f + y, 0));
        locations.Add("Statue", new Vector3(128.2f + x, -27.1f + y, 0));

        locations.Add("PostOffice", new Vector3(-38.5f + x, -100f + y, 0));
        locations.Add("Park", new Vector3(38.5f + x, -100f + y, 0));
        locations.Add("DimSumResto", new Vector3(86.6f + x, -100f + y, 0));
        locations.Add("ShootingRange", new Vector3(128.2f + x, -100f + y, 0));

        locations.Add("Fromagerie", new Vector3(-80.6f + x, -183.3f + y, 0));
        locations.Add("HappyBar", new Vector3(-38.5f + x, -183.3f + y, 0));
        locations.Add("School", new Vector3(3.4f + x, -183.3f + y, 0));
        locations.Add("Spa", new Vector3(45.3f + x, -183.3f + y, 0));
        locations.Add("Police", new Vector3(86.6f + x, -183.3f + y, 0));
        locations.Add("Bakery", new Vector3(128.2f + x, -183.3f + y, 0));

        locations.Add("Butchery", new Vector3(-80.6f + x, -240.8f + y, 0));
        locations.Add("Telecom", new Vector3(-38.5f + x, -240.8f + y, 0));
        locations.Add("Library", new Vector3(3.4f + x, -240.8f, 0));
        locations.Add("Neighborhood", new Vector3(86.6f + x, -240.8f, 0));
    }
}

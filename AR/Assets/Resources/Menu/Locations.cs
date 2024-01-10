using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locations : MonoBehaviour
{
    public Dictionary<string, Vector3> locations = new Dictionary<string, Vector3>();
    void Start()
    {
        locations.Add("JosBugers", new Vector3(-67.3f, 48.9f, 0));
    }
}

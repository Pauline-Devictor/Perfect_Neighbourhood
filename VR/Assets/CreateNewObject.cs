using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateNewObject : MonoBehaviour
{
    [SerializeField] GameObject itemToSpawn;
    [SerializeField] Transform spawningTransform;
    private float count = 0; // bc isOn always return true even if false

    public void CreateNew()
    {
        count++;
        if (itemToSpawn && spawningTransform && count%2 == 1)
        {
            Instantiate(itemToSpawn, spawningTransform);

        }

    }
}
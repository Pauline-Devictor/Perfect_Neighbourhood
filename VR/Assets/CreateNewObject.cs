using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateNewObject : MonoBehaviour
{
    [SerializeField] GameObject itemToSpawn;
    [SerializeField] Transform spawningTransform;

    public void CreateNew()
    {
        Debug.Log("coucou");
        if (itemToSpawn && spawningTransform)
            Instantiate(itemToSpawn, spawningTransform);
    }
}
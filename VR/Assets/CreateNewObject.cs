using UnityEngine;

public class CreateNewObject : MonoBehaviour
{
    [SerializeField] GameObject itemToSpawn;
    [SerializeField] Transform spawningTransform;
    private float count = 0; // bc isOn always return true even if false

    public void CreateNew()
    {
       
            Instantiate(itemToSpawn, spawningTransform);

        

    }
}
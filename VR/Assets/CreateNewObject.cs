using UnityEngine;

public class CreateNewObject : MonoBehaviour
{
    [SerializeField] GameObject itemToSpawn;
    [SerializeField] Transform spawningTransform;
    private float count = 0; // bc isOn always return true even if false

    public void CreateNew()
    {
        count++;
        if (itemToSpawn && spawningTransform && count%2 == 0 && count !=0)
        {
            Instantiate(itemToSpawn, spawningTransform);

        }

    }
}
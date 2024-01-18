using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsList : MonoBehaviour
{
    public TextAsset objectsCSV;
    public List<ObjectSus> objectSus;
    private string[,] objectsArray;

    public class ObjectSus
    {
        public string name;
        public string position;
        public int time;
    }
    
    void Start()
    {
        RetrieveData retrieveData = GetComponent<RetrieveData>();
        objectsArray = retrieveData.SplitCsvGrid(objectsCSV);
        objectSus = new List<ObjectSus>();
        buildList();
    }

    public void buildList()
    {
        for(int i = 1; i < objectsArray.GetLength(0) - 1; i++)
        {
            ObjectSus obj = new ObjectSus();
            obj.name = objectsArray[i, 0];
            obj.position = objectsArray[i, 1];
            obj.time = convertToDigit(objectsArray[i, 2]);
            objectSus.Add(obj);
        }
    }
    
    public int convertToDigit(string hour){
        switch(hour){
            case "12h":
                return 0;
            case "13h":
                return 1;
            case "14h":
                return 2;
            case "15h":
                return 3;
            case "16h":
                return 4;
            case "17h":
                return 5;
            default:
                return -1;
        }
    }
    
}

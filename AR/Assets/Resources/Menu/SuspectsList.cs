using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuspectsList : MonoBehaviour
{
    public TextAsset suspectsCSV;
    public List<Suspect> suspects;
    private string[,] suspectsArray;

    public class Suspect
    {
        public string name;
        public string gender;
        public string onePM;
        public string twoPM;
        public string threePM;
        public string fourPM;
        public string fivePM;
        public string hair;
        public string height;
        public string relation;
        public string clothing;
    }

    void Start()
    {
        RetrieveData retrieveData = GetComponent<RetrieveData>();
        suspectsArray = retrieveData.SplitCsvGrid(suspectsCSV);
        suspects = new List<Suspect>();
        buildList();
    }

    public void buildList()
    {
        for(int i = 1; i < suspectsArray.GetLength(0) - 1; i++)
        {
            Suspect suspect = new Suspect();
            suspect.name = suspectsArray[i, 0];
            suspect.gender = suspectsArray[i, 1];
            suspect.onePM = suspectsArray[i, 2];
            suspect.twoPM = suspectsArray[i, 3];
            suspect.threePM = suspectsArray[i, 4];
            suspect.fourPM = suspectsArray[i, 5];
            suspect.fivePM = suspectsArray[i, 6];
            suspect.hair = suspectsArray[i, 7];
            suspect.height = suspectsArray[i, 8];
            suspect.relation = suspectsArray[i, 9];
            suspect.clothing = suspectsArray[i, 11];
            suspects.Add(suspect);
        }
    }

}

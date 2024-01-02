using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FilteringData : MonoBehaviour
{
    public SuspectsList suspectsList;
    List<SuspectsList.Suspect> suspects;
    List<SuspectsList.Suspect> filteredSuspects;
    
    void Start()
    {
        suspects = new List<SuspectsList.Suspect>();
        filteredSuspects = new List<SuspectsList.Suspect>();
        suspects = suspectsList.suspects;
        FindDistinctHairColors();
    }

    void FindDistinctHairColors()
    {
        List<string> hairColors = new List<string>();
        foreach(SuspectsList.Suspect suspect in suspects)
        {
            if(!hairColors.Contains(suspect.hair))
            {
                hairColors.Add(suspect.hair);
            }
        }
        GameObject.Find("HairFilter").GetComponent<TMP_Dropdown>().AddOptions(hairColors);
    }

}

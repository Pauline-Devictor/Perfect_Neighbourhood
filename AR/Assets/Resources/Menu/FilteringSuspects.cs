using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FilteringSuspects : MonoBehaviour
{
    List<Suspect> suspects;
    public List<Suspect> filteredSuspects;

    void Start()
    {
        StartCoroutine(SetupFilters());
    }

    private void RealStart()
    {
        RetrieveData retrieveData = GetComponent<RetrieveData>();
        filteredSuspects = new List<Suspect>();

        suspects = retrieveData.Suspects;

        FindDistinctHairColors();
        PutDistinctHeightSlots();
        FindDistinctClothes();
        FindDistinctGender();
        FindDistinctRelation();
    }

    IEnumerator SetupFilters()
    {
        yield return new WaitForSeconds(0.5f);
        RealStart();
    }

    /* SETUP THE FILTERS */

    void FindDistinctHairColors()
    {
        List<string> hairColors = new List<string>();
        foreach (Suspect suspect in suspects)
        {
            if (!hairColors.Contains(suspect.hair))
            {
                hairColors.Add(suspect.hair);
            }
        }
        GameObject.Find("HairFilter").GetComponent<TMP_Dropdown>().AddOptions(hairColors);
    }

    void PutDistinctHeightSlots()
    {
        List<string> heights = new List<string>();
        foreach (Suspect suspect in suspects)
        {
            if (!heights.Contains(suspect.height))
            {
                heights.Add(suspect.height);
            }
        }
        heights.Sort();
        List<string> heightsSlots = new List<string>();
        int min = int.Parse(heights[0]);
        int max = int.Parse(heights[heights.Count - 1]);
        int i;
        for (i = min + 5; i <= max; i = i + 5)
        {
            heightsSlots.Add((i - 5).ToString() + " - " + i.ToString());
        }
        if (i - 5 != max)
        {
            heightsSlots.Add((i - 5).ToString() + " - " + max.ToString());
        }
        GameObject.Find("HeightFilter").GetComponent<TMP_Dropdown>().AddOptions(heightsSlots);
    }

    void FindDistinctGender()
    {
        List<string> genders = new List<string>();
        foreach (Suspect suspect in suspects)
        {
            if (!genders.Contains(suspect.gender))
            {
                genders.Add(suspect.gender);
            }
        }
        GameObject.Find("GenderFilter").GetComponent<TMP_Dropdown>().AddOptions(genders);
    }

    void FindDistinctClothes()
    {
        List<string> clothings = new List<string>();
        foreach (Suspect suspect in suspects)
        {
            if (!clothings.Contains(suspect.clothing))
            {
                clothings.Add(suspect.clothing);
            }
        }
        GameObject.Find("ClothesFilter").GetComponent<TMP_Dropdown>().AddOptions(clothings);
    }

    void FindDistinctRelation()
    {
        List<string> relations = new List<string>();
        foreach (Suspect suspect in suspects)
        {
            if (!relations.Contains(suspect.relation))
            {
                relations.Add(suspect.relation);
            }
        }
        GameObject.Find("RelationFilter").GetComponent<TMP_Dropdown>().AddOptions(relations);
    }

    /* FILTER DATA */

    public void FilterTheSuspects()
    {
        filteredSuspects.Clear();
        filteredSuspects.AddRange(suspects);
        FilterByHairColor(GameObject.Find("HairFilter").GetComponent<TMP_Dropdown>());
        FilterByHeight(GameObject.Find("HeightFilter").GetComponent<TMP_Dropdown>());
        FilterByGender(GameObject.Find("GenderFilter").GetComponent<TMP_Dropdown>());
        FilterByClothes(GameObject.Find("ClothesFilter").GetComponent<TMP_Dropdown>());
        FilterByRelation(GameObject.Find("RelationFilter").GetComponent<TMP_Dropdown>());
        GameObject placeholder = GameObject.Find("ScrollSuspectsNames/Viewport/Suspects");
        placeholder.GetComponent<TextMeshProUGUI>().text = "";
        foreach (Suspect suspect in filteredSuspects)
        {
            placeholder.GetComponent<TMPro.TextMeshProUGUI>().text += suspect.name + "\n";
        }
        GetComponent<FilteringPositions>().suspects = filteredSuspects;
        GetComponent<FilteringPositions>().FilteringPositionsByTime();
    }

    private void FilterByHairColor(TMP_Dropdown dropdown)
    {
        if (dropdown.value == 0) return;

        string hairColor = dropdown.options[dropdown.value].text;
        filteredSuspects.RemoveAll(suspect => suspect.hair != hairColor);
    }

    private void FilterByHeight(TMP_Dropdown dropdown)
    {
        if (dropdown.value == 0) return;
        string height = dropdown.options[dropdown.value].text;
        List<string> heightRange = new List<string>();
        heightRange.AddRange(height.Split(' '));
        filteredSuspects.RemoveAll(suspect => int.Parse(suspect.height) < int.Parse(heightRange[0]) || int.Parse(suspect.height) > int.Parse(heightRange[2]));
    }

    private void FilterByGender(TMP_Dropdown dropdown)
    {
        if (dropdown.value == 0) return;

        string gender = dropdown.options[dropdown.value].text;
        filteredSuspects.RemoveAll(suspect => suspect.gender != gender);
    }

    private void FilterByClothes(TMP_Dropdown dropdown)
    {
        if (dropdown.value == 0) return;

        string clothes = dropdown.options[dropdown.value].text;
        filteredSuspects.RemoveAll(suspect => suspect.clothing != clothes);
    }

    private void FilterByRelation(TMP_Dropdown dropdown)
    {
        if (dropdown.value == 0) return;
        string relation = dropdown.options[dropdown.value].text;
        filteredSuspects.RemoveAll(suspect => suspect.relation != relation);
    }

}
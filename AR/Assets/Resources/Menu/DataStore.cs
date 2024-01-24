using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class DataStore : MonoBehaviour
{
    public TextAsset susCsvFile;
    public TextAsset objCsvFile;
    private List<Suspect> suspects = null;
    private string[,] suspectsArray;


    public List<Suspect> Suspects
    {
        get
        {
            if (suspects == null)
            {
                suspectsArray = SplitCsvGrid(susCsvFile);
                suspects = new List<Suspect>();
                BuildSusList();
            }
            return suspects;
        }
    }
    private List<ObjectSus> objects = null;
    private string[,] objectsArray;

    public List<ObjectSus> Objects
    {
        get
        {
            if (objects == null)
            {
                objectsArray = SplitCsvGrid(objCsvFile);
                objects = new List<ObjectSus>();
                BuildObjectList();
            }
            return objects;
        }
    }

    public string[,] SplitCsvGrid(TextAsset csvFile)
    {
        string csvText = csvFile.text;
        string[] lines = csvText.Split("\n"[0]);

        //find max rows and columns
        int rows = lines.Length;
        int cols = 0;
        for (int i = 0; i < lines.Length; i++)
        {
            string[] row = SplitCsvLine(lines[i]);
            cols = Mathf.Max(cols, row.Length);
        }

        //create new 2D string grid to output to
        string[,] outputGrid = new string[rows, cols];
        for (int i = 0; i < lines.Length; i++)
        {
            string[] row = SplitCsvLine(lines[i]);
            for (int j = 0; j < row.Length; j++)
            {
                outputGrid[i, j] = row[j];
            }
        }

        return outputGrid;
    }

    private string[] SplitCsvLine(string line)
    {
        return (from System.Text.RegularExpressions.Match m in System.Text.RegularExpressions.Regex.Matches(line,
                       @"(((?<x>(?=[,\r\n]+))|""(?<x>([^""]|"""")+)""|(?<x>[^,\r\n]+)),?)",
                                  System.Text.RegularExpressions.RegexOptions.ExplicitCapture)
                select m.Groups[1].Value).ToArray();
    }

    void BuildSusList()
    {
        for (int i = 1; i < suspectsArray.GetLength(0) - 1; i++)
        {
            Suspect suspect = new()
            {
                name = suspectsArray[i, 0],
                gender = suspectsArray[i, 1]
            };
            suspect.positions[0] = suspectsArray[i, 2];
            suspect.positions[1] = suspectsArray[i, 3];
            suspect.positions[2] = suspectsArray[i, 4];
            suspect.positions[3] = suspectsArray[i, 5];
            suspect.positions[4] = suspectsArray[i, 6];
            suspect.hair = suspectsArray[i, 7];
            suspect.height = suspectsArray[i, 8];
            suspect.relation = suspectsArray[i, 9];
            suspect.clothing = suspectsArray[i, 11];
            suspects.Add(suspect);
        }
    }

    void BuildObjectList()
    {
        for (int i = 1; i < objectsArray.GetLength(0) - 1; i++)
        {
            ObjectSus obj = new ObjectSus();
            obj.name = objectsArray[i, 0];
            obj.position = objectsArray[i, 1];
            obj.time = ConvertToDigit(objectsArray[i, 2]);
            Objects.Add(obj);
        }
    }

    int ConvertToDigit(string hour)
    {
        return hour switch
        {
            "12h" => 0,
            "13h" => 1,
            "14h" => 2,
            "15h" => 3,
            "16h" => 4,
            "17h" => 5,
            _ => -1,
        };
    }
}
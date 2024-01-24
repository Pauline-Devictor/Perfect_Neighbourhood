using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class RetrieveData : MonoBehaviour
{
    public TextAsset susCsvFile;
    public TextAsset objCsvFile;
    public string[,] result;
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
                buildSusList();
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
                buildObjectList();
            }
            return objects;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
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

    void buildSusList()
    {
        for (int i = 1; i < suspectsArray.GetLength(0) - 1; i++)
        {
            Suspect suspect = new Suspect();
            suspect.name = suspectsArray[i, 0];
            suspect.gender = suspectsArray[i, 1];
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

    void buildObjectList()
    {
        for (int i = 1; i < objectsArray.GetLength(0) - 1; i++)
        {
            ObjectSus obj = new ObjectSus();
            obj.name = objectsArray[i, 0];
            obj.position = objectsArray[i, 1];
            obj.time = convertToDigit(objectsArray[i, 2]);
            Objects.Add(obj);
        }
    }

    int convertToDigit(string hour)
    {
        switch (hour)
        {
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class RetrieveData : MonoBehaviour
{
    TextAsset csvFile;
    public string[,] result;

    // Start is called before the first frame update
    void Start()
    {
    }

    public string[,] SplitCsvGrid(TextAsset csvFile)
    {
        this.csvFile = csvFile;
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
}
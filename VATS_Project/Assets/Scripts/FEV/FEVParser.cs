using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Parser class used to read data from CSV files
public class FEVParser : MonoBehaviour
{
    void Start()
    {
        // Load csv file from Resources folder
        TextAsset fevFile = Resources.Load<TextAsset>("FEVs/sample1");

        // Split document into rows
        string[] fevLines = fevFile.text.Split("\n"[0]);

        for (int i = 1; i < fevLines.Length; i++)
        {
            string[] fevData = (fevLines[i].Trim()).Split(","[0]);
            for (int j = 1; j < fevData.Length; j++)
            {
                Debug.Log(fevData[j]);
            }  
        }
    }

    
}

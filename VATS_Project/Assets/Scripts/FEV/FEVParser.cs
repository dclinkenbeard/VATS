using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

using System.IO;
using LitJson;

// Parser class used to read data from XML files
public class FEVParser : MonoBehaviour
{
    void Start()
    {
        loadData();
    }


    void loadData()
    {
        TextAsset fevFile = Resources.Load<TextAsset>("FEVs/EelSample");
        XmlSerializer fevSerializer = new XmlSerializer(typeof(FEV));
        using (StringReader fevReader = new StringReader(fevFile.text))
        {
            FEV fev = (fevSerializer.Deserialize(fevReader)) as FEV;
            SubmitFEV(fev);
        }
    }

    // Temp function for turning FEV data into fish. Will be part of a loader class in the future 
    public void SubmitFEV(FEV fev)
    {
        string path = Application.dataPath + "/JSON/Fish_Encyclopedia.JSON.txt";
        string json = File.ReadAllText(path);
        JsonData itemData = new JsonData();

        itemData = JsonMapper.ToObject(json);
        string newJson = JsonMapper.ToJson(fev);

        itemData["fish"].Add(JsonMapper.ToObject(newJson));


        // Writes data to JSON file in a readable format
        JsonWriter fevWriter = new JsonWriter();
        fevWriter.PrettyPrint = true;
        JsonMapper.ToJson(itemData, fevWriter);
        // File.WriteAllText(path, fevWriter.ToString());

        Debug.Log(JsonMapper.ToJson(itemData));;
    }


    
}

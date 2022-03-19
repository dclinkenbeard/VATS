using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

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
        }
    }

    
}

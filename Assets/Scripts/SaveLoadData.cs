using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;



public class SaveLoadData {

    public static List<PlayerTime> dataB = new List<PlayerTime>();

    public string LoadData(string filePath, string fileName){

        // Verificando existencia del archivo json a cargar
        string fullPath = "C:/Users/avlui/OneDrive/Documentos/Games/Timer/Assets"+ "/" + filePath + "/" + fileName + ".json";
        if(File.Exists(fullPath)){

            // Cargando archivo json
            string textJson = File.ReadAllText(fullPath);
            Debug.Log("Data load susefully.");
            return textJson;
        }
        else{
            Debug.Log("File no found");
            return default;
        }

    }

    public void UpdateData(PlayerTime newData, string filePath, string fileName){

        // Verificando existencia del archivo json a cargar
        string fullPath = "C:/Users/avlui/OneDrive/Documentos/Games/Timer/Assets" + "/" + filePath + "/" + fileName + ".json";
        if(!File.Exists(fullPath)){
            File.Create(fullPath);
        }
        // Actualizando archivo json
        string jSon = File.ReadAllText(fullPath);
        List<PlayerTime> newDataList = JsonConvert.DeserializeObject<List<PlayerTime>>(jSon);
        newDataList.Add(newData);
        
        var dataToAdd = JsonConvert.SerializeObject(newDataList);
        File.WriteAllText(fullPath, dataToAdd);

        string textJson = File.ReadAllText(fullPath);
        Debug.Log("File update: " + textJson );
    }

}

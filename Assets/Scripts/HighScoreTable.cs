using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class HighScoreTable : MonoBehaviour{

    private Transform entryObj;
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> highScoreEntryTransformList;

    SaveLoadData DB;
    private const string pathData = "Data/test";
    private const string nameFileData = "DataTimes";
    private List<PlayerTime> data;
    private string lastLoadData;

    private void Awake(){
        entryObj = GameObject.Find("HighScoreTable").transform;
        entryContainer = entryObj.Find("HighScoreEntryContainer");
        entryTemplate = entryContainer.Find("HighScoreTemplate");


        entryTemplate.gameObject.SetActive(false);  

        DB = new SaveLoadData();
        lastLoadData= DB.LoadData(pathData, nameFileData);
        data = JsonConvert.DeserializeObject<List<PlayerTime>>(lastLoadData);

        //Sort data by score (or time) ascending
        for(int i=0; i<data.Count; i++){
            for(int j = i+1; j <data.Count; j++){
                if(data[j].time < data[i].time){
                    
                    //swap
                    PlayerTime  temp = data[i];
                    data[i] = data[j];
                    data[j] = temp;
                }
            }
        }

        //Print Scores
        highScoreEntryTransformList = new List<Transform>();
        foreach(PlayerTime highScoreEntry in data){
            CreateHighScoreEntryTransform(highScoreEntry, entryContainer, highScoreEntryTransformList);
        }
    }

    private void CreateHighScoreEntryTransform(PlayerTime highScoreEntry, Transform container, List<Transform> transformList ){

        float templateHeight = 35f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight*transformList.Count);
        entryTransform.gameObject.SetActive(true);

        // En la tabla se agregan maximo 20 registros.
        if(transformList.Count <=19){
            int rank = transformList.Count+1;
            string rankString;
            switch(rank){
                case 1: rankString = "1ST"; break;
                case 2: rankString = "2ND"; break;
                case 3: rankString = "3RD"; break;

                default: rankString = rank + "TH"; break;
            } 
            entryTemplate.Find("PosText").GetComponent<Text>().text = rankString;

            string time = highScoreEntry.watchTime;
            entryTemplate.Find("ScoreText").GetComponent<Text>().text = time;

            string name = highScoreEntry.name;
            entryTemplate.Find("NameText").GetComponent<Text>().text = name;

            if(rank == 1){
                entryTransform.Find("Trophy").gameObject.SetActive(true);
            }
            else{
                entryTransform.Find("Trophy").gameObject.SetActive(false);
            }

            entryTransform.Find("ScoreBG").gameObject.SetActive(rank % 2 == 1);
            transformList.Add(entryTransform);
        }
        
    }
}

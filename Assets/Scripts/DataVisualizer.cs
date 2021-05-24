﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using SimpleJSON;
using Microsoft.MixedReality.Toolkit.UI;

public class DataVisualizer : MonoBehaviour
{
    public PressableButtonHoloLens2[] DateButtons;
    public PressableButtonHoloLens2[] RegionalButtons;
    public PressableButtonHoloLens2 SelectedDate = null;
    public PressableButtonHoloLens2 SelectedRegion = null;
    public string CurrentDate;
    public string CurrentRegion;

    public int totalCases = 0;
    public int totalConfirmedRegional = 0;
    public int confirmedCasesIndian = 0;
    public int confirmedCasesForeign = 0;
    public int discharged = 0;
    public int deaths = 0;

    public GameObject RegionPanel;

    private JSONArray dataArray;
    private const string URL = "https://api.rootnet.in/covid19-in/stats/history";

    public IEnumerator LoadJSONfromWeb()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(URL)) {
            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                print(request.error);
            }

            else
            {
                if (request.isDone)
                {
                    JSONNode jsonData = JSON.Parse(System.Text.Encoding.UTF8.GetString(request.downloadHandler.data));
                    if (jsonData == null)
                    {
                        print("No Data");
                    }
                    else
                    {
                        dataArray = jsonData["data"].AsArray;
                    }
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadJSONfromWeb());
    }

    public void PrintData(PressableButtonHoloLens2 SelectedButton)
    {
        RegionPanel.SetActive(true);
        SelectedDate = SelectedButton;
        CurrentDate = SelectedDate.GetComponent<ButtonManager>().Label;
        for(int i = 0; i<dataArray.Count; i++)
        {
            if(CurrentDate == dataArray[i]["day"].Value)
            {
                totalCases = dataArray[i]["summary"]["total"].AsInt;
                print("totalCases" + totalCases);
                deaths = dataArray[i]["summary"]["deaths"].AsInt;
                print("deaths" + deaths);
                confirmedCasesIndian = dataArray[i]["summary"]["confirmedCasesIndian"].AsInt;
                print("confirmedCasesIndian" + confirmedCasesIndian);
                confirmedCasesForeign = dataArray[i]["summary"]["confirmedCasesForeign"].AsInt;
                print("confirmedCasesForeign" + confirmedCasesForeign);
                discharged = dataArray[i]["summary"]["discharged"].AsInt;
                print("discharged" + discharged);
            }
        }
    }

    public void PrintRegionalData(PressableButtonHoloLens2 SelectedRegionButton)
    {
        SelectedRegion = SelectedRegionButton;
        CurrentRegion = SelectedRegion.GetComponent<ButtonManager>().Label;

        for (int j = 0; j < dataArray.Count; j++)
        {
            for (int i = 0; i < dataArray[j]["regional"].Count; i++)
            {
                if (dataArray[j]["regional"][i]["loc"] == CurrentRegion && dataArray[j]["day"]== CurrentDate)
                {
                    confirmedCasesIndian = dataArray[j]["regional"][i]["confirmedCasesIndian"].AsInt;
                    confirmedCasesForeign = dataArray[j]["regional"][i]["confirmedCasesForeign"].AsInt;
                    deaths = dataArray[j]["regional"][i]["deaths"].AsInt;
                    discharged = dataArray[j]["regional"][i]["discharged"].AsInt;
                    totalConfirmedRegional = dataArray[j]["regional"][i]["totalConfirmed"].AsInt;
                    print(confirmedCasesIndian);
                    print(confirmedCasesForeign);
                    print(discharged);
                    print(deaths);
                    print(totalConfirmedRegional);
                }
            }
        }
    }
}

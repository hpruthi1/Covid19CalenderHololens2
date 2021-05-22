using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using SimpleJSON;
using Microsoft.MixedReality.Toolkit.UI;

public class DataVisualizer : MonoBehaviour
{
    public PressableButtonHoloLens2[] DateButtons;
    public string Date;

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

    public void PrintData()
    {
        for (int i = 0; i<DateButtons.Length; i++)
        {
            Date = DateButtons[i].GetComponentInChildren<TextMeshPro>().text;
            if(Date == dataArray[dataArray.Count - 1]["day"])
            {
                print(dataArray[dataArray.Count-1]["summary"]["total"]);
            }
        }
    }
}

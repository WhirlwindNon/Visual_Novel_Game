using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class OllamaAPI : MonoBehaviour
{
    private IEnumerator GetSimpleRequest()
    {

        var json = " {\n    \"model\": \"llama3.2-vision:11b\",\n    \"messages\": [\n        {\n            \"role\": \"user\",\n            \"content\": \"привет как дела?\"\n        }\n    ],\n    \"stream\": false\n}\n ";
        using (UnityWebRequest www = UnityWebRequest.Post("http://www.phimosis.space:3002/api/chat", json, "application/json"))
        {
            Debug.Log("ready");
            yield return www.SendWebRequest();
            
            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log(www.downloadHandler.text);
            }
            else
            {
                Debug.Log(www.error);
            }
        }
    }

}

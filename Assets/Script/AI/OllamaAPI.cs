using System.Collections;

using UnityEngine;
using UnityEngine.Networking;

namespace VisualNovelGame
{
    public class OllamaAPI : MonoBehaviour
    {
        private IEnumerator GetSimpleRequest()
        {
            var json =
                " {\n    \"model\": \"llama3.2-vision:11b\",\n    \"messages\": [\n        {\n            \"role\": \"user\",\n            \"content\": \"привет как дела?\"\n        }\n    ],\n    \"stream\": false\n}\n ";

            using (var www =
                   UnityWebRequest.Post("http://ollama.phimosis.space", json, "application/json"))
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
}
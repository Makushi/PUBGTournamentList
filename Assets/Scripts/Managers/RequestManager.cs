using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class RequestManager : MonoBehaviour
{
    private string url = "https://api.pubg.com/tournaments";
    private void OnEnable()
    {
        EventManager.onListInitialized += SendRequest;
    }

    private void OnDisable()
    {
        EventManager.onListInitialized -= SendRequest;
    }

    //This function fires the request to the PUBG API
    public void SendRequest()
    {
        //Notify that a request was sent (disable inputs, show loading screen, etc)
        EventManager.RequestSent();
        StartCoroutine(SendAPIRequest());
    }

    IEnumerator SendAPIRequest()
    {
        //Setting up the request url
        UnityWebRequest www = UnityWebRequest.Get(url);

        //Content type
        www.SetRequestHeader("Content-Type", "application/json");

        //Accept field
        www.SetRequestHeader("accept", "application/vnd.api+json");

        //API Key
        www.SetRequestHeader("Authorization", "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJqdGkiOiI0OTEwYmM3MC02NTgwLTAxMzktNjcxMi0wMTE1YzJjNDNmZWIiLCJpc3MiOiJnYW1lbG9ja2VyIiwiaWF0IjoxNjE1NTY3NTMyLCJwdWIiOiJibHVlaG9sZSIsInRpdGxlIjoicHViZyIsImFwcCI6Ii01OTVjYWQ2NS03MmFlLTRlOTktYTgxNS0wZmI5ZDJlOWM1OTkifQ.b53TzN5f77Is5ArZ4X5CZU5UDZrkzt5-wrnP1V8r9vk");

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError)
        {
            //Show error if request failed
            Debug.Log(www.error);
        }
        else
        {
            //If request was successful
            //Parse JSON data
            Tournaments tournaments = JsonUtility.FromJson<Tournaments>(www.downloadHandler.text.ToString());

            //Send data to the list manager
            EventManager.FillList(tournaments);

            //Notify that the request was completed successfully
            EventManager.RequestCompleted();
        }
    }
}

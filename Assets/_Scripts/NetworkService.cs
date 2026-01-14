using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using static System.Net.WebRequestMethods;

public class NetworkService
{
    private const string webImage = "https://upload.wikimedia.org/wikipedia/commons/c/c5/Moraine_Lake_17092005.jpg";

    private const string jsonApi = "https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/Paris?unitGroup=metric&contentType=json&key=5869LJUHW5XPFWVFP9KN9UUQV";
   

    private IEnumerator CallAPI(string url, WWWForm form, Action<string> callback)
    {
        using (UnityWebRequest request = (form == null) ?
            UnityWebRequest.Get(url) : UnityWebRequest.Post(url, form))
        {

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError($"network problem: {request.error}");
            }
            else if (request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"response error: {request.responseCode}");
            }
            else
            {
                callback(request.downloadHandler.text);
            }
        }
    }

    //public IEnumerator GetWeatherXML(Action<string> callback)
    //{
    //    return CallAPI(xmlApi, null, callback);
    //}
    //public IEnumerator GetWeatherJSON(Action<string> callback)
    //{
    //    return CallAPI(jsonApi, null, callback);
    //}

    //public IEnumerator LogWeather(string name, float cloudValue, Action<string> callback)
    //{
    //    WWWForm form = new WWWForm();
    //    form.AddField("message", name);
    //    form.AddField("cloud_value", cloudValue.ToString());
    //    form.AddField("timestamp", DateTime.UtcNow.Ticks.ToString());

    //    return CallAPI(localApi, form, callback);
    //}
    public IEnumerator DownloadImage(Action<Texture2D> callback)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(webImage);
        yield return request.SendWebRequest();
        callback(DownloadHandlerTexture.GetContent(request));
    }
}
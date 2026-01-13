using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }

    private NetworkService network;

    public void Startup(NetworkService service)
    {
        Debug.Log("Weather manager starting...");

        network = service;
        StartCoroutine(network.GetWeatherXML(OnXMLDataLoaded));
        status = ManagerStatus.Initializing;
    }

    public void OnXMLDataLoaded(string data)
    {
        Debug.Log(data);

        status = ManagerStatus.Started;
    }
}


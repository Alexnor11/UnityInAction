using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
[RequireComponent (typeof(InventoryManager))]
[RequireComponent(typeof(MissionManager))]
[RequireComponent(typeof(DataManager))]

public class Managers : MonoBehaviour
{
    public static PlayerManager Player {  get; private set; }
    public static MissionManager Mission { get; private set; }
    public static InventoryManager Inventory { get; private set; }    
    public static DataManager Data {  get; private set; }

    private List<IGameManager> startSequence;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        Player = GetComponent<PlayerManager>();
        Mission = GetComponent<MissionManager>();
        Inventory = GetComponent<InventoryManager>();
        Data = GetComponent<DataManager>();
        

        startSequence = new List<IGameManager>();
        startSequence.Add(Player);
        startSequence.Add(Mission);
        startSequence.Add(Inventory);
        startSequence.Add(Data);

        StartCoroutine(StartupManager());
    }

    private IEnumerator StartupManager()
    {
        NetworkService network = new NetworkService();
        
        foreach(IGameManager manager in startSequence)
        {
            manager.Startup(network);
        }
        yield return null;

        int numModules = startSequence.Count;
        int numReady = 0;

        while(numReady < numModules)
        {
            int lastReady = numReady;
            numReady = 0;

            foreach(IGameManager manager in startSequence)
            {
                if(manager.status == ManagerStatus.Started)
                {
                    numReady++;
                }
            }
            if (numReady > lastReady)
                Debug.Log($"Progress: {numReady}/{numModules}");
                Messenger<int, int>.Broadcast(StartupEvent.MANAGERS_PROGRESS,
                    numReady, numModules);
            yield return null;
        }
        Debug.Log("All manager started up");
        Messenger.Broadcast(StartupEvent.MANAGERS_STARTED);
    }
}

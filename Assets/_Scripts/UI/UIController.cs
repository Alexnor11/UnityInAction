using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] TMP_Text healthLabel;
    [SerializeField] InventoryPopup popup;
    [SerializeField] TMP_Text levelEnding;

    private void OnEnable()
    {
        Messenger.AddListener(GameEvent.HEALTH_UPDATED, OnHealthUpdated);
        Messenger.AddListener(GameEvent.LEVEL_COMPLETE, OnLevelComplete);
    }

    private void OnDisable()
    {
        Messenger.RemoveListener(GameEvent.HEALTH_UPDATED, OnHealthUpdated);
        Messenger.RemoveListener(GameEvent.LEVEL_COMPLETE, OnLevelComplete);
    }

    private void Start()
    {
        OnHealthUpdated();
        popup.gameObject.SetActive(false);
        levelEnding.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            bool isShowing = popup.gameObject.activeSelf;
            popup.gameObject.SetActive(!isShowing);
            popup.Refresh();
        }
    }    

    private void OnHealthUpdated()
    {
        string massage = $"Health {Managers.Player.health}/{Managers.Player.maxHealth}";
        healthLabel.text = massage;
    }

    private void OnLevelComplete()
    {
        StartCoroutine(CompleteLevel());
    }

    private IEnumerator CompleteLevel()
    {
        levelEnding.gameObject.SetActive (true);
        levelEnding.text = "Level Complete";

        yield return new WaitForSeconds(2);
        Managers.Mission.GoToNext();
    }
}

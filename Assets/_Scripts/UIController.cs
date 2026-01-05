using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] TMP_Text scoreLabel;

    private void Update()
    {
        scoreLabel.text = Time.realtimeSinceStartup.ToString();
    }

    public void OnOpenSettings()
    {
        Debug.Log("open settings");
    }
}

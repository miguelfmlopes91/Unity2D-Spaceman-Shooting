using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField]
    private Text healthText;
    [SerializeField]
    private Text speedText;

    private PlayerStats stats;

    [SerializeField]
    private float healthMultiplier = 1.3f;

    private void OnEnable() {
        stats = PlayerStats.instance;
        UpdateValues();
    }
    void UpdateValues(){
        healthText.text = "HEALTH: " + stats.maxHealth.ToString();
        speedText.text = "SPEED: " + stats.movementSpeed.ToString();
    }

    public void UpgradeHealth(){
        stats.maxHealth =(int)(stats.maxHealth * healthMultiplier);
        Debug.Log("Upgrading to " + stats.maxHealth.ToString());
        UpdateValues();
    }
    public void UpgradeSpeed(){
        stats.movementSpeed =(int)(stats.movementSpeed * healthMultiplier);
        Debug.Log("Upgrading to " + stats.movementSpeed.ToString());
        UpdateValues();
    }
}

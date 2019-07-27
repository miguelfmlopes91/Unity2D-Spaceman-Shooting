using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    string hoverOverSound = "ButtonHover";
        [SerializeField]
    string pressedButtonSound = "ButtonPress";

    Bardo.AudioManager audioManager;

    private void Start() {
        audioManager = Bardo.AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("No AudioManager found");
        }
    }
   public void StartGame(){
       audioManager.PlaySound(pressedButtonSound);
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
   }

   public void QuitGame(){
        audioManager.PlaySound(pressedButtonSound);
        Application.Quit();
        Debug.Log("We quit the game");
   }

   public void OnMouseOver() 
   {
       audioManager.PlaySound(hoverOverSound);
   }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Animator transition;
    public void StartGame()
    {
        transition.SetTrigger("start");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame()
    {
        Application.Quit(); 
    }
    public void BackToMainMenu()
    {
        if(Input.GetKey(KeyCode.Escape)) { SceneManager.LoadScene(0); }
    }
}

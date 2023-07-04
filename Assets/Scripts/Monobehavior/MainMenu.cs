using CortexDeveloper.ECSMessages.Service;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Animator transition;
    private EntityManager _entityManager;
    public GameObject panel;
    public GameObject txt_score;
    private void Start()
    {
        _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
    }
    public void Update()
    {
        if(Input.GetKey(KeyCode.K))
        {
            StartGame();
        }
    }
    public void StartGame()
    {   
        transition.SetTrigger("start");
        panel.gameObject.SetActive(false);
        txt_score.gameObject.SetActive(true);
        MessageBroadcaster.PrepareMessage().AliveForUnlimitedTime().PostImmediate(_entityManager, new StartCommand
        {
           startGame = true,
        });        
    }
    public void QuitGame()
    {
        Application.Quit(); 
    }
    public void BackToMainMenu()
    {
        
    }
}

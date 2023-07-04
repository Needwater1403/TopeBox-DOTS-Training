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
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject txt_score;
    [SerializeField] private GameObject btn_back;
    [SerializeField] private GameObject txt_dead;
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
        btn_back.gameObject.SetActive(true);
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
        panel.gameObject.SetActive(true);
        txt_score.gameObject.SetActive(false);
        btn_back.gameObject.SetActive(false);
        txt_dead.gameObject.SetActive(false);
        MessageBroadcaster.PrepareMessage().AliveForOneFrame().PostImmediate(_entityManager, new ResetCommand
        {
            resetGame = true,
        });
        MessageBroadcaster.RemoveAllMessagesWith<StartCommand>(_entityManager);
    }
}

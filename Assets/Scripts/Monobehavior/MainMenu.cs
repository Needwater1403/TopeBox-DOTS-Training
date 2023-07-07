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
    [SerializeField] private GameObject txt_win;
    private bool canSpawnBoss;
    private Entity _scoreBoard;
    private Entity _winStatus;
    private void Start()
    {
        _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        _scoreBoard = _entityManager.CreateEntityQuery(typeof(ScoreComponent)).GetSingletonEntity();
        _winStatus = _entityManager.CreateEntityQuery(typeof(WinStatus)).GetSingletonEntity();
        canSpawnBoss = true;
    }
    void Update()
    {
        var score = _entityManager.GetComponentData<ScoreComponent>(_scoreBoard).score;
        var status = _entityManager.GetComponentData<WinStatus>(_winStatus).isWin;
        if (score >= 100 && canSpawnBoss)
        {
            MessageBroadcaster.PrepareMessage().AliveForOneFrame().PostImmediate(_entityManager, new SpawnBossCommand
            {
                spawn = true,
            });
            canSpawnBoss = false;
        }
        if(status)
        {
            txt_win.gameObject.SetActive(true);
        }
        else
        {
            txt_win.gameObject.SetActive(false);
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

        MessageBroadcaster.PrepareMessage().AliveForOneFrame().PostImmediate(_entityManager, new ResetCommand
        {
            resetGame = true,
        });
        MessageBroadcaster.RemoveAllMessagesWith<StartCommand>(_entityManager);
        canSpawnBoss = true;
    }
}

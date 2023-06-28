using Components;
using System.Collections;
using System.Collections.Generic;
using Systems;
using TMPro;
using Unity.Entities;
using Unity.Scenes;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txt_score;
    [SerializeField] private TextMeshProUGUI txt_dead;
    private EntityManager _entityManager;
    private Entity _scoreBoard;
    private Entity _player;
    
    private void Start()
    {
        _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        //yield return new WaitForSeconds(1f);
        _scoreBoard = _entityManager.CreateEntityQuery(typeof(ScoreComponent)).GetSingletonEntity();
        _player = _entityManager.CreateEntityQuery(typeof(PlayerDeadStatus)).GetSingletonEntity();
    }

    void Update()
    {
        var score = _entityManager.GetComponentData<ScoreComponent>(_scoreBoard).score;
        var isDead = _entityManager.GetComponentData<PlayerDeadStatus>(_player).isDead;
        txt_score.text = "Score: " + score.ToString();
        if(isDead) 
        {
            txt_dead.gameObject.SetActive(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public enum GameStates
    {
        INTRO,
        GAMEPLAY,
        PAUSE,
        WIN,
        LOSE
    }

    public StateMachine<GameStates> stateMachine;

    // Start is called before the first frame update
    void Start()
    {
        stateMachine = new StateMachine<GameStates>();
        stateMachine.Init();
        InitStates();
        stateMachine.SwitchState(GameStates.INTRO);
    }
    private void InitStates()
    {
        stateMachine.RegisterStates(GameStates.INTRO, new GMStateIntro());
        stateMachine.RegisterStates(GameStates.GAMEPLAY, new GMStateGameplay());
        stateMachine.RegisterStates(GameStates.PAUSE, new GMStatePause());
        stateMachine.RegisterStates(GameStates.WIN, new GMStateWin());
        stateMachine.RegisterStates(GameStates.LOSE, new GMStateLose());
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T>
{
    public IState<T> CurState { get; protected set; }

    private T m_sender;

    // 스테이트의 상태를 설정
    public StateMachine(T sender, IState<T> state)
    {
        m_sender = sender;
        SetState(state);
    }

    // sender가 비어있다면 리턴
    public void SetState(IState<T> state)
    {
        if (m_sender == null)
        {
          //  Debug.LogError("invalid m_sender");
            return;
        }

        // curstate가 state라면? 리턴
        if (CurState == state)
        {
          //  Debug.LogWarningFormat("Already Define State - {0}", state);
            return;
        }

        if (CurState != null)
            CurState.OnExit(m_sender);

        CurState = state;

        if (CurState != null)
            CurState.OnEnter(m_sender);
    }

    public void OnFixedUpdate()
    {
        if (m_sender == null)
        {
           // Debug.LogError("invalid m_sener");
            return;
        }
        CurState.OnFixedUpdate(m_sender);
    }

    public void OnUpdate()
    {
        if (m_sender == null)
        {
          //  Debug.LogError("invalid m_sener");
            return;
        }
        CurState.OnUpdate(m_sender);
    }
}

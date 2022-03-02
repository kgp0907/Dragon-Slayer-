using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EStateMachine<T>
{
    public EState<T> CurState { get; protected set; }

    public T m_sender;

  
    // 스테이트의 상태를 설정
    public EStateMachine(T sender, EState<T> enemystate)
    {
        m_sender = sender;
    }
 
    // sender가 비어있다면 리턴
    public void SetState(EState<T> enemystate)
    {
        if (m_sender == null)
        {
            return;
        }
        if (CurState == enemystate)
        {
            return;
        }

        if (CurState != null)
            CurState.OnExit(m_sender);

        CurState = enemystate;

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
            return;
        }
        CurState.OnUpdate(m_sender);
    }
}

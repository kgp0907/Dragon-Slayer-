using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;
public class Player : MonoBehaviour
{
    public enum eState
    {
        MOVE,
        BUFF,
        SPATK,
        NORMALATK1,
        NORMALATK2,
        NORMALATK3,
        CHARGEATK,
        DODGE,
        HIT,
        DEAD,
    }
    public string a_id;
    private StateMachine<Player> p_sm;
    private Dictionary<eState, IState<Player>> p_states = new Dictionary<eState, IState<Player>>();
    public bool AnimationName => PlayerAnimator.GetCurrentAnimatorStateInfo(0).IsName(a_id);
    public float AnimationProgress => PlayerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    public Player_TakeDamage p_takedamage;
    [HideInInspector]
    public Vector3 targetPosition;
    public Animator PlayerAnimator;
    public Player_InputManagement inputmanager;
    public CharacterController characterController;
    public Transform target;
    public bool atking = false;
    public bool Lockon=false;
    public bool Hit;
    public bool SmashHit;
    public bool attaking=false;
    public static float PlayerDamage = 5f;
    public GameObject AtkColision;
    public GameObject DeadAtkColision;
    public GameObject[] WeaponPos;
    public GameObject BuffPos;
    public GameObject Character;
    public GameObject RagDoll;
    public ParticleSystem BurnHit;
    public ParticleSystem MagicSword;
    public Rigidbody RagdollPoint;
    void Start()
    {
        RagDoll.SetActive(false);
        MagicSword.Stop();
        BurnHit.Stop();
        p_takedamage = GetComponent<Player_TakeDamage>();
        inputmanager = GetComponent<Player_InputManagement>();
        PlayerAnimator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        AtkColision.SetActive(false);
        DeadAtkColision.SetActive(false);
        p_states.Add(eState.MOVE, new StateMove());
        p_states.Add(eState.SPATK, new State_SpAtk());
        p_states.Add(eState.BUFF, new State_Buff());
        p_states.Add(eState.NORMALATK1, new State_NormalAtk1());
        p_states.Add(eState.NORMALATK2, new State_NormalAtk2());
        p_states.Add(eState.NORMALATK3, new State_NormalAtk3());
        p_states.Add(eState.CHARGEATK, new State_ChargeAtk());
        p_states.Add(eState.DODGE, new StateDodge());
        p_states.Add(eState.DEAD, new StateDead());
        p_states.Add(eState.HIT, new StateHit());
        p_sm = new StateMachine<Player>(this, p_states[eState.MOVE]); 
    }
    void Update()
    {
        p_sm.OnUpdate();
        LockOn();
    }
    private void FixedUpdate()
    {
        p_sm.OnFixedUpdate();
    }
    public void ChangeState(eState state)
    {
        p_sm.SetState(p_states[state]);
    }
    
    public void LockOn()
    {
        if (Lockon)
        {
            targetPosition = new Vector3(target.transform.position.x, transform.position.y, target.position.z);
            transform.LookAt(targetPosition);
        }
    }
    private void OnApplicationFocus(bool focus)
    {
        if (focus)
            Cursor.lockState = CursorLockMode.Locked;
        else
            Cursor.lockState = CursorLockMode.None;
    }
}

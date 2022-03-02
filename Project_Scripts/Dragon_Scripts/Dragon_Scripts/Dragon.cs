using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
public class Dragon : MonoBehaviour
{  public enum EnemyState
    {
        PATROL,
        CHASE,   
        READY,
        STUN,
        ATTACK_BREATH,
        ATTACK_BITE,
        ATTACK_LEG,
        ATTACK_RUSH,
        FLY_TAKEOFF,
        FLY_RUNTAKEOFF,
        FLY_FAST,
        FLY_AROUND,
        FLY_LAND,
        FLY_RUNLAND,     
        FLY_BREATHATK,
        FLY_BACKWARD,
        DEAD,
    }
    [HideInInspector]
    public float actReadyTime;
    [HideInInspector]
    public Vector3 aroundPoint;
    public float AttackRange => navMeshAgent.stoppingDistance;
    public float RemainDistance => navMeshAgent.remainingDistance;
    public bool ArrivePoint => navMeshAgent.pathPending;
    public bool AnimationName => dragonAnimator.GetCurrentAnimatorStateInfo(0).IsName(a_id);
    public float AnimationProgress => dragonAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    public Dictionary<EnemyState, EState<Dragon>> e_states = new Dictionary<EnemyState, EState<Dragon>>();
    public EStateMachine<Dragon> e_sm;
    public EStateMachine<Dragon> curstate;
    public Enemy_Pattern Enemy_pattern; 
    public Enemy_AI enemy_ai;
    public ShakeCamera shakecamera;
    public Animator dragonAnimator;
    public Transform target;
    public NavMeshAgent navMeshAgent;
    public GameObject BreatheColision;
    public GameObject BiteAtkColision;
    public GameObject LandColision;
    public string a_id;
    public float FlyHeight = 50f;
    public bool isFly = false;
    public bool Attacking;
    public bool enableact = false;
    public bool dead = false;
    public bool Stunned;
    public float ReturnTime = 1.2f;
    public float ActReadyTime = 0f;
    public float SightRange=30f;
    public float AroundSpeed = 1f;
    public float RotationSpeed = 10f;
    public float RushSpeed = 5f;
    public float FlyMoveSpeed = 8f;
    public float SkillReturnTime=10f;
    public EnemyState_Patrol enemypatrol;
    public GameObject BreathePos;
    public GameObject FlamePos;
    public GameObject MeteorPos;
    public GameObject LandPos;
    public GameObject SmokePos;
    public GameObject HitEffect;
    public Transform[] WayPoints;
    public Light LightForce;
    private void Awake()
    {
        actReadyTime = ActReadyTime;
        enemy_ai = GetComponent<Enemy_AI>();
        Enemy_pattern = GetComponent<Enemy_Pattern>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        dragonAnimator = GetComponent<Animator>();
        LandColision.SetActive(false);
        BiteAtkColision.SetActive(false);
        BreatheColision.SetActive(false);
        e_states.Add(EnemyState.CHASE, new EnemyState_Chase());
        e_states.Add(EnemyState.PATROL, new EnemyState_Patrol());
        e_states.Add(EnemyState.READY, new EnemyState_Ready());
        e_states.Add(EnemyState.STUN, new EnemyState_Stun());
        e_states.Add(EnemyState.ATTACK_LEG, new EnemyState_AtkLeg());
        e_states.Add(EnemyState.ATTACK_BITE, new EnemyState_AtkBite());
        e_states.Add(EnemyState.ATTACK_BREATH, new EnemyState_AtkBreath());
        e_states.Add(EnemyState.FLY_RUNLAND, new EnemyState_FlyRunLand());
        e_states.Add(EnemyState.ATTACK_RUSH, new EnemyState_Rush());
        e_states.Add(EnemyState.FLY_FAST, new EnemyState_FlyFast());
        e_states.Add(EnemyState.FLY_AROUND, new EnemyState_FlyAround());
        e_states.Add(EnemyState.FLY_TAKEOFF, new EnemyState_TakeOff());
        e_states.Add(EnemyState.FLY_RUNTAKEOFF, new EnemyState_RunTakeOff());
        e_states.Add(EnemyState.FLY_LAND, new EnemyState_Land());
        e_states.Add(EnemyState.FLY_BREATHATK, new EnemyState_FlyBreatheAtk());
        e_states.Add(EnemyState.FLY_BACKWARD, new EnemyState_FlyBackward());
        e_states.Add(EnemyState.DEAD, new EnemyState_Dead());
        e_sm = new EStateMachine<Dragon>(this, null);  
    }
    void Update()
    {
        e_sm.OnUpdate();
    }
    private void FixedUpdate()
    {
        e_sm.OnFixedUpdate();
    }
    public void ChangeState(EnemyState state)
    {
        e_sm.SetState(e_states[state]);
    }
    public void StartMove()
    {
        dragonAnimator.SetBool("Walk", true);
        navMeshAgent.isStopped = false;
    }
    public void StopMove()
    {
        if (isFly == true)
            return; 
        navMeshAgent.velocity = Vector3.zero;
        dragonAnimator.SetBool("Walk", false);
        navMeshAgent.isStopped = true;
    }
    public bool IsState(EnemyState state)
    {
        return e_sm.CurState == e_states[state];
    }
}

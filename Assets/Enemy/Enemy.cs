using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    public List<Transform> Waypoint = new List<Transform>();
    [SerializeField]
    public float ChaseDistance;
    [SerializeField]
    public Player Player;
    BaseState _currentState;

    [HideInInspector]
    public PatrolState PatrolState = new PatrolState();
    [HideInInspector]
    public ChaseState ChaseState = new ChaseState();
    [HideInInspector]
    public RetreatState RetreatState = new RetreatState();
    [HideInInspector]
    public NavMeshAgent NavMeshAgent;

    public void SwitchState(BaseState state)
    {
        _currentState.ExitState(this);
        _currentState = state;
        _currentState.EnterState(this);
    }

    void Start()
    {
        if (Player != null)
        {
            Player.OnpowerupStart += StartRetreating;
            Player.OnpowerupStop += StopRetreating;
        }
    }
    void Awake()
    {
        _currentState = PatrolState;
        _currentState.EnterState(this);
        NavMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (_currentState != null)
        {
            _currentState.UpdateState(this);
        }
    }

    void StartRetreating()
    {
        SwitchState(RetreatState);
    }

    void StopRetreating()
    {
        SwitchState(PatrolState);
    }
}

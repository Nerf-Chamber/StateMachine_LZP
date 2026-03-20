using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : Character
{
    [SerializeField] private SphereCollider followArea;
    [SerializeField] private NodeSO root;
    [SerializeField] private NodeSO currentState;
    [SerializeField] private float attackDistanceModifier;

    private float attackDistance;

    public Player player;
    public PathBehaviour pathBehaviour;
    public Transform playerTransform;
    public NavMeshAgent agent;
    public Condition patrol;
    public Condition follow;
    public Condition attack;
    public Condition dead;
    public int enemyDamage;

    protected override void Awake()
    {
        base.Awake();
        agent = GetComponent<NavMeshAgent>();
        attackDistance = followArea.radius / attackDistanceModifier;

        patrol = new Condition(nameof(patrol));
        follow = new Condition(nameof(follow));
        attack = new Condition(nameof(attack));
        dead = new Condition(nameof(dead));

        ChangeState();
    }
    private void Update()
    {
        currentState.OnUpdate(this);
        ChangeState();
    }

    // HANDLE STATE CHECKS
    public override void Hurt(int damage)
    {
        base.Hurt(damage);
        if (healthPoints <= 0) dead.check = true;
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.name == nameof(Player))
        {
            follow.check = true;
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        if (collider.name == nameof(Player))
        {
            follow.check = false;
            attack.check = false;
        }
    }
    private void OnTriggerStay(Collider collider) 
    {
        if (collider.name == nameof(Player))
            attack.check = (playerTransform.position - transform.position).magnitude <= attackDistance;
    }

    private void ChangeState()
    {
        foreach (var node in root.children)
        {
            if (node.StateCondition(this))
            {
                if (currentState != null && currentState.OnEndCondition(this))
                {
                    currentState.OnFinish(this);
                    currentState = null;
                }
                if (node != currentState) node.OnStart(this);
                currentState = node;
                //node.OnStart(this);
                break;
            }

        }
    }
}
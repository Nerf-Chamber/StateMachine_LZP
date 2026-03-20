using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(VisionDetectionBehaviour))]
public class Enemy : Character
{
    [SerializeField] private VisionDetectionBehaviour _vision;
    [SerializeField] private NodeSO root;
    [SerializeField] private NodeSO currentState;
    [SerializeField] private float attackDistance;

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
        _vision = GetComponent<VisionDetectionBehaviour>();
        agent = GetComponent<NavMeshAgent>();
        // attackDistance = followArea.radius / attackDistanceModifier;

        patrol = new Condition(nameof(patrol));
        follow = new Condition(nameof(follow));
        attack = new Condition(nameof(attack));
        dead = new Condition(nameof(dead));

        ChangeState();
    }
    private void Update()
    {
        follow.check = _vision.hasDetectedPlayer;
        if (follow.check)
        {
            float dist = Vector3.Distance(transform.position, playerTransform.position);
            attack.check = dist <= attackDistance;
        }
        else
            attack.check = false;

        currentState.OnUpdate(this);
        ChangeState();
    }

    public override void Hurt(int damage)
    {
        base.Hurt(damage);
        if (healthPoints <= 0) dead.check = true;
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
                break;
            }

        }
    }
}
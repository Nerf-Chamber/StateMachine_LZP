using UnityEngine;

[CreateAssetMenu(fileName = "PatrolStateSO", menuName = "Scriptable Objects/PatrolStateSO")]
public class PatrolStateSO : NodeSO
{
    [SerializeField] private float waitTimeOnPoint;
    private float time = 0f;

    public override bool OnEndCondition(Enemy enemy) { return enemy.follow.check || enemy.attack.check || enemy.dead.check; }
    public override bool StateCondition(Enemy enemy) { return !enemy.follow.check && !enemy.attack.check && !enemy.dead.check; }
    public override void OnStart(Enemy enemy) { enemy.agent.destination = enemy.pathBehaviour.GetCurrentPathPoint(); }
    public override void OnFinish(Enemy enemy) { }
    public override void OnUpdate(Enemy enemy) 
    {
        if (enemy.agent.remainingDistance <= 0.1f)
        {
            time += Time.deltaTime;
            if (time >= waitTimeOnPoint)
            {
                time = 0f;
                enemy.agent.destination = enemy.pathBehaviour.GetNextPathPoint();
            }
        }
    }
}
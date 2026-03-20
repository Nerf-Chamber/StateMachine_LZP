using UnityEngine;

[CreateAssetMenu(fileName = "FollowStateSO", menuName = "Scriptable Objects/FollowStateSO")]
public class FollowStateSO : NodeSO
{
    public override bool OnEndCondition(Enemy enemy) 
    {
        Debug.Log(!enemy.follow.check || enemy.dead || enemy.attack.check);
        return !enemy.follow.check || enemy.dead || enemy.attack.check; 
    }
    public override bool StateCondition(Enemy enemy) { return enemy.follow.check && !enemy.dead; }
    public override void OnStart(Enemy enemy) { }
    public override void OnFinish(Enemy enemy) { enemy.agent.destination = enemy.transform.position; }
    public override void OnUpdate(Enemy enemy) { enemy.agent.destination = enemy.playerTransform.position; }
}
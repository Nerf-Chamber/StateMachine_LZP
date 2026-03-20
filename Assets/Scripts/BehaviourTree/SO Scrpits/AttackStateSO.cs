using UnityEngine;

[CreateAssetMenu(fileName = "AttackStateSO", menuName = "Scriptable Objects/AttackStateSO")]
public class AttackStateSO : NodeSO
{
    public override bool OnEndCondition(Enemy enemy) { return !enemy.attack.check || enemy.dead.check; }
    public override bool StateCondition(Enemy enemy) { return enemy.attack.check && !enemy.dead.check; }
    public override void OnStart(Enemy enemy) { }
    public override void OnFinish(Enemy enemy) { }
    public override void OnUpdate(Enemy enemy) 
    {
        enemy.player.Hurt(enemy.enemyDamage);
    }
}
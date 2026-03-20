using UnityEngine;

[CreateAssetMenu(fileName = "DeadStateSO", menuName = "Scriptable Objects/DeadStateSO")]
public class DeadStateSO : NodeSO
{
    public override bool OnEndCondition(Enemy enemy) { return false; } // No pot tornar a la vida
    public override bool StateCondition(Enemy enemy) { return enemy.dead.check; }
    public override void OnStart(Enemy enemy) { }
    public override void OnFinish(Enemy enemy) { }
    public override void OnUpdate(Enemy enemy) 
    {
        Debug.Log("Tamo muelto");
        enemy.gameObject.SetActive(false);
    }
}
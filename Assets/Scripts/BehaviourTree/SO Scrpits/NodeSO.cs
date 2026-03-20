using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NodeSO", menuName = "Scriptable Objects/NodeSO")]
public class NodeSO : ScriptableObject
{
    public List<NodeSO> children;
    public virtual bool OnEndCondition(Enemy enemy) { return false; }
    public virtual bool StateCondition(Enemy enemy) { return true; }
    public virtual void OnStart(Enemy enemy) { }
    public virtual void OnFinish(Enemy enemy) { }
    public virtual void OnUpdate(Enemy enemy) { }
}
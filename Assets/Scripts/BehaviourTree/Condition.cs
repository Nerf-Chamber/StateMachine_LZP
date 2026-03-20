using UnityEngine;

public class Condition : MonoBehaviour
{
    public string name;
    public bool check;
    public Condition(string name)
    {
        this.name = name;
        check = false;
    }
}
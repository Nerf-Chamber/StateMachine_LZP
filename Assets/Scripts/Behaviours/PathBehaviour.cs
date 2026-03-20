using UnityEngine;

public class PathBehaviour : MonoBehaviour
{
    [SerializeField] private Transform[] pathPoints;
    private int index;

    public Vector3 GetCurrentPathPoint() { return pathPoints[index].position; }
    public Vector3 GetNextPathPoint()
    {
        if (pathPoints.Length == 0) return transform.position;
        return pathPoints[GetNextPathPointIndex()].position;
    }
    private int GetNextPathPointIndex()
    {
        index += 1;
        index %= pathPoints.Length;
        return index;
    }
}
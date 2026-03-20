using System.Collections;
using UnityEngine;

public class VisionDetectionBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private LayerMask obstructionMask;
    [SerializeField] private float radius;
    [SerializeField] private float memoryTime;
    
    [Range(0,360)]
    [SerializeField] private float angle;

    private float lastTimeSeen;
    public bool canSeePlayer;
    public bool hasDetectedPlayer;

    private void Start() => StartCoroutine(FOVRoutine());
    private IEnumerator FOVRoutine()
    {
        float delay = 0.25f;
        WaitForSeconds wait = new WaitForSeconds(delay);

        while (true)
        {
            yield return wait;
            FOVCheck();

            if (canSeePlayer)
            {
                lastTimeSeen = Time.time;
                hasDetectedPlayer = true;
            }
            else
            {
                if (Time.time - lastTimeSeen > memoryTime)
                    hasDetectedPlayer = false;
            }
        }
    }

    private void FOVCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform targetTransform = rangeChecks[0].transform;
            Vector3 directionToTarget = (targetTransform.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, targetTransform.position);
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    canSeePlayer = true;
                else
                    canSeePlayer = false;
            }
            else { canSeePlayer = false; }
        }
        else if (canSeePlayer)
            canSeePlayer = false;
    }

    private void OnDrawGizmos()
    {
        // Radi
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);

        // Angle (línies del con)
        Vector3 left = DirFromAngle(-angle / 2);
        Vector3 right = DirFromAngle(angle / 2);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + left * radius);
        Gizmos.DrawLine(transform.position, transform.position + right * radius);

        // Línia cap al target
        if (target != null)
        {
            Gizmos.color = canSeePlayer ? Color.green : Color.red;
            Gizmos.DrawLine(transform.position, target.transform.position);
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Forward
        Gizmos.color = Color.white;
        Gizmos.DrawRay(transform.position, transform.forward * radius);
    }

    private Vector3 DirFromAngle(float angleInDegrees)
    {
        float finalAngle = angleInDegrees + transform.eulerAngles.y;
        return new Vector3(Mathf.Sin(finalAngle * Mathf.Deg2Rad), 0, Mathf.Cos(finalAngle * Mathf.Deg2Rad));
    }
}
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    [SerializeField] private float viewDistance;
    [SerializeField] private float angleView;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private LayerMask obstacleMask;

    public bool SeeTarget(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        float angle = Vector3.Angle(transform.forward, direction);
        if(angle > angleView)
            return false;

        float distance = Vector3.Distance(target.position, transform.position);
        if(distance > viewDistance) 
            return false;

        if(Physics.Raycast(transform.position + Vector3.up, direction, distance, obstacleMask))
            return false;
        
        return true;
    }

}

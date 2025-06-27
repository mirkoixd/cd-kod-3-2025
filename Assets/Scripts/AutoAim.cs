using UnityEngine;

public class AutoAim : MonoBehaviour
{
    public LayerMask targetLayer;
    private Weapon weapon;

    void Update()
    {
        if (weapon == null)
        {
            weapon = GetComponentInChildren<Weapon>();
            if (weapon == null)
                return;
        }

        float detectionRadius = weapon.fireRadius;

        Collider[] hits = new Collider[10];
        int count = Physics.OverlapSphereNonAlloc(transform.position, detectionRadius, hits, targetLayer);

        Transform closest = null;
        float minDist = float.MaxValue;

        for (int i = 0; i < count; i++)
        {
            float dist = Vector3.Distance(transform.position, hits[i].transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = hits[i].transform;
            }
        }

        if (closest != null)
        {
            Vector3 dir = (closest.position - transform.position).normalized;
            dir.y = 0f;

            if (dir != Vector3.zero)
            {
                Quaternion lookRot = Quaternion.LookRotation(dir);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * 5f);
            }
        }
        else
        {
            Vector3 defaultDir = -Vector3.forward;
            Quaternion defaultRot = Quaternion.LookRotation(defaultDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, defaultRot, Time.deltaTime * 5f);
        }
    }
}

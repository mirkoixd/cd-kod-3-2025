using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Settings")]
    public GameObject bulletPrefab;
    public float fireRadius = 10f;
    public float fireRate = 1f;
    public float damage = 10f;
    public float maxDistance = 20f;
    public float bulletSpeed = 15f;
    public Transform firePoint;

    [Header("Target Settings")]
    public LayerMask enemyLayer;

    private float fireCooldown;
    private Transform currentTarget;

    public bool HasTarget()
    {
        return currentTarget != null;
    }

    public Vector3 GetDirectionToTarget()
    {
        if (currentTarget == null) return transform.forward;
        Vector3 dir = currentTarget.position - transform.position;
        dir.y = 0f;
        return dir.normalized;
    }

    void Update()
    {
        fireCooldown -= Time.deltaTime;

        Collider[] hits = new Collider[10];
        int count = Physics.OverlapSphereNonAlloc(transform.position, fireRadius, hits, enemyLayer);

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

        if (closest != null && fireCooldown <= 0f)
        {
            // Строго по горизонтали
            Vector3 targetPos = closest.position;
            targetPos.y = firePoint.position.y;
            Vector3 direction = (targetPos - firePoint.position).normalized;

            Debug.DrawRay(firePoint.position, direction * 5f, Color.red, 2f);

            Shoot(direction);
            fireCooldown = 1f / fireRate;
        }
    }



void Shoot(Vector3 direction)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.LookRotation(direction));
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.Init(direction, bulletSpeed, maxDistance, damage);
        }
    }
}

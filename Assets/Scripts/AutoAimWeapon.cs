using UnityEngine;

public class AutoAimWeapon : MonoBehaviour
{
    [Header("Weapon Settings")]
    public GameObject bulletPrefab;
    public float fireRadius = 15f;
    public float fireRate = 1f;
    public float damage = 10f;
    public float maxDistance = 20f;
    public float bulletSpeed = 15f;
    public Transform firePoint;

    [Header("Target Settings")]
    public LayerMask enemyLayer;

    private float fireCooldown;
    private Transform currentTarget;

    void Update()
    {
        fireCooldown -= Time.deltaTime;

        FindTarget();

        if (currentTarget != null)
        {
            Vector3 dir = (currentTarget.position - transform.position);
            dir.y = 0f;
            dir.Normalize();

            Transform owner = transform.root;
            if (dir != Vector3.zero)
            {
                Quaternion lookRot = Quaternion.LookRotation(dir);
                owner.rotation = Quaternion.Slerp(owner.rotation, lookRot, Time.deltaTime * 5f);
            }

            if (fireCooldown <= 0f)
            {
                Shoot(dir);
                fireCooldown = 1f / fireRate;
            }
        }
        else
        {
            Transform owner = transform.root;
            Vector3 defaultDir = -Vector3.forward;
            Quaternion defaultRot = Quaternion.LookRotation(defaultDir);
            owner.rotation = Quaternion.Slerp(transform.rotation, defaultRot, Time.deltaTime * 5f);
        }
    }

    void FindTarget()
    {
        Collider[] hits = new Collider[10];
        int count = Physics.OverlapSphereNonAlloc(transform.position, fireRadius, hits, enemyLayer);

        currentTarget = null;
        float minDist = float.MaxValue;

        for (int i = 0; i < count; i++)
        {
            float dist = Vector3.Distance(transform.position, hits[i].transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                currentTarget = hits[i].transform;
            }
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

using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed;
    private float maxDistance;
    private float damage;
    private Vector3 direction;
    private float traveled = 0f;

    public void Init(Vector3 dir, float spd, float maxDist, float dmg)
    {
        direction = dir.normalized;
        speed = spd;
        maxDistance = maxDist;
        damage = dmg;
    }

    void Update()
    {
        float move = speed * Time.deltaTime;
        transform.position += direction * move;
        traveled += move;

        if (traveled >= maxDistance)
            Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        Character target = other.GetComponent<Character>();
        if (target != null && target != GetComponentInParent<Character>())
        {
            target.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}

using UnityEngine;

public class SpeedBoostPickup : MonoBehaviour
{
    public float duration = 5f;
    public float speedMultiplier = 2f;

    void OnTriggerEnter(Collider other)
    {
        ISpeedable speedTarget = other.GetComponent<ISpeedable>();
        if (speedTarget != null)
        {
            speedTarget.ApplySpeedBoost(speedMultiplier, duration);
            Destroy(gameObject);
        }
    }
}

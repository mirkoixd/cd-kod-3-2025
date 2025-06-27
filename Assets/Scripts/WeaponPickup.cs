using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public GameObject weaponPrefab;

    void OnTriggerEnter(Collider other)
    {
        WeaponHolder holder = other.GetComponent<WeaponHolder>();
        if (holder != null)
        {
            holder.EquipWeapon(weaponPrefab);
            Destroy(gameObject);
        }
    }
}

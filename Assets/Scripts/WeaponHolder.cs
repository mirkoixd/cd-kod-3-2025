using UnityEngine;
using System.Collections;

public class WeaponHolder : MonoBehaviour
{
    public GameObject startWeaponPrefab;
    public Transform handPoint;

    private GameObject currentWeapon;

    public void EquipWeapon(GameObject weaponPrefab)
    {
        StartCoroutine(ReplaceWeapon(weaponPrefab));
    }

    private IEnumerator ReplaceWeapon(GameObject newWeaponPrefab)
    {
        yield return null;

        if (currentWeapon != null)
            Destroy(currentWeapon);

        currentWeapon = Instantiate(newWeaponPrefab, handPoint);
        currentWeapon.transform.localPosition = Vector3.zero;
        currentWeapon.transform.localRotation = Quaternion.identity;
    }

    void Start()
    {
        if (startWeaponPrefab != null)
        {
            EquipWeapon(startWeaponPrefab);
        }
    }
}

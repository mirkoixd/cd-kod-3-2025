using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 8, -6);
    public Vector3 rotationEuler = new Vector3(45, 0, 0);

    void LateUpdate()
    {
        if (target == null) return;

        transform.position = target.position + offset;
        transform.rotation = Quaternion.Euler(rotationEuler);
    }
}

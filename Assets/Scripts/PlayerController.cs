using UnityEngine;

public class PlayerController : MonoBehaviour, ISpeedable
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 360f;
    public float sprintMultiplier = 2f;

    private float speedMultiplier = 1f;
    private float boostTimeRemaining = 0f;

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (controller == null)
            controller = gameObject.AddComponent<CharacterController>();
    }

    void Update()
    {
        if (boostTimeRemaining > 0f)
        {
            boostTimeRemaining -= Time.deltaTime;
            if (boostTimeRemaining <= 0f)
                speedMultiplier = 1f;
        }

        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        Vector3 input = new Vector3(inputX, 0, inputZ);
        if (input.magnitude > 1f) input.Normalize();

        float finalSpeed = moveSpeed * speedMultiplier;

        Vector3 move = input * finalSpeed;
        controller.SimpleMove(move);
    }

    public void ApplySpeedBoost(float multiplier, float duration)
    {
        speedMultiplier = multiplier;
        boostTimeRemaining = duration;
    }
}

using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    [Header("Cube Movment Settings")]
    public float speed = 2f;
    public float range = 3f;
    public Vector3 direction = Vector3.forward;
    private Vector3 startPosition;
    private float directionMultiplier = 1f;

    void OnEnable()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        Vector3 offset = transform.position - startPosition;
        float distance = Vector3.Dot(offset, direction.normalized);

        if (Mathf.Abs(distance) >= range)
        {
            directionMultiplier *= -1;
        }


        transform.Translate(direction.normalized * speed * directionMultiplier * Time.deltaTime);
    }
}
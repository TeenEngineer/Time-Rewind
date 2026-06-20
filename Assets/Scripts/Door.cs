using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private float openDistance = 3f;
    [SerializeField] private float openSpeed = 2f;

    private bool opening = false;
    private Vector3 targetPosition;

    private void Start()
    {
        targetPosition = transform.position + Vector3.down * openDistance;
    }

    private void Update()
    {
        if (opening)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                openSpeed * Time.deltaTime
            );
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Key")
        {
            opening = true;

            // Optional: remove the key
            Destroy(collision.gameObject);
        }
    }
}

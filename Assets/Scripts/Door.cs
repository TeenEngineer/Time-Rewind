using System.Net;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private float openDistance = 3f;
    [SerializeField] private float openSpeed = 2f;
    [SerializeField] private Button button;

    private bool opening = false;
    private Vector3 targetPosition1;
    private Vector3 targetPosition2;

    private void Start()
    {
        targetPosition1 = transform.position + Vector3.down * openDistance;
        targetPosition2 = transform.position;
    }

    private void Update()
    {
        if (opening)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition1,
                openSpeed * Time.deltaTime
            );
        }
        if (button != null)
        {
            if (button.isPressed) opening = true;
            else
            {
                opening = false;
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    targetPosition2,
                    openSpeed * Time.deltaTime
                );
            }
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

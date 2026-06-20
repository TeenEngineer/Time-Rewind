using UnityEngine;

public class GrabObjects : MonoBehaviour
{
    [SerializeField] private Transform grabPoint;
    [SerializeField] private Transform rayPoint;
    [SerializeField] private float rayDistance = 1.5f;
    [SerializeField] private ShowKey showkey;

    private GameObject grabbedObject;
    private int layerIndex;

    void Start()
    {
        layerIndex = LayerMask.NameToLayer("Objects");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (grabbedObject == null)
            {
                TryGrab();
            }
            else
            {
                DropObject();
            }
        }
    }

    void TryGrab()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            rayPoint.position,
            transform.right,
            rayDistance
        );

        if (hit.collider == null)
            return;

        if (hit.collider.gameObject.layer != layerIndex)
            return;

        grabbedObject = hit.collider.gameObject;

        Rigidbody2D rb = grabbedObject.GetComponent<Rigidbody2D>();

        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.bodyType = RigidbodyType2D.Kinematic;

        grabbedObject.transform.position = grabPoint.position;
        grabbedObject.transform.SetParent(grabPoint);

        showkey.disabled = true;
    }

    void DropObject()
    {
        Rigidbody2D rb = grabbedObject.GetComponent<Rigidbody2D>();

        grabbedObject.transform.SetParent(null);

        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;

        grabbedObject = null;

        showkey.disabled = false;
    }
}
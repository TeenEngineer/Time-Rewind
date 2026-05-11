using System.Collections.Generic;
using UnityEngine;

public class RewindObject : MonoBehaviour
{
    private bool isRewinding = false;
    private Rigidbody2D body;
    private List<TransformData> history = new List<TransformData>();
    [SerializeField] private float recordTime = 5f;

    void Record()
    {
        if (history.Count > Mathf.Round(recordTime / Time.fixedDeltaTime))
        {
            history.RemoveAt(history.Count - 1);
        }
        Vector2 pos = (body != null) ? body.position : (Vector2)transform.position;
        history.Insert(0, new TransformData(pos, transform.rotation));
    }

    void Rewind()
    {
        if (history.Count > 0)
        {
            TransformData data = history[0];

            if (body != null) body.position = data.position;
            else transform.position = data.position;

            transform.rotation = data.rotation;
            history.RemoveAt(0);
        }
    }

    public void StartRewind()
    {
        isRewinding = true;
        if (body != null)
        {
            body.linearVelocity = Vector2.zero;
            body.bodyType = RigidbodyType2D.Kinematic;
        }
    }

    public void StopRewind()
    {
        isRewinding = false;
        if (body != null)
        {
            body.linearVelocity = Vector2.zero;
            body.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    private struct TransformData
    {
        public Vector2 position;
        public Quaternion rotation;
        public TransformData(Vector2 pos, Quaternion rot)
        {
            position = pos;
            rotation = rot;
        }
    }

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        RewindManager.Register(this);
    }

    void OnDestroy()
    {
        RewindManager.Unregister(this);
    }

    void FixedUpdate()
    {
        if (isRewinding) Rewind();
        else Record();
    }
}
using UnityEngine;

public class StopMusic : MonoBehaviour
{
    [SerializeField] private GameObject audiosource;
    private bool isOnce = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && !isOnce)
        {
            Destroy(audiosource);
            isOnce = true;
        }
    }
}

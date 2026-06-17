using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float bounceForce = 20f;
    [SerializeField] private AudioClip Sound;
    void Start()
    {
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
            AudioManager.Instance.PlaySound(Sound);
        }
    }
}

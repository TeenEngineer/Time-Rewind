using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float normalScale = 1f;
    [SerializeField] private float hoverScale = 1.1f;
    [SerializeField] private float animSpeed = 8f;

    private float targetScale;

    void Start()
    {
        targetScale = normalScale;
    }

    void Update()
    {
        float current = transform.localScale.x;
        float newScale = Mathf.Lerp(current, targetScale, animSpeed * Time.deltaTime);
        transform.localScale = Vector3.one * newScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetScale = hoverScale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetScale = normalScale;
    }
}
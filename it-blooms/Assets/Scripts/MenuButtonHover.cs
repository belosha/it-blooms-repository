using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Настройки плашки")]
    [SerializeField] private GameObject highlightImage;

    [Header("Настройки увеличения при наведении")]
    [Tooltip("Во сколько раз увеличивается кнопка (например, 1.1 = на 10% больше).")]
    [SerializeField] private float hoverScale = 1.1f;
    [Tooltip("Скорость увеличения.")]
    [SerializeField] private float scaleSpeed = 10f;

    private Vector3 originalScale;
    private Vector3 targetScale;

    void Start()
    {
        // Запоминаем исходный размер кнопки
        originalScale = transform.localScale;
        targetScale = originalScale;

        // При старте плашка скрыта
        if (highlightImage != null)
        {
            highlightImage.SetActive(false);
        }
    }

    void Update()
    {
        // Плавное изменение размера кнопки каждый кадр
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, scaleSpeed * Time.deltaTime);
    }

    // Когда курсор заходит на кнопку
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (highlightImage != null)
        {
            highlightImage.SetActive(true);
        }
        
        // Устанавливаем увеличенный размер
        targetScale = originalScale * hoverScale;
    }

    // Когда курсор уходит с кнопки
    public void OnPointerExit(PointerEventData eventData)
    {
        if (highlightImage != null)
        {
            highlightImage.SetActive(false);
        }
        
        // Возвращаем исходный размер
        targetScale = originalScale;
    }
}
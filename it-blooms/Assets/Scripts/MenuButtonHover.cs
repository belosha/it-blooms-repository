using UnityEngine;
using UnityEngine.EventSystems; // Пространство имен для отслеживания мыши

public class MenuButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Перетащи сюда объект плашки")]
    [SerializeField] private GameObject highlightImage;

    void Start()
    {
        // При старте игры плашка автоматически скрывается
        if (highlightImage != null)
        {
            highlightImage.SetActive(false);
        }
    }

    // Срабатывает, когда мышка заходит на кнопку
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (highlightImage != null)
        {
            highlightImage.SetActive(true);
        }
    }

    // Срабатывает, когда мышка уходит с кнопки
    public void OnPointerExit(PointerEventData eventData)
    {
        if (highlightImage != null)
        {
            highlightImage.SetActive(false);
        }
    }
}
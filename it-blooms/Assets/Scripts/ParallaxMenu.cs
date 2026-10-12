using UnityEngine;
using UnityEngine.InputSystem;

public class ParallaxMenu : MonoBehaviour
{
    [Header("Настройки параллакса")]
    [Tooltip("Сила смещения слоя. Чем больше значение, тем сильнее слой реагирует на мышь.")]
    [SerializeField] private float intensity = 5f;
    
    [Tooltip("Плавность движения (чем больше значение, тем быстрее и плавнее задержка).")]
    [SerializeField] private float smoothing = 5f;

    private Vector3 initialPosition;

    void Start()
    {
        // Запоминаем исходные координаты объекта на Canvas
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        // Проверяем, активна ли мышь в системе
        if (Mouse.current == null) return;

        // Считываем позицию курсора через New Input System
        Vector2 mousePos = Mouse.current.position.ReadValue();

        // Переводим координаты мыши относительно центра экрана (от -0.5 до 0.5)
        float normalizedX = (mousePos.x / Screen.width) - 0.5f;
        float normalizedY = (mousePos.y / Screen.height) - 0.5f;

        // Рассчитываем новую позицию для слоя
        Vector3 targetPosition = initialPosition + new Vector3(normalizedX * intensity, normalizedY * intensity, 0f);

        // Плавный переход к новой позиции
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, smoothing * Time.deltaTime);
    }
}
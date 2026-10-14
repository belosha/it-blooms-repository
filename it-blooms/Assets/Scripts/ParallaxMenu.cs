using UnityEngine;
using UnityEngine.InputSystem;

public class ParallaxMenu : MonoBehaviour
{
    [Header("Настройки параллакса")]
    [Tooltip("Сила смещения слоя.")]
    [SerializeField] private float intensity = 5f;
    [Tooltip("Плавность движения за мышью.")]
    [SerializeField] private float smoothing = 5f;
    [SerializeField] private bool invertDirection = false;

    [Header("Настройки старта (Приближение)")]
    [Tooltip("На сколько процентов объект крупнее в самом начале (например, 1.2 = на 20% больше).")]
    [SerializeField] private float startScaleMultiplier = 1.15f;
    [Tooltip("Скорость приближения при запуске игры.")]
    [SerializeField] private float introSpeed = 3f;

    private Vector3 initialPosition;
    private Vector3 targetScale;
    private bool isIntroFinished = false;

    void Start()
    {
        initialPosition = transform.localPosition;
        targetScale = transform.localScale;

        // В начале игры делаем объект чуть крупнее (эффект отдаления/приближения)
        transform.localScale = targetScale * startScaleMultiplier;
    }

    void Update()
    {
        // 1. Плавное приближение к исходному размеру при старте
        if (!isIntroFinished)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, introSpeed * Time.deltaTime);

            // Как только размер почти дошел до нормы, считаем интро завершенным
            if (Vector3.Distance(transform.localScale, targetScale) < 0.001f)
            {
                transform.localScale = targetScale;
                isIntroFinished = true;
            }
        }

        // 2. Стандартный параллакс за мышкой
        if (Mouse.current == null) return;

        Vector2 mousePos = Mouse.current.position.ReadValue();

        float normalizedX = (mousePos.x / Screen.width) - 0.5f;
        float normalizedY = (mousePos.y / Screen.height) - 0.5f;

        float currentIntensity = invertDirection ? -intensity : intensity;

        Vector3 targetPosition = initialPosition + new Vector3(normalizedX * currentIntensity, normalizedY * currentIntensity, 0f);

        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, smoothing * Time.deltaTime);
    }
}
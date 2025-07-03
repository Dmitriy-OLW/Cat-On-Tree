using UnityEngine;

public class AxeLogic : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource audioSource;

    [Header("Sounds")]
    [SerializeField] private AudioClip woodHitSound;
    [SerializeField] private AudioClip glassBreakSound;

    [Header("Particles")]
    [SerializeField] private Transform particlePoint;
    [SerializeField] private GameObject woodParticles;

    [Header("Glass Objects")]
    [SerializeField] private GameObject staticGlass;
    [SerializeField] private GameObject glassParts;

    private float particleTimer;
    private bool particlesActive;
    private bool canHitWood = true;
    private const float HitCooldown = 0.5f;

    private void Awake()
    {
        // Инициализация объектов стекла
        if (glassParts != null)
        {
            glassParts.SetActive(false);
        }

        // Отключаем частицы дерева, если они есть
        if (woodParticles != null)
        {
            woodParticles.SetActive(false);
        }
    }

    private void Update()
    {
        // Таймер для отключения частиц
        if (particlesActive)
        {
            particleTimer += Time.deltaTime;
            
            if (particleTimer >= HitCooldown)
            {
                if (animator != null)
                {
                    animator.SetInteger("Cat_State", 0);
                }
                woodParticles.SetActive(false);
                particlesActive = false;
                particleTimer = 0f;
                canHitWood = true; // Разрешаем новый удар по дереву
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Обработка удара по дереву
        if (other.CompareTag("Wood") && canHitWood)
        {
            HandleWoodHit();
        }
        // Обработка удара по стеклу
        else if (other.CompareTag("Glass")  && other.gameObject.layer == 6)
        {
            HandleGlassBreak();
        }
    }

    private void HandleWoodHit()
    {
        // Запрещаем новые удары по дереву до завершения таймера
        canHitWood = false;
        
        // Воспроизведение звука
        if (audioSource != null && woodHitSound != null)
        {
            audioSource.PlayOneShot(woodHitSound);
        }

        // Передача значения в аниматор
        if (animator != null)
        {
            animator.SetInteger("Cat_State", 4);
        }

        // Активация частиц
        if (woodParticles != null && particlePoint != null)
        {
            woodParticles.transform.position = particlePoint.position;
            woodParticles.SetActive(true);
            particlesActive = true;
            particleTimer = 0f;
        }
    }

    private void HandleGlassBreak()
    {
        // Воспроизведение звука
        if (audioSource != null && glassBreakSound != null)
        {
            audioSource.PlayOneShot(glassBreakSound);
        }

        // Отключение целого стекла и включение осколков
        if (staticGlass != null)
        {
            staticGlass.SetActive(false);
        }

        if (glassParts != null)
        {
            glassParts.SetActive(true);
        }
    }
}
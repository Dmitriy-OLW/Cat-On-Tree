using UnityEngine;

public class TriggerController : MonoBehaviour
{
    public TriggerChecker[] triggers; // Массив триггеров, которые нужно проверить
    public GameObject interactObject; // Объекты, которые нужно включать/выключать
    public GameObject staticObject; // Статический объект, который выключается в Awake

    [SerializeField]
    private Animator animator;
    private float timer;
    private bool isTimerRunning;
    private const float requiredTime = 3f; // Требуемое время для таймера (3 секунды)

    private void Awake()
    {
        if (staticObject != null)
        {
            staticObject.SetActive(false);
        }
    }
    
    private void Update()
    {
        // Проверяем все триггеры
        bool allTriggersActive = CheckAllTriggers();

        if (allTriggersActive)
        {
            // Если таймер не запущен - запускаем его
            if (!isTimerRunning)
            {
                timer = 0f;
                isTimerRunning = true;
            }
            
            // Увеличиваем таймер
            timer += Time.deltaTime;

            // Если таймер достиг нужного времени
            if (timer >= requiredTime)
            {
                // Передаем параметр в аниматор
                if (animator != null)
                {
                    animator.SetInteger("Cat_State", 3);
                }
                string[] tagsToDisable = { "Textile", "Pitchfork", "Rake", "Shovel", "Bayonet_Shovel" };

                foreach (string tag in tagsToDisable)
                {
                    GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
                    foreach (GameObject obj in objects)
                    {
                        obj.SetActive(false);
                    }
                }
                
                if (interactObject != null)
                {
                    interactObject.SetActive(false);
                }

                // Включаем статический объект
                if (staticObject != null)
                {
                    staticObject.SetActive(true);
                }

                // Сбрасываем таймер
                isTimerRunning = false;
            }
        }
        else
        {
            // Если хотя бы один триггер неактивен - сбрасываем таймер
            if (isTimerRunning)
            {
                timer = 0f;
                isTimerRunning = false;
            }
        }
    }

    private bool CheckAllTriggers()
    {
        if (triggers == null || triggers.Length == 0) return false;

        foreach (var trigger in triggers)
        {
            if (trigger == null || !trigger.isObjectInTrigger)
            {
                return false;
            }
        }

        return true;
    }
}
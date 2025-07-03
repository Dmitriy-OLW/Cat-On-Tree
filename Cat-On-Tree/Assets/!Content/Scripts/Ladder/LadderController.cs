using UnityEngine;

public class LadderController : MonoBehaviour
{
    [SerializeField] private TriggerChecker[] triggerCheckers;
    [SerializeField] private GameObject ladderStatic;
    [SerializeField] private GameObject ladderInteract;

    private float timer = 0f;
    private bool isTimerRunning = false;
    private const float requiredTime = 3f;

    private void Awake()
    {
        if (ladderInteract != null)
        {
            ladderInteract.SetActive(false);
        }

        if (ladderStatic != null)
        {
            ladderStatic.SetActive(true);
        }
    }

    private void Update()
    {
        bool allTriggersActive = CheckAllTriggers();

        if (allTriggersActive)
        {
            if (!isTimerRunning)
            {
                timer = 0f;
                isTimerRunning = true;
            }

            timer += Time.deltaTime;

            if (timer >= requiredTime)
            {
                CompleteLadderAssembly();
                isTimerRunning = false;
            }
        }
        else
        {
            if (isTimerRunning)
            {
                ResetTimer();
            }
        }
    }

    private bool CheckAllTriggers()
    {
        if (triggerCheckers == null || triggerCheckers.Length == 0)
            return false;

        foreach (TriggerChecker checker in triggerCheckers)
        {
            if (checker == null || !checker.isObjectInTrigger)
            {
                return false;
            }
        }

        return true;
    }

    private void ResetTimer()
    {
        timer = 0f;
        isTimerRunning = false;
    }

    private void CompleteLadderAssembly()
    {
        if (ladderInteract != null)
        {
            ladderInteract.SetActive(true);
        }

        if (ladderStatic != null)
        {
            ladderStatic.SetActive(false);
        }
        GameObject[] Objects = GameObject.FindGameObjectsWithTag("Ladder_Part");
        
        // Перебираем все найденные объекты
        foreach (GameObject Object in Objects)
        {
            Object.SetActive(false);
        }
        
    }
}
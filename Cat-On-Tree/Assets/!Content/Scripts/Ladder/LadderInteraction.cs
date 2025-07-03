using UnityEngine;

public class LadderInteraction : MonoBehaviour
{
    [SerializeField] private TriggerChecker[] triggerCheckers;
    public Animator animator;
    public GameObject socer_controller;
    //public GameObject ladder_interact;
    public GameObject ladder_static;

    private bool isInTrigger = false;
    private float timer = 0f;
    private const float triggerTime = 3f;

    private void Awake()
    {
        // Отключаем ladder_static при старте
        if (ladder_static != null)
        {
            ladder_static.SetActive(false);
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

    private void Update()
    {
        bool isInTrigger = CheckAllTriggers();
        if (isInTrigger)
        {
            // Увеличиваем таймер
            timer += Time.deltaTime;

            // Проверяем, достигли ли мы 3 секунд
            if (timer >= triggerTime)
            {
                // Передаем в аниматор переменную
                if (animator != null)
                {
                    animator.SetInteger("Cat_State", 1);
                }

                // Выключаем socer_controller
                if (socer_controller != null)
                {
                    socer_controller.SetActive(false);
                }


                GameObject[] Objects = GameObject.FindGameObjectsWithTag("Ladder");
                
                foreach (GameObject Object in Objects)
                {
                    Object.SetActive(false);
                }

                if (ladder_static != null)
                {
                    ladder_static.SetActive(true);
                }

                // Сбрасываем флаг и таймер, чтобы не выполнять это повторно
                isInTrigger = false;
                timer = 0f;
            }
        }
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ladder"))
        {
            // Начинаем отсчет времени при входе в триггер
            isInTrigger = true;
            timer = 0f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ladder"))
        {
            // Сбрасываем таймер при выходе из триггера
            isInTrigger = false;
            timer = 0f;
        }
    }*/
}
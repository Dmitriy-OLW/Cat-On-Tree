using System;
using UnityEngine;

public class BaitLogic : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject[] ifAcriv_Objs;
    [SerializeField] private string animator_END; // Добавлено новое поле для имени анимации
    private int animationStateHash = Animator.StringToHash("Cat_State");
    private bool isInTrigger = false;
    private float pausedAnimationTime = 0f;

    private void Start()
    {
        gameObject.GetComponent<Collider>().enabled = false;
    }

    private void Update()
    {
        foreach (GameObject obj in ifAcriv_Objs)
        {
            if (obj.activeInHierarchy)
            {
                gameObject.GetComponent<Collider>().enabled = true;
            }
        }

        // Проверяем, если текущая анимация соответствует animator_END
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(animator_END))
        {
            gameObject.SetActive(false); // Отключаем этот скрипт
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Meat") && !isInTrigger)
        {
            isInTrigger = true;
            
            // Устанавливаем состояние анимации и воспроизводим с того же места
            animator.SetInteger(animationStateHash, 2);
            
            if (pausedAnimationTime > 0)
            {
                animator.Play("EndCutscene_2", -1, pausedAnimationTime);
                animator.speed = 1f; // Восстанавливаем нормальную скорость анимации
            }
            else
            {
                animator.speed = 1f; // Запускаем анимацию с нормальной скоростью
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Meat"))
        {
            isInTrigger = false;
            
            // Сохраняем текущее время анимации перед остановкой
            pausedAnimationTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
            animator.speed = 0f; // Останавливаем анимацию
        }
    }
}
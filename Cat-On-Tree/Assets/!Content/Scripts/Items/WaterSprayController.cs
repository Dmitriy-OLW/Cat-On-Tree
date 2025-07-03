using UnityEngine;

public class WaterSprayController : MonoBehaviour
{
    [SerializeField] private GameObject sprayObject; // Объект пшикалки, который нужно включать/выключать
    [SerializeField] private float sprayDuration = 2f; // Длительность работы пшикалки в секундах
    [SerializeField] private VRHandInteractor VRHandScript_R; // Ссылка на другой скрипт
    [SerializeField] private VRHandInteractor VRHandScript_L; // Ссылка на другой скрипт
    [SerializeField] private AudioSource SoundPsik;
    
    private float timer = 0f;
    private bool isSpraying = false;


    private void Start()
    {
        // Инициализация
        if (sprayObject != null)
        {
            sprayObject.SetActive(false);
        }
    }

    private void Update()
    {
        // Если пшикалка активна, обновляем таймер
        if (isSpraying)
        {
            timer += Time.deltaTime;

            // Если время истекло, выключаем пшикалку
            if (timer >= sprayDuration)
            {
                StopSpray();
            }
        }

        // Проверяем нажатие кнопки и условие из другого скрипта
        if (!isSpraying && (VRHandScript_L.activateSpray == true || VRHandScript_R.activateSpray == true))
        {
            StartSpray();
        }
    }

    private void StartSpray()
    {
        if (sprayObject != null)
        {
            sprayObject.SetActive(true);
        }
        isSpraying = true;
        timer = 0f;
        SoundPsik.Play();
    }

    private void StopSpray()
    {
        if (sprayObject != null)
        {
            sprayObject.SetActive(false);
        }
        isSpraying = false;
        VRHandScript_L.activateSpray = false;
        VRHandScript_R.activateSpray = false;
    }
}
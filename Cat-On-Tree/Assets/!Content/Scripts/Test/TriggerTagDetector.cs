using UnityEngine;

public class TriggerTagDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Выводим тег объекта, который вошел в триггер
        Debug.Log("Объект вошел в триггер. Тег: " + other.tag);
    }

    private void OnTriggerStay(Collider other)
    {
        // Можно использовать этот метод, если нужно постоянно проверять объект в триггере
        Debug.Log("Объект в триггере. Тег: " + other.tag);
    }

    private void OnTriggerExit(Collider other)
    {
        // Выводим тег объекта, который вышел из триггера
        Debug.Log("Объект вышел из триггера. Тег: " + other.tag);
    }
}
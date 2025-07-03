using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScene : MonoBehaviour
{
    void Update()
    {
        // Проверяем нажатие клавиши R
        if (Input.GetKeyDown(KeyCode.R))
        {
            // Получаем имя текущей сцены и перезагружаем её
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
        }
    }
}
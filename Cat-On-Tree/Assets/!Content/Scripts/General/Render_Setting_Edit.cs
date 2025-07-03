using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public class Render_Setting_Edit : MonoBehaviour
{


    private UniversalRenderPipelineAsset urpAsset;

    private void Start()
    {
        // Получаем текущий URP Asset
        urpAsset = GraphicsSettings.currentRenderPipeline as UniversalRenderPipelineAsset;
        
        if (urpAsset == null)
        {
            Debug.LogError("Universal Render Pipeline Asset не найден!");
            enabled = false; // Отключаем скрипт, если URP не используется
        }
    }

    private void Update()
    {
        // Проверяем, нажата ли клавиша '/'
        if (Input.GetKey(KeyCode.Slash))
        {
            // Увеличиваем renderScale на 0.02 при нажатии '+'
            if (Input.GetKeyDown(KeyCode.Plus) || Input.GetKeyDown(KeyCode.KeypadPlus))
            {
                
                ChangeRenderScale(0.02f);
            }
            // Уменьшаем renderScale на 0.02 при нажатии '-'
            else if (Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus))
            {
                ChangeRenderScale(-0.02f);
            }
            // Сбрасываем renderScale к 1 при нажатии '0'
            else if (Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Keypad0))
            {
                SetRenderScale(1.0f);
            }
        }
    }

    private void ChangeRenderScale(float delta)
    {
        if (urpAsset != null)
        {
            float newScale = urpAsset.renderScale + delta;
            newScale = Mathf.Clamp(newScale, 0.1f, 2.0f); // Ограничиваем диапазон
            urpAsset.renderScale = newScale;
            Debug.Log($"Render Scale изменён: {urpAsset.renderScale}");
        }
    }

    private void SetRenderScale(float value)
    {
        if (urpAsset != null)
        {
            urpAsset.renderScale = value;
            Debug.Log($"Render Scale установлен: {urpAsset.renderScale}");
        }
    }
}
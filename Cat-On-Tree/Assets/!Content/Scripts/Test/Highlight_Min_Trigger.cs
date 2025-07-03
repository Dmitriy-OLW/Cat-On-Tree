using UnityEngine;

public class Highlight_Min_Trigger : MonoBehaviour
{
    
    [SerializeField] private string[] tagsToHighlight = {
        "Axe", "Shovel", "Bayonet_Shovel", "Pitchfork", "Rake",
        "Key", "Ladder", "Ladder_Part", "Meat", "Textile",
        "Laser", "Mouse", "Spray"
    };

    private void OnTriggerEnter(Collider other)
    {
        foreach (string tag in tagsToHighlight)
        {
            if (other.CompareTag(tag))
            {
                Renderer renderer = other.GetComponent<Renderer>();
                if (renderer != null)
                {
                    Material material = renderer.material;
                    material.DisableKeyword("_EMISSION");
                }
                break;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        foreach (string tag in tagsToHighlight)
        {
            if (other.CompareTag(tag))
            {
                Renderer renderer = other.GetComponent<Renderer>();
                if (renderer != null)
                {
                    Material material = renderer.material;
                    material.EnableKeyword("_EMISSION");

                    // Устанавливаем цвет и интенсивность эмишена
                    Color emissionColor = new Color(0f, 60f / 255f, 100f / 255f);
                    float intensity = 1f; // Базовая интенсивность

                    // Проверяем, является ли тег "Mouse" или "Laser"
                    if (other.CompareTag("Mouse"))
                    {
                        intensity = 2f; // Увеличиваем интенсивность для этих тегов
                    }

                    // Умножаем цвет на интенсивность
                    material.SetColor("_EmissionColor", emissionColor * intensity);
                }
                break;
            }
        }
    }
}

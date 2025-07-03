using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Readers;

    /*
1. Axe  
2. Shovel  
3. Bayonet_Shovel  
4. Pitchfork  
5. Rake  
6. Key  
7. Ladder  
8. Ladder_Part  
9. Meat  
10. Textile
     */

public class TriggerSocketController : MonoBehaviour
{
    [Header("Основные настройки")]
    [SerializeField] private string triggerTag = "Ladder"; // Тег объекта, который активирует триггер[SerializeField] private string triggerTag = "Player"; // Тег объекта, который активирует триггер
    [SerializeField] private GameObject player; // Объект игрока
    [SerializeField] private float activationDistance = 3f; // Дистанция активации
    [SerializeField] private GameObject obj_socet; // Основной объект

    [Header("Настройки ввода")]
    [SerializeField]
    private XRInputValueReader<float> button_A; // Читатель ввода для кнопки A

    [Header("Настройки префабов")]
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private GameObject objectAboveArrowPrefab;
    [SerializeField] private Material sharedMaterial;

    [Header("Позиционирование стрелки")]
    [SerializeField] private float arrowHeight = 0.2f;

    [Header("Позиционирование объекта над стрелкой")]
    [SerializeField] private Vector3 objectPositionOffset = new Vector3(0f, 0.3f, 0f);
    [SerializeField] private Vector3 objectRotation = Vector3.zero;

    [Header("Настройки анимации")]
    [SerializeField] private float floatHeight = 0.1f;
    [SerializeField] private float floatSpeed = 1f;
    [SerializeField] private float rotationSpeed = 30f;

    private GameObject arrowInstance;
    private GameObject objectAboveArrowInstance;
    private bool isPlayerInRange = false;
    private bool buttonPressed = false;

    private void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player object not assigned!", this);
            return;
        }

        // Инициализация объектов
        if (obj_socet != null)
        {
            obj_socet.SetActive(false);
        }

        CreateAndSetupArrow();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        // Если вошедший объект имеет нужный тег - включаем obj_socet
        if (other.CompareTag(triggerTag))
        {
            if (obj_socet != null)
            {
                obj_socet.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Если вышедший объект имеет нужный тег - выключаем obj_socet
        if (other.CompareTag(triggerTag))
        {
            if (obj_socet != null)
            {
                obj_socet.SetActive(false);
            }
        }
    }

    private void Update()
    {
        
        if (player == null) return;

        // Проверяем расстояние до игрока
        float distance = Vector3.Distance(transform.position, player.transform.position);
        isPlayerInRange = distance <= activationDistance;

        // Проверяем нажатие кнопки
        if (button_A != null)
        {
            buttonPressed = button_A.ReadValue() > 0.5f;
            Debug.Log(buttonPressed);
        }

        // Обрабатываем логику активации
        HandleActivationLogic();
    }

    private void HandleActivationLogic()
    {
        // Если игрок в радиусе и нажал кнопку
        if (isPlayerInRange && buttonPressed )
        {
            // Активируем/деактивируем стрелку и объект
            if (arrowInstance != null && objectAboveArrowInstance != null)
            {
                arrowInstance.SetActive(true);
                objectAboveArrowInstance.SetActive(true);
            }
            
        }
        else
        {
            arrowInstance.SetActive(false);
            objectAboveArrowInstance.SetActive(false); 
        }
    }

    private void CreateAndSetupArrow()
    {
        if (arrowPrefab != null)
        {
            arrowInstance = Instantiate(arrowPrefab, 
                transform.position + Vector3.up * arrowHeight, 
                Quaternion.identity, 
                transform);
            arrowInstance.SetActive(false);
            AddAnimation(arrowInstance);
        }

        if (objectAboveArrowPrefab != null && arrowInstance != null)
        {
            // Используем objectPositionOffset вместо фиксированной высоты
            objectAboveArrowInstance = Instantiate(objectAboveArrowPrefab, 
                arrowInstance.transform.position + objectPositionOffset, 
                Quaternion.Euler(objectRotation), 
                transform);
            objectAboveArrowInstance.SetActive(false);

            RemoveUnwantedComponents(objectAboveArrowInstance);
            ApplyMaterialToAllChildren(objectAboveArrowInstance, sharedMaterial);
            RemoveCollidersFromChildren(objectAboveArrowInstance);
            AddAnimation(objectAboveArrowInstance);
        }
    }

    private void RemoveUnwantedComponents(GameObject target)
    {
        var componentsToRemove = new System.Type[] { typeof(XRGrabInteractable), typeof(Rigidbody) };
        foreach (var componentType in componentsToRemove)
        {
            var component = target.GetComponent(componentType);
            if (component != null) Destroy(component);
        }
    }

    private void ApplyMaterialToAllChildren(GameObject parent, Material material)
    {
        if (material == null) return;

        foreach (var renderer in parent.GetComponentsInChildren<Renderer>())
        {
            renderer.material = material;
        }
    }

    private void RemoveCollidersFromChildren(GameObject parent)
    {
        foreach (var collider in parent.GetComponentsInChildren<Collider>())
        {
            Destroy(collider);
        }
    }

    private void AddAnimation(GameObject target)
    {
        if (target == null) return;

        var animation = target.AddComponent<SimpleAnimation>();
        animation.floatHeight = floatHeight;
        animation.floatSpeed = floatSpeed;
        animation.rotationSpeed = rotationSpeed;
    }

    private void OnDrawGizmosSelected()
    {
        // Визуализация радиуса активации в редакторе
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, activationDistance);
    }
}

public class SimpleAnimation : MonoBehaviour
{
    public float floatHeight = 0.1f;
    public float floatSpeed = 1f;
    public float rotationSpeed = 30f;

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.localPosition;
    }

    private void Update()
    {
        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.localPosition = new Vector3(startPosition.x, newY, startPosition.z);
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
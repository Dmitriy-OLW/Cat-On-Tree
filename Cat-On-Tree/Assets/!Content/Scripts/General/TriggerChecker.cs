using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.Interactables;


[RequireComponent(typeof(XRSocketInteractor))]
public class TriggerChecker : MonoBehaviour
{
    public bool isObjectInTrigger = false;
    
    [SerializeField] private string requiredTag = "YourRequiredTag"; // Укажите нужный тег в инспекторе
    [SerializeField] private bool disableColliders = true;

    private XRSocketInteractor socketInteractor;
    

    //public bool IsObjectInTrigger => isObjectInTrigger;

    private void Awake()
    {
        socketInteractor = GetComponent<XRSocketInteractor>();
    }

    private void OnEnable()
    {
        socketInteractor.selectEntered.AddListener(OnObjectPlaced);
        socketInteractor.selectExited.AddListener(OnObjectRemoved);
    }

    private void OnDisable()
    {
        socketInteractor.selectEntered.RemoveListener(OnObjectPlaced);
        socketInteractor.selectExited.RemoveListener(OnObjectRemoved);
    }

    private void OnObjectPlaced(SelectEnterEventArgs args)
    {
        var interactable = args.interactableObject.transform;
        
        // Проверяем тег объекта
        if (interactable.CompareTag(requiredTag))
        {
            isObjectInTrigger = true;
            
            if (disableColliders)
            {
                //отключаем шейдер
                var meshRenders_obj = interactable.GetComponentsInChildren<Transform>();
                foreach (var meshRender_obj in meshRenders_obj)
                {
                    if (meshRender_obj.tag == "Shader")
                    {
                        meshRender_obj.GetComponent<MeshRenderer>().enabled = false;
                    }
                    
                }
                // Отключаем все BoxCollider и CapsuleCollider на объекте и его дочерних элементах
                var boxColliders = interactable.GetComponentsInChildren<BoxCollider>();
                foreach (var collider in boxColliders)
                {
                    collider.enabled = false;
                }

                var capsuleColliders = interactable.GetComponentsInChildren<CapsuleCollider>();
                foreach (var collider in capsuleColliders)
                {
                    collider.enabled = false;
                }
            }
        }
        else
        {
            // Если тег не совпадает, выкидываем объект из сокета
            socketInteractor.interactionManager.SelectExit(
                socketInteractor as IXRSelectInteractor, 
                args.interactableObject as IXRSelectInteractable);
        }
    }

    private void OnObjectRemoved(SelectExitEventArgs args)
    {
        var interactable = args.interactableObject.transform;
        
        // Восстанавливаем коллайдеры при извлечении объекта
        if (disableColliders)
        {
            
            
            
            var boxColliders = interactable.GetComponentsInChildren<BoxCollider>();
            foreach (var collider in boxColliders)
            {
                collider.enabled = true;
            }

            var capsuleColliders = interactable.GetComponentsInChildren<CapsuleCollider>();
            foreach (var collider in capsuleColliders)
            {
                collider.enabled = true;
            }
        }
        
        isObjectInTrigger = false;
    }
}
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Readers;

using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic;

[RequireComponent(typeof(XRDirectInteractor))]
public class VRHandInteractor : MonoBehaviour
{
    public bool activateSpray = false;
    public bool activateMouse = false;
    public bool activateLaser = false;
    
    
    
    [SerializeField]
    XRInputValueReader<float> m_TriggerInput;   //Index

    [SerializeField]
    XRInputValueReader<float> m_GripInput;  
    [SerializeField]
    Animator animator;
    
    
    private XRDirectInteractor directInteractor;
    
    // Текущий выбранный объект (в руке)
    private IXRSelectInteractable currentSelectedObject;
    
    // Список объектов в зоне взаимодействия (в поле)
    private List<IXRInteractable> objectsInRange = new List<IXRInteractable>();

    private void Awake()
    {
        directInteractor = GetComponent<XRDirectInteractor>();
        
        // Подписываемся на события выбора/отпускания
        directInteractor.selectEntered.AddListener(OnSelectEntered);
        directInteractor.selectExited.AddListener(OnSelectExited);
    }

    private void Update()
    {
        // Обновляем список объектов в зоне взаимодействия
        UpdateObjectsInRange();
        
        // Пример использования:
        if (currentSelectedObject != null)
        {
            //Debug.Log($"Currently holding: {currentSelectedObject.transform.name}");
        }
        
        /*if (objectsInRange.Count > 0)
        {
            string objectsList = "Objects in range: ";
            foreach (var obj in objectsInRange)
            {
                objectsList += obj.transform.name + ", ";
            }
            Debug.Log(objectsList);
        }*/
        // Обновляем значения lendtree для рук
        HandUpdate();
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        currentSelectedObject = args.interactableObject;
        //Debug.Log($"Picked up: {currentSelectedObject.transform.name}");
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        //Debug.Log($"Dropped: {args.interactableObject.transform.name}");
        currentSelectedObject = null;
    }

    private void UpdateObjectsInRange()
    {
        // Очищаем предыдущий список
        objectsInRange.Clear();
        
        // Получаем все объекты в зоне взаимодействия
        directInteractor.GetValidTargets(objectsInRange);
        
        // Удаляем текущий выбранный объект из списка (если он есть)
        if (currentSelectedObject != null)
        {
            objectsInRange.Remove(currentSelectedObject as IXRInteractable);
        }
    }

    // Публичные методы для доступа из других скриптов
    public IXRSelectInteractable GetCurrentSelected()
    {
        return currentSelectedObject;
    }

    public List<IXRInteractable> GetObjectsInRange()
    {
        return objectsInRange;
    }

    public bool IsObjectInRange(GameObject obj)
    {
        foreach (var interactable in objectsInRange)
        {
            if (interactable.transform.gameObject == obj)
                return true;
        }
        return false;
    }

    private void OnDestroy()
    {
        // Отписываемся от событий при уничтожении объекта
        if (directInteractor != null)
        {
            directInteractor.selectEntered.RemoveListener(OnSelectEntered);
            directInteractor.selectExited.RemoveListener(OnSelectExited);
        }
    }


    private void HandUpdate()
    {

        if (currentSelectedObject != null)
        {
            if (currentSelectedObject.transform.tag == "Key")
            {
                
                float value = m_GripInput.ReadValue() * -1;
                animator.SetFloat("Grip", value);
            }
            else if (currentSelectedObject.transform.tag == "Textile")
            {
                
                float value = m_GripInput.ReadValue() ;
                animator.SetFloat("Grip", value * -1);
                animator.SetFloat("Trigger", value);
            }
            else if (currentSelectedObject.transform.tag == "Mouse")
            {
                float value = m_GripInput.ReadValue() * 0.4f;
                animator.SetFloat("Grip", value);
                currentSelectedObject.transform.eulerAngles =
                    currentSelectedObject.transform.eulerAngles + new Vector3(0, 180, 0);
                if (m_TriggerInput.ReadValue() > 0.5)
                {
                    activateMouse = true;
                }
            }
            else if (currentSelectedObject.transform.tag == "Spray" )
            {
                float value = m_GripInput.ReadValue() / 4 * 3;
                //animator.SetFloat("Trigger", m_TriggerInput.ReadValue());
                animator.SetFloat("Grip", value);
                if (m_TriggerInput.ReadValue() > 0.5)
                {
                    activateSpray = true;
                }
            }
            else if (currentSelectedObject.transform.tag == "Ladder"  || currentSelectedObject.transform.tag == "Ladder_Part" )
            {
                float value = m_GripInput.ReadValue() / 4 * 3;
                animator.SetFloat("Trigger", m_GripInput.ReadValue());
                animator.SetFloat("Grip", value);
                
                
            }
            else if (currentSelectedObject.transform.tag == "Laser")
            {
                if (m_TriggerInput.ReadValue() > 0.5)
                {
                    activateLaser = true;
                }
                
                animator.SetFloat("Grip", m_GripInput.ReadValue());
            }
            else if (currentSelectedObject.transform.tag == "Meat")
            {
                
                
                animator.SetFloat("Grip", m_GripInput.ReadValue());
                animator.SetFloat("Trigger", m_GripInput.ReadValue() * 0.2f);
            }
            else
            {
                animator.SetFloat("Grip", m_GripInput.ReadValue());
            }
        }
        else
        {
            animator.SetFloat("Trigger", m_TriggerInput.ReadValue());
            animator.SetFloat("Grip", m_GripInput.ReadValue());
        }

        if (m_TriggerInput.ReadValue() < 0.5)
        {
            activateLaser = false;
            activateMouse = false;
            activateSpray = false;
        }
        
    }
}


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
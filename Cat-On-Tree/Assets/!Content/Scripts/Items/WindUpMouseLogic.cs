using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Readers;

using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class WindUpMouseLogic : MonoBehaviour
{
    [SerializeField] private VRHandInteractor VRHandScript_R;
    [SerializeField] private VRHandInteractor VRHandScript_L;
    [SerializeField] private GameObject interactMouse; // Основная мышь, с которой можно взаимодействовать
    [SerializeField] private GameObject windUpMousePrefab; // Префаб заводной мыши
    [SerializeField] private GameObject XROriginCamera;
    private AudioSource soundofMouse;
    [SerializeField] private float forceMagnitude = 5f; // Сила воздействия
    [SerializeField] private float mouseDuration = 10f; // Длительность движения мыши

    private GameObject spawnedMouse;
    private Animator mouseAnimator;
    private Rigidbody mouseRigidbody;
    private float timer;
    private bool isMouseActive;

    private void Start()
    {
        soundofMouse = windUpMousePrefab.GetComponent<AudioSource>();
    }

    private void Update()
    {
        // Проверяем активацию спрея из любого контроллера
        if ((VRHandScript_L != null && VRHandScript_L.activateMouse) || 
            (VRHandScript_R != null && VRHandScript_R.activateMouse))
        {
            if (!isMouseActive && interactMouse.activeSelf)
            {
                ActivateWindUpMouse();
            }
        }

        // Логика таймера для активной мыши
        if (isMouseActive)
        {
            timer += Time.deltaTime;

            // Разблокируем X и Z на 3 секунде
            if (timer >= 2f)
            {
                mouseRigidbody.constraints &= ~RigidbodyConstraints.FreezeRotationX;
                mouseRigidbody.constraints &= ~RigidbodyConstraints.FreezeRotationZ;
                // Применяем силу вперед относительно направления мыши
                if (mouseRigidbody != null)
                {
                    mouseRigidbody.AddForce(spawnedMouse.transform.forward * forceMagnitude);
                }
            }

            

            // Выключаем мышь по истечении времени
            if (timer >= mouseDuration)
            {
                DeactivateWindUpMouse();
            }
        }
    }

    private void ActivateWindUpMouse()
    {
        // Выключаем основную мышь
        interactMouse.SetActive(false);

        // Получаем направление взгляда камеры (без учета наклона вверх/вниз)
        Vector3 cameraForward = XROriginCamera.transform.forward;
        cameraForward.y = 0f; // Игнорируем вертикальную составляющую
        cameraForward.Normalize();

        // Создаем новую мышь из префаба
        spawnedMouse = Instantiate(windUpMousePrefab, interactMouse.transform.position, Quaternion.LookRotation(cameraForward));
        soundofMouse = spawnedMouse.GetComponent<AudioSource>();
        // Находим и отключаем XRGrabInteractable
        XRGrabInteractable grabInteractable = spawnedMouse.GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.enabled = false;
        }

        // Настраиваем Rigidbody
        mouseRigidbody = spawnedMouse.GetComponent<Rigidbody>();
        if (mouseRigidbody != null)
        {
            mouseRigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }

        // Ищем аниматор на дочернем объекте
        mouseAnimator = spawnedMouse.GetComponentInChildren<Animator>();
        if (mouseAnimator != null)
        {
            mouseAnimator.enabled = true;
        }

        if (soundofMouse != null)
        {
            soundofMouse.Play();
        }
        spawnedMouse.SetActive(true);

        // Запускаем таймер
        timer = 0f;
        isMouseActive = true;
    }

    private void DeactivateWindUpMouse()
    {
        if (spawnedMouse != null)
        {
            spawnedMouse.SetActive(false);
            Destroy(spawnedMouse);
        }

        isMouseActive = false;
    }
}
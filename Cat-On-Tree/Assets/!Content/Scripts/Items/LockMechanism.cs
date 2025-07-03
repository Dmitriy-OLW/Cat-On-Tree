using UnityEngine;

public class LockMechanism : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private TriggerChecker triggerChecker;
    [SerializeField] private DoorScript doorScript;

    [Header("Obj")]
    [SerializeField] private GameObject SocetforKey;
    [SerializeField] private GameObject staticInKey;
    [SerializeField] private Rigidbody Loker;
    
    [Header("Sounds")]
    [SerializeField] private AudioClip KeySound;
    [SerializeField] private AudioSource unlockSound;


    [Header("Settings")]
    [SerializeField] private Vector3 keyRotation = new Vector3(0, 180, 0);
    [SerializeField] private float unlockDelay = 1f;
    [SerializeField] private float rotationDuration = 1.5f;
    [SerializeField] private float doorOpenDelay = 0.5f;

    private bool isUnlocking = false;
    private float timer = 0f;
    private Quaternion initialRotation;
    private Quaternion targetRotation;
    private bool rotationStarted = false;
    private bool doorOpened = false;

    private void Start()
    {
        if (staticInKey != null)
        {
            staticInKey.SetActive(false);
            initialRotation = staticInKey.transform.localRotation;
            targetRotation = Quaternion.Euler(keyRotation) * initialRotation;
        }
    }

    private void Update()
    {
        if (!isUnlocking && triggerChecker != null && triggerChecker.isObjectInTrigger)
        {
            StartUnlockProcess();
            
        }

        if (isUnlocking)
        {
            timer += Time.deltaTime;

            // Первая фаза: выключение ключей
            if (timer >= unlockDelay && !rotationStarted)
            {
                DisableKeys();
                rotationStarted = true;
                timer = 0f; // Сбрасываем таймер для следующей фазы
            }

            // Вторая фаза: поворот static_in_key
            if (rotationStarted && timer <= rotationDuration)
            {
                RotateStaticKey();
            }

            // Третья фаза: открытие двери
            if (rotationStarted && timer >= rotationDuration + doorOpenDelay && !doorOpened)
            {
                Loker.isKinematic = false;
                OpenDoor();
                doorOpened = true;
                isUnlocking = false;
            }
        }
    }

    private void StartUnlockProcess()
    {
        isUnlocking = true;
        timer = 0f;
        
        if (unlockSound != null)
        {
            unlockSound.PlayOneShot(KeySound);;
        }
    }

    private void DisableKeys()
    {
        GameObject[] keys = GameObject.FindGameObjectsWithTag("Key");
        foreach (GameObject key in keys)
        {
            key.SetActive(false);
        }
        SocetforKey.SetActive(false);
        staticInKey.SetActive(true);
    }

    private void RotateStaticKey()
    {
        if (staticInKey == null) return;

        float progress = Mathf.Clamp01(timer / rotationDuration);
        staticInKey.transform.localRotation = Quaternion.Lerp(initialRotation, targetRotation, progress);
    }

    private void OpenDoor()
    {
        if (doorScript != null)
        {
            doorScript.OpenDoor();
        }
    }
}
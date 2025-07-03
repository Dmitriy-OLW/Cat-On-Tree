using UnityEngine;
using System.Collections;

public class CatMeow : MonoBehaviour
{
    [SerializeField] private AudioClip[] meowSounds; // Массив звуков мяуканья
    [SerializeField] private AudioSource audioSource; // Компонент AudioSource для воспроизведения звуков
    
    [Tooltip("Минимальное время между звуками в секундах")]
    [SerializeField] private float minTimeBetweenMeows = 15f;
    
    [Tooltip("Максимальное время между звуками в секундах")]
    [SerializeField] private float maxTimeBetweenMeows = 60f;
    
    private float timeSinceLastMeow;
    private float timeUntilNextMeow;

    private void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }
        
        ResetMeowTimer();
        timeSinceLastMeow = 0f;
    }

    private void Update()
    {
        timeSinceLastMeow += Time.deltaTime;
        
        if (timeSinceLastMeow >= timeUntilNextMeow)
        {
            PlayRandomMeow();
            ResetMeowTimer();
            timeSinceLastMeow = 0f;
        }
    }

    private void PlayRandomMeow()
    {
        if (meowSounds == null || meowSounds.Length == 0)
        {
            Debug.LogWarning("No meow sounds assigned!");
            return;
        }
        
        int randomIndex = Random.Range(0, meowSounds.Length);
        audioSource.PlayOneShot(meowSounds[randomIndex]);
    }

    private void ResetMeowTimer()
    {
        timeUntilNextMeow = Random.Range(minTimeBetweenMeows, maxTimeBetweenMeows);
    }
}
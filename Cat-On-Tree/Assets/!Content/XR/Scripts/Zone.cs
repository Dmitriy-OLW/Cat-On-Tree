using UnityEngine;

public class Zone : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    bool enteredZone = false;

    private void Awake()
    {
        if (audioSource != null) 
        {
            audioSource = audioSource.GetComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!enteredZone) 
        {
            audioSource.Play();
            enteredZone = true;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

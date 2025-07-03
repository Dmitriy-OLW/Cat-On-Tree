using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Readers;

public class HandAnimation : MonoBehaviour
{
    [SerializeField]
    XRInputValueReader<float> m_TriggerInput;   //Index

    [SerializeField]
    XRInputValueReader<float> m_GripInput;      //Grip

    [SerializeField]
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //Debug.Log($"AAA {m_TriggerInput.ReadValue()} , {m_GripInput.ReadValue()}");
        animator.SetFloat("Trigger", m_TriggerInput.ReadValue());
        animator.SetFloat("Grip", m_GripInput.ReadValue());
    }
}

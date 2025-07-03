using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Readers;

public class HighlightTrigger : MonoBehaviour
{
    [SerializeField] private string tagToHighlight = "Shader";
    
    [SerializeField]
    XRInputValueReader<float> m_GripInput;

    private bool One_Done = false;
    
    

    private void Start()
    {
        GameObject[] shaderObjects = GameObject.FindGameObjectsWithTag("Shader");
        
        // Перебираем все найденные объекты
        foreach (GameObject shaderObject in shaderObjects)
        {
            DeactivateShader(shaderObject);
        }


    }



    private void OnTriggerStay(Collider other)
    {
        if (m_GripInput.ReadValue() > 0.5f)
        {
            if (other.transform.tag == tagToHighlight)
            {
                other.gameObject.GetComponent<MeshRenderer>().enabled = false;

            }
        }
        

    }

    private void DeactivateShader(GameObject shaderObject)
    {
        shaderObject.GetComponent<MeshRenderer>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {

            if (other.transform.tag == tagToHighlight)
            {
                other.gameObject.GetComponent<MeshRenderer>().enabled = true;

            }

    }

    private void OnTriggerExit(Collider other)
    {

        if (other.transform.tag == tagToHighlight)
        {
            other.gameObject.GetComponent<MeshRenderer>().enabled = false;

        }
        
    }

    



   
}
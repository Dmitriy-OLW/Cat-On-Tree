using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class GrabStabilizerXR : XRGrabInteractable
{
    [SerializeField] Transform rightAttachTransform;
    [SerializeField] Transform leftAttachTransform;
    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        

        if (args.interactorObject.transform.TryGetComponent(out LeftHand lefthand))
        {
            Debug.Log("Left hand");
            if (this.TryGetComponent(out XRGrabInteractable xRGrabInteractable))
            {
                xRGrabInteractable.attachTransform = leftAttachTransform;
            }
        }
        if (args.interactorObject.transform.GetComponent<RightHand>())
        {
            Debug.Log("Right Hand");
            if (this.TryGetComponent(out XRGrabInteractable xRGrabInteractable))
            {
                xRGrabInteractable.attachTransform = rightAttachTransform;
            }
        }
        base.OnSelectEntering(args);

        args.interactableObject.transform.SetParent(args.interactorObject.transform);
    }
}

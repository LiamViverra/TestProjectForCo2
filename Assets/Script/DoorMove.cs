using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoorMove : MonoBehaviour
{
    public Transform doorTransform;
    public Vector3 openRotation;
    public float openDuration = 1f;
    public Ease openEase = Ease.OutQuad;

    private Quaternion initialRotation;
    private Tween doorTween;

    void Start()
    {
        doorTransform = gameObject.transform;
        initialRotation = doorTransform.localRotation;
        openRotation = new Vector3(0, 200, 0);
    }

    public void OpenOrClose()
    {
        doorTween?.Kill(); 

        Quaternion targetRotation = (doorTransform.localRotation == initialRotation) ?
            initialRotation * Quaternion.Euler(openRotation) : 
            initialRotation; 

        doorTween = doorTransform.DOLocalRotateQuaternion(targetRotation, openDuration)
                                  .SetEase(openEase).Play();
    }
}

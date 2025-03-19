using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public static CameraMove cameraMove { get; private set; }
    private Ray ray;

    private int layerMask;
    private RaycastHit Hit;
    private const float DistanceSelectingAndViewItem = 8f;

    public Transform ObjPoint;
    public GameObject ObjItem;

    private Collider ObjItemCol;
    private Rigidbody ObjItemRb;

    private CanvasMove canvasMove;

    private void Awake() => cameraMove = this;

    void Start()
    {
        #region бубубуу UPD: взято из личного проекта
        int allLayersMask = ~0; //Битовая маска для всех слоев
        int ignoreRayLayer = LayerMask.NameToLayer("Ignore");
        int ignoreRayLayer2 = LayerMask.NameToLayer("Player");
        int ignoreRayMask = 1 << ignoreRayLayer;
        int ignoreRayMask2 = 1 << ignoreRayLayer2;
        layerMask = allLayersMask & ~ignoreRayMask & ~ignoreRayMask2;
        #endregion
        ObjPoint = PlayerMove.pm.transform.GetChild(1);
        canvasMove = CanvasMove.canvasMove;
    }

    void Update()
    {
        FollowingItem();
    }

    public void click()
    {
        try
        {
            ray = new(transform.position, transform.forward);
            Debug.DrawRay(transform.position, transform.forward * DistanceSelectingAndViewItem, Color.green);

            if (Physics.Raycast(ray, out Hit, DistanceSelectingAndViewItem, layerMask) && Hit.collider)
            {
                if (Hit.collider.tag == "bybyby" && ObjItem == null)
                {
                    ObjItem = Hit.collider.gameObject;

                    ObjItemCol = ObjItem.GetComponent<Collider>();
                    ObjItemRb = ObjItem.GetComponent<Rigidbody>();

                    ObjItemCol.enabled = false;
                    ObjItemRb.isKinematic = true;

                    canvasMove.ViewDropB(true);
                }

                DoorMove dm = Hit.collider.GetComponent<DoorMove>();
                if (dm != null)
                {
                    dm.OpenOrClose();
                    return;
                }
            }
        } 
        catch(Exception ex)
        {
            Debug.LogWarning(ex.Message);
        }
    }

    public void FollowingItem()
    {
        if (ObjItem == null || ObjPoint == null) return;
        ObjItem.transform.position = Vector3.Lerp(ObjItem.transform.position, ObjPoint.transform.position, 0.5f);
        ObjItem.transform.rotation = Quaternion.Lerp(ObjItem.transform.rotation, ObjPoint.transform.rotation, 0.5f);
    }

    public void DropItem()
    {
        if(ObjItem != null)
        {
            ObjItemCol.enabled = true;
            ObjItemRb.isKinematic = false;
            ObjItemRb.AddForce(transform.forward * 10f, ForceMode.Impulse);

            ObjItem = null;
            ObjItemCol = null;
            ObjItemRb = null;
            canvasMove.ViewDropB(false);
        }
    }
}

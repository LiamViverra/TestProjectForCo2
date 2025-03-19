using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasMove : MonoBehaviour
{
    public static CanvasMove canvasMove { get; private set; }
    private Button DropObj;
    private Text textCenter;
    private Text textCount;
    private Button JumpB;
    private Button UseB;

    private void Awake() => canvasMove = this;

    void Start()
    {
        DropObj = transform.GetChild(5).GetComponent<Button>();
        DropObj.onClick.AddListener(() => CameraMove.cameraMove.DropItem());
        DropObj.gameObject.SetActive(false);

        textCenter = transform.GetChild(1).GetComponent<Text>();
        textCount = transform.GetChild(2).GetComponent<Text>();
        
        JumpB = transform.GetChild(6).GetComponent<Button>();
        JumpB.onClick.AddListener(() => PlayerMove.pm.Jump());

        UseB = transform.GetChild(7).GetComponent<Button>();
        UseB.onClick.AddListener(() => CameraMove.cameraMove.click());
    }

    public void ViewDropB(bool v)
    {
        DropObj.gameObject.SetActive(v);
    }

    public void ViewTextCenter(string t)
    {
        textCenter.text = t;
    }

    public void ViewTextCount(int count)
    {
        textCount.text = count.ToString();
    }
}

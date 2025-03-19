using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public static PlayerMove pm { get; private set; }

    #region Передвижение
    [Header("Передвижение")]
    public FixedJoystick joystickMove;
    public FixedJoystick joystick;
    public float SpeedMove;
    private bool PlayerIsGo;
    public bool cursorOn;
    private CharacterController player;
    private float TimerJump;

    [Range(1f, 3f)] public float SpeedJump;
    [Range(0f, 15f)] public float gravity;
    public float TimerJumpLoad;

    private Vector3 MoveDir;

    float x_Move;
    float z_Move;

    float Stamina;

    public bool onMove;

    #endregion

    #region Управление взглядом
    [Header("Управление взглядом")]
    public Camera playerCamera;
    public GameObject playerGameObject;
    public float sensivity;
    public float smoothTime = 0.1f;
    float xRot;
    float yRot;
    float xRotCurrent;
    float yRotCurrent;
    float currentVelosityX;
    float currentVelosityY;

    #endregion

    private void Awake() => pm = this;
    
    void Start()
    {
        #region Move/MouseMove
        joystickMove = CanvasMove.canvasMove.transform.GetChild(3).GetComponent<FixedJoystick>();
        joystick = CanvasMove.canvasMove.transform.GetChild(4).GetComponent<FixedJoystick>();

        SpeedJump = 2f;
        gravity = 4.5f;
        TimerJumpLoad = 0.9f;

        playerCamera = transform.GetComponentInChildren<Camera>();
        playerGameObject = gameObject;

        player = GetComponent<CharacterController>();
        sensivity = 2.8f;
        SpeedMove = 6f;
        PlayerIsGo = false;
        cursorOn = false;

        onMove = true;

        #endregion
    }

    void Update()
    {
        if (onMove)
        {
            playerMove();
            MouseMove();
            TimerJumpMove();
        }
    }

    public void playerMove()
    {
        x_Move = joystickMove.Horizontal;
        z_Move = joystickMove.Vertical;

        Vector3 cameraForward = playerCamera.transform.forward;
        Vector3 cameraRight = playerCamera.transform.right;
        cameraForward.y = 0f;
        cameraRight.y = 0f;
        cameraForward.Normalize();
        cameraRight.Normalize();

        if (player.isGrounded)
            MoveDir = (cameraForward * z_Move + cameraRight * x_Move).normalized;
        
        if (MoveDir != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(MoveDir);
            playerGameObject.transform.rotation = Quaternion.Slerp(
                playerGameObject.transform.rotation,
                targetRotation,
                10f * Time.deltaTime
            );
        }
        player.Move(MoveDir * SpeedMove * Time.deltaTime);

        //Изначально для пк
/*        PlayerIsGo = x_Move != 0 || z_Move != 0; //Проверка нажатия на клавишу w / a / s / d

        if (PlayerIsGo && Input.GetKey(KeyCode.LeftShift)) //Бег. Первое условие это нажание на клавишу 
        {
            Stamina = Mathf.Max(Stamina -= 5f * Time.deltaTime, 0f);
            SpeedMove = Stamina > 0 ? 10f : 6f;
        }
        else
        {
            SpeedMove = 6f;
            Stamina = Mathf.Min(Stamina += 10f * Time.deltaTime, 100f);
        }*/
    }

    public void MouseMove()
    {
        float lookX = joystick.Horizontal;
        float lookY = joystick.Vertical;

        xRot += lookX * sensivity;
        yRot += lookY * sensivity;
        yRot = Mathf.Clamp(yRot, -90, 90);

        xRotCurrent = Mathf.SmoothDamp(xRotCurrent, xRot, ref currentVelosityX, smoothTime);
        yRotCurrent = Mathf.SmoothDamp(yRotCurrent, yRot, ref currentVelosityY, smoothTime);

        playerCamera.transform.localRotation = Quaternion.Euler(-yRotCurrent, xRotCurrent, 0f);
        playerGameObject.transform.rotation = Quaternion.Euler(0f, xRotCurrent, 0f);
    }

    private void TimerJumpMove()
    {
        if (TimerJump > 0f) TimerJump -= 1f * Time.deltaTime;
        if (TimerJump < 0) TimerJump = 0f;
    }

    void FixedUpdate()
    {
        MoveDir.y -= gravity * Time.deltaTime;
    }
    public void Jump()
    {
        if (TimerJump == 0)
        {
            MoveDir.y = SpeedJump;
            TimerJump = TimerJumpLoad;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game gameManager { get; private set; }
    public ScreenOrientation prefOr = ScreenOrientation.LandscapeLeft;

    private void Awake() => gameManager = this;

    private void Start()
    {
        Screen.orientation = prefOr;
    }

    public void EndGame(int count)
    {
        PlayerMove.pm.cursorOn = true;
        PlayerMove.pm.onMove = false;
        string s = $"���� ���������!\n��� ����: {count}";
        CanvasMove.canvasMove.ViewTextCenter(s);
    }
}

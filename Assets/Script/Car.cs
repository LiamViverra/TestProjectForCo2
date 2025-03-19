using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public int Counter;
    private CanvasMove canvas;

    void Start()
    {
        Counter = 0;
        canvas = CanvasMove.canvasMove;
    }

    void Update()
    {
        if(Counter >= 10)
        {
            Game.gameManager.EndGame(Counter);
            this.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "bybyby")
        {
            Counter++;
            canvas.ViewTextCount(Counter);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "bybyby")
        {
            Counter--;
            canvas.ViewTextCount(Counter);
        }
    }
}

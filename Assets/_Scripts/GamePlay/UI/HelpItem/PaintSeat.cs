using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaintSeat : MonoBehaviour
{
    public Button paintSeatButton;
    private void Start()
    {
        paintSeatButton.onClick.AddListener(OnPaintSeatButtonClick);
    }

    void OnPaintSeatButtonClick()
    {
        Debug.Log(3);
    }
}

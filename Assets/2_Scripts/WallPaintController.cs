using System;
using System.Collections;
using System.Collections.Generic;
using PaintIn3D;
using TMPro;
using UnityEngine;

public class WallPaintController : MonoBehaviour
{
    [SerializeField] private P3dPaintable paintable;
    [SerializeField] private P3dPaintableTexture paintableTexture;
    [SerializeField] private P3dColor paintableColor;

    [SerializeField] private GameObject winPanel;

    public TextMeshProUGUI percentText;
    
    private void OnEnable()
    {
        EventManager.finish += FinishGame;
    }

    private void OnDisable()
    {
        EventManager.finish -= FinishGame;
    }

    void FinishGame()
    {
        paintable.enabled = true;
        paintableTexture.enabled = true;
        paintableColor.enabled = true;
        percentText.gameObject.SetActive(true);
    }
    
    private void Update()
    {
        if (Preferences.isFinish)
        {
            percentText.text = (paintableColor.Ratio*100).ToString("#") + "%";
        }
        if( paintableColor.Ratio >= .9f)
        {
            winPanel.SetActive(true);
        }
    }
}

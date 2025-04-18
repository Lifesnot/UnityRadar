using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    private UI_Radar UI_Radar;
    private int index;
    private Text heroName;
    public HeroData[] heros;
    
    private void Start()
    {
        UI_Radar = FindObjectOfType<UI_Radar>();
        heroName = transform.Find("Name").GetComponent<Text>();
    }

    private void Update()
    {
        if (heros == null || heros.Length <= 0) return;
        if (index < heros.Length)
        {
            //更新数据
            UI_Radar.RefreshVerDist(heros[index]);
            heroName.text = heros[index].heroName;
            index++;
        }
    }

    private void OnDestroy()
    {
        UI_Radar = null;
        heros = null;
    }
}

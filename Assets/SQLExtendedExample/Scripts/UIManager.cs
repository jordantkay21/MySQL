using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoSingleton<UIManager>
{
    private int _sphereHitCount;
    private int _cubeHitCount;
    private int _capsuleHitCount;

    [SerializeField]
    private TextMeshProUGUI _sphereText;
    [SerializeField]
    private TextMeshProUGUI _cubeText;
    [SerializeField]
    private TextMeshProUGUI _capsuleText;

    private void SetTexts()
    {
        _sphereText.SetText(_sphereHitCount.ToString());
        _cubeText.SetText(_cubeHitCount.ToString());
        _capsuleText.SetText(_capsuleHitCount.ToString());
    }

    public void SetValues(int id, int hits)
    {
        switch (id)
        {
            case 0:
                _sphereHitCount = hits;
                break;
            case 1:
                _cubeHitCount = hits;
                break;
            case 2:
                _capsuleHitCount = hits;
                break;
        }

        SetTexts();
    }
}

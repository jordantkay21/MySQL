using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    private int id = 1;
    [SerializeField]
    private int _hitCount;

    public void SetHitCount(int hitCount)
    {
        _hitCount = hitCount;
    }
    private void OnMouseDown()
    {
        _hitCount++;
        SQLManager.Instance.AddValueToDatabase(id, _hitCount);
        UIManager.Instance.SetValues(id, _hitCount);
    }

}

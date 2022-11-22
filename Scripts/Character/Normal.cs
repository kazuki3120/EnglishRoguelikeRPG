using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Normal : MonoBehaviour
{
    void Start()
    {
        this.transform.DOLocalMoveY(-20, 2f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutQuad);
    }
}
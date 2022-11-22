using DG.Tweening;
using TMPro;
using UnityEngine;


public class TitleLogo : MonoBehaviour
{
    TextMeshProUGUI tmpro;

    DOTweenTMPAnimator tmproAnimator;

    private void Awake()
    {
        tmpro = GetComponent<TextMeshProUGUI>();
        tmproAnimator = new DOTweenTMPAnimator(tmpro);

        DOTween.Sequence()
            //.Append(tmpro.DOGlowColor(1f, 1))
            //.SetDelay(0.5f)
            .Append(tmpro.DOFade(0, 1f))
            .SetDelay(0.25f)
            //.Append(tmpro.DOFade(1, 1))

            .SetLink(gameObject) //アニメーション破壊（バグ回避）
            .SetLoops(-1, LoopType.Yoyo);

    }

}

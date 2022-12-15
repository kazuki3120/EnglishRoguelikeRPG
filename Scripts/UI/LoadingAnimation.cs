using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class LoadingAnimation : MonoBehaviour
{
    //TextMeshPro
    TextMeshProUGUI tmpro;
    //TextMeshProアニメーション
    DOTweenTMPAnimator tmproAnimator;
    //このアニメーションのシーケンス
    Sequence sequence;


    void Start()
    {

        tmpro = GetComponent<TextMeshProUGUI>();

        tmpro.DOFade(0, 0);


        tmproAnimator = new DOTweenTMPAnimator(tmpro);


        sequence = DOTween.Sequence();
        sequence.SetLoops(-1, LoopType.Restart);

        //アニメーション間隔固定0.4f
        var duration = 0.4f;

        sequence.Pause();

        //1文字ごとにアニメーションさせる
        for (int i = 0; i < tmproAnimator.textInfo.characterCount; ++i)
        {
            //tmproAnimator.DOScaleChar(i, 0.7f, 0);
            Vector3 currCharOffset = tmproAnimator.GetCharOffset(i);

            //シーケンス
            sequence.Join(DOTween.Sequence()
                .Join(tmproAnimator.DOFadeChar(i, 1, duration)) //文字フェードイン
                .Append(tmproAnimator.DOOffsetChar(i, currCharOffset + new Vector3(0, 20, 0), duration).SetEase(Ease.OutFlash, 2)) //オフセット
                .AppendInterval(1f) //インターバル
                .Join(tmproAnimator.DOFadeChar(i, 0, duration)) //フェードアウト
                .SetDelay(0.07f * i) //遅らせる　 文字番号（i）* 間隔（0.07f）
                );
        }
    }

    //アニメーション開始
    public void AnmRestart() => sequence.Restart();

    //アニメーション停止
    public void AnmPause() => sequence.Pause();



}



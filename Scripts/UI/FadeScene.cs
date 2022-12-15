using UnityEngine;
using DG.Tweening;


public class FadeScene : MonoBehaviour
{
    public CanvasGroup fadeCanvas;

    public LoadingAnimation loadingAnimation;
    
    
    //シングルトン化
    static public FadeScene Fadeinstance;

    private void Awake()
    {
        if (Fadeinstance == null)
        {
            Fadeinstance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    //フェードイン
    public void FadeIn()
    {
        fadeCanvas.interactable = true;
        fadeCanvas.blocksRaycasts = true;
        fadeCanvas.DOFade(1, 0.5f);
        //アニメーション開始
        loadingAnimation.AnmRestart();
    }

    //フェードアウト
    public void FadeOut()
    {
        fadeCanvas.interactable = false;
        fadeCanvas.blocksRaycasts = false;
        fadeCanvas.DOFade(0, 0.5f);
        //アニメーション停止
        loadingAnimation.AnmPause();
    }

    //フェードイン＆アウト
    public void FadeInToOut(TweenCallback action = null)
    {
        fadeCanvas.interactable = true;
        fadeCanvas.blocksRaycasts = true;

        //アニメーション開始
        loadingAnimation.AnmRestart();

        //OnComplete >> DOFade 完了してから action() FadeOut() 実行
        fadeCanvas.DOFade(1, 0.5f).OnComplete(
            () =>
            {
                action(); //シーン遷移
                FadeOut(); //フェードアウト
            });
    }

}

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RoadData : MonoBehaviour
{

    //StdDataスクリプト
    public StdData stdData;

    void Awake()
    {   
        //ログインユーザーの取得
        stdData.currentUser();
        //データの取得表示
        stdData.SumAns();
        stdData.CorrectAns();

        StartCoroutine(std());
    }

    //解答数と正解数を取得し終えてから割合を計算
    IEnumerator std()
    {
        yield return new WaitForSeconds(1);
        //正解率を表示
        stdData.RateAns();
        //フェードアウト
        FadeScene.Fadeinstance.FadeOut();
    }



    //戻るボタン
    public void OnBackButton()
    {
        //フェード+画面遷移
        FadeScene.Fadeinstance.FadeInToOut(
            () => SceneManager.LoadSceneAsync("Menuselect"));
    }

}

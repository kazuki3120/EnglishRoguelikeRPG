using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;


public class clearManager : MonoBehaviour
{
    //リザルト読み込み
    public GameObject resultObj;


    //テキスト
    [SerializeField] private Text clearText = default;
    [SerializeField] private Text clearText2 = default;

    //フェード
    [SerializeField] GameObject fadeObj;
    [SerializeField] Fade fade;

    //ボタン
    [SerializeField] Transform resultBtn;
    [SerializeField] Transform menuBtn;

    //リザルト画面
    [SerializeField] GameObject resultCvs;

    //ゲームクリアフラグ
    public static bool gameclearflag = false;



    void Start()
    {
        fadeObj.SetActive(true);

        
        //シングルトン（マップ画面）が残っている場合削除する（DontDestroyからこのシーンに移すことで削除）
        if (MapManager.instance)
        {
            SceneManager.MoveGameObjectToScene(
                MapManager.instance.gameObject, SceneManager.GetActiveScene());
        }

        //マップデータ(json)の削除　
        PlayerPrefs.DeleteKey("MapInfo");
        Debug.Log("PlayerPrefs(MapInfo)を削除しました!");

        //コルーチン
        StartCoroutine(start());
    }

    IEnumerator start()
    {
        //ゲームクリア処理
        if (gameclearflag == true)
        {
            clearText.text = "GAMECLEAR!!";
            //フェードアウトで遷移
            yield return fade.FadeOut(1f);
            clearText2.DOText("Congratulations!", 2);
        }

        //ゲームオーバー処理
        else 
        {
            clearText.text = "GAMEOVER...";
            //フェードアウトで遷移
            yield return fade.FadeOut(1f);
            clearText2.DOText("Lets' Try Again!", 2);
        }

        yield return new WaitForSeconds(2f);

        //ボタンの出現
        resultBtn.transform.DOScale(new Vector3(1, 1, 1), 0.5f);
        menuBtn.transform.DOScale(new Vector3(1, 1, 1), 0.5f);

    }


    //リザルト画面遷移
    public void OnClickResult()
    {
        resultCvs.SetActive(true);
        //resultObj.GetComponent<result>().callresult();
    }


    //メニュー画面遷移

    public void OnClickMenu()
    {
        //フェード+画面遷移
        FadeScene.Fadeinstance.FadeInToOut(
            () => SceneManager.LoadSceneAsync("Menuselect"));
    }


    //リザルト画面を閉じる
    public void OnClickResuktBack()
    {
        resultCvs.SetActive(false);
    }

}

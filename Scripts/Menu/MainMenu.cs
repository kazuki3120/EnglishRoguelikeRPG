using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    //アニメーション定義
    public AnimatedDialog animatedDialog;

    //ボタン
    [SerializeField] private Button[] btn = new Button[4];


    // コンティニューボタン-----------------------------------------------------------------
    public void OnClickContinueButton()
    {
        //PlayPrefsの"MapInfo"が存在するか
        bool key = PlayerPrefs.HasKey("MapInfo");

        if (key) StartCoroutine(Map()); //存在する場合  

        else StartCoroutine(Dialog()); //存在しない場合
    }

    //マップに遷移
    IEnumerator Map()
    {
        btn[1].interactable = false;
        btn[2].interactable = false;
        btn[3].interactable = false;

        yield return new WaitForSeconds(0.5f);

        //フェード+画面遷移
        FadeScene.Fadeinstance.FadeInToOut(
            () => SceneManager.LoadSceneAsync("main"));
    }

    //ダイアログの表示
    IEnumerator Dialog()
    {
        animatedDialog.Open();
        FadeScene.Fadeinstance.fadeCanvas.blocksRaycasts = true; //操作不能にする
        yield return new WaitForSeconds(2);
        animatedDialog.Close();
        yield return new WaitForSeconds(1);
        FadeScene.Fadeinstance.fadeCanvas.blocksRaycasts = false;
    }


    // スタートボタン-----------------------------------------------------------------
    public void OnClickStartButton() => StartCoroutine(stageSelect());

    private IEnumerator stageSelect()
    {
        btn[0].interactable = false;
        btn[2].interactable = false;
        btn[3].interactable = false;

        //シングルトン（マップ画面）が残っている場合削除する（DontDestroyからこのシーンに移すことで削除）
        if (MapManager.instance)
        {
            SceneManager.MoveGameObjectToScene(
                MapManager.instance.gameObject, SceneManager.GetActiveScene());
        }

        //PlayerPrefsのjsonファイル削除
        PlayerPrefs.DeleteKey("MapInfo");
        Debug.Log("PlayerPrefs(MapInfo)を削除しました!");

        yield return new WaitForSeconds(0.5f);

        //フェード+画面遷移
        FadeScene.Fadeinstance.FadeInToOut(
            () => SceneManager.LoadSceneAsync("Stageselect"));
    }


    //学習履歴ボタン--------------------------------------------------------------------
    public void OnClickLearningInfoButton() => StartCoroutine(LearningInfo());

    private IEnumerator LearningInfo()
    {
        btn[0].interactable = false;
        btn[1].interactable = false;
        btn[3].interactable = false;

        yield return new WaitForSeconds(0.5f);

        //フェード+画面遷移
        yield return new WaitForSeconds(0.5f);
        FadeScene.Fadeinstance.FadeInToOut(
            () => SceneManager.LoadSceneAsync("LearningInfo"));
    }


    // オプションボタン-----------------------------------------------------------------
    public void OnClickOptionButton() => StartCoroutine(Option());

    private IEnumerator Option()
    {
        btn[0].interactable = false;
        btn[1].interactable = false;
        btn[2].interactable = false;

        yield return new WaitForSeconds(0.5f);

        //フェード+画面遷移
        FadeScene.Fadeinstance.FadeInToOut(
            () => SceneManager.LoadSceneAsync("Option"));
    }


    //ゲーム終了ボタン------------------------------------------------------------------
    public void OnClickQuitButton() => StartCoroutine(Quit());

    private IEnumerator Quit() //コルーチン
    {
        yield return new WaitForSeconds(0.5f);

        //Prefsにjsonを保存するコード----------------------------------------------------


        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }


}
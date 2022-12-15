using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageSelect : MonoBehaviour
{

    //アニメーション
    private Animator anim;

    //学年ごとのゲームオブジェクト
    [SerializeField] private GameObject unitStudy;
    [SerializeField] private GameObject genreStudy;

    //ボタン
    [SerializeField] private Button[] btn = new Button[4];

    //キャンバス
    [SerializeField] private GameObject unitSelect;

    //ゲームオブジェクト
    [SerializeField] private GameObject stageSelect; //YearSelectcanvas >> StageSelect

    //ジャンルフラグ
    private bool genreCvs;


    void Awake()
    {
        //アニメーションコンポーネント取得
        anim = GetComponent<Animator>();
        //変数の初期化
        genreCvs = false;
    }


    //１年生ボタン処理
    public void OnClick1st()
    {
        btn[1].interactable = false;
        btn[2].interactable = false;
        btn[3].interactable = false;

        StartCoroutine(stage());
        
        UnitSelect.yearStage = 1;
        Debug.Log("学年" + UnitSelect.yearStage);
    }

    //２年生ボタン処理
    public void OnClick2nd()
    {
        btn[0].interactable = false;
        btn[2].interactable = false;
        btn[3].interactable = false;

        StartCoroutine(stage());

        UnitSelect.yearStage = 2;
        Debug.Log("学年" + UnitSelect.yearStage);
    }

    //３年生ボタン処理
    public void OnClick3rd()
    {
        btn[0].interactable = false;
        btn[1].interactable = false;
        btn[3].interactable = false;

        StartCoroutine(stage());

        UnitSelect.yearStage = 3;
        Debug.Log("学年: " + UnitSelect.yearStage);
    }

    //ジャンルボタン処理
    public void OnClickGenre()
    {
        genreCvs = true;
        btn[0].interactable = false;
        btn[1].interactable = false;
        btn[2].interactable = false;

        StartCoroutine(stage());

        UnitSelect.yearStage = 0;
        Debug.Log("ジャンル別 yearStage=" + UnitSelect.yearStage);
    }


    IEnumerator stage()
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("close", true);
        yield return new WaitForSeconds(0.5f);
        //キャンバスを非アクティブ
        stageSelect.SetActive(false);
        //次のキャンバスをアクティブ
        unitSelect.SetActive(true);

        //ジャンル別だったらジャンルをアクティブ
        if (genreCvs == true) genreStudy.SetActive(true);
        else unitStudy.SetActive(true);
    }


    //戻るボタン処理
    public void OnClickBack()
    {
        //単元選択時
        if (unitSelect.activeSelf)
        {
            //フェード+リロードシーン
            FadeScene.Fadeinstance.FadeInToOut(
                () => SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name));
        }

        //学年選択時
        else
        {
            //フェード+メニュー画面遷移
            FadeScene.Fadeinstance.FadeInToOut(
                () => SceneManager.LoadScene("MenuSelect"));
        }
    }


}

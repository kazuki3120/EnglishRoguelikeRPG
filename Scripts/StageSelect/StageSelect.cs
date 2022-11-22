using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageSelect : MonoBehaviour
{

    //アニメーション
    private Animator anim;

    //学年ごとのゲームオブジェクト
    [SerializeField] private GameObject firstStudy;
    [SerializeField] private GameObject secondStudy;
    [SerializeField] private GameObject thirdStudy;

    //ボタン
    [SerializeField] private Button[] btn = new Button[3];

    //キャンバス
    [SerializeField] private GameObject unitSelect; //UnitSelectCanvas
    //ゲームオブジェクト
    [SerializeField] private GameObject stageSelect; //YearSelectcanvas >> StageSelect


    void Awake()
    {
        //アニメーションコンポーネント取得
        anim = GetComponent<Animator>();
    }


    //１年生ボタン処理
    public void OnClick1st()
    {
        btn[1].interactable = false;
        btn[2].interactable = false;

        StartCoroutine(stage());

        //１年生をアクティブ
        firstStudy.SetActive(true);
        
        UnitSelect.yearStage = 1;
        Debug.Log("学年" + UnitSelect.yearStage);
    }

    //２年生ボタン処理
    public void OnClick2nd()
    {
        btn[0].interactable = false;
        btn[2].interactable = false;

        StartCoroutine(stage());

        //１年生をアクティブ
        secondStudy.SetActive(true);

        UnitSelect.yearStage = 2;
        Debug.Log("学年" + UnitSelect.yearStage);
    }

    //３年生ボタン処理
    public void OnClick3rd()
    {
        btn[0].interactable = false;
        btn[2].interactable = false;

        StartCoroutine(stage());

        //１年生をアクティブ
        thirdStudy.SetActive(true);

        UnitSelect.yearStage = 3;
        Debug.Log("学年: " + UnitSelect.yearStage);
    }

    IEnumerator stage()
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("close", true);
        yield return new WaitForSeconds(0.5f);
        //キャンバスを非アクティブ
        stageSelect.SetActive(false);
        //UnitSelectキャンバスをアクティブ
        unitSelect.SetActive(true);
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
                () => SceneManager.LoadSceneAsync("MenuSelect"));
        }
    }


}

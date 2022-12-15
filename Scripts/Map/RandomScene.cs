using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class RandomScene : MonoBehaviour
{
    [SerializeField] private Fade fade;
    List<int> numbers = new List<int>();


    private int yearFlag;
    private int unitFlag;
    private List<int> genreFlag;


    void Start()
    {
        //学年　フラグ (1 ~ 3)
        yearFlag = UnitSelect.yearStage;
        //unit フラグ (1 ~ 4)
        unitFlag = UnitSelect.unitStage;
        //ジャンル別　フラグ (1 ~ 22)
        genreFlag = UnitSelect.genreList;
    }


    //戦闘用ランダムシーンチェンジ
    public void RandomSceneChange()
    {
        Debug.Log("学年" + yearFlag);
        Debug.Log("単元" + unitFlag);

        //フェードで画面遷移（演出１秒間）
        fade.FadeIn(1f, () =>
        {
            SceneManager.LoadSceneAsync("1");
            fade.FadeOut(1f);
        });
    }


    //イベント用ランダムシーンチェンジ
    public void RandomSceneChangeEvent()
    {
        //フェードで画面遷移（演出１秒間）
        fade.FadeIn(1f, () =>
        {
            SceneManager.LoadSceneAsync("1");
            fade.FadeOut(1f);
        });
    }
}
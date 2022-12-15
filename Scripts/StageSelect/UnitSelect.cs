using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class UnitSelect : MonoBehaviour
{
    [SerializeField] GameObject[] Units = new GameObject[4];
    [SerializeField] GameObject unit;
    [SerializeField] GameObject genre;

    //学年
    public static int yearStage = 0;

    //Unit
    public static int unitStage = 0;

    //ジャンル
    public static List<int> genreList  = new List<int>();


    void Start()
    {
        //学年に合わせてキャンバスの子オブジェクトをアクティブ化
        if (yearStage == 0) genre.gameObject.SetActive(true);

        else StartCoroutine(GetChildren(unit.gameObject));
    }

    IEnumerator GetChildren(GameObject obj)
    {
        Transform children = obj.GetComponentInChildren<Transform>();

        //子要素がいなければ終了
        if (children.childCount == 0) yield break;

        //子オブジェクトを全て取得
        foreach (Transform ob in children)
        {
            yield return new WaitForSeconds(0.5f);
            Transform trs = ob.gameObject.GetComponent<Transform>();
            trs.transform.DOScale(new Vector3(1, 1, 1), 0.5f);

            GetChildren(ob.gameObject);
        }
    }


    //Unit選択ボタン処理
    public void OnClickUnitFirst()
    {
        unitStage = 1;
        OnClickStart();

    }

    public void OnClickUnitSecond() 
    {
        unitStage = 2;
        OnClickStart();
    }

    public void OnClickUnitThird()
    {
        unitStage = 3;
        OnClickStart();
    }
    
    public void OnClickUnitFour()
    {
        unitStage = 4;
        OnClickStart();
    }


    //ジャンル選択トグル処理（すべてのジャンルトグルにアタッチ）
    public void OnClickGenreToggle(int genreNum)
    {
        //オンの場合リストに追加、オフの場合リストから削除
        if (genreList.Contains(genreNum) == true) 
        {
            genreList.Remove(genreNum);
            Debug.Log("削除：" + genreNum);
        }
        
        else 
        {
            genreList.Add(genreNum);
            Debug.Log("追加：" + genreNum);
        }

        Debug.Log("リスト：" + string.Join(",", genreList.Select(n => n.ToString())));
    }


    //スタートボタン処理
    public void OnClickStart()
    {
        Debug.Log("Unit：" + unitStage);
        Debug.Log("リスト：" + string.Join(",", genreList.Select(n => n.ToString())));

        //値がない時は処理終了（Unit or Genre）
        if(unitStage == 0 && genreList.Count == 0) return;

        //画面遷移
        else
        {
            FadeScene.Fadeinstance.FadeInToOut(
            () => SceneManager.LoadScene("main"));
        }
    }

}

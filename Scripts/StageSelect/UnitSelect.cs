using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UnitSelect : MonoBehaviour
{
    [SerializeField] GameObject[] Units = new GameObject[4];
    [SerializeField] GameObject ffff;
    [SerializeField] GameObject ssss;
    [SerializeField] GameObject tttt;

    //学年
    public static int yearStage = 0;

    //Unit
    public static int unitStage = 0;



    void Start()
    {
        //学年に合わせてキャンバスの子オブジェクトをアクティブ化
        if (yearStage == 1) StartCoroutine(GetChildren(ffff.gameObject));
        else if (yearStage == 2) StartCoroutine(GetChildren(ssss.gameObject));
        else if (yearStage == 3) StartCoroutine(GetChildren(tttt.gameObject));
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

    //マップ画面遷移
    public void OnClickUnitFirst()
    {
        unitStage = 1;
        Debug.Log("Unit: " + unitStage);
        //フェード+マップ画面遷移
        FadeScene.Fadeinstance.FadeInToOut(
            () => SceneManager.LoadSceneAsync("main"));
    }



}

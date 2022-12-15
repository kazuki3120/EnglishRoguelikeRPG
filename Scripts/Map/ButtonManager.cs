using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class ButtonManager : MonoBehaviour
{
    MapInfo mapInfo;

    //解放するボタンの配列
    [SerializeField] private GameObject[] objArray;

    //ライン
    public GameObject lineObject;
    LineRenderer lines;
    [SerializeField] private GameObject Line;

    //最後の敵を倒したときのフラグ
    public static bool fin = false;


    void Start()
    {
        mapInfo = new MapInfo();

        Button b = GetComponent<Button>();

        //線を引く
        foreach (GameObject obj in objArray)
        {
            GameObject newLine = Instantiate(Line) as GameObject;
            newLine.transform.parent = lineObject.transform;

            lines = newLine.GetComponent<LineRenderer>();
            //線の幅を決める
            lines.startWidth = 2f;
            lines.endWidth = 2f;

            //頂点の数を決める
            lines.positionCount = 2;
            lines.SetPosition(0, this.transform.position);
            lines.SetPosition(1, obj.transform.position);
        }
    }

    void Update()
    {
        //クリアした時の処理
        if (MapManager.instance.Clear == true)
        {
            //次のステージの解放
            StageClear();
        }
    }



    //----------------------------------------------------------------------------------
    //ステージのボタンが押されたときに呼ばれるメソッド（バトル画面に遷移）
    //----------------------------------------------------------------------------------
    public void TransitionBattle()
    {
        //アニメーションのコンポーネント取得
        MapManager.instance.animator = GetComponent<Animator>();

        //解放したボタンを無効化
        foreach (GameObject obj in MapManager.instance.cpArray)
        {
            Button btn = obj.GetComponent<Button>();
            btn.interactable = false;
        }

        //押したボタンの無効化
        Button thisBtn = GetComponent<Button>();
        thisBtn.interactable = false;


        //解放したいステージを配列に入れておく
        //配列の要素数指定
        MapManager.instance.cpArray = new GameObject[2];
        //配列の長さが0だった場合、１にする
        if (MapManager.instance.cpArray.Length == 0) MapManager.instance.cpArray = new GameObject[1];

        Debug.Log("cpArrayの要素数: " + MapManager.instance.cpArray.Length);

        //アクティブ状態のボタンのみコピー
        int index = 0;
        foreach (GameObject cp in objArray)
        {
            if (cp.activeSelf)
            {
                MapManager.instance.cpArray[index] = cp;
                Debug.Log($"cpArray[{index}]: {MapManager.instance.cpArray[index].name}");
                index++;
            }
        }
    }


    //最後のステージの処理
    public void finStage()
    {
        //もし最後のステージだったらフラグをtrueにする
        fin = true;
        Debug.Log("最後のステージ");
    }



    //----------------------------------------------------------------------------------
    //ステージクリアした時に呼ばれるメソッド
    //----------------------------------------------------------------------------------
    public void StageClear()
    {
        //アニメーション"clear"に遷移
        MapManager.instance.animator.SetBool("Clear", true);

        //MapInfo復元
        string json2 = PlayerPrefs.GetString("MapInfo");
        mapInfo = MapInfo.CreateFromJSON(json2);


        //次ステージの解放
        int index = 0;
        foreach (GameObject obj in MapManager.instance.cpArray)
        {
            Button btn = obj.GetComponent<Button>();
            btn.interactable = true;

            //一時保存したクリアステージをJson保存変数に入れる
            mapInfo.NextStage[index] = btn.name;
            Debug.Log(obj.name);

            index += 1;
        }

        //セーブ
        //jsonとして保存
        string json = JsonUtility.ToJson(mapInfo);
        Debug.Log(json);
        PlayerPrefs.SetString("MapInfo", json);
        PlayerPrefs.Save();


        //クリアフラグをfalseに
        MapManager.instance.Clear = false;
    }


}

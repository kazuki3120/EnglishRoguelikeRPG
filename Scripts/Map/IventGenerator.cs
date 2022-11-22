using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class IventGenerator : MonoBehaviour
{

    //イベントボタンを全て格納
    public List<GameObject> eventList;

    //使用するイベントボタンを格納
    public List<GameObject> useList;

    //イベントのランダムオブジェクト
    private GameObject randomObj;

    //イベントのランダム配列番号
    private int choiceNum;

    //初回フラグ
    private bool firstTime;



    void Start()
    {
        //Save.cs >> MapInfo
        MapInfo mapInfo = new MapInfo();

        // PlayPrefsに"MapInfo"が存在するか
        bool key = PlayerPrefs.HasKey("MapInfo");

        //存在する場合
        if (key)
        {
            firstTime = false; //初回フラグ
            //json復元
            string json2 = PlayerPrefs.GetString("MapInfo");
            mapInfo = MapInfo.CreateFromJSON(json2);
        }

        //存在しない場合
        else firstTime = true; //初回フラグ


        //-----------------------------------------------------------
        //イベントオブジェクトの生成
        //-----------------------------------------------------------

        for (int i = 0; i < 3; i++)
        {
            //イベントオブジェクトを取得

            //データが存在しない場合 >> ランダムでイベントを生成
            if (firstTime == true)
            {
                //イベントリストの中からランダムで1つを選ぶ
                int rnd = Random.Range(0, eventList.Count);
                randomObj = eventList[rnd];
                //randomObj = eventList[Random.Range(0, eventList.Count)];

                //MapInfoクラスの配列に保存
                mapInfo.iventRND.Add(rnd);
            }

            //データが存在する場合 >> PlayerPrefs(json)データを読み込む
            else randomObj = eventList[mapInfo.iventRND[i]];

            //選んだオブジェクトをuseListに追加
            useList.Add(randomObj);
            //選んだオブジェクトをActive
            randomObj.SetActive(true);

            //選んだオブジェクトの名前を取得（ボタンを削除する時に使用）
            string ivent = randomObj.name;

            Debug.Log("オンにするボタン(イベント): " + ivent);

            //対するオブジェクトを削除

            //イベントオブジェクトの名前の先頭3文字（例: "1-2 i" >> "1-2"）
            string offbtn = ivent.Substring(0, 3);
            //戦闘ボタンの中からオフにするボタンの名前が一致するものを探す
            GameObject trg = GameObject.Find(offbtn);
            //選んだオブジェクトを非アクティブ
            trg.SetActive(false);

            //選んだオブジェクトのリスト番号を取得
            choiceNum = eventList.IndexOf(randomObj);
            //同じリスト番号をmyListから削除（重複させないため）
            eventList.RemoveAt(choiceNum);

            Debug.Log("オフにするボタン: " + offbtn);
        }

        //-----------------------------------------------------------
        //クリアステージ情報の復元
        //-----------------------------------------------------------

        if (firstTime == false)
        {
            Debug.Log("復元するステージ: " + mapInfo.NextStage);
            MapManager.instance.cpArray = new GameObject[2];

            int index = 0;
            foreach (string objName in mapInfo.NextStage)
            {
                GameObject clearObj = GameObject.Find(objName); //ゲームオブジェクトを探す
                Button clearBtn = clearObj.GetComponent<Button>(); //ボタンコンポーネントの取得
                clearBtn.interactable = true; //ボタンの有効化
                MapManager.instance.cpArray[index] = clearObj; //copyArrayに値がないため入れる
                index++;
            }
        }

        else
        {
            GameObject firstObj = GameObject.Find("1-1");
            Button firstBtn = firstObj.GetComponent<Button>(); //ボタンコンポーネントの取得
            firstBtn.interactable = true; //ボタンの有効化
        }


        //-----------------------------------------------------------
        //マップ情報の保存（json化してPlayerPrefsに保存）
        //-----------------------------------------------------------

        mapInfo.year = UnitSelect.yearStage; //学年
        mapInfo.Unit = UnitSelect.unitStage; //単元

        string json = JsonUtility.ToJson(mapInfo);
        Debug.Log(json);
        PlayerPrefs.SetString("MapInfo", json);
        PlayerPrefs.Save();
    }
}

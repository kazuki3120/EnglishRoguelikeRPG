using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//イベント情報を保存するクラス----------------------------------------------------------------------------

[System.Serializable]
public class MapInfo
{
    //イベントの乱数を保存
    public List<int> iventRND = new List<int>();

    //学年
    public int year;

    //単元
    public int Unit;

    //クリアステージ状況
    public string[] NextStage = new string[2];


    public static MapInfo CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<MapInfo>(jsonString);
    }
}

/*
    public int stageNum;

    //ランダムをリストに入れ、オブジェクトを生成するタイミングでリストからデータを取る
    public List<float> ButtonPositionX = new List<float>();
    public List<float> ButtonPositionY = new List<float>();
*/
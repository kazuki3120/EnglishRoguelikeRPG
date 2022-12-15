using UnityEngine;
using NCMB;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

public class StdData : MonoBehaviour
{
    // NCMBを利用するためのクラス
    private NCMBObject _testClass;




    //--------------------------------------------------------------------------------
    // カレントユーザー情報の取得
    //--------------------------------------------------------------------------------
    public async UniTask<string> currentUser()
    {
        //変数
        string userObjID = null;
        NCMBUser currentUser = NCMBUser.CurrentUser;
        if (currentUser != null)
        {
            UnityEngine.Debug.Log("ログイン中のユーザー: " + currentUser.ObjectId);
            //変数に代入
            userObjID = currentUser.ObjectId;
        }
        //ログイン情報を取得できなかった場合タイトル画面に遷移
        else
        {
            SceneManager.LoadScene("Title");
            UnityEngine.Debug.Log("未ログインまたは取得に失敗");
        }
        await UniTask.Delay(1);
        return userObjID;
    }


    //--------------------------------------------------------------------------------
    // ジャンル名の取得
    //--------------------------------------------------------------------------------
    public async UniTask<string> genreNames(int genreID)
    {
        string text = "";

        //テーブル
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("GenreClass");

        //ジャンルIDと一致するものを検索
        query.WhereEqualTo("genreID", genreID);
        query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
        {
            if (e != null)
            {
                //件数取得失敗時の処理
                text = "取得失敗";
                Debug.Log("エラー：取得に失敗しました");
            }
            else
            {
                //ジャンルIDと関連付いたジャンル名の取得
                text = objList[0]["genreName"].ToString();
            }
        });

        await UniTask.Delay(500);
        return text;
    }


    //--------------------------------------------------------------------------------
    //回答数
    //--------------------------------------------------------------------------------
    public async UniTask<float> sumAns(string userID, int genreID, DateTime date)
    {
        float sum = 0;

        //テーブル
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("AnsData");

        //条件　ユーザーID
        query.WhereEqualTo("userID", userID);
        //条件　ジャンル分け
        if (genreID != 0) query.WhereEqualTo("genreId", genreID.ToString());
        //条件日付　分け
        if (date != null) query.WhereGreaterThanOrEqualTo("updateDate", date);


        //条件下で検索
        query.CountAsync((int count, NCMBException e) =>
        {
            if (e != null)
            {
                //件数取得失敗時の処理
                sum = 0;
                Debug.Log("回答数 : " + count);
            }
            else
            {
                //"sum"に件数を保存
                sum = count;
                Debug.Log("回答数 : " + count);
            }
        });

        await UniTask.Delay(500);
        return sum;
    }


    //--------------------------------------------------------------------------------
    //正解数
    //--------------------------------------------------------------------------------
    public async UniTask<float> correctAns(string userID, int genreID, DateTime date)
    {
        float correct = 0;

        //テーブル
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("AnsData");

        //条件　ユーザーID
        query.WhereEqualTo("userID", userID);
        //条件　ジャンル分け
        if (genreID != 0) query.WhereEqualTo("genreId", genreID.ToString());
        //条件日付　分け
        if (date != null) query.WhereGreaterThanOrEqualTo("updateDate", date);
        //条件　正解フラグ（0=不正解, 1=正解）
        query.WhereEqualTo("ans", 1);


        //条件下で検索
        query.CountAsync((int count, NCMBException e) =>
        {
            if (e != null)
            {
                //件数取得失敗時の処理
                correct = 0;
                Debug.Log("回答数 : " + count);
            }
            else
            {
                //"correct"に件数を保存
                correct = count;
                Debug.Log("正解数 : " + correct);
            }
        });

        await UniTask.Delay(500);
        return correct;
    }


    //--------------------------------------------------------------------------------
    //正解率
    //--------------------------------------------------------------------------------
    public float rateAns(float sum, float correct)
    {
        float val = correct / sum * 100;
        Debug.Log("正解率 : " + val);

        //非数値だった場合0を返す
        if (Double.IsNaN(val)) val = 0;

        return val;
    }
}
using UnityEngine;
using UnityEngine.UI;
using NCMB;
using UnityEngine.SceneManagement;
using System;

public class StdData : MonoBehaviour
{
    // NCMBを利用するためのクラス
    private NCMBObject _testClass;

    //テキスト格納
    [SerializeField] GameObject SumAnsText;
    [SerializeField] GameObject CorrectAnsText;
    [SerializeField] GameObject RateAnsText;

    //変数
    public string userObjID = "";
    public float sum; //回答数
    public float correct; //正解数
    public float rate; //正解率


    //--------------------------------------------------------------------------------
    // カレントユーザー情報の取得
    //--------------------------------------------------------------------------------
    public void currentUser()
    {
        NCMBUser currentUser = NCMBUser.CurrentUser;
        if (currentUser != null)
        {
            UnityEngine.Debug.Log("ログイン中のユーザー: " + currentUser.ObjectId);
            userObjID = currentUser.ObjectId;
        }
        //ログイン情報を取得できなかった場合
        else
        {
            FadeScene.Fadeinstance.FadeOut();
            SceneManager.LoadSceneAsync("Title");
            UnityEngine.Debug.Log("未ログインまたは取得に失敗");
        }
    }


    //--------------------------------------------------------------------------------
    //回答数
    //--------------------------------------------------------------------------------
    public void SumAns()
    {
        //テーブル
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("AnsData");

        //条件
        query.WhereEqualTo("userID", userObjID);

        //"sum"にカウントを保存
        query.CountAsync((int count, NCMBException e) =>
        {
            if (e != null)
            {
                //件数取得失敗時の処理
                Debug.Log("エラー：取得に失敗しました");

                Text sumAnsText = SumAnsText.GetComponent<Text>();
                sumAnsText.text = "取得の失敗";
            }
            else
            {
                //件数を出力
                sum = count;
                Debug.Log("回答数 : " + sum);

                Text sumAnsText = SumAnsText.GetComponent<Text>();
                sumAnsText.text = sum + " 問";
            }
        });
    }


    //--------------------------------------------------------------------------------
    //正解数
    //--------------------------------------------------------------------------------
    public void CorrectAns()
    {
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("AnsData");

        query.WhereEqualTo("userID", userObjID);
        query.WhereEqualTo("ans", 1);

        query.CountAsync((int count, NCMBException e) =>
        {
            if (e != null)
            {
                Debug.Log("エラー：取得に失敗しました");

                Text correctAnsText = CorrectAnsText.GetComponent<Text>();
                correctAnsText.text = "取得の失敗";
            }
            else
            {
                correct = count;
                Debug.Log("正解数 : " + correct);

                Text correctAnsText = CorrectAnsText.GetComponent<Text>();
                correctAnsText.text = correct + " 問";
            }
        });
    }


    //--------------------------------------------------------------------------------
    //正解率
    //--------------------------------------------------------------------------------
    public void RateAns()
    {
        rate = correct / sum * 100;
        Debug.Log("正解率 : " + rate);
        Text rateAnsText = RateAnsText.GetComponent<Text>();
        //小数点第２位四捨五入
        rateAnsText.text = Math.Round(rate, 1, MidpointRounding.AwayFromZero) + " %";
    }

}
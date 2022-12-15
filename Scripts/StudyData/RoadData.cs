using UnityEngine;
using UnityEngine.UI;
using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

public class RoadData : MonoBehaviour
{

    //StdDataスクリプト
    public StdData stdData;


    //キャンバス
    [SerializeField] private GameObject totalCanvas;
    [SerializeField] private GameObject genreCanvas;

    //テキスト 累計学習履歴
    [SerializeField] Text SumAnsText;
    [SerializeField] Text CorrectAnsText;
    [SerializeField] Text RateAnsText;

    //テキスト ジャンル学習履歴
    [SerializeField] Text genreListText;
    [SerializeField] Text genreText;
    [SerializeField] Text GenreName;
    [SerializeField] Text GenreSumAnsText;
    [SerializeField] Text GenreCorrectAnsText;
    [SerializeField] Text GenreRateAnsText;

    //ボタン ジャンル学習履歴
    [SerializeField] private Button rightBtn;
    [SerializeField] private Button leftBtn;

    //ボタン 表示期間　※トグルだと複雑な処理をしにくいためボタンで代替
    [SerializeField] private Button totalButon;
    [SerializeField] private Button todayButton;
    [SerializeField] private Button weekButton;
    [SerializeField] private Button monthButton;

    //NCMB処理時のロード画面
    [SerializeField] private GameObject roadingStd;


    //ユーザーID
    private string userID = null;
    //ジャンルID
    private int genreID = 1;
    //日付け
    DateTime TodayNow;
    DateTime date;


    async void Awake()
    {
        //現在の日時の取得
        TodayNow = DateTime.Now;
        Debug.Log("現在の日時: " + TodayNow);

        //ユーザー情報の取得
        userID = await stdData.currentUser();

        //初期表示（全期間の学習履歴）
        OnClickTotalStd();
    }

    void Update()
    {
        //ジャンルIDが最小値、最大値の時にボタンの無効化
        if (genreID == 1) leftBtn.interactable = false;
        else leftBtn.interactable = true;

        if (genreID == 20) rightBtn.interactable = false;
        else rightBtn.interactable = true;
    }


    //---------------------------------------------
    //学習履歴の表示
    //---------------------------------------------

    //累計
    public void OnClickTotalStd()
    {
        //トグル（ボタン）の処理
        totalButon.interactable = false;
        todayButton.interactable = true;
        weekButton.interactable = true;
        monthButton.interactable = true;

        //日付条件定義
        date = new DateTime();

        //学習履歴の表示
        Std();
    }

    //今日
    public void OnClickTodayStd()
    {
        //トグル（ボタン）の処理
        totalButon.interactable = true;
        todayButton.interactable = false;
        weekButton.interactable = true;
        monthButton.interactable = true;

        //日付条件定義
        date = new DateTime(TodayNow.Year, TodayNow.Month, TodayNow.Day, 00, 00, 00);
        Debug.Log(date);

        //学習履歴の表示
        Std();
    }

    //１週間
    public void OnClickWeekStd()
    {
        //トグル（ボタン）の処理
        totalButon.interactable = true;
        todayButton.interactable = true;
        weekButton.interactable = false;
        monthButton.interactable = true;

        //日付条件定義 
        date = new DateTime(TodayNow.Year, TodayNow.Month, TodayNow.Day, 00, 00, 00).AddDays(-7);
        Debug.Log(date);

        //学習履歴の表示
        Std();
    }

    //１か月
    public void OnClickMonthStd()
    {
        //トグル（ボタン）の処理
        totalButon.interactable = true;
        todayButton.interactable = true;
        weekButton.interactable = true;
        monthButton.interactable = false;

        //日付条件定義
        date = new DateTime(TodayNow.Year, TodayNow.Month, TodayNow.Day, 00, 00, 00).AddMonths(-1);
        Debug.Log(date);

        //学習履歴の表示
        Std();
    }

    //処理
    async void Std()
    {
        //ロード画面始め
        roadingStd.SetActive(true);

        //ジャンルフラグ 0 >> ジャンル判定なし
        genreID = 0;

        //合計回答数と正解数を非同期で処理
        var (sum, correct) = await UniTask.WhenAll(stdData.sumAns(userID, genreID, date), stdData.correctAns(userID, genreID, date));

        //処理完了後、正解率の処理
        float rate = stdData.rateAns(sum, correct);


        //回答データの表示
        totalCanvas.SetActive(true);
        genreCanvas.SetActive(false);

        SumAnsText.text = sum + " 問";
        CorrectAnsText.text = correct + " 問";
        RateAnsText.text = Math.Round(rate, 1, MidpointRounding.AwayFromZero) + " %"; //小数点第２位四捨五入

        //ロード画面終わり
        roadingStd.SetActive(false);
    }





    //---------------------------------------------
    //ジャンル学習履歴の表示
    //---------------------------------------------
    public async void OnClickGenreStd()
    {
        //ロード画面始め
        roadingStd.SetActive(true);

        //ジャンルIDが0だった場合1に設定
        if (genreID == 0) genreID = 1;

        //ジャンル名の取得
        String genreName = await stdData.genreNames(genreID);

        //合計回答数と正解数を非同期で処理
        var (sum, correct) = await UniTask.WhenAll(stdData.sumAns(userID, genreID, date), stdData.correctAns(userID, genreID, date));

        //処理完了後、正解率の処理
        float rate = stdData.rateAns(sum, correct);


        totalCanvas.SetActive(false);
        genreCanvas.SetActive(true);

        //ジャンル名の表示
        genreText.text = $"ジャンル名 ({genreID}/20)";
        GenreName.text = genreName;
        Debug.Log("ジャンル名: " + genreName);

        //回答データの表示
        GenreSumAnsText.text = sum + " 問";
        GenreCorrectAnsText.text = correct + " 問";
        GenreRateAnsText.text = Math.Round(rate, 1, MidpointRounding.AwayFromZero) + " %"; //小数点第２位四捨五入

        //ロード画面終わり
        roadingStd.SetActive(false);
    }


    //次のジャンル
    public void OnClickNextGenre()
    {
        //二重防止　ジャンル２０（最大値）の場合処理終了
        if (genreID == 20) return;

        //ジャンルIDを加算
        genreID++;
        //ジャンル学習履歴の表示処理
        OnClickGenreStd();
    }

    //前のジャンル
    public void OnClickPrevGenre()
    {
        //二重防止　ジャンル１（最小値）の場合処理終了
        if (genreID == 1) return;

        //ジャンルIDを減算
        genreID--;
        //ジャンル学習履歴の表示処理
        OnClickGenreStd();
    }



    //---------------------------------------------
    //戻るボタン
    //---------------------------------------------
    public void OnBackButton()
    {
        //フェード+画面遷移
        FadeScene.Fadeinstance.FadeInToOut(
            () => SceneManager.LoadSceneAsync("Menuselect"));
    }


}
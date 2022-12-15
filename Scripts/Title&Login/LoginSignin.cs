using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using NCMB;
using UnityEngine.SceneManagement;

public class LoginSignin : MonoBehaviour
{
    //入力欄
    public InputField UserName;
    public InputField PassWord;

    [SerializeField] Text logText;

    //ユーザーID （回答記録時使用）
    public static string userID = null;


    void awake()
    {
        //テキストコンポーネント取得
        logText = GetComponent<Text>();
    }


    //---------------------------------------------------------------------------------------------------
    // ログイン
    //---------------------------------------------------------------------------------------------------
    public void Login()
    {
        print("ユーザー名：　" + UserName.text);
        print("パスワード：　" + PassWord.text);

        // NCMBUserのインスタンス作成
        NCMBUser user = new NCMBUser();

        // ユーザー名とパスワードでログイン
        NCMBUser.LogInAsync(UserName.text, PassWord.text, (NCMBException e) =>
        {
            //ログイン失敗時
            if (e != null)
            {
                StartCoroutine(MessageLog("ユーザーIDまたは\nパスワードが間違っています"));
                UnityEngine.Debug.Log("ログインに失敗: " + e.ErrorMessage);
            }
            //ログイン成功時
            else
            {
                StartCoroutine(MessageLog("ログインに成功しました!"));
                UnityEngine.Debug.Log("ログインに成功！");

                //ユーザーのオブジェクトIDをuserIDに入れる
                userID = NCMBUser.CurrentUser.ObjectId;
                UnityEngine.Debug.Log("ユーザーのオブジェクトID：" + userID);

                //フェード+画面遷移
                FadeScene.Fadeinstance.FadeInToOut(
                    () => SceneManager.LoadSceneAsync("MenuSelect"));
            }
        });
    }


    //---------------------------------------------------------------------------------------------------
    // 新規会員登録
    //---------------------------------------------------------------------------------------------------
    public void SignUp()
    {
        print(UserName.text);
        print(PassWord.text);

        // NCMBUserのインスタンス作成 
        NCMBUser user = new NCMBUser();

        // ユーザ名とパスワードの設定
        user.UserName = UserName.text;
        user.Password = PassWord.text;

        // 会員登録
        user.SignUpAsync((NCMBException e) =>
        {
            if (e != null)
            {
                StartCoroutine(MessageLog("新規登録に失敗しました\nIDまたはパスワードが既に使われています"));
                UnityEngine.Debug.Log("新規登録に失敗: " + e.ErrorMessage);
            }
            else
            {
                StartCoroutine(MessageLog("新規登録に成功しました!\nログインボタンを押してください"));
                UnityEngine.Debug.Log("新規登録に成功");
            }
        });
    }


    //---------------------------------------------------------------------------------------------------
    // メッセージログ表示コルーチン
    //---------------------------------------------------------------------------------------------------
    IEnumerator MessageLog(string message)
    {
        //メッセージを３秒間表示して消す
        logText.text = message;
        yield return new WaitForSeconds(3);
        logText.text = null;
    }


    //---------------------------------------------------------------------------------------------------
    // ログアウト  　※オートログイン未実装　オートログイン実装時使用
    //---------------------------------------------------------------------------------------------------
    public void Userlogout()
    {
        NCMBUser.LogOutAsync((NCMBException e) =>
        {
            if (e == null)
            {
                UnityEngine.Debug.Log("ログアウト成功");
            }
            else
            {
                UnityEngine.Debug.Log("ログアウトに失敗: " + e.ErrorMessage);
            }
        });
    }
}
using System.Collections;
using UnityEngine;
using NCMB;

public class AutoLogin : MonoBehaviour
{
    private const string userNameKey = "_user_name_";
    private const string passwordKey = "_pass_word_";

    //---------------------------------------------------------------------------------------------
    // アプリ起動時に呼ばれるメソッド (オートログイン)
    //---------------------------------------------------------------------------------------------
    IEnumerator Start()
    {
        // Unity(端末)に保存された `ユーザ名` と `パスワード` を取得 (保存されていない場合は空文字で初期化)
        string userName = PlayerPrefs.GetString(userNameKey, "");
        string password = PlayerPrefs.GetString(passwordKey, "");

        // Unityにユーザ情報が保存されているか
        if (userName == "")
        {
            // 保存なし -> ユーザ登録処理
            Debug.Log("No User Info on Unity");
            yield return StartCoroutine(userRegistrationCoroutine());
        }
        else
        {
            // 保存あり -> 現在ログインしているか
            if (NCMBUser.CurrentUser == null)
            {
                // ログインしていない -> ログイン処理
                yield return StartCoroutine(loginCoroutine(userName, password));
            }
        }
    }

    //---------------------------------------------------------------------------------------------
    // 任意文字数のランダム文字列を生成するメソッド
    //---------------------------------------------------------------------------------------------
    const string letters = "abcdefghijklmnopqrstuvwxyz1234567890";
    private string genRandomString(uint strLength)
    {
        string randStr = "";
        for (int i = 0; i < strLength; i++)
        {
            char randLetter = letters[Mathf.FloorToInt(Random.value * letters.Length)];
            randStr += randLetter;
        }
        return randStr;
    }

    //---------------------------------------------------------------------------------------------
    // 会員登録を行うメソッド
    //---------------------------------------------------------------------------------------------
    private IEnumerator userRegistrationCoroutine()
    {
        // NCMBUserクラスのインスタンス生成
        NCMBUser user = new NCMBUser();

        // パスワード
        string password = genRandomString(16);
        user.Password = password;

        // UserIDが重複したらやり直し
        bool isSuccess = false;
        while (!isSuccess)
        {
            bool isConnecting = true;
            // ランダム文字列でユーザ名を設定してユーザ登録実行（非同期処理）
            user.UserName = genRandomString(16);
            user.SignUpAsync((NCMBException e) => {
                if (e != null)
                {
                    // ユーザ登録失敗
                    if (e.ErrorCode != NCMBException.DUPPLICATION_ERROR)
                    {
                        // ユーザ名重複以外のエラーが発生
                        Debug.Log("Failed to registerate: " + e.ErrorMessage);
                    }
                }
                else
                {
                    // ユーザ登録成功
                    isSuccess = true;
                    Debug.Log("Succeeded to registrate");
                    // Unity(端末)に情報を設定
                    PlayerPrefs.SetString(userNameKey, user.UserName);
                    PlayerPrefs.SetString(passwordKey, password);
                }
                // ユーザ登録処理（１ループ）終了
                isConnecting = false;
            });
            // ユーザ登録処理（１ループ）が終了するまで以下の行でストップ（強制的に同期処理にする）
            yield return new WaitWhile(() => { return isConnecting; });
        }
    }

    //---------------------------------------------------------------------------------------------
    // ログインを行うメソッド
    //--------------------------------------------------------------------------------------------- 
    private IEnumerator loginCoroutine(string userName, string password)
    {
        bool isConnecting = true;
        NCMBUser.LogInAsync(userName, password, (NCMBException e) => {
            if (e != null)
            {
                // ログイン失敗
                Debug.Log("Failed to login: " + e.ErrorMessage);
            }
            else
            {
                // ログイン成功
                Debug.Log("Succeeded to login");
            }
            // ログイン処理終了
            isConnecting = false;
        });
        // ログイン処理が終了するまで以下の行でストップ
        yield return new WaitWhile(() => { return isConnecting; });
    }
}
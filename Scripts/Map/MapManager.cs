using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    //クリア判定
    public bool Clear = false;

    //配列コピー　クリア時Json保存するための一時保存
    public GameObject[] cpArray;

    //アニメーション
    public Animator animator;


    //インスタンス
    static public MapManager instance;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    //戻るボタン
    public void OnClickHome()
    {
        //フェード+画面遷移
        FadeScene.Fadeinstance.FadeInToOut(
            () => SceneManager.LoadSceneAsync("Menuselect"));
    }
}

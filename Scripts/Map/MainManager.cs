using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    
    //戻るボタン
    public void OnClickHome()
    {
        //フェード+画面遷移
        FadeScene.Fadeinstance.FadeInToOut(
            () => SceneManager.LoadSceneAsync("Menuselect"));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] AnimatedDialog animatedDialog;

    public void OnClickStartButton()
    {
        //パネルをオンにしてタイトル画面の操作不能にする
        panel.SetActive(true);

        //ログイン画面を開く
        animatedDialog.Open();
    }
}

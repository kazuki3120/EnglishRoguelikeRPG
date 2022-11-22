using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionManager : MonoBehaviour
{

    [SerializeField] AudioMixer audioMixer; //ミキサー
    [SerializeField] AudioSource audioSource; //オーディオソース
    [SerializeField] AudioClip se1; //SE
    [SerializeField] Slider bgmSlider; //BGMスライダー
    [SerializeField] Slider seSlider; //SEスライダー
    [SerializeField] Toggle BGM_toggle; //BGMトグル
    [SerializeField] Toggle SE_toggle; //SEトグル
    [SerializeField] Text option_text; //テキスト


    void Awake()
    {
        //スライダーを動かした時の処理を登録
        bgmSlider.onValueChanged.AddListener(SetAudioMixerBGM);
        seSlider.onValueChanged.AddListener(SetAudioMixerSE);

        // 設定をロード
        bgmSlider.value = PlayerPrefs.GetFloat("BGM Volume", 1);
        seSlider.value = PlayerPrefs.GetFloat("SE Volume", 1);

        Debug.Log("BGM音量(" + bgmSlider.value + ") SE音量(" + seSlider.value + ")をロードしました");
    }


    void Update()
    {
        //音量が０じゃない場合ミュートボタンは常にOFF
        if (bgmSlider.value != 0) BGM_toggle.isOn = false;
        if (seSlider.value != 0) SE_toggle.isOn = false;
    }


    //BGM
    public void SetAudioMixerBGM(float value)
    {
        //-80~0に変換
        var volume = Mathf.Clamp(Mathf.Log10(value) * 60f, -80f, 0f);
        //audioMixerに代入
        audioMixer.SetFloat("BGM", volume);

        Debug.Log($"BGM:{(int)volume}");
    }


    //SE
    public void SetAudioMixerSE(float value)
    {
        //-80~0に変換
        var volume = Mathf.Clamp(Mathf.Log10(value) * 60f - 40f, -80f, 0f);
        //audioMixerに代入
        audioMixer.SetFloat("SE", volume);
        //SEを鳴らす
        audioSource.PlayOneShot(se1);

        Debug.Log($"SE:{(int)volume}");
    }


    // BGMミュートトグル
    public void bgmMuteButton()
    {
        if (bgmSlider.value != 0) bgmSlider.value = 0;
    }


    // SEミュートトグル
    public void seMuteButton()
    {
        if (seSlider.value != 0) seSlider.value = 0;
    }


    //--------------------ボタン処理----------------------------------------

    //設定保存
    public void OptionSaveButton()
    {
        PlayerPrefs.SetFloat("BGM Volume", bgmSlider.value);
        PlayerPrefs.SetFloat("SE Volume", seSlider.value);

        Debug.Log("BGM音量(" + bgmSlider.value + ") SE音量(" + seSlider.value + ")を保存しました");

        option_text.text = "設定を保存しました！";
        StartCoroutine("TextSet");
    }

    IEnumerator TextSet()
    {
        //テキストを３秒表示して消す
        yield return new WaitForSeconds(3.0f);
        option_text.text = "";
    }


    //音量設定リセット
    public void OnResetButton()
    {
        //BGM, SEのPlayerPrefs削除
        PlayerPrefs.DeleteKey("BGM Volume");
        PlayerPrefs.DeleteKey("SE Volume");

        //ボリューム１に設定（初期設定）
        bgmSlider.value = 1;
        seSlider.value = 5;
    }


    //戻るボタン
    public void OnClickBackButton()
    {
        //フェード+画面遷移
        FadeScene.Fadeinstance.FadeInToOut(
            () => SceneManager.LoadSceneAsync("MenuSelect"));
    }

}
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class RandomScene : MonoBehaviour
{
    public Fade fade;
    List<int> numbers = new List<int>();

    void Start()
    {
        //DontDestroyOnLoad(this);

        for (int i = 2; i < EditorBuildSettings.scenes.Length; i++)
        {
            numbers.Add(i);
        }

        //numbers.RemoveAt(SceneManager.GetActiveScene().buildIndex);

    }

    public void RandomSceneChange()
    {
        int ransu = numbers[Random.Range(2, numbers.Count)];


        //フェードで画面遷移
        //fade.FadeIn(1f, () =>　SceneManager.LoadScene(ransu)　);
        fade.FadeIn(1f, () => SceneManager.LoadScene("1"));
        

        numbers.Remove(ransu);

        

    }

    public void RandomSceneChangeEvent()
    {
        int ransu = numbers[Random.Range(10, 12)];

        SceneManager.LoadScene(ransu);

        numbers.Remove(ransu);

        //フェードで画面遷移
        //fade.FadeIn(1f, () =>
        //{
        //    SceneManager.LoadScene(ransu);
        //});

    }



}
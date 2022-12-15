using UnityEngine;
using UnityEngine.SceneManagement;


public class battle : MonoBehaviour
{
    [SerializeField] Fade fade;

    void Start()
    {
        fade.FadeOut(1f);
    }

    public void TransitionScene()
    {
        MapManager.instance.Clear = true;
        SceneManager.LoadScene("main");
    }

    public void GameOver()
    {
        SceneManager.LoadScene("ClearScene");
    }


}

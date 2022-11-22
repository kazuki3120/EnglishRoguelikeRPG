using UnityEngine;
using UnityEngine.SceneManagement;


public class battle : MonoBehaviour
{

    public void TransitionScene()
    {
        MapManager.instance.Clear = true;
        SceneManager.LoadScene("main");
    }


}

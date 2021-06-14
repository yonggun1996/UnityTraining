using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public void Change()
    {
        SceneManager.LoadScene("Scene1Start");
    }

    public void changescene1()
    {
        SceneManager.LoadScene("Scene1");
    }

    public void changescene2()
    {
        SceneManager.LoadScene("Scene2");
    }

    public void changescene3()
    {
        SceneManager.LoadScene("Scene3");
    }
}

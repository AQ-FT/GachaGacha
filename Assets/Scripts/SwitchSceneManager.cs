using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchSceneManager : MonoBehaviour
{
    public void SwitchScene()
    {
        SceneManager.LoadScene("GachaResult", LoadSceneMode.Single);
    }

    public void SwitchMain()
    {
        SceneManager.LoadScene("Main Scene", LoadSceneMode.Single);
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainmenu : MonoBehaviour
{
    public GameObject inputName;
    public static string Pseudo;
    public GameObject UI;
    public GameObject Game;

    public void readStringInput(string s)
    {
        Pseudo = s;
        Debug.Log(Pseudo);
    }

    public void PlayGame()
    {
        StartCoroutine(play());
    }
    
    public void QuitGame()
    {
        StartCoroutine(quit());
    }

    public void Btomenu()
    {
        StartCoroutine(Bmenu());
    }
    IEnumerator play()
    {
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene("Snake", LoadSceneMode.Single);
    }
    
    IEnumerator quit()
    {
        yield return new WaitForSeconds(0.3f);
        Application.Quit();
    }

    IEnumerator Bmenu()
    {
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene("mainmenu", LoadSceneMode.Single);
    }


}

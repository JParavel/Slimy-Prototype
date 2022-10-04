using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvaStart : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Option() {
        SceneManager.LoadScene(2);
    }

    public void Quit (){
        SceneManager.LoadScene(0);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPlay()
    {
        SceneManager.LoadScene(1);
    }
    public void OnExit()
    {
        if (UnityEngine.Application.isPlaying)
            EditorApplication.ExitPlaymode();
        else
            Application.Quit();
    }
}

//Author: Rok Kos <kosrok97@gmail.com>
//File: UIManager.cs
//File path: /D/Documents/Unity/DragonHack2017/UIManager.cs
//Date: 21.05.2017
//Description: UI Functions

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    [SerializeField] Transform startMenuPanel;
    [SerializeField] Transform howToPanel;
    [SerializeField] Transform AboutPanel;
    private int state = 0;  // 0 - Home, 1 - How to, 2 - About, 3 - Main

	// Use this for initialization
	void Start () {


        if (SceneManager.GetActiveScene().name == "Start") {
            state = 0;
            GoBack();
        } else if (SceneManager.GetActiveScene().name == "Main") {
            state = 3;
        }
        
        //panelBoxes.gameObject.SetActive(false);
        //startMenuPanel.gameObject.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Escape)) {
            decide();
        }
    }

    public void gotoMain () {
        state = 3;
        SceneManager.LoadScene("Main");
    }

    public void gotoMenu () {
        state = 0;
        SceneManager.LoadScene("Start");
    }

    void GoBack () {
        startMenuPanel.gameObject.SetActive(true);
        howToPanel.gameObject.SetActive(false);
        AboutPanel.gameObject.SetActive(false);
        state = 0;
    }

    public void gotoAbout () {
        state = 1;
        startMenuPanel.gameObject.SetActive(false);
        howToPanel.gameObject.SetActive(false);
        AboutPanel.gameObject.SetActive(true);
    }

    public void gotoHowTo () {
        state = 2;
        startMenuPanel.gameObject.SetActive(false);
        howToPanel.gameObject.SetActive(true);
        AboutPanel.gameObject.SetActive(false);
    }

    void decide () {
        switch (state) {
            case 0:
                //Application.Quit();
                break;
            case 1:
                GoBack();
                break;

            case 2:
                GoBack();
                break;
            case 3:
                gotoMenu();
                break;
        }
    }
}

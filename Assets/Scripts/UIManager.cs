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

    [SerializeField] Transform panelBoxes;
    [SerializeField] Transform startMenuPanel;

	// Use this for initialization
	void Start () {
        //panelBoxes.gameObject.SetActive(false);
        //startMenuPanel.gameObject.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void gotoMain () {
        SceneManager.LoadScene("Main");
    }
}

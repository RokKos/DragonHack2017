using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class start_menu_script : MonoBehaviour {

    [SerializeField] Transform panelBoxes;
    [SerializeField] Transform startMenuPanel;

    // Use this for initialization
    void Start () {
        panelBoxes.gameObject.SetActive(false);
        startMenuPanel.gameObject.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

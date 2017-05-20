//Author: Rok Kos <kosrok97@gmail.com>
//File: GameController.cs
//File path: /D/Documents/Unity/DragonHack2017/GameController.cs
//Date: 20.05.2017
//Description: Controling whole game loop

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    [SerializeField] GameObject boxPrefab;
    [SerializeField] Transform panelBoxes;
    [SerializeField] Transform canvas;
    GameObject[] listOfBoxes;
    [Range(0, 2)]
    public float scale = 0.3f; 

    const int MAXBOXES = 90; // 81 Small boxes and 9 big boxes

	// Use this for initialization
	void Start () {
        listOfBoxes = new GameObject[MAXBOXES];
        scale = 0.2f;
        for (int i = 0; i < 9; ++i) {
            for (int j = 0; j < 9; ++j) {
                Vector2 pos = new Vector2(i * 0.7f, j * 0.7f);  // Just temporary position
                GameObject temp = Instantiate(boxPrefab, pos, Quaternion.identity, panelBoxes);
                temp.transform.localScale = new Vector3(scale * (1.0f / canvas.localScale.x), scale * (1.0f / canvas.localScale.y), 1);  //  Get the right scale
                // Move into rigth postition
                temp.transform.position = new Vector2((i-4) * temp.GetComponent<Renderer>().bounds.size.x, (j - 4) * temp.GetComponent<Renderer>().bounds.size.y);
                listOfBoxes[i * 9 + j] = temp;
            }
        }
		
	}
	
	// Update is called once per frame
	void Update () {
        changeScale();
	}

    void changeScale () {
        for (int i = 0; i < 9; ++i) {
            for (int j = 0; j < 9; ++j) {
                listOfBoxes[i * 9 + j].transform.localScale = new Vector3(scale * (1.0f / canvas.localScale.x), scale * (1.0f / canvas.localScale.y), 1);
                listOfBoxes[i * 9 + j].transform.position = new Vector2((i - 4) * listOfBoxes[i * 9 + j].GetComponent<Renderer>().bounds.size.x, (j - 4) * listOfBoxes[i * 9 + j].GetComponent<Renderer>().bounds.size.y);
            }
        }
    }
}

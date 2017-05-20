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
    GameObject[] listOfBoxes;

    const int MAXBOXES = 90; // 81 Small boxes and 9 big boxes

	// Use this for initialization
	void Start () {
        listOfBoxes = new GameObject[MAXBOXES];

        for (int i = 0; i < 9; ++i) {
            for (int j = 0; j < 9; ++j) {
                Vector2 pos = new Vector2(i * 5, j * 5);
                GameObject temp = Instantiate(boxPrefab, pos, Quaternion.identity);
                listOfBoxes[i * 9 + j] = temp;
            }
        }
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

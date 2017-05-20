﻿//Author: Rok Kos <kosrok97@gmail.com>
//File: GameController.cs
//File path: /D/Documents/Unity/DragonHack2017/GameController.cs
//Date: 20.05.2017
//Description: Controling whole game loop

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameController : MonoBehaviour {
    [SerializeField] Button boxPrefab;
    [SerializeField] GameObject cubeBoxPrefab;
    [SerializeField] Transform panelBoxes;
    [SerializeField] Transform canvas;
    Button[] listOfBoxes;
    GameObject[] cubeBoxesList;

    [Range(0, 10)]
    public float scale = 3f;

    public int prejsnjaPoteza = -1;
    public bool firstPlayer = true;

    const int MAXBOXES = 81; // 81 Small boxes and 9 big boxes

	// Use this for initialization
	void Start () {
        firstPlayer = true;
        listOfBoxes = new Button[MAXBOXES];
        cubeBoxesList = new GameObject[9];
        scale = 3f;

        // Spawning small boxes
        for (int i = 0; i < 9; ++i) {
            for (int j = 0; j < 9; ++j) {
                Vector2 pos = new Vector2(i * 0.7f, j * 0.7f);  // Just temporary position
                Button temp = Instantiate(boxPrefab, panelBoxes, false);
                // Move into rigth postition
                Debug.Log((i - 4) * temp.GetComponent<RectTransform>().rect.height +" " + (i - 4) * temp.GetComponent<RectTransform>().rect.width);
                //temp.transform.position = new Vector3((i - 4) * temp.GetComponent<RectTransform>().rect.height, (j - 4) * temp.GetComponent<RectTransform>().rect.width, 1);
                temp.transform.localScale = new Vector3(scale, scale, scale);
                temp.GetComponent<RectTransform>().localPosition = new Vector3((i - 4) * temp.GetComponent<RectTransform>().rect.height * scale, (j - 4) * temp.GetComponent<RectTransform>().rect.width * scale, 1);
                //temp.transform.SetParent(panelBoxes);
                //new Vector3(scale * (1.0f / canvas.localScale.x), scale * (1.0f / canvas.localScale.y), 1);  //  Get the right scale
                temp.GetComponent<box_script>().stanje = 0;
                temp.GetComponent<box_script>().pozicija = (i % 3) * 3 + (j % 3); 
                listOfBoxes[i * 9 + j] = temp;
            }
        }

        // Spawning big boxes
        /*
        for (int i = 0; i < 3; ++i) {
            for (int j = 0; j < 3; ++j) {
                Vector2 pos = new Vector2(i * 0.7f, j * 0.7f);  // Just temporary position
                GameObject temp = Instantiate(cubeBoxPrefab, pos, Quaternion.identity, panelBoxes);
                temp.transform.localScale = new Vector3(3 * scale * (1.0f / canvas.localScale.x), 3 * scale * (1.0f / canvas.localScale.y), 1);  //  Get the right scale (3 times biger than normal)
                // Move into rigth postition
                temp.transform.position = new Vector2((i - 4) * temp.GetComponent<RectTransform>().rect.height, (j - 4) * temp.GetComponent<RectTransform>().rect.width);
                temp.GetComponent<CubeBox>().stanje = 0;
                temp.GetComponent<CubeBox>().pozicija = i * 3 + j;

                List<GameObject> trenutni_otroci = new List<GameObject>();
                for (int k = 0; k < 3; ++k) {
                    for (int l = 0; l < 3; ++l) {
                        trenutni_otroci.Add(listOfBoxes[i * 27 + j * 3 + k * 9 + l]);
                    }

                }
                temp.GetComponent<CubeBox>().otroci = trenutni_otroci;

                cubeBoxesList[i * 3 + j] = temp;
                
            }
        }
        */
		
	}
	
	// Update is called once per frame
	void Update () {
        changeScale();
	}

    void changeScale () {
        for (int i = 0; i < 9; ++i) {
            for (int j = 0; j < 9; ++j) {
                listOfBoxes[i * 9 + j].transform.localScale = new Vector3(scale, scale, scale);
                listOfBoxes[i * 9 + j].GetComponent<RectTransform>().localPosition = new Vector3((i - 4) * listOfBoxes[i * 9 + j].GetComponent<RectTransform>().rect.height * scale, (j - 4) * listOfBoxes[i * 9 + j].GetComponent<RectTransform>().rect.width * scale, 1);
            }
        }
    }

    bool konecIgre () {
        // Check rows
        for (int i = 0; i < 3; ++i) {
            if (cubeBoxesList[i * 3].GetComponent<box_script>().stanje == cubeBoxesList[i * 3 + 1].GetComponent<box_script>().stanje && cubeBoxesList[i * 3].GetComponent<box_script>().stanje == cubeBoxesList[i * 3 + 2].GetComponent<box_script>().stanje) {
                return true;
            }
        }
        // Check columns
        for (int i = 0; i < 3; ++i) {
            if (cubeBoxesList[i].GetComponent<box_script>().stanje == cubeBoxesList[i + 3].GetComponent<box_script>().stanje && cubeBoxesList[i].GetComponent<box_script>().stanje == cubeBoxesList[i + 6].GetComponent<box_script>().stanje) {
                return true;
            }
        }

        // Check diagonals
        if (cubeBoxesList[0].GetComponent<box_script>().stanje == cubeBoxesList[4].GetComponent<box_script>().stanje && cubeBoxesList[0].GetComponent<box_script>().stanje == cubeBoxesList[8].GetComponent<box_script>().stanje) {
            return true;
        }

        if (cubeBoxesList[2].GetComponent<box_script>().stanje == cubeBoxesList[4].GetComponent<box_script>().stanje && cubeBoxesList[2].GetComponent<box_script>().stanje == cubeBoxesList[6].GetComponent<box_script>().stanje) {
            return true;
        }

        return false;
    }
}

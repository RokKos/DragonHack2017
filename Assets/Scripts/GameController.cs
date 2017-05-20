//Author: Rok Kos <kosrok97@gmail.com>
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
    [SerializeField] Button cubeBoxPrefab;
    [SerializeField] Button gridPrefab;

    [SerializeField] Transform panelBoxes;
    [SerializeField] Transform canvas;
    Button[] listOfBoxes;
    public Button[] cubeBoxesList;
    private Button[] gridList;

    [Range(0, 10)]
    public float scale = 3f;

    public int prejsnjaPoteza = -1;
    public bool firstPlayer = true;

    const int MAXBOXES = 81; // 81 Small boxes and 9 big boxes

	// Use this for initialization
	void Start () {
        firstPlayer = true;
        listOfBoxes = new Button[MAXBOXES];
        cubeBoxesList = new Button[9];
        gridList = new Button[9];
        scale = 3f;

        // Spawning grid
        for (int i = 0; i < 3; ++i) {
            for (int j = 0; j < 3; ++j) {
                Button temp = Instantiate(gridPrefab, panelBoxes, false);
                // Move into rigth postition
                temp.transform.localScale = new Vector3(3 * (scale + 0.2f), 3 * (scale + 0.2f), 3 * (scale + 0.2f));
                temp.GetComponent<RectTransform>().localPosition = new Vector3((i - 1) * temp.GetComponent<RectTransform>().rect.height * (scale + 0.2f) * 3, (1 - j) * temp.GetComponent<RectTransform>().rect.width * (scale + 0.2f) * 3, 1);
                gridList[i * 3 + j] = temp;
            }
        }

        // Spawning small boxes
        for (int i = 0; i < 9; ++i) {
            for (int j = 0; j < 9; ++j) {
                Button temp = Instantiate(boxPrefab, panelBoxes, false);
                // Move into rigth postition
                temp.transform.localScale = new Vector3(scale, scale, scale);
                temp.GetComponent<RectTransform>().localPosition = new Vector3((i - 4) * temp.GetComponent<RectTransform>().rect.height * (scale+0.2f), (4 - j) * temp.GetComponent<RectTransform>().rect.width * (scale + 0.2f), -1);
                temp.GetComponent<box_script>().stanje = 0;
                temp.GetComponent<box_script>().pozicija = (i % 3) * 3 + (j % 3);
                temp.name = i + " " + j;
                listOfBoxes[i * 9 + j] = temp;
            }
        }

        // Spawning big boxes

        for (int i = 0; i < 3; ++i) {
            for (int j = 0; j < 3; ++j) {
                Button temp = Instantiate(cubeBoxPrefab, panelBoxes, false);
                // Move into rigth postition
                temp.transform.localScale = new Vector3(3 * (scale + 0.2f), 3 * (scale + 0.2f), 3 * (scale + 0.2f));
                temp.GetComponent<RectTransform>().localPosition = new Vector3((i - 1) * temp.GetComponent<RectTransform>().rect.height * (scale + 0.2f) * 3, (1 - j) * temp.GetComponent<RectTransform>().rect.width * (scale + 0.2f) * 3, 1);
                temp.GetComponent<CubeBox>().stanje = 0;
                temp.GetComponent<CubeBox>().pozicija = i * 3 + j;
                //temp.GetComponentInParent<GameObject>().SetActive(false);
                //temp.interactable = false;  // This works because alpha of disable cubeBox is 0
                temp.gameObject.SetActive(false);

                List<Button> trenutni_otroci = new List<Button>();
                for (int k = 0; k < 3; ++k) {
                    for (int l = 0; l < 3; ++l) {
                        trenutni_otroci.Add(listOfBoxes[i * 27 + j * 3 + k * 9 + l]);
                        listOfBoxes[i * 27 + j * 3 + k * 9 + l].GetComponent<box_script>().parent = i * 3 + j;
                    }

                }
                temp.GetComponent<CubeBox>().otroci = trenutni_otroci;

                cubeBoxesList[i * 3 + j] = temp;

            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        //changeScale();
	}

    void changeScale () {
        for (int i = 0; i < 9; ++i) {
            for (int j = 0; j < 9; ++j) {
                listOfBoxes[i * 9 + j].transform.localScale = new Vector3(scale, scale, scale);
                listOfBoxes[i * 9 + j].GetComponent<RectTransform>().localPosition = new Vector3((i - 4) * listOfBoxes[i * 9 + j].GetComponent<RectTransform>().rect.height * scale, (j - 4) * listOfBoxes[i * 9 + j].GetComponent<RectTransform>().rect.width * scale, 1);
            }
        }
    }

    public bool konecIgre () {
        // Check rows
        for (int i = 0; i < 3; ++i) {
            if (cubeBoxesList[i * 3].GetComponent<CubeBox>().stanje != 0 && cubeBoxesList[i * 3].GetComponent<CubeBox>().stanje == cubeBoxesList[i * 3 + 1].GetComponent<CubeBox>().stanje && cubeBoxesList[i * 3].GetComponent<CubeBox>().stanje == cubeBoxesList[i * 3 + 2].GetComponent<CubeBox>().stanje) {
                return true;
            }
        }
        // Check columns
        for (int i = 0; i < 3; ++i) {
            if (cubeBoxesList[i + 3].GetComponent<CubeBox>().stanje != 0 && cubeBoxesList[i].GetComponent<CubeBox>().stanje == cubeBoxesList[i + 3].GetComponent<CubeBox>().stanje && cubeBoxesList[i].GetComponent<CubeBox>().stanje == cubeBoxesList[i + 6].GetComponent<CubeBox>().stanje) {
                return true;
            }
        }

        // Check diagonals
        if (cubeBoxesList[0].GetComponent<CubeBox>().stanje != 0 && cubeBoxesList[0].GetComponent<CubeBox>().stanje == cubeBoxesList[4].GetComponent<CubeBox>().stanje && cubeBoxesList[0].GetComponent<CubeBox>().stanje == cubeBoxesList[8].GetComponent<CubeBox>().stanje) {
            return true;
        }

        if (cubeBoxesList[4].GetComponent<CubeBox>().stanje != 0 && cubeBoxesList[2].GetComponent<CubeBox>().stanje == cubeBoxesList[4].GetComponent<CubeBox>().stanje && cubeBoxesList[2].GetComponent<CubeBox>().stanje == cubeBoxesList[6].GetComponent<CubeBox>().stanje) {
            return true;
        }

        return false;
    }

    public void nextMoveHint (int polje) {
        if (prejsnjaPoteza != -1) {
            gridList[prejsnjaPoteza].GetComponent<Image>().color = Color.black;
        }
        gridList[polje].GetComponent<Image>().color = Color.red;
    }
}

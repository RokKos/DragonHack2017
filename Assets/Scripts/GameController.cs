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
    [SerializeField] Button EmptyBox;
    [SerializeField] Image player1Arrow;
    [SerializeField] Image player2Arrow;
    [SerializeField] Transform endGamePanel;

    [SerializeField] Image win;

    [SerializeField] Transform panelBoxes;
    [SerializeField] Transform canvas;
    [SerializeField] Text player1TimeText;
    [SerializeField] Text player2TimeText;
    Button[] listOfBoxes;
    public Button[] cubeBoxesList;
    private Button[] gridList;

    public Sprite[] GameOverImages;

    [Range(0, 10)]
    public float scale = 3f;

    [Range(0, 10)]
    public float travelLeng = 0.2f;

    public int prejsnjaPoteza = -1;
    public bool firstPlayer = true;
    private float player1Time = 0.0f;
    private float player2Time = 0.0f;
    private float lastTime = 0.0f;

    public struct Poteza {
        public int cubeBox;
        public int smallBox;

        public Poteza (int cubeBox, int smallBox) {
            this.cubeBox = cubeBox;
            this.smallBox = smallBox;
        }
    }

    private List<Poteza> allMoves;
    private Button lastMoveBox;

    const int MAXBOXES = 81; // 81 Small boxes and 9 big boxes

	// Use this for initialization
	void Start () {
        firstPlayer = true;
        listOfBoxes = new Button[MAXBOXES];
        cubeBoxesList = new Button[9];
        gridList = new Button[9];
        allMoves = new List<Poteza>();
        scale = 3f;
        travelLeng = 0.2f;
        //Time
        player1Time = 0;
        player2Time = 0;
        lastTime = Time.time;
        player1TimeText.text = player2Time.ToString();
        player2TimeText.text = player2Time.ToString();
        player1Arrow.enabled = true;
        player2Arrow.enabled = false;


        endGamePanel.gameObject.SetActive(false);
        panelBoxes.gameObject.SetActive(true);

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

        // Spawn last move box
        lastMoveBox = Instantiate(EmptyBox, panelBoxes, false);
        lastMoveBox.transform.localScale = new Vector3(scale, scale, scale);
        lastMoveBox.GetComponent<Image>().color = Color.green;
        lastMoveBox.gameObject.SetActive(false);
        lastMoveBox.name = "lastMoveBox";
    }
	
	// Update is called once per frame
	void Update () {
        //changeScale();\
        // Calculates new time
        if(firstPlayer) {
            player1Time += Time.time - lastTime;
            player1TimeText.text = player1Time.ToString("0.00");
        }
        else {
            player2Time += Time.time - lastTime;
            player2TimeText.text = player2Time.ToString("0.00");
        }
        lastTime = Time.time;
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

    public bool konecPotez () {
        for (int i = 0; i < 3; ++i) {
            for (int j = 0; j < 3; ++j) {
                if (cubeBoxesList[i * 3 + j].GetComponent<CubeBox>().stanje == 0) {
                    return false;
                }
            }
        }

        return true;
    }

    public void nextMoveHint (int polje) {
        Debug.Log(prejsnjaPoteza + " " + polje);
        // Ce ni zacetek
        if (prejsnjaPoteza != -1 && prejsnjaPoteza != 10) {
            Debug.Log("Stop: " + prejsnjaPoteza);
            //StopCoroutine(fadeAnimacija(prejsnjaPoteza));
            gridList[prejsnjaPoteza].GetComponent<Image>().color = Color.black;
        } else if (prejsnjaPoteza == 10) {
            for (int i = 0; i < 9; ++i) {
                gridList[i].GetComponent<Image>().color = Color.black;
                //StopCoroutine(gridList[i].GetComponent<Grid>().fadeAnimacija());
            }
        }
        if (polje == 10) {
            for (int i = 0; i < 9; ++i) {
                if (cubeBoxesList[i].GetComponent<CubeBox>().stanje == 0) {
                    if (!firstPlayer) {
                        gridList[i].GetComponent<Image>().color = Color.red;
                    } else {
                        gridList[i].GetComponent<Image>().color = Color.yellow;
                    }
                }
                
                //StartCoroutine(gridList[i].GetComponent<Grid>().fadeAnimacija());
            }
        } else if (polje != -1) {
            Debug.Log("START: " + polje);
            if (!firstPlayer) {
                gridList[polje].GetComponent<Image>().color = Color.red;
            } else {
                gridList[polje].GetComponent<Image>().color = Color.yellow;
            }
            //StartCoroutine(fadeAnimacija(polje));
        }
    }

    public IEnumerator fadeAnimacija (int polje) {
        int diff = -1;
        while (true) {

            if (gridList[polje].GetComponent<Image>().color.a < 0.5 || gridList[polje].GetComponent<Image>().color.a == 1.0) {
                diff *= -1;
            }
            Color c = gridList[polje].GetComponent<Image>().color;
            gridList[polje].GetComponent<Image>().color = new Color(c.r, c.g, c.b, c.a + diff * 0.05f);
            yield return null;
        }

    }

    public IEnumerator shakeCamera () {
        float travel = 0.0f;
        // V levo smer
        while (travel < travelLeng) {
            travel += 0.1f;
            panelBoxes.position = new Vector2(panelBoxes.position.x + 0.1f, panelBoxes.position.y);
            yield return null;
        }
        travel = 0.0f;
        // V desno smer
        while (travel < 2 * travelLeng) {
            travel += 0.1f;
            panelBoxes.position = new Vector2(panelBoxes.position.x - 0.1f, panelBoxes.position.y);
            yield return null;
        }
        travel = 0.0f;
        // Nazaj na prvotno
        while (travel < travelLeng) {
            travel += 0.1f;
            panelBoxes.position = new Vector2(panelBoxes.position.x + 0.1f, panelBoxes.position.y);
            yield return null;
        }
        panelBoxes.position = new Vector2(0.0f, 0.0f);
    }

    public void changePicture () {
        if (firstPlayer) {
            player1Arrow.enabled = true;//.gameObject.SetActive(true);
            player2Arrow.enabled = false;//gameObject.SetActive(false);
        } else {
            player1Arrow.enabled = false;//.gameObject.SetActive(true);
            player2Arrow.enabled = true;//gameObject.SetActive(false);
        }

    }

    public void ShowWinner (int winner) {
        win.sprite = GameOverImages[winner];
        endGamePanel.gameObject.SetActive(true);
        panelBoxes.gameObject.SetActive(false);
    }

    public void addLastMove (int cubeBox, int smallBox) {
        allMoves.Add(new Poteza(cubeBox, smallBox));
    }

    public void showPrevius () {
        int length = allMoves.Count - 1;
        if (length >= 0) {
            moveBox(length);
            
            // Ta del kode ce bi hoteli spreminjati prav krogce in krizce
            //if (length - 1 >= 0) {
                //Poteza previusPrevius = allMoves[length - 1];
                //cubeBoxesList[previusPrevius.cubeBox].GetComponent<CubeBox>().otroci[previusPrevius.smallBox].GetComponent<Image>().color = Color.white;
                
            //}
            // Prejsnjega nastavi na crno barvo
            //cubeBoxesList[previus.cubeBox].GetComponent<CubeBox>().otroci[previus.smallBox].GetComponent<Image>().color = Color.black;
        }
    }

    private void moveBox (int index) {
        Poteza previus = allMoves[index];
        // Nastavi last box na rpavo mesto
        lastMoveBox.gameObject.SetActive(true);
        int bigX = previus.cubeBox / 3;
        int bigY = previus.cubeBox % 3;
        int smallX = previus.smallBox / 3;
        int smallY = previus.smallBox % 3;

        lastMoveBox.GetComponent<RectTransform>().localPosition = new Vector3((3 * bigX + smallX - 4) * lastMoveBox.GetComponent<RectTransform>().rect.height * (scale + 0.2f), (4 - (3 * bigY + smallY)) * lastMoveBox.GetComponent<RectTransform>().rect.width * (scale + 0.2f), -1);
    }


    public void undoMove () {
        int length = allMoves.Count;
        if (length > 0) {
            Poteza previus = allMoves[length - 1];
            // Set picture to deafult
            cubeBoxesList[previus.cubeBox].GetComponent<CubeBox>().otroci[previus.smallBox].GetComponent<Image>().sprite = cubeBoxesList[previus.cubeBox].GetComponent<CubeBox>().otroci[previus.smallBox].GetComponent<box_script>().images[0];
            // Set value of small box to zero
            cubeBoxesList[previus.cubeBox].GetComponent<CubeBox>().otroci[previus.smallBox].GetComponent<box_script>().stanje = 0;
            // Set value of bix box to zero
            // Because there could not be two moves that won in that square at once
            cubeBoxesList[previus.cubeBox].GetComponent<CubeBox>().stanje = 0;
            // Set picute of big one to default
            cubeBoxesList[previus.cubeBox].GetComponent<Image>().sprite = cubeBoxesList[previus.cubeBox].GetComponent<CubeBox>().defaultPicture;
            cubeBoxesList[previus.cubeBox].gameObject.SetActive(false);

            if (length > 1) {
                // Set grid to default color
                // just call next move hint
                // Because this will erase this move
                // But we must set prejsnja poteza to 2 back in history not only one like now
                nextMoveHint(allMoves[length - 1].cubeBox);
                prejsnjaPoteza = allMoves[length - 2].smallBox;
                moveBox(length - 2);
            } else {
                nextMoveHint(-1);
                prejsnjaPoteza = -1;
                lastMoveBox.gameObject.SetActive(false);
            }

            // Set different player
            firstPlayer = !firstPlayer;

            allMoves.RemoveAt(length - 1);
        }
    }
}

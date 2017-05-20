using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class box_script : MonoBehaviour {
    public int stanje = 0;
    public int pozicija = -1;
    public int parent = -1;
    private GameController gameController;

    [SerializeField] Sprite[] images;
    Image myImage;


    private void Start () {
        stanje = 0;
        gameController = FindObjectOfType<GameController>();
        myImage = GetComponent<Image>();
        myImage.sprite = images[0];
    }

    public void onClick () {
        Debug.Log("Here");
        if (!legalMove()) {
            Debug.Log("Ilegal");
            return;
        }

        gameController.nextMoveHint(pozicija);

        gameController.prejsnjaPoteza = pozicija;

        if (gameController.firstPlayer) {
            stanje = 1;
        } else {
            stanje = 2;
        }

        myImage.sprite = images[stanje];

        gameController.cubeBoxesList[parent].GetComponent<CubeBox>().izpisiOtroke();

        
        
        if (gameController.cubeBoxesList[parent].GetComponent<CubeBox>().triVVrsto()) {
            gameController.cubeBoxesList[parent].GetComponent<Image>().sprite = images[stanje];
            gameController.cubeBoxesList[parent].gameObject.SetActive(true);
            Debug.Log("Zmaga v majhnem");
        }

        if (gameController.konecIgre()) {
            //TODO: Kdo je zmagal
            Debug.Log("Zmaga");
        }

        gameController.firstPlayer = !gameController.firstPlayer;

    }

    bool legalMove () {
        // Prva poteza
        if (gameController.prejsnjaPoteza == -1) {
            return true;
        }
        
        return gameController.prejsnjaPoteza == parent && stanje == 0;
    }
}

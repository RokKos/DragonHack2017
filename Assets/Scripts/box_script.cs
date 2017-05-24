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
        Debug.Log(gameController.prejsnjaPoteza);
        if (!legalMove()) {
            // Show error to user
            StopCoroutine(gameController.shakeCamera());
            StartCoroutine(gameController.shakeCamera());
            Debug.Log("Ilegal");
            return;
        }
        // Postavitev stanja malega kvadratka
        if (gameController.firstPlayer) {
            stanje = 1;
        } else {
            stanje = 2;
        }

        myImage.sprite = images[stanje];

        //gameController.cubeBoxesList[parent].GetComponent<CubeBox>().izpisiOtroke();

        // Preverjanmje zmage
        if (gameController.cubeBoxesList[parent].GetComponent<CubeBox>().triVVrsto()) {
            //TODO: Kdo je zmagal
            if(!gameController.firstPlayer) {
                gameController.cubeBoxesList[parent].GetComponent<Image>().sprite = images[4];  // Big X
                gameController.cubeBoxesList[parent].GetComponent<CubeBox>().stanje = 2;
            }
            else {
                gameController.cubeBoxesList[parent].GetComponent<Image>().sprite = images[3];  // Big O 
                gameController.cubeBoxesList[parent].GetComponent<CubeBox>().stanje = 1;
            }
            gameController.cubeBoxesList[parent].gameObject.SetActive(true);
            Debug.Log("Zmaga v majhnem");
        }

        if (gameController.cubeBoxesList[parent].GetComponent<CubeBox>().zasedenoDoKonca()) {
            gameController.cubeBoxesList[parent].GetComponent<CubeBox>().stanje = 3;
            Debug.Log("Zasedeno");
        }
        
        if (gameController.konecIgre()) {
            //TODO: Kdo je zmagal
            if (gameController.firstPlayer) {
                gameController.ShowWinner(0);
            } else {
                gameController.ShowWinner(1);
            }
            
            Debug.Log("Zmaga");
        }

        if (gameController.konecPotez()) {
            gameController.ShowWinner(2);
            Debug.Log("Zmanjkalo potez");
        }

        // Kazanje naslednje poteze
        if (gameController.cubeBoxesList[pozicija].GetComponent<CubeBox>().stanje != 0) {
            gameController.nextMoveHint(10);
            gameController.prejsnjaPoteza = 10;
        } else {
            gameController.nextMoveHint(pozicija);
            gameController.prejsnjaPoteza = pozicija;
        }

        // Kazanje prejsnje poteze
        gameController.addLastMove(parent, pozicija);
        gameController.showPrevius();

        gameController.firstPlayer = !gameController.firstPlayer;
        gameController.changePicture();

    }

    bool legalMove () {
        // Prva poteza
        if (gameController.prejsnjaPoteza == -1) { 
            return true;
        }
        if (gameController.prejsnjaPoteza == 10 && stanje == 0) {
            return true;
        } else if (gameController.prejsnjaPoteza == 10 && stanje != 0) {
            return false;
        }
        Debug.Log("lalalala");
        return gameController.prejsnjaPoteza == parent && stanje == 0;
    }
}

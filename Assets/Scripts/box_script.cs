using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class box_script : MonoBehaviour {
    public int stanje = -1;
    public int pozicija = -1;
    public int parent = -1;
    private GameController gameController;

    private void Start () {
        gameController = FindObjectOfType<GameController>();
    }

    public void onClick () {
        Debug.Log("Here");
        if (!legalMove()) {
            return;
        }
        if (gameController.firstPlayer) {
            stanje = 1;
        } else {
            stanje = 2;
        }

        gameController.firstPlayer = !gameController.firstPlayer;
        
        if (gameController.cubeBoxesList[parent].GetComponent<CubeBox>().triVVrsto()) {
            //TODO: Kdo je zmagal
            Debug.Log("Zmaga v majhnem");
        }

        if (gameController.konecIgre()) {
            //TODO: Kdo je zmagal
            Debug.Log("Zmaga");
        }

    }

    bool legalMove () {
        return gameController.prejsnjaPoteza == parent;
    }
}

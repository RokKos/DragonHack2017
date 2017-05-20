//Author: Rok Kos<kosrok97@gmail.com>
//File: CubeBox.cs
//File path: /D/Documents/Unity/DragonHack2017/CubeBox.cs
//Date: 20.05.2017
//Description: Scipt that controls cube box (9x9)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBox : MonoBehaviour {

    public List<GameObject> otroci;
    public int pozicija = -1;
    public int stanje = 0;  // 0 - empty, 1 - player 1, 2 - player 2

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    bool triVVrsto () {
        // Check rows
        for (int i = 0; i < 3; ++i) {
            if (otroci[i*3].GetComponent<box_script>().stanje == otroci[i * 3 + 1].GetComponent<box_script>().stanje && otroci[i * 3].GetComponent<box_script>().stanje == otroci[i * 3 + 2].GetComponent<box_script>().stanje) {
                return true;
            }
        }
        // Check columns
        for (int i = 0; i < 3; ++i) {
            if (otroci[i].GetComponent<box_script>().stanje == otroci[i + 3].GetComponent<box_script>().stanje && otroci[i].GetComponent<box_script>().stanje == otroci[i + 6].GetComponent<box_script>().stanje) {
                return true;
            }
        }

        // Check diagonals
        if (otroci[0].GetComponent<box_script>().stanje == otroci[4].GetComponent<box_script>().stanje && otroci[0].GetComponent<box_script>().stanje == otroci[8].GetComponent<box_script>().stanje) {
            return true;
        }

        if (otroci[2].GetComponent<box_script>().stanje == otroci[4].GetComponent<box_script>().stanje && otroci[2].GetComponent<box_script>().stanje == otroci[6].GetComponent<box_script>().stanje) {
            return true;
        }

        return false;
    }
}

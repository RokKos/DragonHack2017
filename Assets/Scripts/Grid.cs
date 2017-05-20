//Author: Rok Kos<kosrok97@gmail.com>
//File: Grid.cs
//File path: /D/Documents/Unity/DragonHack2017/Grid.cs
//Date: 20.05.2017
//Description: Scipt that controls grid layout

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grid : MonoBehaviour {
    Image image;

    private void Start () {
        image = GetComponent<Image>();
    }

    public IEnumerator fadeAnimacija () {
        int diff = -1;
        while (true) {

            if (image.color.a < 0.5 || image.color.a == 1.0) {
                diff *= -1;
            }

            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a + diff * 0.05f);
            yield return null;
        }

    }
}

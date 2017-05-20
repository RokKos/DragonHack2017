using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class box_script : MonoBehaviour {

    [SerializeField] Sprite[] images;

    Image myImage;
    int stanje = 0;

	// Use this for initialization
	void Start () {
        myImage = GetComponent<Image>();
        myImage.sprite = images[0];

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

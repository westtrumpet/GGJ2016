﻿using UnityEngine;
using System.Collections;

public class playerCustom : MonoBehaviour {

	public Material[] colors;
	public GameObject[] models;
	public GameObject[] stands;
	public Transform modelPos;
	public Transform standPos;
	int colorNum;
	int modelNum;
	int standNum;
	public GameObject modelObj;
	public GameObject standObj;
	Material colorObj;

	public playerCharStats displayStats;

	public int atk;
	public int def;
	public int mag;
	public int rng;
	public int mv;

	public bool charReady;
	public int playerNum;
	public bool turnEnable;

	float timeToNextTurn = 2.0f;
	float turnEnd = 0.0f;

	// Use this for initialization
	void Start () {
		modelObj = Instantiate (models [modelNum], modelPos.position, modelPos.rotation) as GameObject;
		standObj = Instantiate (stands [standNum], standPos.position, standPos.rotation) as GameObject;
		colorObj = colors [colorNum];
		modelObj.GetComponent<Renderer> ().material = colorObj;
		standObj.GetComponent<Renderer> ().material = colorObj;
	}
	
	// Update is called once per frame
	void Update () {
		if (turnEnable) {
			if (Input.GetKeyDown (KeyCode.Q)) {
				modelDown ();
			}
			if (Input.GetKeyDown (KeyCode.W)) {
				modelUp ();
			}
			if (Input.GetKeyDown (KeyCode.A)) {
				standDown ();
			}
			if (Input.GetKeyDown (KeyCode.S)) {
				standUp ();
			}
			if (Input.GetKeyDown (KeyCode.Z)) {
				colorDown ();
			}
			if (Input.GetKeyDown (KeyCode.X)) {
				colorUp ();
			}
			if (Input.GetKeyDown (KeyCode.Space) && isValidSelect() && canTakeTurn() && playerNum ==1) {
				submit ();
				Debug.Log ("Submitted");
			}
			if (Input.GetKeyDown (KeyCode.Return) && isValidSelect() && canTakeTurn() && playerNum ==2) {
				submit ();
				Debug.Log ("Submitted");
			}
		}
	}

	void modelUp() {
		modelNum++;
		if (modelNum >= models.Length) {
			modelNum = 1;
		}
		Destroy (modelObj);
		modelObj = Instantiate (models [modelNum], modelPos.position, modelPos.rotation) as GameObject;
		modelObj.GetComponent<Renderer> ().material = colorObj;
		changeStats ();
	}

	void modelDown() {
		modelNum--;
		if (modelNum <= 0) {
			modelNum = models.Length - 1;
		}
		Destroy (modelObj);
		modelObj = Instantiate (models [modelNum], modelPos.position, modelPos.rotation) as GameObject;
		modelObj.GetComponent<Renderer> ().material = colorObj;
		changeStats ();
	}

	void standUp() {
		standNum++;
		if (standNum >= stands.Length) {
			standNum = 1;
		}
		Destroy (standObj);
		standObj = Instantiate (stands [standNum], standPos.position, standPos.rotation) as GameObject;
		standObj.GetComponent<Renderer> ().material = colorObj;
		changeStats ();
	}

	void standDown() {
		standNum--;
		if (standNum <= 0) {
			standNum = stands.Length - 1;
		}
		Destroy (standObj);
		standObj = Instantiate (stands [standNum], standPos.position, standPos.rotation) as GameObject;
		standObj.GetComponent<Renderer> ().material = colorObj;
		changeStats ();
	}

	void colorUp() {
		colorNum++;
		if (colorNum >= colors.Length) {
			colorNum = 1;
		}
		colorObj = colors [colorNum];
		modelObj.GetComponent<Renderer> ().material = colorObj;
		standObj.GetComponent<Renderer> ().material = colorObj;
		changeStats ();
	}

	void colorDown() {
		colorNum--;
		if (colorNum <= 0) {
			colorNum = colors.Length - 1;
		}
		colorObj = colors [colorNum];
		modelObj.GetComponent<Renderer> ().material = colorObj;
		standObj.GetComponent<Renderer> ().material = colorObj;
		changeStats ();
	}

	void submit () {
		if(turnEnable) charReady = true;
		turnEnd = Time.time;
	}

	public bool isValidSelect() {
		if (colorNum == 0)
			return false;
		if (modelNum == 0)
			return false;
		if (standNum == 0)
			return false;
		return true;
	}

	public bool canTakeTurn(){
		return ((Time.time - turnEnd) > timeToNextTurn);
	}

	void changeStats() {
		int tempAtk = 1;
		int tempDef = 1;
		int tempMag = 1;
		int tempRng = 2;
		int tempMv = 1;

		if (modelNum == 1) {
			tempAtk++;
		}
		if (modelNum == 2) {
			tempDef++;
		}
		if (modelNum == 3) {
			tempMag++;
		}


		if (standNum == 1) {
			tempAtk++;
			tempMag--;
		}
		if (standNum == 2) {
			tempMag++;
			tempRng--;
		}
		if (standNum == 3) {
			tempRng++;
			tempDef--;
		}
		if (standNum == 4) {
			tempDef++;
			tempAtk--;
		}


		if (colorNum == 1) {
			tempAtk++;
			tempDef--;
			tempMv--;
		}
		if (colorNum == 2) {
			tempDef++;
			tempMag--;
			tempMv++;
		}
		if (colorNum == 3) {
			tempMag++;
			tempRng--;
			tempMv--;
		}
		if (colorNum == 4) {
			tempRng++;
			tempAtk--;
			tempMv++;
		}
		if (colorNum == 5) {
			tempAtk++;
			tempMag--;
			tempMv++;
		}
		if (colorNum == 6) {
			tempRng++;
			tempDef--;
			tempMv--;
		}

		atk = tempAtk;
		def = tempDef;
		mag = tempMag;
		rng = tempRng;
		mv = tempMv;

		displayStats.changeDisplay ();
	}

	void OnDisable() {
		Destroy (modelObj);
		Destroy (standObj);
	}
}

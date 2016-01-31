using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	//game set up in phases.
	//
	//First phase is character customization
	//players simultaneously customize team of characters until both players ready
	//
	//Second phase, players take turns setting up teams on board
	//they select a character, then select a valid space

	//board is 10 x 7
	public Tile[][] board;
	public PlayerObj[] player1char;
	public PlayerObj[] player2char;
	public int player1charTurn;
	public int player2charTurn;
	public Transform[] player1charModelWorldPos;
	public Transform[] player1charStandWorldPos;
	public Transform[] player2charModelWorldPos;
	public Transform[] player2charStandWorldPos;

	public playerCustom player1custom;
	public playerCustom player2custom;
	public int player1customCharNum;
	public int player2customCharNum;

	// currentTurn can equal "player1" or "player2"
	public string currentTurn = "player1";
	// phase can equal "customize" , "combat"
	public string phase = "customize";

	public int numChar;

	public bool test;

	float timeToNextTurn = 2.0f;
	float turnEnd = 0.0f;

	public Camera testCam;
	public Camera mainCam;
	public Camera player1Cam;
	public Camera player2Cam;

	void OnEnable() {
		if (test) {
			mainCam.enabled = false;
			player1Cam.enabled = false;
			player2Cam.enabled = false;
			testCam.enabled = true;
		} else {
			mainCam.enabled = true;
		}
	}
	void Start() {
		player1char = new PlayerObj[numChar];
		player2char = new PlayerObj[numChar];
		player1custom.turnEnable = true;
		player2custom.turnEnable = false;
	}

	void nextTurn() {
		if (currentTurn == "player1") {
			currentTurn = "player2";
			player1charTurn++;
			if (player1charTurn >= player1char.Length) {
				player1charTurn = 0;
			}
		}
		if (currentTurn == "player2") {
			currentTurn = "player1";
			player2charTurn++;
			if (player2charTurn >= player2char.Length) {
				player2charTurn = 0;
			}
		}
	}

	void nextSelectTurn() {
		if (player1custom.turnEnable) {
			player1custom.turnEnable = false;
			player2custom.turnEnable = true;
		} else {
			player2custom.turnEnable = false;
			player1custom.turnEnable = true;
		}
	}

	void Update() {
		if (player1custom.charReady && player1custom.turnEnable && canTakeTurn()) {
			turnEnd = Time.time;
			AddCharacterPlayer1 ();
			nextSelectTurn ();
			player1custom.charReady = false;
			player2custom.charReady = false;
			return;
		}
		if (player2custom.charReady && player2custom.turnEnable && canTakeTurn()) {
			turnEnd = Time.time;
			AddCharacterPlayer2 ();
			nextSelectTurn ();
			player1custom.charReady = false;
			player2custom.charReady = false;
			return;
		}

	}

	void AddCharacterPlayer1() {
		if (player1customCharNum < player1char.Length) {
			player1char [player1customCharNum] = new PlayerObj (4, player1custom.atk, player1custom.def, player1custom.mag, player1custom.rng, player1custom.mv);
			player1char [player1customCharNum].addStandAndModel (player1custom.standObj, player1custom.modelObj, player1charStandWorldPos [player1customCharNum], player1charModelWorldPos [player1customCharNum]);
			player1customCharNum++;
		}
	}

	void AddCharacterPlayer2() {
		if (player2customCharNum < player2char.Length) {
			player2char [player2customCharNum] = new PlayerObj (4, player2custom.atk, player2custom.def, player2custom.mag, player2custom.rng, player2custom.mv);
			player2char [player2customCharNum].addStandAndModel (player2custom.standObj, player2custom.modelObj, player2charStandWorldPos [player2customCharNum], player2charModelWorldPos [player2customCharNum]);
			player2customCharNum++;
		}
	}

	bool canTakeTurn(){
		return ((Time.time - turnEnd) > timeToNextTurn);
	}
}

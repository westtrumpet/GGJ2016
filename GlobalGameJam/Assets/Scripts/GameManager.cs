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
	//
	//Third phase is combat
	//players take turns moving one of their characters, which rotate in order
	//and then select eligible targets to attack before going to the next player

	//board is 10 x 7
	public Tile[,] board;
	public GameObject tileButtons;
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

	public playerCharStats player1DisplayStats;
	public playerCharStats player2DisplayStats;

	public int selectedTileX;
	public int selectedTileY;

	// currentTurn can equal "player1" or "player2"
	public string currentTurn = "player1";
	// phase can equal "customize" , "placement" , "combat"
	public string phase = "customize";

	public int numChar;

	public bool test;

	//Used only in customization
	float timeToNextTurn = 2.0f;
	float turnEnd = 0.0f;

	//Used only in placement phase
	int turnsLeft = 6;

	//Used only in combat phase
	//action can be "move" or "attack"
	string action = "move";

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
		board = new Tile[7, 10];
		player1char = new PlayerObj[numChar];
		player2char = new PlayerObj[numChar];
		player1custom.turnEnable = true;
		player2custom.turnEnable = false;
		for (int i = 0; i < 7; i++) {
			
			for (int j = 0; j < 10; j++) {
				board [i, j] = new Tile (i, j);
			}
		}
		tileButtons.SetActive (false);
	}

	void nextTurn() {
		if (currentTurn == "player1") {
			Debug.Log ("Switch turn to player2");
			currentTurn = "player2";
			player1charTurn++;
			if (player1charTurn >= player1char.Length) {
				player1charTurn = 0;
			}
			player1DisplayStats.changeDisplay ();
		}
		else {
			Debug.Log ("Switch turn to player1");
			currentTurn = "player1";
			player2charTurn++;
			if (player2charTurn >= player2char.Length) {
				player2charTurn = 0;
			}
			player2DisplayStats.changeDisplay ();
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
			player1char [player1customCharNum].playerOwner = "player1";
			player1customCharNum++;
		} else {
			player1custom.enabled = false;
			player1DisplayStats.HpDisplay.SetActive (true);
			changeStatDisplay ();
		}
	}

	void AddCharacterPlayer2() {
		if (player2customCharNum < player2char.Length) {
			player2char [player2customCharNum] = new PlayerObj (4, player2custom.atk, player2custom.def, player2custom.mag, player2custom.rng, player2custom.mv);
			player2char [player2customCharNum].addStandAndModel (player2custom.standObj, player2custom.modelObj, player2charStandWorldPos [player2customCharNum], player2charModelWorldPos [player2customCharNum]);
			player2char [player2customCharNum].playerOwner = "player2";
			player2customCharNum++;
		} else {
			player2custom.enabled = false;
			changeStatDisplay ();
			tileButtons.SetActive (true);
			player2DisplayStats.HpDisplay.SetActive (true);
			phase = "placement";
		}
	}

	bool canTakeTurn(){
		return ((Time.time - turnEnd) > timeToNextTurn);
	}

	public void moveChar(Vector2 space, PlayerObj charToMove){
		Tile spaceToMove = board [(int) space.x, (int) space.y];
		if (moveIsValid(space, charToMove)) {
			spaceToMove.occupied = true;
			spaceToMove.charOccupy = charToMove;
			board [(int) charToMove.pos.x, (int) charToMove.pos.y].occupied = false;

		}
	}

	public bool moveIsValid(Vector2 space, PlayerObj charToMove) {

		int charX = (int)charToMove.pos.x;
		int charY = (int)charToMove.pos.y;
		int moveX = (int)space.x;
		int moveY = (int)space.y;
		Tile spaceToMove = board [moveX, moveY];

		if (spaceToMove.occupied)
			return false;
		if ((Mathf.Abs (moveX - charX) + Mathf.Abs (moveY - charY)) > charToMove.move)
			return false;
		return true;
	}

	public void changeStatDisplay() {
		player1DisplayStats.currentChar = player1char [player1charTurn];
		player1DisplayStats.changeDisplay ();
		player2DisplayStats.currentChar = player2char [player2charTurn];
		player2DisplayStats.changeDisplay ();
	}

	public void clickTile(int x){
		selectedTileX = x % 7;
		selectedTileY = (int)x / 7;
		Debug.Log ("X: " + selectedTileX + " Y: " + selectedTileY);
		Debug.Log ("Phase: " + phase);
		if (phase == "placement") {
			if (currentTurn == "player1") {
				placeChar (player1char [player1charTurn]);
			}else{
				placeChar (player2char [player2charTurn]);
			}
		}
		if (phase == "combat") {
			if (action == "move") {
				if (currentTurn == "player1") {
					moveChar (new Vector2 (selectedTileX, selectedTileY), player1char [player1charTurn]);
				} else {
					moveChar (new Vector2 (selectedTileX, selectedTileY), player2char [player2charTurn]);
				}
			} else {
				if (currentTurn == "player1") {
					attack (new Vector2 (selectedTileX, selectedTileY), player1char [player1charTurn]);
				} else {
					attack (new Vector2 (selectedTileX, selectedTileY), player2char [player2charTurn]);
				}
			}
		}
	}

	public bool validPlaceTile() {
		return (!board [selectedTileX, selectedTileY].occupied);
	}

	public void placeChar(PlayerObj playerChar) {
		if (validPlaceTile ()) {
			turnsLeft--;
			board [selectedTileX, selectedTileY].addCharacter (playerChar);
			playerChar.scaleChar (.8f);
			playerChar.moveChar (new Vector2(selectedTileX, selectedTileY));
			if (turnsLeft == 0)
				phase = "combat";
			nextTurn ();
		}
	}

	public bool targetValid(Vector2 targetPos, PlayerObj playerChar, string attackType) {
		if (attackType == "magic" && playerChar.range < (int)(Mathf.Abs (targetPos.x - playerChar.pos.x) + Mathf.Abs (targetPos.y - playerChar.pos.x)))
			return false;
		else if (!board [(int)targetPos.x, (int)targetPos.y].occupied)
			return false;
		else if (board [(int)targetPos.x, (int)targetPos.y].charOccupy.playerOwner == playerChar.playerOwner)
			return false;
		else
			return true;
	}

	public void attack(Vector2 targetPos, PlayerObj playerChar) {
		if (((int)Mathf.Abs (playerChar.pos.x - targetPos.x) + (int)Mathf.Abs (playerChar.pos.y - targetPos.y)) > 1) {
			if (targetValid (targetPos, playerChar, "magic")) {
				board [(int)targetPos.x, (int)targetPos.y].target (playerChar.magic, "magic");
				nextTurn ();
			}
		} else {
			if (targetValid (targetPos, playerChar, "physical")) {
				board [(int)targetPos.x, (int)targetPos.y].target (playerChar.attack, "physical");
				action = "move";
				nextTurn ();
			}
		}
	}
}

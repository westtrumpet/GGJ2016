using UnityEngine;
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
				Debug.Log ("Is Turn: " + turnEnable);
			}
			if (Input.GetKeyDown (KeyCode.Return) && isValidSelect() && canTakeTurn() && playerNum ==2) {
				submit ();
				Debug.Log ("Submitted");
				Debug.Log ("Is Turn: " + turnEnable);
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
	}

	void modelDown() {
		modelNum--;
		if (modelNum <= 0) {
			modelNum = models.Length - 1;
		}
		Destroy (modelObj);
		modelObj = Instantiate (models [modelNum], modelPos.position, modelPos.rotation) as GameObject;
		modelObj.GetComponent<Renderer> ().material = colorObj;
	}

	void standUp() {
		standNum++;
		if (standNum >= stands.Length) {
			standNum = 1;
		}
		Destroy (standObj);
		standObj = Instantiate (stands [standNum], standPos.position, standPos.rotation) as GameObject;
		standObj.GetComponent<Renderer> ().material = colorObj;
	}

	void standDown() {
		standNum--;
		if (standNum <= 0) {
			standNum = stands.Length - 1;
		}
		Destroy (standObj);
		standObj = Instantiate (stands [standNum], standPos.position, standPos.rotation) as GameObject;
		standObj.GetComponent<Renderer> ().material = colorObj;
	}

	void colorUp() {
		colorNum++;
		if (colorNum >= colors.Length) {
			colorNum = 1;
		}
		colorObj = colors [colorNum];
		modelObj.GetComponent<Renderer> ().material = colorObj;
		standObj.GetComponent<Renderer> ().material = colorObj;
	}

	void colorDown() {
			colorNum--;
			if (colorNum <= 0) {
				colorNum = colors.Length - 1;
			}
			colorObj = colors [colorNum];
			modelObj.GetComponent<Renderer> ().material = colorObj;
			standObj.GetComponent<Renderer> ().material = colorObj;
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
}

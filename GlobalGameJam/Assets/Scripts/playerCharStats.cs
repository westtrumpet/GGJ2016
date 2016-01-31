using UnityEngine;
using System.Collections;

public class playerCharStats : MonoBehaviour {

	public playerCustom customStats;
	public PlayerObj currentChar;

	public GameObject HpDisplay;

	public GameObject[] atkPlus;
	public GameObject[] defPlus;
	public GameObject[] magPlus;
	public GameObject[] rngPlus;
	public GameObject[] mvPlus;

	public GameObject[] atkMin;
	public GameObject[] defMin;
	public GameObject[] magMin;
	public GameObject[] rngMin;
	public GameObject[] mvMin;

	public GameObject[] hpPlus;
	public GameObject[] hpMin;

	GameObject[] displaySign(int number, string stat){
		if (stat == "atk" && number >= 0)
			return atkPlus;
		if (stat == "atk" && number < 0)
			return atkMin;
		if (stat == "def" && number >= 0)
			return defPlus;
		if (stat == "def" && number < 0)
			return defMin;
		if (stat == "mag" && number >= 0)
			return magPlus;
		if (stat == "mag" && number < 0)
			return magMin;
		if (stat == "rng" && number >= 0)
			return rngPlus;
		if (stat == "rng" && number < 0)
			return rngMin;
		if (stat == "mv" && number >= 0)
			return mvPlus;
		if (stat == "mv" && number < 0)
			return mvMin;
		return null;
	}

	void displayStat(int number, GameObject[] statArray) {
		for (int i = 0; i < Mathf.Abs(number); i++) {
			statArray [i].SetActive (true);
		}
		for (int i = Mathf.Abs(number); i < statArray.Length; i++) {
			statArray [i].SetActive (false);
		}
	}

	void displayHP(int number) {
		for (int i = 0; i < Mathf.Abs(number); i++) {
			hpPlus [i].SetActive (true);
			hpMin [i].SetActive (false);
		}
		for (int i = Mathf.Abs(number); i < hpMin.Length; i++) {
			hpMin [i].SetActive (true);
			hpPlus [i].SetActive (false);
		}
	}

	public void changeDisplay() {
		if (customStats.enabled == true) {
			displayStat (customStats.atk, displaySign (customStats.atk, "atk"));
			displayStat (customStats.def, displaySign (customStats.def, "def"));
			displayStat (customStats.mag, displaySign (customStats.mag, "mag"));
			displayStat (customStats.rng, displaySign (customStats.rng, "rng"));
			displayStat (customStats.mv, displaySign (customStats.mv, "mv"));
		} else {
			displayStat (currentChar.attack, displaySign (currentChar.attack, "atk"));
			displayStat (currentChar.defense, displaySign (currentChar.defense, "def"));
			displayStat (currentChar.magic, displaySign (currentChar.magic, "mag"));
			displayStat (currentChar.range, displaySign (currentChar.range, "rng"));
			displayStat (currentChar.move, displaySign (currentChar.move, "mv"));
			displayHP (currentChar.currentHealth);
		}
	}

}

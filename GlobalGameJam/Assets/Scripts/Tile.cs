using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	public bool occupied;
	public PlayerObj charOccupy;

	//A player targets a tile, to which the tile receives the damage attempting to be dealt with a damage type
	//returns the amount of damage actually dealt
	//type can be "magic" or "physical"
	public int target(int damage, string type) {
		int damageDealt;
		int damageBlocked;
		if (type == "physical") {
			damageDealt = damage - charOccupy.getDefense();
		}
		else {
		//if (type == "magic") {
			damageDealt = damage - charOccupy.getMagic();
		}
		if (damageDealt <= 0)
			damageDealt = 1;
		damageBlocked = damage - damageDealt;
		charOccupy.takeDamage (damageDealt, damageBlocked);
		return damageDealt;
	}

}

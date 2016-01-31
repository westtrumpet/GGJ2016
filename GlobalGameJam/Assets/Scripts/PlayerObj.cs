using UnityEngine;
using System.Collections;
[System.Serializable]
public class PlayerObj{
	
	public ModelObj MeshesModelObj;

	public string playerOwner;

	public int health;
	public int currentHealth;
	public int attack;
	public int defense;
	public int magic;
	public int range;
	public int move;
	public bool alive;
	public Vector2 pos;

	public void takeDamage(int damageDealt, int damageblocked){
		currentHealth -= damageDealt;
		if (currentHealth <= 0) {
			alive = false;
			MonoBehaviour.Destroy (MeshesModelObj);
		}
	}

	public int getMaxHealth() {
		return health;
	}

	public int getCurrentHealth() {
		return currentHealth;
	}

	public int getAttack() {
		return attack;
	}

	public int getDefense() {
		return defense;
	}

	public int getMagic() {
		return magic;
	}

	public bool isAlive() {
		return alive;
	}

	public PlayerObj(int maxhp, int atk, int def, int mag, int rng, int mv) {
		health = maxhp;
		currentHealth = maxhp;
		attack = atk;
		defense = def;
		magic = mag;
		range = rng;
		move = mv;
		alive = true;
		MeshesModelObj = new ModelObj();
	}

	public void addStandAndModel(GameObject stand, GameObject model, Transform newStandPosition, Transform newModelPosition){
		MeshesModelObj.addStand (stand, newStandPosition);
		MeshesModelObj.addModel (model, newModelPosition);
	}

	public void moveModel(Vector3 position) {
		MeshesModelObj.moveStand (position);
		MeshesModelObj.moveModel (position + (Vector3.up * 3));
	}

	public void moveChar(Vector2 position) {
		pos = position;
		moveModel (new Vector3 (position.y * 4 - 18, 1.5f, position.x * (-4) + 12));
	}

	public void scaleChar(float scale){
		MeshesModelObj.scaleChar (scale);
	}
}

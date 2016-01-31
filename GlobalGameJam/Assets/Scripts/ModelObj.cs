using UnityEngine;
using System.Collections;

public class ModelObj : MonoBehaviour {

	public GameObject standObj;
	public GameObject modelObj;

	void OnDestroy() {
		Destroy (standObj);
		Destroy (modelObj);
	}

	public void addStand(GameObject stand, Transform position){
		standObj = Instantiate (stand, position.position, position.rotation) as GameObject;
	}

	public void addModel(GameObject model, Transform position){
		modelObj = Instantiate (model, position.position, position.rotation) as GameObject;
	}

	public ModelObj() {
	}

	public void moveModel(Vector3 position){
		modelObj.transform.position = position;
	}

	public void moveStand(Vector3 position){
		standObj.transform.position = position;
	}

	public void scaleChar(float scale) {
		modelObj.transform.localScale = new Vector3 (scale, scale, scale);
		standObj.transform.localScale = new Vector3 (scale, scale, scale);
	}

}

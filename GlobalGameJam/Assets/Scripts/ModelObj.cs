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

}

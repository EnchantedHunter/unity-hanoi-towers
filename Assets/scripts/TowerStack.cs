using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerStack: MonoBehaviour  {
	
	private List<GameObject> blockPool;
	private float blockSize;
	private Vector3 vacancyPlace = Vector3.zero;
	private Vector3 upperPlace = Vector3.zero;
	private const float fixedHeigh = 4f;
	private const float blockYConstScale = 0.5f;

	public void Start(){
		blockPool = new List<GameObject> ();
		upperPlace.Set(this.transform.position.x, fixedHeigh , this.transform.position.z);
	}

	public void Add(GameObject gameObject){
		blockPool.Add (gameObject);
	}

	public GameObject GetBlock(){
		if(blockPool.Count>0){
			var lastIndex = blockPool.Count - 1;
			GameObject gameObject = blockPool [lastIndex];
			blockPool.RemoveAt (lastIndex);
			return gameObject;
		}
		return null;
	} 

	public void Clear(){
		foreach (GameObject go in blockPool) {
			Destroy (go);
		}
		blockPool.Clear ();
	}

	public Vector3 GetVacancyPlace(){
		var y = blockPool.Count * blockYConstScale;
		vacancyPlace.Set(this.transform.position.x, y, this.transform.position.z);
		return vacancyPlace;
	}

	public Vector3 GetUpperPlace(){
		return upperPlace;
	}
}

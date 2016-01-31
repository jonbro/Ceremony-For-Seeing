using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InscriptionAnimation : MonoBehaviour {
	public GameObject startPosition, animationPosition;
	public GameObject drawingPoint;
	public GameObject inscriptionPrefab;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void ObjectPlaced(GameObject obj){
		Debug.Log(obj);
		StartCoroutine(RunAnimation(obj));
	}
	IEnumerator RunAnimation(GameObject target){
		yield return new WaitForSeconds(0.5f);
		// lerp the position of the game object up
		LeanTween.move (target, animationPosition.transform.position, 1);
		yield return new WaitForSeconds(1.5f);
		GameObject inscription = (GameObject)Instantiate(inscriptionPrefab, animationPosition.transform.position, Quaternion.identity);
		LineRenderer inscriptionRenderer = inscription.GetComponent<LineRenderer>();
		int numPoints = 512;
		inscriptionRenderer.SetVertexCount(numPoints);
		// push a bunch of junk into it, just so we can make sure it is working
		Vector3 lastDrawingPointPos =  inscription.transform.InverseTransformPoint(drawingPoint.transform.position);
		for (int i = 0; i < numPoints; i++) {
			inscriptionRenderer.SetPosition(i, lastDrawingPointPos);
		}

		float timeToRun = 10; // need to calculate the rates of rotation so that this number works
		int currentPoint = numPoints;
		while(timeToRun > 0){
			yield return new WaitForEndOfFrame();
			timeToRun -= Time.deltaTime;
			Vector3 newDrawingPointPos =  inscription.transform.InverseTransformPoint(drawingPoint.transform.position);
			if((newDrawingPointPos-lastDrawingPointPos).magnitude > 0.05){
				currentPoint--;
				lastDrawingPointPos = newDrawingPointPos;
			}
			// move all the points in the line to the new draw point
			for (int i = 0; i < currentPoint; i++) {
				inscriptionRenderer.SetPosition(i, newDrawingPointPos);
			}
		}
		inscription.transform.SetParent(target.transform);
		LeanTween.move (target, startPosition.transform.position, 1);
		yield return new WaitForSeconds(1.5f);
		SendMessage("RitualComplete", target);
	}
}

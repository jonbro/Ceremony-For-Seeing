using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InscriptionAnimation : MonoBehaviour {
	public GameObject startPosition, animationPosition;
	public GameObject drawingPoint;
	public GameObject inscriptionPrefab;
	public List<GameObject> inscriberArms;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void ObjectPlaced(GameObject obj){
		float timeToRun = 10;

		StartCoroutine(RunAnimation(obj, timeToRun));
	}
	void RunArms(float timeToRun){
		foreach(GameObject arm in inscriberArms){
			GameObject _captureArm = arm;
			Debug.Log(arm.name);
			Quaternion originalRot = _captureArm.transform.rotation;
			RotateAroundAxis axis = _captureArm.GetComponent<RotateAroundAxis>();
			LeanTween.value(_captureArm, (float rotVal)=>{
				_captureArm.transform.localRotation = originalRot*Quaternion.Euler(rotVal*axis.rotationAxis.x, rotVal*axis.rotationAxis.y, rotVal*axis.rotationAxis.z);
			}, 0, 360*Mathf.Floor(1+Random.value*6), timeToRun).setEase(LeanTweenType.easeInOutCubic);
		}
	}
	IEnumerator RunAnimation(GameObject target, float timeToRun){
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
		RunArms(timeToRun);

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

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InscriptionAnimation : MonoBehaviour {
	public GameObject startPosition, animationPosition;
	public GameObject drawingPoint;
	public GameObject inscriptionPrefab;
	public List<GameObject> inscriberArms;
	public static Color HSVToRGB(float H, float S, float V)
	{
		if (S == 0f)
			return new Color(V,V,V);
		else if (V == 0f)
			return Color.black;
		else
		{
			Color col = Color.black;
			float Hval = H * 6f;
			int sel = Mathf.FloorToInt(Hval);
			float mod = Hval - sel;
			float v1 = V * (1f - S);
			float v2 = V * (1f - S * mod);
			float v3 = V * (1f - S * (1f - mod));
			switch (sel + 1)
			{
			case 0:
				col.r = V;
				col.g = v1;
				col.b = v2;
				break;
			case 1:
				col.r = V;
				col.g = v3;
				col.b = v1;
				break;
			case 2:
				col.r = v2;
				col.g = V;
				col.b = v1;
				break;
			case 3:
				col.r = v1;
				col.g = V;
				col.b = v3;
				break;
			case 4:
				col.r = v1;
				col.g = v2;
				col.b = V;
				break;
			case 5:
				col.r = v3;
				col.g = v1;
				col.b = V;
				break;
			case 6:
				col.r = V;
				col.g = v1;
				col.b = v2;
				break;
			case 7:
				col.r = V;
				col.g = v3;
				col.b = v1;
				break;
			}
			col.r = Mathf.Clamp(col.r, 0f, 1f);
			col.g = Mathf.Clamp(col.g, 0f, 1f);
			col.b = Mathf.Clamp(col.b, 0f, 1f);
			return col;
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void ObjectPlaced(GameObject obj){
		float timeToRun = 15;

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
			}, 0, 360*Mathf.Floor(1+Random.value*4), timeToRun).setEase(LeanTweenType.easeInOutExpo);
		}
	}
	IEnumerator RunAnimation(GameObject target, float timeToRun){
		yield return new WaitForSeconds(0.5f);
		// lerp the position of the game object up
		LeanTween.move (target, animationPosition.transform.position, 1).setEase(LeanTweenType.easeInOutCubic);
		yield return new WaitForSeconds(1.5f);
		GameObject inscription = (GameObject)Instantiate(inscriptionPrefab, animationPosition.transform.position, Quaternion.identity);
		LineRenderer inscriptionRenderer = inscription.GetComponent<LineRenderer>();
		float hue = Random.value;
		inscriptionRenderer.SetColors(HSVToRGB(hue, 0.8f, 1.0f), HSVToRGB(hue+0.1f, 1.0f, 0.8f));
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
		LeanTween.move (target, startPosition.transform.position, 1).setEase(LeanTweenType.easeInOutCirc);
		yield return new WaitForSeconds(1.5f);
		SendMessage("RitualComplete", target);
	}
}

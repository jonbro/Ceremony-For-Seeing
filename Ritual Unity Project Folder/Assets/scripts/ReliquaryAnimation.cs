using UnityEngine;
using System.Collections;

public class ReliquaryAnimation : MonoBehaviour {
	GameObject ritualObj;
	public GameObject reliquaryPrefab, stamper, stamperTarget, objectMoveInternal, objectMoveFinal;
	Vector3 stamperStartPosition;
	void ObjectPlaced(GameObject obj){
		// move object back into machine
		ritualObj = obj;
		stamperStartPosition = stamper.transform.position;
		LeanTween.move(obj, objectMoveInternal.transform.position, 3.0f).setEase(LeanTweenType.easeInCubic).setOnComplete(StamperDownAnimation);
	}
	void StamperDownAnimation(){
		LeanTween.move(stamper, stamperTarget.transform.position, 0.5f).setEase(LeanTweenType.easeInQuad).setOnComplete(StamperUpAnimation);
	}
	void StamperUpAnimation(){
		LeanTween.move(stamper, stamperStartPosition, 1.5f).setEase(LeanTweenType.easeInOutCirc).setOnComplete(MoveOutAnimation);
	}
	void MoveOutAnimation(){
		GameObject reliquary = (GameObject)Instantiate(reliquaryPrefab, ritualObj.transform.position, Quaternion.identity);
		reliquary.transform.SetParent(ritualObj.transform);
		LeanTween.move(ritualObj, objectMoveFinal.transform.position, 4.0f).setEase(LeanTweenType.easeOutQuad).setOnComplete(AllAnimationsComplete);
	}
	void AllAnimationsComplete(){
		SendMessage("RitualComplete", ritualObj);
	}
	IEnumerator RunAnimation(){
		yield return null;
	}
}

// Attach this script and a sphere collider to the object. Adjust the sphere radius to your needs. Use the speed and damping variables to tweak the rotation speed.
//
// Author: Vlad Chifor - racocvr [at] gmail (dot) com

using UnityEngine;
using System.Collections;

public class ArcBall : MonoBehaviour
{
	public float damping = 0.9f;
	public float speed = 0.1f;
	public float radius = 1.0f;
	private Vector3 vDown, startDrag, endDrag;
	private Vector3 vDrag;
	private bool dragging;
	private float angularVelocity;
	private Vector3 rotationAxis;
	private Vector3 lastMouse;
	private GameObject holdingObject;
	private Quaternion downR;
	private Quaternion currR;

	void Start ()
	{
		dragging = false;
		angularVelocity = 0;
		rotationAxis = Vector3.zero;
	}
	void OnEnable(){
		dragging = true;
		lastMouse = Input.mousePosition;
		holdingObject = GetComponentsInParent<HoldingObject>()[0].holdingObject;
		vDrag = vDown = Vector3.zero;
		startDrag = MapToSphere(Vector3.zero);
		downR = holdingObject.transform.rotation;
	}
	void OnDisable(){
		dragging = false;
	}
	void Update ()
	{	
		if(holdingObject == null)
			return;
		// extract vDrag from the RaycastHit
		startDrag = MapToSphere(Vector3.zero);
		vDrag = (new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0))*0.01f;
		endDrag = MapToSphere(vDrag);
		Debug.Log(endDrag);
		Vector3 axis = Vector3.Cross(startDrag, endDrag).normalized;
		float angle = Vector3.Angle(startDrag, endDrag);
		Quaternion dragR = Quaternion.AngleAxis(angle, axis);

		currR = dragR*downR;
		holdingObject.transform.rotation = currR;
		downR = holdingObject.transform.rotation;
	}
	private Vector3 MapToSphere(Vector3 position){
		Ray ray = Camera.main.ViewportPointToRay(position+new Vector3(0.5f, 0.5f, 0));

		Vector3 normal = (holdingObject.transform.position - Camera.main.transform.position).normalized;
		Plane plane = new Plane(normal, holdingObject.transform.position);
		float dist = 0;
		plane.Raycast(ray, out dist);
		Vector3 hitPoint = ray.GetPoint(dist);

		float length = Vector3.Distance(hitPoint, holdingObject.transform.position);
		if(length<radius){//on arcball
			float k = (float) (Mathf.Sqrt(radius - length));
			hitPoint -= normal*k;
		} else {//otside.

			Vector3 dir = (hitPoint - holdingObject.transform.position).normalized;
			hitPoint = holdingObject.transform.position + dir*radius;
		}

		return (hitPoint-holdingObject.transform.position).normalized;
	}


}

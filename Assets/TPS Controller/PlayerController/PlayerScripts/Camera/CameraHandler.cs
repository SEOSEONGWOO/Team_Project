using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour {

	public Transform camTrans;
	public Transform pivot;
	public Transform Character;
	public Transform mainTransform;

	public CameraSettings cameraSettings;
	public bool leftPivot;
	public float delta;

	public Transform targetLook;

	public float mouseX;
	public float mouseY;
	public float smoothX;
	public float smoothY;
	public float smoothXVelocity;
	public float smoothYVelocity;
	public float lookAngle;
	public float titlAngle;

	void Start()
	{
		transform.position = camTrans.position;
		transform.forward = targetLook.forward;
	}

	void Update()
	{
		Tick();

	}

	void Tick()
	{
		delta = Time.deltaTime;

		HandlePosition ();
		HandleRotation ();

		Vector3 targetPosition = Vector3.Lerp (mainTransform.position, Character.position, 1);
		mainTransform.position = targetPosition;
	}

	void TargetLook()
	{
		Ray ray = new Ray (camTrans.position, camTrans.forward * 2000);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit))
		{
			targetLook.position = Vector3.Lerp (targetLook.position, hit.point, Time.deltaTime * 40);
		}
		else
		{
			targetLook.position = Vector3.Lerp (targetLook.position, targetLook.forward * 200, Time.deltaTime * 5);
		}
	}

	void HandlePosition()
	{
		float targetX = cameraSettings.normalX;
		float targetY = cameraSettings.normalY;
		float targetZ = cameraSettings.normalZ;

		if (leftPivot) 
		{
			targetX = -targetX;
		}

		Vector3 newPivotPosition = pivot.localPosition;
		newPivotPosition.x = targetX;
		newPivotPosition.y = targetY;

		Vector3 newCameraPosition = camTrans.localPosition;
		newCameraPosition.z = targetZ;

		float t = delta * cameraSettings.pivotSpeed;
		pivot.localPosition = Vector3.Lerp (pivot.localPosition, newPivotPosition, t);
		camTrans.localPosition = Vector3.Lerp (camTrans.localPosition, newCameraPosition, t);
	}

	void HandleRotation()
	{
		mouseX = Input.GetAxis ("Mouse X");
		mouseY = Input.GetAxis ("Mouse Y");

		if (cameraSettings.turnSmooth > 0)
		{
			smoothX = Mathf.SmoothDamp (smoothX, mouseX, ref smoothXVelocity, cameraSettings.turnSmooth);
			smoothY = Mathf.SmoothDamp (smoothY, mouseY, ref smoothYVelocity, cameraSettings.turnSmooth);
		}
		else
		{
			smoothX = mouseX;
			smoothY = mouseY;
		}

		lookAngle += smoothX * cameraSettings.Y_rot_speed;
		Quaternion targetRot = Quaternion.Euler (0, lookAngle, 0);
		mainTransform.rotation = targetRot;

		titlAngle -= smoothY * cameraSettings.X_rot_speed;
		titlAngle = Mathf.Clamp (titlAngle, cameraSettings.minAngle, cameraSettings.maxAngle);
		pivot.localRotation = Quaternion.Euler (titlAngle, 0, 0);


	}
}

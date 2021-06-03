using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;
using Photon.Pun;
using Photon.Realtime;

public class Camera_test : MonoBehaviourPun
{

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

	public Transform tr;
	private Quaternion currRot;
	private Vector3 currPos;

	public static bool cc = true; //카메라 스크립트 제어
	void Awake()
	{
		ReStart();
	}


	void Update()
	{
        /*if (GameM.gameStart)
        {
			ReStart();
			GameM.gameStart = false;
		}*/
		if (photonView.IsMine)
		{
			 if (cc == true)
				if (PlayerCs.isShop)
					Tick();
		}
        else
        {
			return;
		}
		
	}
    void ReStart()
    {
		if (photonView.IsMine)
		{
			camTrans = GameObject.Find("Main Camera").GetComponent<Transform>();
			pivot = GameObject.Find("CameraPivot").GetComponent<Transform>();
			mainTransform = GameObject.Find("CameraHolder").GetComponent<Transform>();
			targetLook = GameObject.Find("TargetLook").GetComponent<Transform>();

			tr = GetComponent<Transform>();

			transform.position = camTrans.position;
			transform.forward = targetLook.forward;
		}
	}
	void Tick()
	{
		delta = Time.deltaTime;

		HandlePosition();
		HandleRotation();

		Vector3 targetPosition = Vector3.Lerp(mainTransform.position, Character.position, 1);
		mainTransform.position = targetPosition;
	}

	void TargetLook()
	{
		Ray ray = new Ray(camTrans.position, camTrans.forward * 2000);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit))
		{
			targetLook.position = Vector3.Lerp(targetLook.position, hit.point, Time.deltaTime * 40);
		}
		else
		{
			targetLook.position = Vector3.Lerp(targetLook.position, targetLook.forward * 200, Time.deltaTime * 5);
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
		pivot.localPosition = Vector3.Lerp(pivot.localPosition, newPivotPosition, t);
		camTrans.localPosition = Vector3.Lerp(camTrans.localPosition, newCameraPosition, t);
	}

	void HandleRotation()
	{
		mouseX = Input.GetAxis("Mouse X");
		mouseY = Input.GetAxis("Mouse Y");

		if (cameraSettings.turnSmooth > 0)
		{
			smoothX = Mathf.SmoothDamp(smoothX, mouseX, ref smoothXVelocity, cameraSettings.turnSmooth);
			smoothY = Mathf.SmoothDamp(smoothY, mouseY, ref smoothYVelocity, cameraSettings.turnSmooth);
		}
		else
		{
			smoothX = mouseX;
			smoothY = mouseY;
		}

		lookAngle += smoothX * cameraSettings.Y_rot_speed;
		Quaternion targetRot = Quaternion.Euler(0, lookAngle, 0);
		mainTransform.rotation = targetRot;

		titlAngle -= smoothY * cameraSettings.X_rot_speed;
		titlAngle = Mathf.Clamp(titlAngle, cameraSettings.minAngle, cameraSettings.maxAngle);
		pivot.localRotation = Quaternion.Euler(titlAngle, 0, 0);
	}
}

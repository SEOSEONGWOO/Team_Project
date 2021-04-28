using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;
using Photon.Pun;
using Photon.Realtime;

public class CameraCs : MonoBehaviourPun
{
	public float speed = 5.0f;
	public float rotSpeed = 120.0f;
	public Transform tr; 
	public PhotonView pv;

	private Quaternion currRot;
	private Vector3 currPos;

	void Start()
	{
		tr = GetComponent<Transform>();
		pv = GetComponent<PhotonView>();

		//동기화 콜백함수가 발생하려면 반드시 필요
		pv.ObservedComponents[0] = this;
		
	}

	void Update()
	{
		if (photonView.IsMine)
        {
			//자신의 플레이어만 키보드 조작
			float h = Input.GetAxis("Horizontal");
			float v = Input.GetAxis("Vertical");

			tr.Translate(Vector3.forward * v * Time.deltaTime * speed);
			tr.Rotate(Vector3.up * h * Time.deltaTime * rotSpeed);
        }
			
		else
		{
			//네트워크로 연결된 다른 유저일 경우에 실시간 전송 받는 변수를 이용해서 이동
			tr.position = Vector3.Lerp(tr.position, currPos, Time.deltaTime * 10.0f);
			tr.rotation = Quaternion.Lerp(tr.rotation, currRot, Time.deltaTime * 10.0f);
		}
	}
	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.IsWriting)
		{
			//자신의 플레이어 정보는 다른 네트워크 사용자에게 송신
			stream.SendNext(tr.position);
			stream.SendNext(tr.rotation);
		}
		else
		{
			//다른 플레이어의 정보는 수신
			currPos = (Vector3)stream.ReceiveNext();
			currRot = (Quaternion)stream.ReceiveNext();
		}
	}
	
}

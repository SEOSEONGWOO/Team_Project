using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming : MonoBehaviour {

	//Camera zoom control
	[Range(10,60)]
	public int zoom = 20; //Customizable zoom
	[Header("Scope texture")]
	public Texture2D crosshair;
	[Header("Sight size in height")]
	public int height = 40;
	[Header("Sight size in width")]
	public int width = 40;

	private int smoothZoom = 10; //Smooth zoom change
	private int normal = 60; //Normal field of view of the camera
	private float isZoomed = 0; //Aiming is enabled by default

	void Update()
	{
        if (Player_Scr.isShop)
        {
			if (Input.GetMouseButton(1)) //Right click on the right mouse button
			{
				isZoomed = 1;  //isZoomed becomes active
			}
			if (isZoomed == 1)
			{
				GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, zoom, Time.deltaTime * smoothZoom); //Ищим камеру на нашем объекте и меняем поле зрения
			}
			if (Input.GetMouseButtonUp(1))
			{

				isZoomed = 0;
			}
			if (isZoomed == 0)
			{
				GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, normal, Time.deltaTime * smoothZoom); //Если зуум не активен, то возвращаем значение поля зрения по умолчанию
			}
		}
		
	}
}

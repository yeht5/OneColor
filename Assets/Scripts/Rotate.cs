using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour  
{  
	Vector3 StartPosition;  
	Vector3 previousPosition;  
	Vector3 offset;  
	Vector3 finalOffset;  
	Vector3 eulerAngle;  

	bool isSlide;  
	float angle;  

	public float scale = 1;  

	RaycastHit hit;  

	void Start()  
	{  

	}  

	void OnGUI()  
	{  

		Ray ray =Camera.main.ScreenPointToRay(Input.mousePosition);  
		if(Physics.Raycast(ray, out hit))  
		{  
			if (hit.transform == transform)  
			{  
				if (Input.GetAxis("Mouse ScrollWheel") != 0)  
				{  
					Debug.Log(Input.GetAxis("Mouse ScrollWheel"));  
					scale = scale + Input.GetAxis("Mouse ScrollWheel");  
					transform.localScale = new Vector3(1 + scale, 1 + scale, 1 + scale);  
				}  
			}  
		}  
	}  

	void Update()  
	{  
		if (Input.GetMouseButtonDown(0))  
		{  
			StartPosition = Input.mousePosition;  
			previousPosition = Input.mousePosition;  
		}  
		if (Input.GetMouseButton(0))  
		{  
			offset = Input.mousePosition - previousPosition;  
			previousPosition = Input.mousePosition;  
			transform.Rotate(Vector3.Cross(offset, Vector3.forward).normalized, offset.magnitude, Space.World);  

		}  
		if (Input.GetMouseButtonUp(0))  
		{  
			finalOffset = Input.mousePosition - StartPosition;  
			isSlide = true;  
			angle = finalOffset.magnitude;  


		}  
		if (isSlide)  
		{  
			transform.Rotate(Vector3.Cross(finalOffset, Vector3.forward).normalized, angle * 2 * Time.deltaTime, Space.World);  
			if (angle > 0)  
			{  
				angle -= 5;  
			}  
			else  
			{  
				angle = 0;  
			}  
		}  
	}  
}  
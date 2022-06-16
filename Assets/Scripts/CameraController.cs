using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Controls the movement of camera */
public class CameraController : MonoBehaviour
{
	void LateUpdate()
	{
		if (PlayerManager.Instance != null && !GameManager.Instance.LevelFinished)
		{
			transform.position += Vector3.forward * PlayerManager.Instance.VerticalSpeed * Time.deltaTime;
		}
	}
}

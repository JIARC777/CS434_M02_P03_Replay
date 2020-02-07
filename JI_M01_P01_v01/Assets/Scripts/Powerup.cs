using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
	// References to the two materials the obstacles will have
	public Material illuminateMat;
	public Material stdMat;
	// Reference for all of the obstacles
	private GameObject[] obstacles;
	private void OnTriggerEnter(Collider other)
	{
		StartCoroutine(ResetMaterial());
	}

	// Run as a couroutine so timing can be applied between setting and resetting the material
	IEnumerator ResetMaterial()
	{
		// find all objects tagged as an obstacle
		obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
		foreach (GameObject obstacle in obstacles)
		{
			// For each obstacle found replace the material
			obstacle.GetComponent<MeshRenderer>().material = illuminateMat;
		}
		// wait for five seconds and reset material back 
		yield return new WaitForSeconds(5);
		foreach (GameObject obstacle in obstacles)
		{
			obstacle.GetComponent<MeshRenderer>().material = stdMat;
		}
	}
}


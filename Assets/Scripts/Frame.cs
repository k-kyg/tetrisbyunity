using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frame : MonoBehaviour
{
	public GameObject tetriMino;
	// Start is called before the first frame update
	void Start()
	{
		tetriMino = GameObject.Find("TetriMino");
	}

	// Update is called once per frame
	void Update()
	{

	}
	private void OnTriggerEnter(Collider other)
	{
		Debug.Log($"frame: {other.gameObject.transform.name}");
		tetriMino.GetComponent<TetrisScript>().SetFrameName(other.gameObject.transform.name);
		tetriMino.GetComponent<TetrisScript>().framenames.Add(other.gameObject.transform.name);
		if (other.gameObject.transform.name == "bottomframe")
		{
			Destroy(this);
		}
		if (other.gameObject.transform.name != "leftframe" || other.gameObject.transform.name != "rightframe" || other.gameObject.transform.name != "")
			tetriMino.GetComponent<TetrisScript>().isColliding = true;
		// tetriMino.GetComponent<TetrisScript>().SetBefore(this.gameObject.transform.position);
	}
	// public Vector3 GetBefore() => this.tetriMino.transform.position;
	private void OnTriggerExit(Collider other)
	{
		if (tetriMino.GetComponent<TetrisScript>().GetFrameName() != "bottomframe") tetriMino.GetComponent<TetrisScript>().SetFrameName("");
		tetriMino.GetComponent<TetrisScript>().isColliding = false;
	}
}

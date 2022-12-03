using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NodeGr
{
	public List<Edge> edgelist = new List<Edge>();
	public NodeGr path = null;
	public GameObject id;
	public float xPos;
	public float yPos;
	public float zPos;
	public float f, g, h;
	public NodeGr cameFrom;
	
	public NodeGr(GameObject i)
	{
		id = i;
		xPos = i.transform.position.x;
		yPos = i.transform.position.y;
		zPos = i.transform.position.z;
		path = null;
	}
	
	public GameObject getId()
	{
		return id;	
	}

}

using UnityEngine;
using UnityEngine.Rendering;
using System.Collections;
using System.Collections.Generic;

public class RoachManager : MonoBehaviour {
	/*
	Manager script handles  the Instantiation of them , and passes in values for the limits of the walls
		* 		-Instantiate at a ramdom point int he bounds of the wall.
		* 		-Call a function to tell it where the center of the wall is, or where it should be looking at in the beginning
		* 		-Call a function to toggle whether it should be moving randomly, or if it should be heading towards the center point
			* 			(for the first call, it will be moving ranndomly)
		* 		-Start the timer for the amount of time they should be moving randomly
			* 		-When that time is up, call the function to tell them to return to the center point
	*/

	Mesh planeMesh;
	Bounds planeBounds;
	GameObject gatheringPoint;
	GameObject roachPrefabReference;
	List<GameObject> roachList;
	public int totalRoaches = 15;
	float boundUp;
	float boundDown;
	float boundLeft;
	float boundRight;
	float roachYSpawn;
	float roachZSpawn;
	float roachRotationy;
	float roachRotationx;
	float roachRotationz;

	string roachPrefabPath = "RoachPrefab/Roach";


	// Use this for initialization
	void Start () {
		roachList = new List<GameObject> ();
		planeMesh = gameObject.GetComponent<MeshFilter> ().mesh;
		planeBounds = planeMesh.bounds;
		boundRight = planeBounds.extents.x * transform.localScale.x + transform.position.x;
		boundLeft = planeBounds.extents.x * transform.localScale.x * -1f + transform.position.x;
		boundUp = planeBounds.extents.z * transform.localScale.z + transform.position.z;
		boundDown = planeBounds.extents.z * transform.localScale.z * -1f + transform.position.z;
		gatheringPoint = transform.FindChild ("GatheringPoint").gameObject;
		roachYSpawn = gatheringPoint.transform.position.y;
		roachZSpawn = gatheringPoint.transform.position.z;
		roachRotationx = gatheringPoint.transform.rotation.x;
		roachRotationy = gatheringPoint.transform.rotation.y;
		roachRotationz = gatheringPoint.transform.rotation.z;
		LoadRoachPrefab ();
		InstantiateRoaches ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void InstantiateRoaches()
	{
		if(roachPrefabReference != null)
		{
			GameObject roachInstantiated;
			Vector3 tempPosition = new Vector3 ();
			float tempXCoordinate;
			float tempYCoordinate;
			float tempZCoordinate;
			int tempEdge;
			for(int i = 0; i < totalRoaches; i++)
			{
				//instantiate roach
				//see which edge it will spawn on
				tempEdge = Random.Range(0, 4);
				switch (tempEdge)
				{
				case 0:
					//right
					tempXCoordinate = boundRight;
					tempZCoordinate = Random.Range (boundDown, boundUp);
					break;
				case 1:
					//left
					tempXCoordinate = boundLeft;
					tempZCoordinate = Random.Range (boundDown, boundUp);
					break;
				case 2:
					//up
					tempXCoordinate = Random.Range (boundLeft, boundRight);
					tempZCoordinate = boundUp;
					break;
				case 3:
					//down
					tempXCoordinate = Random.Range (boundLeft, boundRight);
					tempZCoordinate = boundDown;
					break;
				default:
					//down
					tempXCoordinate = Random.Range (boundLeft, boundRight);
					tempZCoordinate = boundDown;
					break;
				}
				tempPosition.x = tempXCoordinate;
				tempPosition.y = roachYSpawn;
				tempPosition.z = tempZCoordinate;
				roachInstantiated = Instantiate(roachPrefabReference, tempPosition, Quaternion.Euler(roachRotationx, roachRotationy, roachRotationz)) as GameObject;
				roachInstantiated.transform.SetParent (transform);
				roachInstantiated.GetComponent<RoachScript> ().SetGatheringPoint (gatheringPoint.transform.position);
				roachInstantiated.GetComponent<RoachScript> ().SetBounds (boundRight, boundLeft, boundUp, boundDown);
				roachList.Add (roachInstantiated);
			}
		}
	}

	void LoadRoachPrefab()
	{
		roachPrefabReference = Resources.Load (roachPrefabPath) as GameObject;
	}
}

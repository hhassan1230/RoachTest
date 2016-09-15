using UnityEngine;
using System.Collections;

public class RoachScript : MonoBehaviour {

	public float speed = .5f;
	private float diffcultyIncrement = 2f;
	float maxDifficulty = 10f;
	private float randomFloat;
	private float startYTransform;
	float nextRotationTime;
	float rotationTimeSum;
	Vector3 gatheringPoint;
	float boundRight;
	float boundLeft; 
	float boundDown; 
	float boundUp;
	public float gatheringPointRotationOffset = 30f;
	bool returningToGatheringPoint;
	Vector3 tempPosition;



	// Use this for initialization
	void Start () {
		returningToGatheringPoint = false;
		startYTransform = this.transform.position.y;
		SetNextRotationInterval ();
		Invoke ("HeadToGatheringPoint", 10f);
	}

	void FixedUpdate () {
		rotationTimeSum += Time.deltaTime;
		if(rotationTimeSum >= nextRotationTime)
		{
			if(returningToGatheringPoint == true)
			{
				RotateRandomDirection (true);
			}
			else
			{
				RotateRandomDirection (false);
			}
			SetNextRotationInterval ();
		}

		/*
		tempPosition = transform.localPosition;
		tempPosition.z += speed * Time.deltaTime;
		transform.localPosition = tempPosition;
		*/

		transform.Translate (Vector3.forward * speed * Time.deltaTime);
		if(transform.position.x < boundRight && transform.position.x > boundLeft && transform.position.z > boundDown && transform.position.z < boundUp)
		{
			
		}
		else
		{
			RotateRandomDirection (true);
			SetNextRotationInterval ();
		}
	}

	public void SetBounds(float newRight, float newLeft, float newUp, float newDown)
	{
		boundRight = newRight;
		boundLeft = newLeft;
		boundUp = newUp;
		boundDown = newDown;
	}

	public void SetGatheringPoint(Vector3 newPoint)
	{
		gatheringPoint = newPoint;
		transform.LookAt (gatheringPoint);
	}

	void IncreaseSpeed() {
		if (speed < maxDifficulty) {
			speed += diffcultyIncrement;
		}
	}

	void RotateRandomDirection(bool newOutOfBounds = false)
	{
		if(newOutOfBounds == true)
		{
			transform.LookAt (gatheringPoint);
			randomFloat = Random.Range (gatheringPointRotationOffset * -1f, gatheringPointRotationOffset);
		}
		else
		{
			randomFloat = Random.Range (0f, 360f);
		}
		transform.Rotate ( 0, randomFloat, 0);
		/*
		Quaternion tempRotationQuat = transform.localRotation;
		Vector3 tempRotation = tempRotationQuat.eulerAngles;
		tempRotation.z = randomFloat;
		tempRotationQuat.eulerAngles = tempRotation;
		transform.localRotation = tempRotationQuat;
		*/
	}

	void SetNextRotationInterval()
	{
		rotationTimeSum = 0f;
		nextRotationTime = Random.Range (0f, 2f);
	}

	void HeadToGatheringPoint()
	{
		returningToGatheringPoint = true;
	}
}

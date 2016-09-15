using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class RandomMovement : MonoBehaviour {
	public float speed = 3f;
	private float diffculty = 0;
	private float randomFloat;
	private float startYTransform;
	private Vector3 fwd;
	private RaycastHit hit;
	// Use this for initialization
	void Start () {
		startYTransform = this.transform.position.y;
		if (this.gameObject.tag != "Roach") {
			InvokeRepeating("IncreaseDifficulty", 3f, 3f);
		}
	}

	void IncreaseDifficulty() {
		if (speed < 12f) {
			speed += (1f * diffculty);
			diffculty++;
			print (speed);
		}


	}
	// Update is called once per frame
	void FixedUpdate () {
		//Debug.Log (gameObject.name + "Before Translate command: " + transform.position.y);
		transform.Translate (new Vector3 (0, 0, 2) * speed * Time.deltaTime);
		//Debug.Log ("*********");
		/*
		randomFloat = Random.Range (0f, 360f);
		Debug.Log (gameObject.name + ": " + randomFloat);
		fwd = transform.TransformDirection (Vector3.forward);
		Debug.DrawRay (transform.position, fwd * 1, Color.red);

		if (Physics.Raycast(transform.position,fwd, out hit, 1f)) {
			if (hit.collider.tag == "Wall") {
				transform.Rotate (0, randomFloat, 0);
			} 
			if (hit.collider.tag == "Player") {
				Scene scene = SceneManager.GetActiveScene();
				SceneManager.LoadScene (scene.name);
			}
		}
		*/
	}

	void OnCollisionEnter()
	{
		randomFloat = Random.Range (0f, 360f);
		Debug.Log (gameObject.name + ": " + randomFloat);
		Quaternion tempRotationQuat = transform.localRotation;
		Vector3 tempRotation = tempRotationQuat.eulerAngles;
		//transform.Rotate (0, randomFloat, 0);
		tempRotation.y = randomFloat;
		tempRotationQuat.eulerAngles = tempRotation;
		transform.localRotation = tempRotationQuat;
	}
}


/*
 * Have roaches start at random points at edges of the wall.
 * They will walk forward at an angle that is within 30 degrees + or - of looking towards the center point
 * After between 1 and 3 seconds, they willpick a random direction to turn, and walk for 1 to 3 seconds, before repeating.
 * If they hit the boundaries of the wall, then they will will pick a new direction based mathematicall on their current angle 
 * 		at the time of the hit.
 * After about 10 or 15 seconds, they will go back to turning in a limit of an angle towards the center, + or - 10 degrees,
 * while changing within that range every 1 to 3 seconds, until it reaches a collider in the center.
 * 
 * 
 * 
 * Manager script handles  the Instantiation of them , and passes in values for the limits of the walls
 * 		-Instantiate at a ramdomm point int he bounds of the wall.
 * 		-Call a function to tell it where the center of the wall is, or where it should be looking at in the beginning
 * 		-Call a function to toggle whether it should be moving randomly, or if it should be heading towards the center point
 * 			(for the first call, it will be moving ranndomly)
 * 		-Start the timer for the amount of time they should be moving randomly
 * 		-When that time is up, call the function to tell them to return to the center point
 * 
 * Individual roach script handles its own motion, and also whathever animation or effect it has during the transformation to a spider
 * 		-A function that determines a random rotation at pre-specified intervals
 * 			-This function must take an optional paramterr, which will serve as a guide for where to go
 * 		-During its update function, it must check whether it is out of bounds
 */
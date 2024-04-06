using UnityEngine;
using System.Collections;

// basic WASD-style movement control
// commented out line demonstrates that transform.Translate instead of 
// charController.Move doesn't have collision detection

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]
public class FPSInput : MonoBehaviour {
	public float speed = 6.0f;
	public float gravity = -9.8f;

	private CharacterController _charController;
	private PauseManager _pauseManager;
	
	void Start()
    {
		_charController = GetComponent<CharacterController>();
        _pauseManager = FindAnyObjectByType<PauseManager>();
    }
	
	void Update()
    {
		//transform.Translate(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0, Input.GetAxis("Vertical") * speed * Time.deltaTime);
		//float deltaX = Input.GetAxis("Horizontal") * speed;
		float deltaZ = Input.GetAxis("Vertical") * speed;
		Vector3 movement = new Vector3(0, 0, deltaZ);
		movement = Vector3.ClampMagnitude(movement, speed);

		movement.y = gravity;

		movement *= Time.deltaTime;
        // Transforms movement from local space to world space.

        float rot = Input.GetAxis("Horizontal") * 0.5f;
        if (_pauseManager.isPaused) {
            movement = new Vector3(0, 0, 0);
			rot = 0f;
        }
        movement = transform.TransformDirection(movement);
		_charController.Move(movement);

        transform.Rotate(0, rot, 0);
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BallShooting : MonoBehaviour 
{
    public GameObject Ball;
    public float Force = 50.0f;
    public Vector3 Torque = new Vector3(100, 0, 0);
    public float shotgunCooldown;
    private Gun _gun;

    private int shootMode = 0;
    private float timeSinceShot = 0;

	void Start() 
    {

        _gun = GameObject.Find("Gun").GetComponent<Gun>();
    }

	void Update() 
    {

        //check if the gun is ready to fire
        if (_gun.ReadyToFire())
        {
            if (Input.GetMouseButtonDown(0))
            {
                //call Bang method to perform gun animation and sounds

                // Note: transform.position returns object's position in the World space
                if (shootMode == 0) {
                    GameObject ball = (GameObject)Instantiate(Ball, transform.position,
                        Quaternion.identity);
                    Rigidbody ball_rb = ball.GetComponent<Rigidbody>();
                    ball.name = "ball";
                    // Fire the ball 2 unit forward from the camera
                    //ball.transform.position = transform.TransformPoint(Vector3.forward + Vector3.up * 0.5f);
                    //ball_rb.velocity = transform.TransformDirection(new Vector3(0, 0, Force));

                    ball.transform.position = _gun.transform.TransformPoint(Vector3.forward);
                    Vector3 aim = transform.forward * 10f;

                    ball_rb.AddForce(transform.TransformDirection(new Vector3(0, 0, Force) - aim));
                    ball_rb.AddTorque(Torque);
                    _gun.Bang();
                    timeSinceShot = 0;
                }
                else if (shootMode == 1 && timeSinceShot > shotgunCooldown) {
                    for (int i = 0; i < 5; i++) {
                        GameObject ball = (GameObject)Instantiate(Ball, transform.position,
                            Quaternion.identity);
                        Rigidbody ball_rb = ball.GetComponent<Rigidbody>();
                        ball.name = "ball";
                        // Fire the ball 2 unit forward from the camera
                        ball.transform.position = transform.TransformPoint(Vector3.forward * 2f + Vector3.up);
                        //ball_rb.velocity = transform.TransformDirection(new Vector3(0, 0, Force));
                        ball_rb.AddForce(transform.TransformDirection(new Vector3(Random.Range(-0.2f,0.2f), 0, Force)));
                        //ball_rb.AddTorque(Torque);
                    }
                    _gun.Bang();
                    timeSinceShot = 0;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            shootMode = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            shootMode = 1;
        }

        timeSinceShot += Time.deltaTime;
	}

}
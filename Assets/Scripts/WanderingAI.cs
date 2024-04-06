using UnityEngine;
using System.Collections;

public class WanderingAI : MonoBehaviour 
{
	public float speed = 3.0f;  // Wandering forward speed
	public float obstacleRange = 2.0f;

    public float Force = 50.0f;
    public Vector3 Torque = new Vector3(100, 0, 0);

    [SerializeField] private GameObject fireballPrefab;
    private GameObject _fireball;

    private bool _alive;
    private PauseManager _pauseManager;

    void Start()
    {
        _pauseManager = FindAnyObjectByType<PauseManager>();
        _alive = true;
        transform.GetComponent<Rigidbody>().maxLinearVelocity = 2f;
    }

    void Update() 
    {
        if (_pauseManager.isPaused) {
            return;
        }
        if (!_alive) return; // this enemy may die before this enemy game object is destroyed

        Ray ray = new Ray(transform.position, transform.forward);
        Ray floorRay = new Ray(transform.position - transform.up, transform.forward);
		RaycastHit hit;
        bool floorhit = Physics.Raycast(transform.position + (transform.forward * 2), -transform.up, 1.1f);
        if (Physics.SphereCast(ray, 0.75f, out hit))
        {
            GameObject hitObject = hit.transform.gameObject;

            if (hitObject.tag == "Player")
            {
                if (_fireball == null)
                {
                    _fireball = Instantiate(fireballPrefab);
                    _fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
                    _fireball.transform.rotation = transform.rotation;
                    _fireball.GetComponent<Rigidbody>().velocity =
                                transform.TransformDirection(new Vector3(0, 0, Force));
                    _fireball.GetComponent<Rigidbody>().AddTorque(Torque);
                }
            }
            else if (hit.distance < obstacleRange && hitObject.tag != "floor")// && hitObject.tag != "Fire")
            {
                float angle = Random.Range(-110, 110);
                transform.Rotate(0, angle, 0);
                transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }
        if (!floorhit) {
            float angle = Random.Range(-110, 110);
            transform.Rotate(0, angle, 0);
            transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        Debug.DrawRay(transform.position + transform.forward * 2, -transform.up * 1.1f, Color.blue);
        //transform.Translate(0, 0, speed * Time.deltaTime);
        transform.GetComponent<Rigidbody>().AddForce(transform.forward * Time.deltaTime * 400);
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.collider.tag == "respawn") {
            transform.position = new Vector3(Random.Range(-10, 10), 1, Random.Range(-10, 10));
        }
    }

    public void SetAlive(bool alive)
    {
        _alive = alive;
    }
}

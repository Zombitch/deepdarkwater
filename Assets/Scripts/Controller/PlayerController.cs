using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    private static ILogger logger = Debug.unityLogger;

    public PlayerInput inputs;
    public Rigidbody rigidBody;
    public bool invertYAxis = false;
    public float moveSpeed = 150f;
    public float lookSpeed = 150f;
    public float intertie = 2f;

    public Camera camera;

    private Vector3 move = Vector3.zero;
    private Vector3 look = Vector3.zero;
    private Vector3 direction = Vector3.zero;
    private float lastTimeMovement = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.look != Vector3.zero){
            this.transform.Rotate(this.look * this.lookSpeed * Time.deltaTime);
            this.transform.localEulerAngles = new Vector3(this.transform.localEulerAngles.x, this.transform.localEulerAngles.y, 0);
        }
    }

    void FixedUpdate(){
        float higherVelocityValue = Mathf.Max(Mathf.Abs(this.rigidBody.velocity.z), Mathf.Max(Mathf.Abs(this.rigidBody.velocity.x), Mathf.Abs(this.rigidBody.velocity.y)));

        if(this.move != Vector3.zero){
            if(this.move.z != 0) this.direction = this.transform.forward * this.move.z * this.moveSpeed * Time.fixedDeltaTime;
            else if(this.move.x != 0) this.direction = this.transform.right * this.move.x * this.moveSpeed * Time.fixedDeltaTime;

            this.rigidBody.velocity = this.direction;
        }else if(Time.time - this.lastTimeMovement > 1f){
            if(higherVelocityValue > 1.5f){
                this.rigidBody.velocity = this.rigidBody.velocity / this.intertie;
                this.lastTimeMovement = Time.time;
            }else{
                this.rigidBody.velocity = Vector3.zero;
            }
        }
    }

    public void OnMove(InputAction.CallbackContext value){
        Vector2 move2D = value.ReadValue<Vector2>();
        this.move = new Vector3(move2D.x, 0, move2D.y);

        if(this.move == Vector3.zero){
            this.rigidBody.velocity = Vector3.zero;
            this.rigidBody.AddForce(this.direction * 20);
            this.lastTimeMovement = Time.time;
        }
    }

    public void OnLook(InputAction.CallbackContext value){
        Vector2 look2D = value.ReadValue<Vector2>();

        if(this.invertYAxis) this.look = new Vector3(look2D.y, look2D.x, 0);
        else this.look = new Vector3(look2D.y*-1, look2D.x, 0);
    }
}

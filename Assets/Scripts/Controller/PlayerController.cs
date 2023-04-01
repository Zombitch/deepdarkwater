using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    private static ILogger logger = Debug.unityLogger;

    public PlayerInput inputs;
    public bool invertYAxis = false;
    public float moveSpeed = 150f;
    public float lookSpeed = 150f;

    public Camera camera;

    private Vector3 move = Vector3.zero;
    private Vector3 look = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.move != Vector3.zero){
            this.transform.Translate(this.move * this.moveSpeed * Time.deltaTime);
        }
        if(this.look != Vector3.zero){
            this.transform.Rotate(this.look * this.lookSpeed * Time.deltaTime);
            this.transform.localEulerAngles = new Vector3(this.transform.localEulerAngles.x, this.transform.localEulerAngles.y, 0);
        }
    }

    public void OnMove(InputAction.CallbackContext value){
        Vector2 move2D = value.ReadValue<Vector2>();
        this.move = new Vector3(move2D.x, 0, move2D.y);
    }

    public void OnLook(InputAction.CallbackContext value){
        Vector2 look2D = value.ReadValue<Vector2>();

        if(this.invertYAxis) this.look = new Vector3(look2D.y, look2D.x, 0);
        else this.look = new Vector3(look2D.y*-1, look2D.x, 0);
    }
}

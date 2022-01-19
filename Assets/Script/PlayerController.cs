using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerController : NetworkBehaviour
{
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private Transform player;
    [SerializeField]
    private float speed;
    [SerializeField]
    private bool canJump;
    [SerializeField]
    private float jumpHeight;
    [SerializeField]
    private float horizontalSensitivity;
    [SerializeField]
    private float verticalSensitivity;
    [SerializeField]
    private Transform _camera;
    [SerializeField]
    private Rigidbody rb;

    private float camY;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            Destroy(cam);
            return;
        }
        CharacterMove();
        VisualControl();
    }

    public void CharacterMove()
    {

        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        Vector3 MoveVector = transform.TransformDirection(input) * speed;
        rb.MovePosition(rb.position + MoveVector * Time.deltaTime);
    }

    public void VisualControl()
    {
        float x = horizontalSensitivity * Input.GetAxisRaw("Mouse X");
        float y =+ verticalSensitivity * Input.GetAxisRaw("Mouse Y");

        camY -= y;
        camY = Mathf.Clamp(camY, -90f, 90f);

        _camera.transform.localRotation = Quaternion.Euler(camY, 0f, 0f);
        player.Rotate(0, x, 0);
    }
}

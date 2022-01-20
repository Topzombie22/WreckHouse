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
    [SerializeField]
    private float jumpRaycast;

    [SerializeField]
    private LayerMask ground;

    private bool canRaycast;

    private float camY;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
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

        if (Input.GetKeyDown(KeyCode.Space) && canJump == true)
        {
            Jump();
        }

    }

    private void LateUpdate()
    {
        if (!isLocalPlayer)
            return;

        if (Physics.Raycast(this.transform.position, Vector3.down, jumpRaycast, ground))
        {
            canJump = true;
            Debug.DrawRay(this.transform.position, Vector3.down, Color.green, 2.0f);
        }
        else { canJump = false; }
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

    [Command(requiresAuthority = false)]
    public void Jump()
    {
        canJump = false;
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
    }
}

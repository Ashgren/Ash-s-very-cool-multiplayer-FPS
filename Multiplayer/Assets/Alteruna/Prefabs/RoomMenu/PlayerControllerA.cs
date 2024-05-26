using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerA : MonoBehaviour
{
    [Header("Base setup")]
    public float walkingSpeed = 5f;
    public float runningSpeed = 7.5f;
    public float jumpSpeed = 8f;
    public float gravity = 20f;
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;
    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float RotationX = 0f;
    float RotationY = 0f;
    [HideInInspector]
    public bool canMove = true;
    [SerializeField]
    private float cameraYoffset = 0.4f;
    private Alteruna.Avatar _avatar;
    private Camera playerCamera;

    // Start is called before the first frame update
    void Start()
    {
        _avatar = GetComponent<Alteruna.Avatar>();
        if (!_avatar.IsMe)
        {
            return;
        }
        characterController = GetComponent<CharacterController>();
        playerCamera = Camera.main;
        playerCamera.transform.position = new Vector3(transform.position.x, transform.position.y + cameraYoffset, transform.position.z);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_avatar.IsMe) return;
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove && playerCamera != null)
        {
            RotationY += -Input.GetAxis("Mouse Y") * lookSpeed;
            RotationY = Mathf.Clamp(RotationY, -lookXLimit, lookXLimit);

            playerCamera.transform.localRotation = Quaternion.Euler(RotationY, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }
}

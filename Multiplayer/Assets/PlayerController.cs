using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Base setup")]
    public float walkingSpeed;
    public float runningSpeed;
    public float jumpSpeed;
    public float gravity = 20.0f;
    public float sensitivity;
    public float lookXLimit = 45.0f;
    public int health = 100;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    [HideInInspector]
    public bool canMove = true;

    [SerializeField]
    private float cameraYOffset = 0.4f;
    private Camera playerCamera;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerCamera = Camera.main;
        playerCamera.transform.position = new Vector3(transform.position.x, transform.position.y + cameraYOffset, transform.position.z);
        playerCamera.transform.SetParent(transform);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        bool isRunning = Input.GetKey(KeyCode.LeftControl);

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 forward = transform.forward * moveVertical;
        Vector3 right = transform.right * moveHorizontal;

        float curSpeed = isRunning ? runningSpeed : walkingSpeed;

        if (characterController.isGrounded)
        {
            moveDirection = (forward + right).normalized * curSpeed;
            if (Input.GetButton("Jump") && canMove)
            {
                moveDirection.y = jumpSpeed;
            }
            else
            {
                moveDirection.y = 0f;
            }
        }

        moveDirection.y -= gravity * Time.deltaTime;

        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove && playerCamera != null)
        {
            rotationX += -Input.GetAxis("Mouse Y") * sensitivity;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * sensitivity, 0);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "jumpboost")
        {
            Destroy(hit.gameObject);
            Debug.Log("Collision detected with: " + hit.gameObject.name);
            jumpSpeed *= 1.5f;
        }

        if (hit.gameObject.tag == "speedboost")
        {
            Destroy(hit.gameObject);
            Debug.Log("Collision detected with: " + hit.gameObject.name);
            walkingSpeed *= 1.5f;
            runningSpeed *= 1.5f;
        }

        if (hit.gameObject.tag == "damage")
        {
            health--;
            if (health <= 0)
            {
                Debug.Log("Working!");
                SceneManager.LoadScene(0);
            }
        }
    }
}

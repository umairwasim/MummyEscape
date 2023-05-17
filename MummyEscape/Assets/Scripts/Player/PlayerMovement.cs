using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private FixedJoystick joystick;
    [SerializeField] private AnimatorController animatorController;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;

    private Rigidbody playerRB;
    private Vector3 moveVector;

    private void Awake()
    {
        playerRB = GetComponent<Rigidbody>();

        if (animatorController == null)
            animatorController = GetComponent<AnimatorController>();
    }

    private void Update()
    {

        if (GameManager.Instance.GameState != GameState.StartGamePlay)
            return;

        Movement();
        Rotate();

    }

    private void Movement()
    {
        //Get joystick input
        moveVector = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        moveVector *= moveSpeed * Time.deltaTime;

        playerRB.MovePosition(playerRB.position + moveVector);
    }

    private void Rotate()
    {
        //If interacting with joystick
        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            Vector3 direction = Vector3.RotateTowards(transform.forward, moveVector, rotateSpeed * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(direction);

            animatorController.PlayRun();
        }

        //if not interacting with joystick, play idle animation 
        else if (joystick.Horizontal == 0 && joystick.Vertical == 0)
        {
            animatorController.PlayIdle();
        }
    }
}

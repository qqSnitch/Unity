using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;
    public float walk = 10f;
    public float gravity = -30f;
    public float jumpHeight = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        controller=GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;
    }
    //�������� ������� ������ �� InputManager.cs � ��������� �� � ���������
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection=Vector3.zero;
        moveDirection.x=input.x;
        moveDirection.z=input.y;
        controller.Move(transform.TransformDirection(moveDirection)*walk*Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }
        controller.Move(playerVelocity*Time.deltaTime);
    }
    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y=Mathf.Sqrt(jumpHeight*-3.0f*gravity);
        }
    }
}

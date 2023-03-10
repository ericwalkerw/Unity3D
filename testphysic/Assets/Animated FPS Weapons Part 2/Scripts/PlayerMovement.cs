using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController playerControl;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
	[SerializeField] private float crouchSpeed;
	[SerializeField] private Camera cam;
	[SerializeField] private float jumpSpeed = 10.0f;
	[SerializeField] private float gravity = 50.0f;
	public bool isCrouching = false;
	public bool Jumping = false;
	public bool isZooming = false;
	[SerializeField] private AudioClip[] concreteSurface;
	[SerializeField] private AudioClip[] metalSurface;
	[SerializeField] private AudioClip jumpSound;
	[SerializeField] private AudioClip crouchSound;
	private AudioSource mainSound;
	private Vector3 moveDirection = Vector3.zero;
	private Vector3 cameraStartPos;
	private Vector3 cameraEndPos;
	private float cameraMaxPosY = -2f;
	private float lerpTime = 0.2f;
	private float currentLerpTime1;
	private float currentLerpTime2;

	void Start()
    {
        playerControl = GetComponent<CharacterController>();
		cameraStartPos = cam.transform.localPosition;
		cameraEndPos = cam.transform.localPosition + Vector3.up * cameraMaxPosY;
		mainSound = GetComponent<AudioSource>();
	}

    void Update()
    {
        Movement();
		Jump();
		Crouch();
		}

    void Movement()
    {
        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
		if (!isCrouching)
		{	
        Vector3 move = transform.right * horiz + transform.forward * vert;
		playerControl.Move(move * walkSpeed * Time.deltaTime);
		}
		if (isCrouching)
		{
       Vector3 move = transform.right * horiz + transform.forward * vert;
		playerControl.Move(move * crouchSpeed * Time.deltaTime);	
		}
		if (Input.GetButton("Horizontal") && Input.GetKey(KeyCode.LeftShift) && !isCrouching && !Jumping && !isZooming
	   || Input.GetButton("Vertical") && Input.GetKey(KeyCode.LeftShift) && !isCrouching && !Jumping && !isZooming)
		{
	    Vector3 move = transform.right * horiz + transform.forward * vert;
		playerControl.Move(move * runSpeed * Time.deltaTime);
		}
		if (Input.GetMouseButtonDown(1))
		{
			isZooming = true;
		}
		else if (Input.GetMouseButtonUp(1))
		{
			isZooming = false;
		}
    }
		void Jump()
		{
		if (Input.GetButtonDown("Jump") && !isCrouching && !isZooming)
		{
			Jumping = true;
		}
		else if (Input.GetButtonUp("Jump"))
		{
			Jumping = false;
		}
		if (playerControl.isGrounded) {
            moveDirection = new Vector3(0, 0, 0);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= jumpSpeed;
            if (Jumping)
             moveDirection.y = jumpSpeed*2f;

        }
        moveDirection.y -= gravity * Time.deltaTime;
        playerControl.Move(moveDirection * Time.deltaTime);
		
    }
	
	void Crouch()
	{
	float Perc1 = currentLerpTime1/lerpTime;
    float Perc2 = currentLerpTime2/lerpTime;	
    if (Input.GetKeyDown(KeyCode.C))
	{
		if (isCrouching)
		{
			isCrouching = false;
		}
		else
		{
			isCrouching = true;
		}
	}
	if (isCrouching && cam.transform.localPosition.y > -2f)
	{
		currentLerpTime2 = 0;
		currentLerpTime1 += Time.deltaTime;
		cam.transform.localPosition = Vector3.Lerp(cameraStartPos, cameraEndPos, Perc1);
	}
	
	if (!isCrouching && cam.transform.localPosition.y < 0f)
	
	{
		currentLerpTime1 = 0;
		currentLerpTime2 += Time.deltaTime;
		cam.transform.localPosition = Vector3.Lerp(cameraEndPos,cameraStartPos, Perc2);
	}
   }
   
    public void Footsteps()
     {
		 RaycastHit hit = new RaycastHit();
		 string floortag;
		if(playerControl.isGrounded)
		{
		if(Physics.Raycast(transform.position, Vector3.down,out hit ))
        {
		floortag = hit.collider.gameObject.tag;
		if (floortag == "Concrete")
		{
         mainSound.clip = concreteSurface[Random.Range(0, concreteSurface.Length)];
		 mainSound.Play ();
		}
		else if (floortag == "Metal")
		{
         mainSound.clip = metalSurface[Random.Range(0, metalSurface.Length)];
		 mainSound.Play ();
		}
		}
		}
     }
public void JumpAudio()
{
	mainSound.clip = jumpSound;
	mainSound.Play ();
	
}
public void CrouchAudio()
{
	mainSound.clip = crouchSound;
	mainSound.Play ();
	
}
}
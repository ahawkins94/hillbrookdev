using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.hillbrookdev.modules.playerPhysics
{
public class AnimatorPlayer : MonoBehaviour {

	public bool isIdle = true;
	public bool isGrounded = false;
	public bool isJump = false;

	public bool isSwing = false;

	public int moveSpeed = 0;

	bool isDash = false;

	bool isWallJump = false;

	bool isSlide = false;

	bool isCrouch = false;

	bool isFlip = true; //right = true; left = false;
	// Update is called once per frame
	Animator animator;

	void Start() {
		animator = GetComponent<Animator>();
	}

	void Update () {
		isIdle = PlayerRun.playerVariable.isIdle;
		isGrounded = PlayerRun.playerVariable.isGrounded;
		moveSpeed = PlayerRun.playerVariable.moveSpeed;
		isDash = PlayerRun.playerVariable.isDash;
		isWallJump = PlayerRun.playerVariable.isSlide;
		isCrouch = PlayerRun.playerVariable.isCrouch;
		isSwing = PlayerRun.playerVariable.isSwing;
		isJump = PlayerRun.playerVariable.isJump;
		isSlide = PlayerRun.playerVariable.isSlide;

		animator.SetBool("isGrounded", isGrounded);
		animator.SetInteger("moveSpeed", moveSpeed);
		animator.SetBool("isDash", isDash);
		animator.SetBool("isSwing", isSwing);
		animator.SetBool("isJump", isJump);
		animator.SetBool("isIdle", isIdle);
	}

	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Player : MonoBehaviour
{
    private Animator animyeetor;
    private Rigidbody2D rigidBody;
    private Collider2D colliderP;
    private SpriteRenderer sprite;
    private bool canJump = false;
    private float canJumpInternalCooldown;
    private float jumpCounter;
    private Transform respawnPoint;

    [EventRef]
    public string landingSound; 

    public bool firstTime;
    public float returnSpeed = 10;
    public int jumps = 1;
    public float canJumpCooldown = 0.5f;
    public float maxSpeed = 10;
    public float acceleration = 5;
    public float jumpHeight = 10;
    public float glideMultiplier = 0.9f;
    public float justJumped = 0f;
    public float stepHeight = 1f;

    // Start is called before the first frame update
    void Start()
    {
        animyeetor = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        colliderP = GetComponent<Collider2D>();
        rigidBody = GetComponent<Rigidbody2D>();   
    }

    private void FixedUpdate() {

    }

    private void Update() {
        if (firstTime) {
            rigidBody.AddForce(new Vector2(Random.Range(-0.01f, 0.01f), -5f * Time.deltaTime));
            rigidBody.velocity = new Vector2(Mathf.Clamp(rigidBody.velocity.x, -0.1f, 0.1f), Mathf.Clamp(rigidBody.velocity.y, -55f, 0f));

        }
        else {
            if (!canJump) {
                if (IsGrounded()) {
                    canJump = true;
                }

            }
            else if (IsGrounded()) {
                animyeetor.SetBool("IsFlying", false);
                canJumpInternalCooldown = canJumpCooldown;
            }

            if(canJump && IsGrounded() && justJumped <= 0f) ResetJumpCounter();
            else if(justJumped > 0f) {
                justJumped -= Time.deltaTime;
            }

            if (canJumpInternalCooldown >= 0f) {
                canJumpInternalCooldown -= Time.deltaTime;
            }
            else {
                canJump = IsGrounded();
            }

            if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f && Mathf.Abs(rigidBody.velocity.x) < maxSpeed) {
                animyeetor.SetBool("Moving", true);

                float switchBoost = 1f;
                sprite.flipX = rigidBody.velocity.x < 0;
                if (rigidBody.velocity.x * Input.GetAxis("Horizontal") < 0) {
                    switchBoost *= returnSpeed;
                }

                if (Physics2D.Raycast(rigidBody.position + new Vector2(0, stepHeight),rigidBody.velocity,colliderP.bounds.extents.x)) {
                    rigidBody.transform.position = rigidBody.transform.position + new Vector3(0, colliderP.bounds.extents.y + stepHeight);
                }

                rigidBody.AddForce(new Vector2(Input.GetAxis("Horizontal") * acceleration * switchBoost, 0) * Time.deltaTime);
                rigidBody.velocity = new Vector2(Mathf.Clamp(rigidBody.velocity.x, -maxSpeed + 0.3f, maxSpeed - 0.3f), rigidBody.velocity.y);
                animyeetor.SetFloat("Speed", Mathf.Abs(rigidBody.velocity.x) * 0.4f);
            }
            else if (IsGrounded()) {
                animyeetor.SetBool("Moving", false);
                rigidBody.velocity = new Vector2(rigidBody.velocity.x - rigidBody.velocity.x % 1f, rigidBody.velocity.y);
            }

            if (Input.GetButtonDown("Jump") && jumpCounter > 0 && justJumped <= 0f) {
                if (IsGrounded() && canJump == true) {
                    justJumped = 0.1f;
                    canJump = false;
                    ResetJumpCounter();
                    rigidBody.MovePosition(new Vector2(rigidBody.position.x, rigidBody.position.y + colliderP.bounds.extents.y + colliderP.offset.y + 0.3f));
                    animyeetor.SetTrigger("DoJump");

                }
                else {
                    animyeetor.SetTrigger("DoFly");
                }
                jumpCounter -= 1;
                
                rigidBody.velocity = (new Vector2(rigidBody.velocity.x, jumpHeight));
            }
            else if (Input.GetButton("Jump") && !IsGrounded()) {
                animyeetor.SetBool("IsFlying", true);
                if (rigidBody.velocity.y < 0) {
                    rigidBody.velocity = new Vector2(rigidBody.velocity.x, rigidBody.velocity.y * glideMultiplier);
                }
            }


        }
    }

    private bool IsGrounded() {
        if (Physics2D.Raycast(transform.position + new Vector3(colliderP.bounds.extents.x, 0) * 0.9f, -transform.up, colliderP.bounds.extents.y - colliderP.offset.y * 1.2f) ||
            Physics2D.Raycast(transform.position - new Vector3(colliderP.bounds.extents.x, 0) * 0.9f, -transform.up, colliderP.bounds.extents.y - colliderP.offset.y * 1.2f) ||
            Physics2D.Raycast(transform.position, -transform.up, colliderP.bounds.extents.y - colliderP.offset.y * 1.2f)) {
            return true;
        }
        else {
            return false;
        }
    }

    public void ResetJumpCounter() {
        jumpCounter = jumps;
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if(IsGrounded()) {
            if(firstTime) {
                firstTime = false;
                RuntimeManager.PlayOneShot(landingSound);
            }
            animyeetor.SetTrigger("Land");
            animyeetor.SetBool("IsFlying", false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Killer") {
            transform.position = respawnPoint.position;
        }
        if(collision.tag == "Respawn") {
            respawnPoint = collision.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if(!IsGrounded()) {
            animyeetor.SetBool("IsFlying", true);
        }
    }
}

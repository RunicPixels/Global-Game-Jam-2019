using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public bool firstTime;
    public float returnSpeed = 10;
    public int jumps = 2;
    public float canJumpCooldown = 0.5f;
    public float maxSpeed = 10;
    public float acceleration = 5;
    public float jumpHeight = 10;
    public float glideMultiplier = 0.9f;

    // Start is called before the first frame update
    void Start()
    {
        animyeetor = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        colliderP = GetComponent<Collider2D>();
        rigidBody = GetComponent<Rigidbody2D>();   
    }

    private void Update() {
        if (firstTime) {
            rigidBody.AddForce(new Vector2(Random.Range(-0.01f, 0.01f), 2f));
            rigidBody.velocity = new Vector2(Mathf.Clamp(rigidBody.velocity.x, -0.1f, 0.1f), Mathf.Clamp(rigidBody.velocity.y, -20f, 0f));

        }
        else {
            if (!canJump) {
                canJump = IsGrounded();
            }
            else if (IsGrounded()) {
                animyeetor.SetBool("IsFlying", false);
                jumpCounter = jumps;
                canJumpInternalCooldown = canJumpCooldown;
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
                rigidBody.AddForce(new Vector2(Input.GetAxis("Horizontal") * acceleration * switchBoost, 0) * Time.deltaTime);
                rigidBody.velocity = new Vector2(Mathf.Clamp(rigidBody.velocity.x, -maxSpeed + 0.3f, maxSpeed - 0.3f), rigidBody.velocity.y);
                animyeetor.SetFloat("Speed", Mathf.Abs(rigidBody.velocity.x) * 0.4f);
            }
            else if (IsGrounded()) {
                animyeetor.SetBool("Moving", false);
                rigidBody.velocity = new Vector2(rigidBody.velocity.x - rigidBody.velocity.x % 1f, rigidBody.velocity.y);
            }

            if (Input.GetButtonDown("Jump") && jumpCounter > 0) {
                if (IsGrounded()) animyeetor.SetTrigger("DoJump");
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
        if (Physics2D.Raycast(transform.position + new Vector3(colliderP.offset.x, 0), -transform.up, colliderP.bounds.extents.y - colliderP.offset.y * 1.3f) ||
            Physics2D.Raycast(transform.position - new Vector3(colliderP.offset.x, 0), -transform.up, colliderP.bounds.extents.y - colliderP.offset.y * 1.3f)) {
            return true;
        }
        else {
            return false;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision) {
        if(IsGrounded()) {
            if(firstTime) {
                firstTime = false;
            }
            animyeetor.SetTrigger("Land");
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
}

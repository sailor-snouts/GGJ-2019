using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float velocity = 5;
    [SerializeField]
    private float interactionDistance = 1f;
    private bool isMovementLocked = false;
    private Vector2 direction;
    private Vector2 interactDirection;
    private SpriteRenderer sprite;
    private Rigidbody2D rb2d;

    public void LockMovement()
    {
        this.isMovementLocked = true;
    }

    public void UnlockMovement()
    {
        this.isMovementLocked = false;
    }

    void Start()
    {
        this.sprite = this.GetComponent<SpriteRenderer>();
        this.rb2d = this.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        this.rb2d.velocity = this.isMovementLocked ? Vector2.zero : this.direction.normalized * this.velocity;
    }

    void Update()
    {
        if (this.isMovementLocked) { return; }

        this.WalkInput();
        this.Interact();
    }

    void WalkInput()
    {
        this.direction.x = Input.GetAxis("Horizontal");
        this.direction.y = Input.GetAxis("Vertical");

        if (this.direction.x != 0)
        {
            this.sprite.flipX = this.direction.x < 0;
        }

        if(this.direction.magnitude > 0)
        {
            this.interactDirection = this.direction;
        }
    }

    void Interact()
    {
        Debug.DrawLine(this.transform.position, this.transform.position + (Vector3) this.interactDirection * this.interactionDistance, Color.red);
        if(Input.GetButtonDown("Fire1") || Input.GetButtonDown("Fire2"))
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(this.transform.position, this.interactDirection, this.interactionDistance);
            foreach(RaycastHit2D hit in hits)
            {
                if (hit.collider.gameObject == this.gameObject) continue;

                switch(hit.collider.gameObject.tag)
                {
                    case "Interactable":
                        hit.collider.GetComponent<InteractableBase>().Interact();
                        break;
                    default:
                        Debug.Log("Playe is interacting with a " + hit.collider.gameObject.tag);
                        break;
                }
            }
        }
    }
}

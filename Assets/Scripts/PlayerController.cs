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
    private Vector2 direction;
    private SpriteRenderer sprite;
    private Rigidbody2D rb2d;


    void Start()
    {
        this.sprite = this.GetComponent<SpriteRenderer>();
        this.rb2d = this.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        this.rb2d.velocity = this.direction.normalized * this.velocity;
    }

    void Update()
    {
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
    }

    void Interact()
    {
        Debug.DrawLine(this.transform.position, this.transform.position + (Vector3) this.direction * this.interactionDistance, Color.red);
        if(Input.GetButtonDown("Fire1"))
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(this.transform.position, this.direction, this.interactionDistance);
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

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private static readonly int Speed = Animator.StringToHash("Speed");
    public float moveSpeed;

    public Rigidbody2D rigidBody;
    private Vector3 _velocity = Vector3.zero;

    public Animator animator;
    public SpriteRenderer spriteRenderer;
    
    void FixedUpdate()
    {
        float horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        
        MovePlayer(horizontalMovement);
        Flip(rigidBody.linearVelocity.x);
        
        float characterVelocity = Mathf.Abs(rigidBody.linearVelocity.x);
        animator.SetFloat(Speed, characterVelocity);
    }

    void MovePlayer(float movement)
    {
        if (!gameObject.GetComponent<PlayerInteraction>().isInteracting)
        {
            Vector3 targetVelocity = new Vector2(movement, rigidBody.linearVelocity.y);
            rigidBody.linearVelocity = Vector3.SmoothDamp(rigidBody.linearVelocity, targetVelocity, ref _velocity, .05f);
        }
    }

    void Flip(float velocity)
    {
        if (velocity > 0.1f) spriteRenderer.flipX = true;
        else if (velocity < -0.1f) spriteRenderer.flipX = false;
    }
}

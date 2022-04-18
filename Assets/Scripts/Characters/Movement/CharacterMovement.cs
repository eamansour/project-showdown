using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 1f;
    
    private bool facingRight = true;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Move(Vector3 movement)
    {
        transform.position += movement * Time.deltaTime * moveSpeed;
        animator.SetFloat("Speed", Mathf.Abs(movement.x));
        
        if (movement.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (movement.x < 0 && facingRight)
        {
            Flip();
        }        
    }

    public void MoveTo(Vector3 target, float stoppingDistance)
    {
        Vector3 movement = transform.position.x < target.x
            ? new Vector3(1f, 0f, 0f)
            : new Vector3(-1f, 0f, 0f);
        
        if (Mathf.Abs((transform.position - target).x) <= stoppingDistance)
        {
            movement.x = 0f;
        }
        Move(movement);   
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);       
    }
}

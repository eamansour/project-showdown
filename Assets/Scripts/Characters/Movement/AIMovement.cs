using UnityEngine;

public abstract class AIMovement : MonoBehaviour
{
    public bool stopMoving { get; set; } = false;
    protected CharacterMovement characterMovement { get; private set; }
    private Vector3 wanderPosition = Vector3.zero;

    protected virtual void Start()
    {
        characterMovement = GetComponent<CharacterMovement>();
    }

    protected void Wander()
    {
        if (Mathf.Abs((transform.position - wanderPosition).x) <= 1f)
        {
            wanderPosition = new Vector3(Random.Range(-12f, 12f), 0f, 0f);
        }
        characterMovement.MoveTo(wanderPosition, 1f);
    }    
}

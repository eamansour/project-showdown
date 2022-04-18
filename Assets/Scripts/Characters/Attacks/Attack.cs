using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    [field: SerializeField]
    public LayerMask opponentLayer { get; private set; }

    [SerializeField]
    protected int damage;

    [SerializeField]
    protected float defaultAttackCooldown;
    protected float currentAttackCooldown;

    [SerializeField]
    protected Transform attackPoint;

    [SerializeField]
    protected float attackRange;

    protected Animator anim;
    protected AIMovement aiMovement;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        aiMovement = GetComponent<AIMovement>();
    }

    private void Update()
    {
        if (currentAttackCooldown <= 0)
        {
            aiMovement.stopMoving = true;
            DoAttack();
            currentAttackCooldown = defaultAttackCooldown;
        }
        else
        {
            currentAttackCooldown -= Time.deltaTime;
        }        
    }

    protected abstract void DoAttack();
}

using UnityEngine;

public class MeleeAttack : Attack
{
    [SerializeField]
    private bool destroyOnHit = false;

    protected override void DoAttack()
    {
        // Ready to attack - Only attack if there are enemies nearby
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, opponentLayer);
        if (enemiesToDamage.Length <= 0)
        {
            aiMovement.stopMoving = false;
            return;
        }

        anim.SetTrigger("Attacking");
        foreach (Collider2D enemyCol in enemiesToDamage)
        {
            enemyCol.GetComponent<Health>().TakeDamage(damage);
        }

        if (destroyOnHit)
        {
            GetComponent<EffectController>().PlayEffect();
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}

using System.Collections;
using UnityEngine;

public class RangedAttack : Attack
{
    [SerializeField]
    private GameObject impactEffect;

    [SerializeField]
    private LineRenderer lineRenderer;

    [SerializeField]
    private string shootSound;

    private GameObject hitPrefab;
    private GameObject bullet;

    protected override void DoAttack()
    {
        // Ready to attack - Only attack if there are enemies nearby
        RaycastHit2D hit = Physics2D.Raycast(attackPoint.position, transform.right, attackRange, opponentLayer);
        if (!hit)
        {
            aiMovement.stopMoving = false;
            return;
        }

        anim.SetTrigger("Attacking");
        Health health = hit.transform.GetComponent<Health>();
        if (health)
        {
            health.TakeDamage(damage);
        }
        Instantiate(impactEffect, hit.point, Quaternion.identity);

        lineRenderer.SetPosition(0, attackPoint.position);
        lineRenderer.SetPosition(1, hit.point);

        SoundManager.PlaySound(shootSound);
        StartCoroutine(ShootLine());
    }

    private IEnumerator ShootLine()
    {
        lineRenderer.enabled = true;

        yield return Time.deltaTime;

        lineRenderer.enabled = false;
    }
}
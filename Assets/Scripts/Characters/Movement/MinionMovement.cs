using UnityEngine;

public class MinionMovement : AIMovement
{
    [SerializeField]
    private float sightDistance = 20f;

    private Attack attack;
    private float stoppingDistance;

    protected override void Start()
    {
        base.Start();

        attack = GetComponent<Attack>();
        stoppingDistance = Random.Range(2f, 5f);
    }

    private void Update()
    {
        if (WaveSpawner.state == SpawnState.COUNTING)
        {
            Wander();
        }
        else
        {
            DoMovement();
        }
    }

    private void DoMovement()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, sightDistance, attack.opponentLayer);
        if (!stopMoving && hit)
        {
            // Move towards target
            characterMovement.MoveTo(hit.transform.position, 0f);
        }
        else if (!stopMoving)
        {
            Wander();
        }
    }
}

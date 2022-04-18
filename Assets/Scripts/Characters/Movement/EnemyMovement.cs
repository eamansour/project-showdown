using UnityEngine;

public class EnemyMovement : AIMovement
{
    private static GameObject tower;

    protected override void Start()
    {
        base.Start();
        if (!tower)
        {
            tower = GameObject.FindGameObjectWithTag("Tower");
        }        
    }

    private void Update()
    {
        if (!stopMoving && tower)
        {
            characterMovement.MoveTo(tower.transform.position, 1f);
        }
    }
}

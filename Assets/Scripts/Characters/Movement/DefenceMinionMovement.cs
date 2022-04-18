using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DefenceMinionMovement : AIMovement
{
    private List<Transform> defenceTransforms;
    private int targetDefenceIndex = 0;

    private DefenceBuilder defenceBuilder;

    protected override void Start()
    {
        base.Start();
        defenceBuilder = GetComponent<DefenceBuilder>();
        defenceTransforms = GameObject.FindGameObjectsWithTag("Defence").Select(go => go.transform).ToList();
    }

    private void Update()
    {
        // Move to defences when waiting for waves
        if (WaveSpawner.state == SpawnState.COUNTING)
        {
            DoMovement();    
        }
        else
        {
            Wander();
        }
    }

    private void DoMovement()
    {
        Transform target = defenceBuilder.GetDamagedDefence();
        if (target)
        {
            // Move to repair damaged defence
            characterMovement.MoveTo(target.position, 0.1f);
        }
        else
        {
            // Move to defence locations
            if (Mathf.Abs((transform.position - defenceTransforms[targetDefenceIndex].position).x) <= 0.1f)
            {
                targetDefenceIndex = (targetDefenceIndex + 1) % defenceTransforms.Count;
            }

            if (!defenceBuilder.isBuilding)
            {
                characterMovement.MoveTo(defenceTransforms[targetDefenceIndex].position, 0f);
            }
        }
    }
}
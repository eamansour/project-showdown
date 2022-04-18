using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceBuilder : MonoBehaviour
{
    public bool isBuilding { get; private set; } = false;

    [SerializeField]
    private GameObject defencePrefab;

    private LayerMask defenceLayer;

    [SerializeField]
    private float buildRange;

    [SerializeField]
    private int repair = 10;

    private float buildCooldown = 1f;
    private bool doingDefence = false;

    private Animator anim;

    private List<Health> spawnedDefences = new List<Health>();

    private void Start()
    {
        anim = GetComponent<Animator>();
        defenceLayer = 1 << LayerMask.NameToLayer("Defence");
    }

    private void Update()
    {
        if (WaveSpawner.state == SpawnState.COUNTING)
        {
            if (!doingDefence)
            {
                StartCoroutine(DoDefence());
            }
        }
    }

    public IEnumerator DoDefence()
    {
        // Check for defence point
        Collider2D defencePoint = Physics2D.OverlapCircle(transform.position, buildRange, defenceLayer);
        if (!defencePoint) yield break;
        doingDefence = true;

        anim.SetTrigger("Building");

        // Create a new defence if one doesn't exist here
        if (defencePoint.transform.childCount == 0)
        {
            isBuilding = true;
            GameObject defence =
                Instantiate(defencePrefab, defencePoint.transform.position, defencePrefab.transform.rotation, defencePoint.transform);
            spawnedDefences.Add(defence.GetComponent<Health>());
            SoundManager.PlaySound("WallSpawn");
        }
        else
        {
            // Heal nearby defence if it needs repair
            Health defenceHealth = defencePoint.GetComponentInChildren<Health>();
            if (defenceHealth.currentHealth < defenceHealth.startHealth)
            {
                isBuilding = true;
                defenceHealth.Heal(repair);
            }
        }
        yield return new WaitForSeconds(buildCooldown);
        isBuilding = false;
        doingDefence = false;
    }

    public Transform GetDamagedDefence()
    {
        spawnedDefences.RemoveAll(defence => !defence);

        for (int i = 0; i < spawnedDefences.Count; i++)
        {
            
            if (spawnedDefences[i].currentHealth < spawnedDefences[i].startHealth)
            {
                return spawnedDefences[i].transform;
            }
        }
        return null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, buildRange);
    }
}
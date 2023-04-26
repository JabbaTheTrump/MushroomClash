using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArmyMovement : MonoBehaviour
{
    public float moveSpeed = 1f;
    public GameObject targetObject;
    public UnitManager armyUnitManager;
    public Transform armySpriteTransform;
    private UnitManager targetUnitManager;


    void Start()
    {
        targetUnitManager = targetObject.GetComponent<UnitManager>();
    }

    void FixedUpdate()
    {
        transform.position += armySpriteTransform.right * moveSpeed * Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject == targetObject)
        {
            FactionModule targetFaction = targetUnitManager.ownerFaction;
            FactionModule armyFaction = armyUnitManager.ownerFaction;

            if (targetFaction == armyFaction)
            {
                Debug.Log("joining city");
                StartCoroutine(JoinCity());
            }
            else
            {
                moveSpeed = 0;
                StartCoroutine(AttackCity());
            }
        }
    }

    public void MoveToPosition(GameObject target)
    {
        targetObject = target;
        RotateTowards(targetObject.transform.position);
    }

    void RotateTowards(Vector3 target)
    {
        target = target - transform.position;
        float angle = Mathf.Atan2(target.y,target.x) * Mathf.Rad2Deg;
        armySpriteTransform.rotation = Quaternion.AngleAxis(angle,Vector3.forward);
    }

    IEnumerator JoinCity() //when an army is reinforcing a friendly city
    {
        yield return new WaitForSeconds(1f);
        targetUnitManager.manPower += armyUnitManager.manPower;
        Debug.Log(targetUnitManager.unitName + " has been reinforced");
        Destroy(gameObject);
    }

    IEnumerator AttackCity()
    {
        moveSpeed = 0;
        targetUnitManager.isAttacked = true;

        Debug.Log(targetUnitManager.unitName + " is being attacked!");


        //attack loop (reduces MP)
        while (armyUnitManager.manPower > 0 && targetUnitManager.manPower > 0)
        {
            yield return new WaitForSeconds(StaticPropertyVariables.ins.battleSpeed);
            int armyManpower = armyUnitManager.manPower;
            int cityManpower = targetUnitManager.manPower;
            int casualtyRate = Mathf.CeilToInt(0.05f * Mathf.Max(armyManpower, cityManpower));
            int casualties = 1 + Random.Range(casualtyRate, 2 * casualtyRate);
            Debug.Log("casualties: " + casualties);
            armyUnitManager.manPower -= casualties;
            targetUnitManager.manPower -= casualties;
        }
        
        //what happens if the attacking army is wiped out
        if (armyUnitManager.manPower <= 0)
        {
            StartCoroutine(targetUnitManager.CalculateManpower());
            targetUnitManager.isAttacked = false;
            Destroy(gameObject);
        }

        //what happens if the attacking army wins
        else
        {
            if (targetUnitManager.tag == "City")
            {
                
                StartCoroutine(JoinCity());
                GameEvents.instance.Events_CityCaptured(armyUnitManager.ownerFaction, targetUnitManager);
            }
            else
            {
                Debug.Log(targetUnitManager.unitName + " has been destroyed!");
                targetUnitManager.isAttacked = false;
                Destroy(targetUnitManager);
            }
        }
    }
}

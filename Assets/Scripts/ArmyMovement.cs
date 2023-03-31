using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArmyMovement : MonoBehaviour
{
    public float moveSpeed = 1f;
    public GameObject targetObject;
    public UnitManager armyUnitManager;
    public TextMesh unitMP;
    private UnitManager targetUnitManager;


    void Start()
    {
        targetUnitManager = targetObject.GetComponent<UnitManager>();
    }

    void FixedUpdate()
    {
        transform.position += transform.right * moveSpeed * Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject == targetObject)
        {
            if (targetUnitManager.owner == armyUnitManager.owner)
            {
                Debug.Log("joining city; " + targetUnitManager.owner + " " + armyUnitManager.owner);
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
        transform.rotation = Quaternion.AngleAxis(angle,Vector3.forward);
        unitMP.transform.eulerAngles = new Vector3(0, 0, 0);
    }

    IEnumerator JoinCity()
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
        while (armyUnitManager.manPower > 0 && targetUnitManager.manPower > 0)
        {
            armyUnitManager.manPower--;
            targetUnitManager.manPower--;
            yield return new WaitForSeconds(0.2f);
        }
        
        if (armyUnitManager.manPower <= 0)
        {
            targetUnitManager.isAttacked = false;
            Destroy(gameObject);
        }
        else
        {
            if (targetUnitManager.tag == "City")
            {
                Debug.Log(targetUnitManager.unitName + " has been captured!");
                targetUnitManager.owner = armyUnitManager.owner;
                targetUnitManager.isAttacked = false;
                StartCoroutine(JoinCity());
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

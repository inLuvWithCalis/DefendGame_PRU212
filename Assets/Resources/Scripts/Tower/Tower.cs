using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using UnityEngine.Windows;

public enum Target
{
    WeakestEnememies,
    StrongestEnememies,
    First,
    Last
}
public class Tower : MonoBehaviour
{
    [Header("Target Selection")]
    public Target target;

    [Header("Attribute")]
	public int limitTower;
	public float firerate = .2f;
    public bool isHuman = false;
    public GameObject range;
    private float checkCounter;
    public bool onlyAttackFlying;

    [Header("References")]
    public GameObject bulletPrefab;
    public Transform firingPoint;
    private List<EnemyController> eList = new List<EnemyController>();
    public EnemyController enemyController { get; set; }
    [HideInInspector]
    public TowerUpgradeController upgrader;
	[HideInInspector]
	public CircleCollider2D circleCollider;
	private Base theBase;
    public float dmgUpdate;

    private void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        upgrader = GetComponent<TowerUpgradeController>();
       

	}
    void Start()
    {
        theBase = FindObjectOfType<Base>();
        checkCounter = firerate;
		removeRange();
	}
    
    private void Update()
    {
        if (theBase.currentHealth > 0)
        {
            SelectionTarget();
            RotateTowardsTarget();
            if (enemyController != null)
            {                
                Shoot();
                ShotAnimator(true);
            }
        }

        if (TowerManager.Instance.selectedTower == this)
        {
            getRange();
        }
        else
        {
           // TowerManager.Instance.CloseTowerUpgradePanel();
           removeRange();
		}
        /*else if (TowerManager.Instance.isPlacing == false)
        {
            removeRange();
        }*/
    }

    public void getRange()
    {
        float radius = circleCollider.radius;
        range.transform.localScale = new Vector3(radius, radius, radius);
        range.SetActive(true);
    }

    public void removeRange()
    {
        range.SetActive(false);
    }

    private void SelectionTarget()
    {
        switch (target)
        {
            case Target.First:
                if (eList.Count > 0)
                {
                    enemyController = eList[0];
                }
                else
                {
                    enemyController = null;
				}
                break;
            case Target.Last:
                if (eList.Count > 0)
                {
                    enemyController = eList[eList.Count - 1];
                }
                else
                {
					enemyController = null;
				}
                break;
            case Target.StrongestEnememies:
				if (eList.Count > 0)
				{
					var desc = eList.OrderByDescending(o => o.GetComponent<EnemyController>().enemyHeath);
					enemyController = desc.FirstOrDefault();
                }
                else
                {
					enemyController = null;
				}

				break;
            case Target.WeakestEnememies:
                if (eList.Count > 0)
                {
                    var asc = eList.OrderBy(o => o.GetComponent<EnemyController>().enemyHeath);
                    enemyController = asc.FirstOrDefault();
                }
                else
                {
                    enemyController = null;
                }
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D objec)
    {

        if (objec.CompareTag(("Enemy")))
        {
            EnemyController enemy = objec.gameObject.GetComponent<EnemyController>();
            if (onlyAttackFlying)
            {
                if (enemy != null && enemy.canFly)
                {
                    eList.Add(enemy);
                }                                               
            }
            else
            {
                if (enemy != null && !enemy.canFly)
                {
                    eList.Add(enemy);
                }                 
            }

        }

    }

    private void OnTriggerExit2D(Collider2D objec)
    {
        if (objec.CompareTag(("Enemy")))
        {
            EnemyController enemy = objec.gameObject.GetComponent<EnemyController>();
            if (eList.Contains(enemy))
            {
                eList.Remove(enemy);
            }
        }
    }

    private void RotateTowardsTarget()
    {
        if (isHuman)
        {
            if (enemyController == null) return;
        }
        else
        {
            if (enemyController == null) return;
            float angle = Vector3.SignedAngle(transform.up, enemyController.transform.position - transform.position, transform.forward);
            transform.Rotate(0f, 0f, angle);
        }
       
       
        //Xac dinh bool right or left
    }
    private void Shoot()
    {

        checkCounter -= Time.deltaTime;
        if (checkCounter <= 0)
        {
            checkCounter = firerate;
            GameObject bullet = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.bulletDamage = dmgUpdate;
            bulletScript.SetTarget(enemyController);
            if (onlyAttackFlying) 
                bulletScript.SetAttackFlying();
        }

    }

    void ShotAnimator(bool isShot) {
        Animator weaponAnimator = this.GetComponentInChildren<Animator>();
        if (weaponAnimator != null) {
            weaponAnimator.SetBool("isShot", isShot);        
        }
    }
    public void getTower()
    {

		if (TowerManager.Instance.isPlacing == false)
        {
     //       AudioManeger.Instance.PlaySFX("btn_Click");

			TowerManager.Instance.selectedTower = this;
			TowerManager.Instance.moveTowerSelectionEffect();
			TowerManager.Instance.meme();
        }
       
	}
       

    /* private void OnDrawGizmos()
     {
         if (!_gameStarted)
             Gizmos.DrawWireSphere(transform.position, range);
     }*/


}

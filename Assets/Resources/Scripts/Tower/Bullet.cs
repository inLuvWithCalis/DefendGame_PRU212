using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bullet : MonoBehaviour
{
	[Header("References")]

	[Header("Attributes")]
	public float firerateBullet = 0.001f;
	public float bulletDamage;
	public float bulletSnow;
	public float bulletSnowTime;
	public float damagePercentFire;
	public float bulletFireTime;

	[Header("Boom")]
	public float rangeBoom = 3;
	public bool isBoom = false;
	public GameObject boomAnimation;
	public float timeAnimation = 0.1f;

	private EnemyController target;
	private bool onlyAttackFlying;
	public void SetTarget(EnemyController _target)
	{
		target = _target;
	}

	public void SetAttackFlying()
	{
		onlyAttackFlying = true;
	}
	//FixedUpdate is call once per 0.02s(xu li vat li) 
	private void FixedUpdate()
	{
		if (target == null)
		{
			Destroy(gameObject);
			return;
		}

		float angle = Vector3.SignedAngle(transform.up, target.transform.position - transform.position, transform.forward);
		transform.Rotate(0f, 0f, angle);

		transform.position = Vector2.MoveTowards(transform.position, target.transform.position,
		firerateBullet * Time.deltaTime);

	}
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag(("Enemy")))
		{
			DamageEnemy(other);
		}
	}

	private void EffectAndDmgEnemy(Collider2D other)
	{
		switch (this.gameObject.tag)
		{
			case "DanBang":
				AudioManager.Instance.PlaySFX("");
				other.gameObject.GetComponent<EnemyController>().ApplySlowEffect(bulletSnowTime, bulletSnow);
				break;
			case "DanLua":
				AudioManager.Instance.PlaySFX("");
				other.gameObject.GetComponent<EnemyController>().ApplyBurnEffect(bulletFireTime, damagePercentFire);
				break;
			default:
				break;
		}
	}


	private void DamageEnemy(Collider2D enemy)
	{
		if ((onlyAttackFlying && !enemy.gameObject.GetComponent<EnemyController>().canFly)
			|| (!onlyAttackFlying && enemy.gameObject.GetComponent<EnemyController>().canFly))
			return;
		if (enemy.CompareTag(("Enemy")))
		{
			if (isBoom)
			{
				Vector3 boomPosition = enemy.gameObject.GetComponent<EnemyController>().transform.position;
				DamageBoom(boomPosition);
				AudioManager.Instance.PlaySFX("bombExplode");

			}
			else
			{
				
				EffectAndDmgEnemy(enemy);
				AudioManager.Instance.PlaySFX("gun");
				enemy.gameObject.GetComponent<EnemyController>().takeDamage(bulletDamage);
			}
		}
		Destroy(gameObject);
	}

	private void DamageBoom(Vector3 position)
	{
		boomAnimation.transform.localScale = this.transform.localScale * rangeBoom;
		GameObject boom = Instantiate(boomAnimation, position, Quaternion.identity);
		Destroy(boom, timeAnimation);
		Collider2D[] listEnemies = Physics2D.OverlapCircleAll(position, rangeBoom);
		foreach (Collider2D enemy in listEnemies)
		{
			if (enemy.CompareTag(("Enemy")))
			{
				enemy.gameObject.GetComponent<EnemyController>().takeDamage(bulletDamage);
				EffectAndDmgEnemy(enemy);
			}
		}
		Destroy(gameObject);
	}
}

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    [Header("Attribute")]
    public float moveSpeed;
    public float enemyHeath;
    public int moneyOnDeath = 50;
    public bool canFly;

    [Header("References")]
    public Animator animator;
    public Slider enemyHeathSlider;
    public GameObject stateIcon;
    public GameObject burnEffect;

    private Path thePath;
    private int currentPoint;
    private bool reachedEnd;
    private Vector2 input;
    private Base theBase;
    private float originalMoveSpeed;
    private float originalHeath;    
    private EnemyState enemyState = EnemyState.normal;
    
    [Header("Materials")]
    [SerializeField] private Material burnedMaterial;
    [SerializeField] private Material slowMaterial;
    [SerializeField] private Material stunnedMaterial;
    private                  Material defaultMaterial;
    private                  SpriteRenderer enemyRenderer;
    
     enum EnemyState
    {
        normal,
        burn,
        slow,
        stunned
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        // Initialize the enemy's path and base material
        enemyRenderer = GetComponent<SpriteRenderer>();
        defaultMaterial = enemyRenderer.material;
        
        if (canFly)//random start's position
        {
            Camera mainCamera = Camera.main;

            float cameraLeft = mainCamera.transform.position.x - mainCamera.orthographicSize * mainCamera.aspect;
            float cameraRight = mainCamera.transform.position.x + mainCamera.orthographicSize * mainCamera.aspect;

            // Generate a random X position between cameraLeft and cameraRight
            float randomX = Random.Range(cameraLeft, cameraRight);

            // Calculate the Y position at the bottom of the camera view
            float cameraBottom = mainCamera.transform.position.y - mainCamera.orthographicSize;

            // Create a Vector3 for the random position at the bottom
            Vector3 randomPosition = new Vector3(randomX, cameraBottom, 0f);

            gameObject.transform.position = randomPosition;
        }
        
        thePath = FindObjectOfType<Path>();
        theBase = FindObjectOfType<Base>();
        enemyHeathSlider.maxValue = enemyHeath;
        enemyHeathSlider.value = enemyHeath;
        originalHeath = enemyHeath;
        originalMoveSpeed= moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (theBase != null &&  theBase.currentHealth > 0 )
        {
            if (canFly)
            {
                currentPoint = thePath.points.Length - 1;
            }
            transform.position = Vector2.MoveTowards(transform.position, thePath.points[currentPoint].position,
            moveSpeed * Time.deltaTime);
            //animation of move
            input = (thePath.points[currentPoint].position - transform.position).normalized;
            animator.SetFloat("moveX", input.x);
            animator.SetFloat("moveY", input.y);

            if (Vector2.Distance(transform.position, thePath.points[currentPoint].position) < .2f)
            {
                currentPoint = currentPoint + 1;
                if (currentPoint >= thePath.points.Length)
                {
                    theBase.takeDamage(enemyHeath);
                    Destroy(gameObject);
                }
            }
        }
    }
    public void takeDamage(float damage)
    {
        enemyHeath = Mathf.Clamp(enemyHeath - damage, 0, enemyHeathSlider.maxValue);
        if (enemyHeath > 0)
        {
			AudioManager.Instance.PlaySFX("enemyImpact");
			enemyHeathSlider.value = enemyHeath;
        }
        else
        {
            AudioManager.Instance.PlaySFX("enemyDeath");
            Money.instance.GiveMoney(moneyOnDeath);
            Destroy(gameObject);
        }
    }
	public void takeDamageFire(float damage)
	{
		enemyHeath = Mathf.Clamp(enemyHeath - damage, 0, enemyHeathSlider.maxValue);
		if (enemyHeath > 0)
		{
			enemyHeathSlider.value = enemyHeath;
		}
		else
		{
			AudioManager.Instance.PlaySFX("enemyDeath");
			Money.instance.GiveMoney(moneyOnDeath);
			Destroy(gameObject);
		}
	}


	public void ApplyBurnEffect(float duration, float damagePercent) {
		
		float totalDamage = originalHeath * damagePercent;
        StartCoroutine(BurnEnemy(duration, totalDamage));
    }

    public void ApplySlowEffect(float duration, float slowPercent) {
		StartCoroutine(SlowEnemy(duration, slowPercent));
    }

    public void ApplyStunnedEffect(float duration){
		AudioManager.Instance.PlaySFX("stun");
		StartCoroutine(StunEnemy(duration));
    }

    private IEnumerator SlowEnemy(float duration, float slowPercent)
    {
        float elapsedTime = 0;
        moveSpeed = moveSpeed * (1 - slowPercent);
        
        this.enemyRenderer.material = slowMaterial;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            if (enemyState == EnemyState.stunned)
            {
                yield return null;
            }
            else
            {
                UpdateState(EnemyState.slow);
                animator.speed = 1 - slowPercent;                
                yield return null;
            }
        }
        // Reset the material to default
        this.enemyRenderer.material = defaultMaterial;
        animator.speed = 1;
        moveSpeed = originalMoveSpeed;
        UpdateState(EnemyState.normal);
    }

    private IEnumerator StunEnemy(float duration)
    {
        float elapsedTime = 0;
        
        this.enemyRenderer.material = stunnedMaterial;

        UpdateState(EnemyState.stunned);
        while (elapsedTime < duration)
        {
            moveSpeed = 0;
            animator.speed = 0;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // Reset the material to default
        this.enemyRenderer.material = defaultMaterial;
        animator.speed = 1;
        moveSpeed = originalMoveSpeed;
        UpdateState(EnemyState.normal);
    }

    private IEnumerator BurnEnemy(float duration, float totalDamage)
    {
        float elapsedTime = 0;
        float damageByTime;
        
        // Change the material to burnedMaterial
        enemyRenderer.material = burnedMaterial;
    
        if (burnEffect != null) burnEffect.SetActive(true);                 
        UpdateState(EnemyState.burn);
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            if(elapsedTime <= duration)
            {
                damageByTime = totalDamage * (Time.deltaTime / duration);
            }
            else
            {
                damageByTime = totalDamage * ((Time.deltaTime - (elapsedTime - duration)) / duration);
            }
			takeDamageFire(damageByTime);
            yield return null;
        }
        // Reset the material to default
        enemyRenderer.material = defaultMaterial;
        if (burnEffect != null)
            burnEffect.SetActive(false);
        UpdateState(EnemyState.normal);
    }

    private void UpdateState(EnemyState state)
    {
        enemyState = state;
        SpriteRenderer spriteRenderer = stateIcon.GetComponent<SpriteRenderer>();
        Object[]  sprites = Resources.LoadAll("Spirites/Enemy/States");
        switch (state)
        {
            case EnemyState.normal:
                spriteRenderer.sprite = null;
                break;
            case EnemyState.burn:
                spriteRenderer.sprite = (Sprite) sprites[25];
                break;
            case EnemyState.slow:
                spriteRenderer.sprite = (Sprite)sprites[72];
                break;
            case EnemyState.stunned:
                spriteRenderer.sprite = (Sprite)sprites[56];
                break;
        }
    }

}

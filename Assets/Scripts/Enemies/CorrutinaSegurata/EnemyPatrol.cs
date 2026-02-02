using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
	public float speed = 1f;
	public float wallAware = 0.5f;
	public LayerMask groundLayer;
	public float playerAware = 3f;
	public float aimingTime = 0.5f;
	public float shootingTime = 1.5f;

	public Rigidbody2D _rigidbody;
	public Animator _animator;
	public Weapon _weapon;

	// Movement
	private Vector2 _movement;
	private bool _facingRight;

	public bool _isAttacking;
	public PlayerHealth playerHealth;
	public GameObject shootingArea;
    public GameObject Weapon;
	public EnemyPatrol enemyPatrol;

    void Awake()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
		_animator = GetComponent<Animator>();
	}

	// Start is called before the first frame update
	void Start()
	{
		if (transform.localScale.x < 0f) {
			_facingRight = false;
		} else if (transform.localScale.x > 0f) {
			_facingRight = true;
		}
	}

	// Update is called once per frame
	void Update()
	{
		Vector2 direction = Vector2.right;

		if (_facingRight == false) {
			direction = Vector2.left;
		}

		if (_isAttacking == false) {
			if (Physics2D.Raycast(transform.position, direction, wallAware, groundLayer)) {
				Flip();
			}
		}

		if (playerHealth.currentHealth <= 0)
		{
			shootingArea.SetActive(false);
			Weapon.SetActive(false);
			enemyPatrol.enabled = false;
		}
	}

	private void FixedUpdate()
	{
		float horizontalVelocity = speed;

		if (_facingRight == false) {
			horizontalVelocity = horizontalVelocity * -1f;
		}

		if (_isAttacking) {
			horizontalVelocity = 0f;
		}

		_rigidbody.velocity = new Vector2(horizontalVelocity, _rigidbody.velocity.y);
	}

	private void LateUpdate()
	{
		_animator.SetBool("Idle", _rigidbody.velocity == Vector2.zero);
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (_isAttacking == false && collision.CompareTag("Player")) {
			StartCoroutine("AimAndShoot");
			Debug.Log("Corrutina AimAndShoot llamada");
		}
	}

	private void Attack1()
	{
        if (_isAttacking == false && Input.GetKeyDown(KeyCode.T))
        {
            StartCoroutine("AimAndShoot");
        }
    }

	private void Flip()
	{
		_facingRight = !_facingRight;
		float localScaleX = transform.localScale.x;
		localScaleX = localScaleX * -1f;
		transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
	}

    //IEnumerator AimAndShoot
	private IEnumerator AimAndShoot()
	{
		_isAttacking = true;

		yield return new WaitForSeconds(1f);

		_animator.SetTrigger("Shoot");
		CanShoot();

        yield return new WaitForSeconds(1f);

		_isAttacking = false;
	}

    void CanShoot()
	{
		if (_weapon != null /*&& playerHealth.currentHealth > 0*/) 
		{
			_weapon.Shoot();
		}
	}

	private void OnEnable()
	{
		_isAttacking = false;
	}

	private void OnDisable()
	{
		StopCoroutine("AimAndShoot");
		_isAttacking = false;
	}
}

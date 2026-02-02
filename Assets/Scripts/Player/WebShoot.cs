using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebShoot : MonoBehaviour
{
	public int damage = 1;
	public float speed = 2f;
	public Vector2 direction;

	public float livingTime = 3f;
	public Color initialColor = Color.white;
	public Color finalColor;

	private SpriteRenderer _renderer;
	private Rigidbody2D _rigidbody;
	public PlayerAttack playerAttack;
	private float _startingTime;
	private bool _returning;

	void Awake()
	{
		_renderer = GetComponent<SpriteRenderer>();
		_rigidbody = GetComponent<Rigidbody2D>();
	}

	// Start is called before the first frame update
	void Start()
    {
		//  Save initial time
		_startingTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
		// Change bullet's color over time
		float _timeSinceStarted = Time.time - _startingTime;
		float _percentageCompleted = _timeSinceStarted / livingTime;

		_renderer.color = Color.Lerp(initialColor, finalColor, _percentageCompleted);

		if (_percentageCompleted >= 1f) {
			Explode();
		}
	}

	private void FixedUpdate()
	{
		//  Move object
		Vector2 movement = direction.normalized * speed;
		_rigidbody.velocity = movement;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
        /*
		if (_returning == false && collision.CompareTag("Player")) 
		{
			// Tell player to get hurt
			collision.SendMessageUpwards("TakeDamage", damage);
            Explode();
		}
		*/

        if (_returning == false && collision.CompareTag("Destructible") || collision.CompareTag("Enemy"))
        {
            // Tell player to get hurt
            collision.SendMessageUpwards("TakeDamage");
            Explode();
        }

        if (_returning == true && collision.CompareTag("Enemy")) 
		{
			collision.SendMessageUpwards("AddDamage");
			Explode();
		}

        if (playerAttack.isAttacking == true)
        {
            if (collision.CompareTag("Enemy") || collision.CompareTag("Destructible"))
            {
                //collision.SendMessageUpwards("AddDamage");
                collision.SendMessageUpwards("TakeDamage");
            }
        }
    }

    public void AddDamage()
	{
		_returning = true;
		direction = direction * -1f;
	}

	public void Explode()
	{
		speed = 0f;

		_renderer.enabled = false;

		Destroy(gameObject, 0.5f);
	}
}

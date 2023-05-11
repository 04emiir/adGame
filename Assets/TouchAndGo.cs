using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TouchAndGo : MonoBehaviour {
	
	[SerializeField]
	float moveSpeed = 12f;

	Rigidbody2D rb;

	Touch touch;
	Vector3 touchPosition, whereToMove;
	bool isMoving = false;
	Animator anim;

    int life;

	float previousDistanceToTouchPos, currentDistanceToTouchPos;

	public TextMeshProUGUI vidasText;

	void Start () {
		life = 3;
		rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
	
	void Update () {

		if (life <= 0) {
            SceneManager.LoadScene("menu");
        }

		if (isMoving)
			currentDistanceToTouchPos = (touchPosition - transform.position).magnitude;
		
		if (Input.touchCount > 0) {
			touch = Input.GetTouch (0);

			if (touch.phase == TouchPhase.Began) {
					previousDistanceToTouchPos = 0;
					currentDistanceToTouchPos = 0;
					isMoving = true;
					touchPosition = Camera.main.ScreenToWorldPoint (touch.position);
					touchPosition.z = 0;
					whereToMove = (touchPosition - transform.position).normalized;
					rb.velocity = new Vector2 (whereToMove.x * moveSpeed, whereToMove.y * moveSpeed);
				}
		}

		if (currentDistanceToTouchPos > previousDistanceToTouchPos) {
			isMoving = false;
			rb.velocity = Vector2.zero;
		}

		if (isMoving)
			previousDistanceToTouchPos = (touchPosition - transform.position).magnitude;
	}

    public void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag == "enemy") {
			Destroy(collision.gameObject);
			life--;
            anim.Play("Green Hurt - Animation");
			anim.Play("Green Idle - Animation");
			vidasText.text = "Vidas: " + life.ToString();
		}
    }
}

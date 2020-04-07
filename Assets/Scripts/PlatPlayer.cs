using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatPlayer : MonoBehaviour
{
    public float jumpforce = 12.0f;
    public float speed = 250.0f;
    private Animator _anim;

    private Rigidbody2D _body;
    private BoxCollider2D _box;
    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _box = GetComponent<BoxCollider2D>();
        float deltaX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        Vector2 movement = new Vector2(deltaX, _body.velocity.y);

        _body.velocity = movement;

        Vector3 max = _box.bounds.max;
        Vector3 min = _box.bounds.min;
        Vector2 corner1 = new Vector2(max.x, min.y -.1f);
        Vector2 corner2 = new Vector2(min.x, min.y - .2f);
        Collider2D hit = Physics2D.OverlapArea(corner1, corner2);

        bool grounded = false;

        if(hit != null)
        {
            grounded = true;
        }

        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            _body.AddForce(Vector2.up * jumpforce, ForceMode2D.Impulse);
        }

        _anim.SetFloat("Speed", Mathf.Abs(deltaX));
        if (!Mathf.Approximately(deltaX, 0))
        {
            transform.localScale = new Vector3(Mathf.Sign(deltaX), 1, 1);
        }
    }
}

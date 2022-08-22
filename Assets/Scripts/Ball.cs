using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    private GameManager Gm;
    public Rigidbody Rb;
    private Vector2 _FirstPos;
    private Vector2 _SecondPos;
    private Vector2 _CurrentPos;
    public float _MoveSpeed;
    public float _CurrentGroundNumber;
    public Image _LevelBar;


    void Start()
    {
        Gm = GameObject.FindObjectOfType<GameManager>();
    }

    void Update()
    {
        Swipe();
        _LevelBar.fillAmount = _CurrentGroundNumber / Gm._GroundNumbers;

        if (_LevelBar.fillAmount == 1)
        {
            Gm.LevelUpdate();
        }
    }

    void Swipe()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _FirstPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }

        if (Input.GetMouseButtonUp(0))
        {
            _SecondPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            _CurrentPos = new Vector2(
                    _SecondPos.x - _FirstPos.x,
                    _SecondPos.y - _FirstPos.y
                );
            _CurrentPos.Normalize();

            if (_CurrentPos.y < 0 && _CurrentPos.x > -.5f && _CurrentPos.x < .5f)
            {
                //back
                Rb.velocity = Vector3.back * _MoveSpeed;
            }
            else if (_CurrentPos.y > 0 && _CurrentPos.x > -.5f && _CurrentPos.x < .5f)
            {
                //forward
                Rb.velocity = Vector3.forward * _MoveSpeed;
            }
            else if (_CurrentPos.x < 0 && _CurrentPos.y > -.5f && _CurrentPos.y < .5f)
            {
                //left
                Rb.velocity = Vector3.left * _MoveSpeed;
            }
            else if (_CurrentPos.x > 0 && _CurrentPos.y > -.5f && _CurrentPos.y < .5f)
            {
                //right
                Rb.velocity = Vector3.right * _MoveSpeed;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<MeshRenderer>().material.color != Color.red)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                collision.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
                Constraints();
                _CurrentGroundNumber++;
            }
        }
    }

    private void Constraints()
    {
        Rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
    }
}

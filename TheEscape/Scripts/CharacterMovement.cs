using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] float velocity = 5;
    [SerializeField] float swipeThreshold = 125;

    Rigidbody2D rigidbody2d;
    Vector3 startPosition;
    bool moving;
    float horizontal;
    float vertical;
    bool caught;
    List<Vector2> movements = new List<Vector2>();
    Vector2 startTouch;
    bool drag;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }

    void Update()
    {
        if(!caught)
        {
            moving = Mathf.Abs(rigidbody2d.velocity.x) > 0.5f || Mathf.Abs(rigidbody2d.velocity.y) > 0.5f;

            #region Standard Controls
            if (Input.GetButtonDown("MoveRight"))
            {
                movements.Add(new Vector2(1, 0));
            }
            else if (Input.GetButtonDown("MoveLeft"))
            {
                movements.Add(new Vector2(-1, 0));
            }
            else if (Input.GetButtonDown("MoveUp"))
            {
                movements.Add(new Vector2(0, 1));
            }
            else if (Input.GetButtonDown("MoveDown"))
            {
                movements.Add(new Vector2(0, -1));
            }
            #endregion

            #region Mobile Controls
            if (Input.touches.Length > 0)
            {
                if(Input.touches[0].phase == TouchPhase.Began)
                {
                    startTouch = Input.touches[0].position;
                    drag = true;
                }
                else if(Input.touches[0].phase == TouchPhase.Canceled || Input.touches[0].phase == TouchPhase.Ended)
                {
                    startTouch = Vector2.zero;
                    drag = false;
                }

                if (drag)
                {
                    var distance = Input.touches[0].position - startTouch;
                    if(distance.magnitude > swipeThreshold)
                    {
                        if(Mathf.Abs(distance.x) > Mathf.Abs(distance.y))
                        {
                            if(distance.x > 0)
                            {
                                movements.Add(new Vector2(1,0));
                            }
                            else
                            {
                                movements.Add(new Vector2(-1,0));
                            }
                        }
                        else
                        {
                            if (distance.y > 0)
                            {
                                movements.Add(new Vector2(0,1));
                            }
                            else
                            {
                                movements.Add(new Vector2(0,-1));
                            }
                        }

                        startTouch = Input.touches[0].position;
                    }
                }
            }
            #endregion

            if (!moving)
            {
                rigidbody2d.velocity = Vector2.zero;

                if (movements.Count > 0)
                {
                    horizontal = movements[0].x;
                    vertical = movements[0].y;
                    movements.RemoveAt(0);
                }
                else
                {
                    horizontal = 0;
                    vertical = 0;
                }
            }

            rigidbody2d.velocity += new Vector2(velocity * horizontal, velocity * vertical);
        }
    }

    public void Die()
    {
        StartCoroutine(WaitForRestart());
    }

    IEnumerator WaitForRestart()
    {
        rigidbody2d.velocity = Vector2.zero;
        var movementsSaved = movements.Count;
        for (int i = 0; i < movementsSaved; i++)
        {
            movements.RemoveAt(0);
        }
        caught = true;

        yield return new WaitForSeconds(1.5f);

        transform.position = startPosition;
        caught = false;
        var inventory = GetComponent<Inventory>();
        if (inventory.HasKey())
            inventory.ReturnKey();
    }
}

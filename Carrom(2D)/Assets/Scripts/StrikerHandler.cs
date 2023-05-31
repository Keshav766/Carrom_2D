using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StrikerHandler : MonoBehaviour
{
    [SerializeField] float maxPullDistance = 2f;               
    [SerializeField] float maxForce = 10f;                     
    [SerializeField] float repositionDelay = 1f;               
    [SerializeField] Rigidbody2D strikerRigidbody;             
    [SerializeField] Transform scalingCircle;                  
    [SerializeField] Slider sliderForStriker;
    [SerializeField] Vector2 startPosition;
    [SerializeField] GameObject GameManagerRef;
    
    Vector2 endPosition;                     
    bool isDragging = false;                 
    bool isShotTaken = false;                
    float shotTimer = 0f;
    GameManager gameManagerScriptRef;

    void OnEnable()
    {
        transform.position = startPosition;
        sliderForStriker.onValueChanged.AddListener(StrikerXPos);
        gameManagerScriptRef = GameManagerRef.GetComponent<GameManager>();
    }

     void StrikerXPos(float xValue)
    {
        transform.position = new Vector2(xValue, startPosition.y);
    }

    void Update()
    {
            if (isShotTaken)
            {
                shotTimer += Time.deltaTime;
                if (shotTimer >= repositionDelay)
                {
                    isShotTaken = false;
                    shotTimer = 0f;
                    RepositionStriker();
                    
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    if (IsMouseOverStriker(mousePosition))
                    {
                        isDragging = true;
                    }
                }
                else if (Input.GetMouseButtonUp(0) && isDragging)
                {
                    isDragging = false;
                    ApplyForceToStriker();
                    ResetScalingCircle();
                    isShotTaken = true;
                }

                if (isDragging)
                {
                    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    endPosition = GetClampedEndPosition(mousePosition);
                    UpdateScalingCircle();
                }
            }
    }

    bool IsMouseOverStriker(Vector2 mousePosition)
    {
        Collider2D collider = GetComponent<Collider2D>();
        return collider == Physics2D.OverlapPoint(mousePosition);
    }

    Vector2 GetClampedEndPosition(Vector2 mousePosition)
    {
        Vector2 direction = mousePosition - startPosition;
        float distance = Mathf.Clamp(direction.magnitude, 0f, maxPullDistance);
        return startPosition + direction.normalized * distance;
    }

    void ApplyForceToStriker()
    {
        Vector2 direction = (startPosition - endPosition).normalized;
        float forceMagnitude = Vector2.Distance(startPosition, endPosition) * maxForce;
        Vector2 force = direction * forceMagnitude;
        strikerRigidbody.AddForce(force, ForceMode2D.Impulse);
    }

    void UpdateScalingCircle()
    {
        scalingCircle.up = (endPosition - startPosition).normalized;
        float scaleFactor = Vector2.Distance(startPosition, endPosition) / maxPullDistance;
        scalingCircle.localScale = Vector3.one * scaleFactor;
    }

    void ResetScalingCircle()
    {
        scalingCircle.localScale = Vector3.zero;
    }

    void RepositionStriker()
    {
        strikerRigidbody.velocity = Vector2.zero;
        strikerRigidbody.angularVelocity = 0f;
        strikerRigidbody.position = startPosition;
        gameManagerScriptRef.playerTurn = false;
    }
}
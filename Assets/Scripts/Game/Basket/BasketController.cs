using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketController : MonoBehaviour
{
    #region Variables
    private RandomColorPick basketCurrentColor; //Basket Color

    private Vector3 startInputPos;  // Startpos (mose/touch)
    private Vector3 basketStartPos; // Basket start position
    private bool isDragging = false;

    private float screenWidth;  // Screen width in world coordinates
    private float halfBasketWidth;  // Half the width of the basket

    [SerializeField]
    private float timeBetweenColorChange; //Time between color change

    private float nextColorChangeTime = 5; //Time when next color Change

    [SerializeField]
    private GameObject popAudio;
    #endregion

    #region MonoBehaviourFunctions
    void Start()
    {
        // Define screen width and basket
        screenWidth = Camera.main.orthographicSize * Camera.main.aspect;
        halfBasketWidth = GetComponent<SpriteRenderer>().bounds.extents.x;

        //Take Basket component RandomColorPick for color change
        basketCurrentColor = gameObject.GetComponent<RandomColorPick>();
    }

    void Update()
    {
        if (GM.instanse.gameOnPause)
        {
            return;
        }
        RandomColorChange();

#if UNITY_IOS || UNITY_ANDROID
        HandleTouchInput();  // Call a method to handle touches on mobile devices
#else
        HandleMouseInput();  // Call a method to control the mouse on a PC
#endif
    }
    #endregion

    #region InputFunction
    // Method for mouse control (PC)
    void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Start dragging
            isDragging = true;
            startInputPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            basketStartPos = transform.position;
        }

        if (Input.GetMouseButton(0) && isDragging)
        {
            // Drag and drop the basket
            Vector3 currentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 offset = currentPos - startInputPos;
            Vector3 newBasketPos = new Vector3(basketStartPos.x + offset.x, basketStartPos.y, basketStartPos.z);

            // Limit by horizontal screen boundaries
            newBasketPos.x = Mathf.Clamp(newBasketPos.x, -screenWidth + halfBasketWidth, screenWidth - halfBasketWidth);

            transform.position = newBasketPos;
        }

        if (Input.GetMouseButtonUp(0))
        {
            // End drag
            isDragging = false;
        }
    }

    // Method for touch control (mobile devices)
    void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                // Start dragging
                isDragging = true;
                startInputPos = Camera.main.ScreenToWorldPoint(touch.position);
                basketStartPos = transform.position;
            }

            if (touch.phase == TouchPhase.Moved && isDragging)
            {
                // Drag and drop the basket
                Vector3 currentPos = Camera.main.ScreenToWorldPoint(touch.position);
                Vector3 offset = currentPos - startInputPos;
                Vector3 newBasketPos = new Vector3(basketStartPos.x + offset.x, basketStartPos.y, basketStartPos.z);

                // Limit by horizontal screen boundaries
                newBasketPos.x = Mathf.Clamp(newBasketPos.x, -screenWidth + halfBasketWidth, screenWidth - halfBasketWidth);

                transform.position = newBasketPos;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                // End drag
                isDragging = false;
            }
        }
    }
    #endregion

    #region OnTriggerEnter2D
    //Touch tracking with shapes
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Take the Random ColorPick component from the touched object
        RandomColorPick target = collision.GetComponent<RandomColorPick>();
        //Checking if the color of the object that was touched is the same
        if (target.currentName == basketCurrentColor.currentName)
        {
            //add score if currentName == basketCurrentColor.currentName
            Destroy(collision.gameObject);
            Instantiate(popAudio);
            GM.instanse.score++;
        }
        else
        {
            //remove score if currentName == basketCurrentColor.currentName
            Destroy(collision.gameObject);
            Instantiate(popAudio);
            GM.instanse.score--;
        }
    }
    #endregion

    #region RandomColorChange
    private void RandomColorChange()
    {
        if (Time.time > nextColorChangeTime)
        {
            basketCurrentColor.ColorPick();

            nextColorChangeTime = Time.time + timeBetweenColorChange;
        }
    }
    #endregion
}

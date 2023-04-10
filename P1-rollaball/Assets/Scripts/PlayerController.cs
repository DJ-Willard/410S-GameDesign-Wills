using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public float jumpForce = 25.0f;

    private Rigidbody rb;
    private float movementX;
    private float movementY;
    private int count;
    private bool hasJumped = false;
    private bool hasDoublejumped = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //init count to zero
        count = 0;
        //check win condition
        SetCountText();
        //set text property of the Win Text UI
        winTextObject.SetActive(false);
    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Ground collision detected");
            hasJumped = false;
            hasDoublejumped = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!hasJumped)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                hasJumped = true;
            }
            else if (!hasDoublejumped)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                hasDoublejumped = true;
            }
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if (count >= 16)
        {
            // Set the text value of your 'winText'
            winTextObject.SetActive(true);
        }
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            //add one to the score variable 'count'
            count = count + 1;
            //Run the SetCountText() funstion to see it win
            SetCountText();
        }

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    private float maxSpeed;
    bool facingRight;
    Rigidbody2D myBody;
    Animator myAnimator;

    [SerializeField] private AudioSource jumpSound;

    // ===============================
    private bool isGrounded; // Tạo ra một biến để kiểm tra xem đuối tượng đã "chạm đất" hay chưa
    private int jumpCount; // Biến đếm số lần nhảy
    private bool canJump; // Biến kiểm tra xem có thể nhảy hay không

    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        facingRight = true;
        // ================================
        // Đặt trường hợp ban đầu là đối tượng "đang ở" trên mặt đất và chưa có lần nhảy nào cả
        isGrounded = false;
        jumpCount = 0;
        canJump = true;
        maxSpeed = 5;
    }

    // Update is called once per frame
    void Update()
    {
        float move = Input.GetAxis("Horizontal");

        myAnimator.SetFloat("Speed", Mathf.Abs(move));
        // Thực hiện tạo di chuyển cho nhân vật
        myBody.velocity = new Vector2(move * maxSpeed, myBody.velocity.y);
        

        // Thực hiện quay mặt và thân
        if (move > 0 && !facingRight)
        {
            Flip();
        }
        else if (move < 0 && facingRight)
        {
            Flip();
        }

        Jump_Ground();
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    // Kiểm tra sự "va chạm" giữa đối tượng với mặt đất
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    // Trường hợp đối tượng không ở trên mặt đất
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void Jump_Ground()
    {
        if (isGrounded)
        {
            jumpCount = 0; // Đặt lại số lần nhảy về 0 khi đối tượng chạm đất
            canJump = true; // Cho phép nhảy
        }
        if (canJump && Input.GetKeyDown("space"))
        {
            jumpCount++;
            jumpSound.Play();
            if (jumpCount < 2)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector3(0, 10, 0);
            }

            if (jumpCount >= 2)
            {
                canJump = false; // Không cho phép nhảy nữa nếu đã nhảy đủ 2 lần
            }
        }
    }

}
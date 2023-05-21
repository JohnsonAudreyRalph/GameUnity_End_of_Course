using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private bool isGrounded; // Tạo ra một biến để kiểm tra xem đuối tượng đã "chạm đất" hay chưa
    private int jumpCount; // Biến đếm số lần nhảy
    private bool canJump; // Biến kiểm tra xem có thể nhảy hay không
    void Start()
    {
        // Debug.Log("Hello");
        // Đặt trường hợp ban đầu là đối tượng "đang ở" trên mặt đất và chưa có lần nhảy nào cả
        isGrounded = false;
        jumpCount = 0;
        canJump = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Nếu đối tượng đang chạm đất thì mới nhảy
        /*
        if (Input.GetKey("space") && isGrounded && jumpCount < 3)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector3(0, 10, 0);
            jumpCount++;
        }
        */
        if (isGrounded)
        {
            jumpCount = 0; // Đặt lại số lần nhảy về 0 khi đối tượng chạm đất
            canJump = true; // Cho phép nhảy
        }
        if (canJump && Input.GetKeyDown("space"))
        {
            jumpCount++;
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
}

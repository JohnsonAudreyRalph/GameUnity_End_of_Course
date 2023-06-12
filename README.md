# Bài tập lớn môn học: Lập trình Game 3D với Unity
***
## Thông tin về đề tài:
> Giáo viên hướng dẫn: ThS. Đỗ Duy Cốp

> Sinh viên thực hiện: Phạm Sỹ Quang

> Lớp:                 55KMT

> MSSV:                K195480106018
***
[Click vào đây để có thể tải và cài đặt](https://drive.google.com/file/d/1VXdVF_d1QBM52HeN76qbjzNCgxOTgKOt/view?usp=sharing)
## Thông tin về đề tài
### Mô tả về Game:
#### Cốt truyện Game

Game được xây dựng theo thể loại phiêu lưu mạo hiểm.
Nội dung game là một hành trình của RoboCop, trong quá trình phiêu lưu để tìm kiếm kho báu, cậu đã vô tình lạc vào trong một hang động dưới lòng đất. Game sẽ là quá trình RoboCop tìm kiếm kho báu, tránh đi những cạm bẫy và tìm đường quay trở lại mặt đât.
Cuộc hành trình đầy khó khăn của RoboCop sẽ như thế nào?
Những khó khăn, những cạm bẫy đầy chết chóc sẽ như thế nào?
Hãy giúp cho RoboCop cùng vượt qua hang động và tiến đến kho báu cho chính mình.

Hình ảnh giao diện ban đầu
![Giao diện vào game ban đầu](https://i.imgur.com/EeHRvLW.png)
#### Mô tả quá trình chơi Game:

Game khi bắt đầu. Người dùng cần bấm nút “START” để có thể vào giao diện chơi, người chơi sử dụng bàn phím để điều khiển nhân vật qua phía bên trái, phải, ngảy lên trên, khi người chơi di chuyển thì Camera sẽ tự động di chuyển theo người chơi. Người chơi điều khiển nhân vật để có thể tránh cạm bẫy và tiến về đích, trong quá trình tiến về đích, nếu người chơi vô tình va chạm vào nhưng chướng ngại thì sẽ chết và quay về điểm hồi sinh ban đầu.


### Logic chơi Game:
Logic chơi game khá là đơn giản, nhưng cũng cần sự khéo léo để người chơi có thể vượt qua những cạm bẫy đầy chết chóc. Cũng cần người chơi có đôi mắt tinh tường để có thể phát hiện ra các bậc thang, các đệm nhảy để nhân vật có thể tiếp tục tiến lên phía trước

Giao diện khi chơi game
![Giao diện khi chơi game](https://i.imgur.com/GaVwwsT.png)

Người chơi cần thật tinh mắt để có thể nhìn thấy bậc
![Người chơi cần thật tinh mắt để có thể nhìn thấy bậc](https://i.imgur.com/8Htgiu7.png)

Hình ảnh khi người chơi đến đích - qua màn
![Hình ảnh khi người chơi đến đích - qua màn](https://i.imgur.com/vm7o86T.png)

![Hỉnh ảnh ở màn 2](https://i.imgur.com/WZshYO2.png)

Hình ảnh kết thúc Game
![Hình ảnh kết thúc Game](https://i.imgur.com/7UiZXCg.png)

### Các module chính:
Các modul chính được sử dụng lần lượt là:
- Module **Kiểm tra nhân vật trên mặt đất**
```
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
```
- Module **Kiểm tra sự va chạm của nhân vật **
```
  private void OnCollisionEnter2D(Collision2D collision)
  {
      if (collision.gameObject.CompareTag("Trap"))
      {
          Die();
      }
  }
  private void Die()
  {
      rb.bodyType = RigidbodyType2D.Static; // Đặt bodyType thành Static
      anim.SetTrigger("death"); // Kích hoạt trigger "death" trong Animator để chạy hoạt cảnh chết
      soundDeath.Play();
  }
  private void RestartLevel()
  {
      SceneManager.LoadScene(SceneManager.GetActiveScene().name);
      Debug.Log("Chào mừng bạn về nhà");
  }
```
- Module **Qua màn**
```
  private AudioSource next_LeverSound;
  private void Start()
  {
      next_LeverSound = GetComponent<AudioSource>();
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
      if(collision.gameObject.name =="Player")
      {
          next_LeverSound.Play();
          Invoke("CompleteLever", 2f);
      }
  }

  private void CompleteLever()
  {
      // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
      int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
      int sceneCount = SceneManager.sceneCountInBuildSettings;

      if (nextSceneIndex < sceneCount)
      {
          SceneManager.LoadScene(nextSceneIndex);
      }
      else
      {
          Debug.Log("Đã hoàn thành cấp độ cuối cùng");
      }    
  }
```
- Module **Camera đi theo người**
```
  public Transform target; // Vị trí của nhân vật đang đứng
  private float smoothing; // Làm cho camera trong quá trình quay trở nên mượt mà hơn
  Vector3 offset; // Vị trí từ nhân vật đến vị trí của Camera
  float lowY; // Trường hợp khi nhân vật rơi xuống quá vị trí quy định ==> Camera sẽ không đi theo nữa
  // Start is called before the first frame update
  void Start()
  {
      smoothing = 5;
      offset = transform.position - target.position;
      lowY = transform.position.y;
  }

  // Update is called once per frame
  void Update()
  {
      Vector3 targetCamPost = target.position + offset;

      transform.position = Vector3.Lerp(transform.position, targetCamPost, smoothing * Time.deltaTime);

      if (transform.position.y < lowY)
      {
          transform.position = new Vector3(transform.position.x, lowY, transform.position.z);
      }
  }
```
- Module **Lấy Iterm**
```
  private int Coins_Number = 0;
  [SerializeField] private Text Coins_Text;
  [SerializeField] private AudioSource jumpClolector;

  private void OnTriggerEnter2D(Collider2D collsion)
  {
      if (collsion.gameObject.CompareTag("coins"))
      {
          Destroy(collsion.gameObject);
          Coins_Number++;
          jumpClolector.Play();
          Debug.Log("Số coin: " + Coins_Number);
          Coins_Text.text = "COINS: " + Coins_Number;
      }
  }
```
### Các khó khăn đã vượt qua:
- Khó khăn trong việc xây dựng ý tưởng và thiết kế giao diện.
- Khó khăn trong việc thiết kế nhân vật.
### Kỹ thuật đã sử dụng:
- Sử dụng những kỹ thuật đã có sẵn và được Unity hỗ trợ như:
   Tạo giao diện UI
   Hiển thị giao diện dưới dạng 2D, 3D
   Hỗ trợ các Font chữ
   ....
### Ngồn ảnh và Âm thanh:
- Ảnh được Dowload từ trên mạng:
[Click vào đây để dowload ảnh](https://drive.google.com/drive/folders/1e4McbCOr-kRMb5ZYlQmQs8zUzvQ0Nrn9?usp=sharing)
- Âm thanh sử dụng âm thanh free từ assetstoreUity:
[Click vào đây để truy cập](https://assetstore.unity.com/?category=audio&price=0-0&orderBy=1)

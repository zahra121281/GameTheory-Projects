using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

public class Cat : MonoBehaviour
{
    public Animator animator;
    public DynamicPath dynamicPath;
    public Handlegame handlegame;

    public GameObject end;

    // تنظیمات لاین‌ها
    private float[] lanePositions = new float[3]; // موقعیت‌های چپ، وسط، راست
    private int currentLane = 1; // شروع از لاین وسط
    private bool isMoving = false; // برای جلوگیری از حرکت هم‌زمان

    void Start()
    {
        // تنظیم موقعیت لاین‌ها
        lanePositions[0] = -0.66f - 0.54f;
        lanePositions[1] = -0.66f;
        lanePositions[2] = -0.66f + 0.54f;

        // گرفتن Animator از آبجکت متصل‌شده
        animator = GetComponent<Animator>();
        animator.SetBool("jump", false);
        dynamicPath.record = 0;
        dynamicPath.Record.text = "0";
        animator.SetBool("GameOver", false);
        animator.SetBool("Start", true);
        

    }
    public void Starting() 
    {
        if (animator != null)
        {
            gameObject.transform.position=new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, lanePositions[1]);
            animator.Rebind(); // بازنشانی به حالت پیش‌فرض
            animator.Update(0); // آپدیت برای نمایش حالت پیش‌فرض
            animator.enabled = true;
        }
    }
    void Update()
    {
        // بررسی فشرده شدن کلید Space
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("jump", true);
            ChangeAnimationSpeed(1.5f, 1.1f); // سرعت عادی انیمیشن با مدت زمان مشخص
            print("Jump!");
        }

        // حرکت به چپ
        if (Input.GetKeyDown(KeyCode.LeftArrow) && !isMoving)
        {
            MoveToLane(currentLane - 1);
        }

        // حرکت به راست
        if (Input.GetKeyDown(KeyCode.RightArrow) && !isMoving)
        {
            MoveToLane(currentLane + 1);
        }
    }
   

    void ChangeAnimationSpeed(float speed, float duration)
    {
        animator.speed = speed; // تغییر سرعت
        StartCoroutine(ResetSpeed(duration));
    }

    System.Collections.IEnumerator ResetSpeed(float duration)
    {
        yield return new WaitForSeconds(duration);
        animator.speed = 1.0f; // بازگشت به سرعت عادی
    }

    void MoveToLane(int targetLane)
    {
        // بررسی اینکه لاین هدف معتبر باشد
        if (targetLane < 0 || targetLane >= lanePositions.Length)
            return;

        isMoving = true; // جلوگیری از حرکت هم‌زمان

        // موقعیت جدید لاین (فقط محور z تغییر می‌کند)
        Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y, lanePositions[targetLane]);

        // جهت چرخش گربه
        float targetRotationY = targetLane > currentLane ? 15f : -15f;

        // شروع حرکت دقیق به لاین
        StartCoroutine(SmoothMoveWithRotation(targetPosition, targetRotationY));

        currentLane = targetLane; // به‌روزرسانی لاین فعلی
    }

    System.Collections.IEnumerator SmoothMoveWithRotation(Vector3 targetPosition, float targetRotationY)
    {
        float speed = 7f; // سرعت حرکت
        float rotationSpeed = 300f; // سرعت چرخش
        Quaternion initialRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0, targetRotationY, 0);

        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            // حرکت گربه به موقعیت هدف
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            // چرخش گربه
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            yield return null;
        }

        // تنظیم موقعیت نهایی
        transform.position = targetPosition;

        // بازگشت به حالت صاف
        StartCoroutine(ResetRotation(initialRotation));

        isMoving = false; // اجازه حرکت مجدد
    }

    System.Collections.IEnumerator ResetRotation(Quaternion initialRotation)
    {
        float rotationResetSpeed = 300f; // سرعت بازگشت چرخش
        while (Quaternion.Angle(transform.rotation, initialRotation) > 0.1f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, initialRotation, rotationResetSpeed * Time.deltaTime);
            yield return null;
        }

        // بازگشت به حالت صاف نهایی
        transform.rotation = initialRotation;
    }

    // مدیریت برخورد
    private void OnCollisionEnter(Collision collision)
    {
        // بررسی برخورد با هر چیزی به جز زمین
        if (!collision.gameObject.CompareTag("Ground"))
        {
            // توقف حرکت مسیر
            if (dynamicPath != null)
            {
                dynamicPath.StopMovement();
            }
            dynamicPath.record = dynamicPath.record > dynamicPath.score ? dynamicPath.record : dynamicPath.score;
            dynamicPath.Record.text = dynamicPath.record.ToString();
            handlegame.Record.text = dynamicPath.record.ToString();
            // خاموش کردن تمام انیمیشن‌ها
            animator.enabled = false;
            //start.SetActive(false);
            end.SetActive(true);
        }
    }
}

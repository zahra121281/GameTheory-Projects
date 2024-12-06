using UnityEngine;
using System.Collections.Generic;
using UnityEditor.Rendering;
using TMPro;
using Unity.VisualScripting;
using System;
using NUnit;
using UnityEngine.SceneManagement;

public class DynamicPath : MonoBehaviour
{
    [Header("Path Block Settings")]
    public GameObject blockPrefab; // Prefab بلاک مسیر
    public int initialBlocks = 15; // تعداد بلاک‌هایی که در ابتدا تولید می‌شوند
    public float blockWidth = 5f; // عرض هر بلاک
    public float blockLength = 12f; // طول هر بلاک
    public float moveSpeed = 0.7f; // سرعت حرکت مسیر
    public float delayBeforeMovement = 4.5f; // تاخیر قبل از شروع حرکت

    [Header("Environment Objects")]
    public GameObject treePrefab1; // Prefab درخت نوع 1
    public GameObject treePrefab2; // Prefab درخت نوع 2
    public GameObject rockPrefab1; // Prefab سنگ نوع 1
    public GameObject rockPrefab2; // Prefab سنگ نوع 2
    public GameObject fencePrefab; // Prefab فنس

    public int maxTreesPerOuterLane = 12; // تعداد حداکثر درختان در هر لاین اطراف
    public int maxRocksPerOuterLane = 4; // تعداد حداکثر سنگ‌ها در هر لاین اطراف
    public int maxObjectsInMiddleLane = 1; // تعداد محدود سنگ‌ها و درخت‌ها در لاین‌های وسط

    private Queue<GameObject> activeBlocks = new Queue<GameObject>(); // لیست بلاک‌های فعال
    private Vector3 nextSpawnPosition = new Vector3(0, 0, 0); // مکان ایجاد بلاک بعدی
    private bool isMoving = false; // آیا مسیر حرکت می‌کند؟

    public int score = 0; // امتیاز

    public TextMeshPro Score;
    public TextMeshPro Speed;
    public TextMeshPro Record;
    private float distanceCovered = 0f;
    public AudioSource backgroundMusic; // متغیر برای AudioSource

    public int record;
    
    public void starting()
    {
        score = 0;
        moveSpeed = 0.7f;
        Speed.text= Math.Round(moveSpeed * 10).ToString();
        Score.text= score.ToString();
        activeBlocks.Clear();
        nextSpawnPosition = new Vector3(0, 0, 0);
        isMoving = false;
        
        for (int i = 0; i < 1; i++)
        {
            SpawninitBlock();
        }
        for (int i = 2; i < initialBlocks; i++)
        {
            SpawnBlock();
        }
        
        Invoke(nameof(StartMovement), delayBeforeMovement);
        Invoke(nameof(PlayMusic), delayBeforeMovement);
    }
    public void ClearCanvasContent(GameObject canvas)
    {
        // پاک کردن تمام فرزندان داخل Canvas
        foreach (Transform child in canvas.transform)
        {
            if(child.tag=="Ground")
                Destroy(child.gameObject);
        }
    }


    
    void Start()
    {
        record = 0;
    }
    void PlayMusic()
    {
        if (backgroundMusic != null)
        {
            backgroundMusic.Play(); // پلی کردن موسیقی
        }
    }
    void Update()
    {
        if (isMoving)
        {
            MovePath();
            // محاسبه مسافت طی‌شده
            distanceCovered += moveSpeed * Time.deltaTime;

            // افزودن امتیاز بر اساس مسافت
            if (distanceCovered >= blockWidth)
            {
                AddScore(1); // افزودن 1 امتیاز
                Score.text = score.ToString();
                record = record >score ? record : score;
                Record.text = record.ToString();
                distanceCovered -= blockWidth; // کم کردن عرض بلاک از مسافت طی‌شده
            }
            if (activeBlocks.Count > 0 && activeBlocks.Peek().transform.position.x > blockWidth * (initialBlocks / 2))
            {
                Destroy(activeBlocks.Dequeue());
                SpawnBlock();
            }
            ReduceSpeedOverTime();
            Speed.text= Math.Round(moveSpeed*10).ToString();
        }
    }
    void ReduceSpeedOverTime()
    {
        float maxSpeed = 7f; // حداقل سرعتی که مسیر می‌تواند داشته باشد
        float speedReductionRate = 0.001f; // نرخ کاهش سرعت در هر فریم

        if (moveSpeed > maxSpeed)
        {
            moveSpeed += speedReductionRate * Time.deltaTime; // کاهش سرعت با توجه به زمان
            moveSpeed = Mathf.Max(moveSpeed, maxSpeed); // اطمینان از نرسیدن به کمتر از حداقل سرعت
        }
    }

    void StartMovement()
    {
        isMoving = true;
    }

    public void StopMovement()
    {
        isMoving = false;
        moveSpeed = 0f; // توقف حرکت
    }

    void SpawnBlock()
    {
        Vector3 spawnPosition = activeBlocks.Count > 0
            ? activeBlocks.ToArray()[activeBlocks.Count - 1].transform.position - new Vector3(blockWidth, 0, 0)
            : nextSpawnPosition;

        GameObject newBlock = Instantiate(blockPrefab, spawnPosition, Quaternion.identity, gameObject.transform);
        activeBlocks.Enqueue(newBlock);
        PopulateBlock(newBlock);
    }
    void SpawninitBlock()
    {
        Vector3 spawnPosition = activeBlocks.Count > 0
            ? activeBlocks.ToArray()[activeBlocks.Count - 1].transform.position - new Vector3(blockWidth, 0, 0)
            : nextSpawnPosition;

        GameObject newBlock = Instantiate(blockPrefab, spawnPosition, Quaternion.identity,gameObject.transform);
        activeBlocks.Enqueue(newBlock);
    }

    void MovePath()
    {
        foreach (GameObject block in activeBlocks)
        {
            block.transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
        }
    }

    void PopulateBlock(GameObject block)
    {
        // افزودن درخت‌ها، سنگ‌ها و اشیاء محیطی
        for (int lane = 0; lane <= 4; lane++)
        {
            if (lane == 0 || lane == 4) // لاین‌های اطراف
            {
                int treeCount = UnityEngine.Random.Range(maxTreesPerOuterLane - 2, maxTreesPerOuterLane + 1);
                for (int i = 0; i < treeCount; i++)
                {
                    Vector3 randomPosition = GetRandomPositionInLane(lane);
                    GameObject newTree = Instantiate(treePrefab2, block.transform.position + randomPosition, Quaternion.identity, block.transform);
                    newTree.transform.localScale = new Vector3(0.5f, 0.5f, 0.25f);
                }

                int rockCount = UnityEngine.Random.Range(maxRocksPerOuterLane - 1, maxRocksPerOuterLane + 1);
                for (int i = 0; i < rockCount; i++)
                {
                    Vector3 randomPosition = GetRandomPositionInLane(lane);
                    GameObject newRock = Instantiate(UnityEngine.Random.value > 0.5f ? rockPrefab1 : rockPrefab2, block.transform.position + randomPosition, Quaternion.identity, block.transform);
                    newRock.transform.localScale = Vector3.one * 0.3f;
                }
            }
            else // لاین‌های وسط
            {
                int objectCount = UnityEngine.Random.Range(0, maxObjectsInMiddleLane + 1);
                for (int i = 0; i < objectCount; i++)
                {
                    Vector3 randomPosition = GetRandomPositionInLane(lane);
                    print(randomPosition.z);
                    GameObject randomObject = treePrefab1;
                    GameObject newObject = Instantiate(randomObject, block.transform.position-new Vector3(0,0,block.transform.position.z) + randomPosition, Quaternion.identity, block.transform);
                    print(newObject.transform.position.z+"ح");
                    newObject.transform.localScale = new Vector3(0.7f,0.7f,0.4f);
                    print(newObject.transform.position.z + "حههههههه");
                }
            }
        }

        // افزودن فنس‌ها
        AddFences(block);
    }

    void AddFences(GameObject block)
    {
        float fenceHeight = 0.25f; // ارتفاع فنس از سطح زمین
        float fenceScale = 0.08f; // مقیاس فنس‌ها

        // فنس‌ها در ابتدای لاین 0
        for (int i = 0; i < 18; i++)
        {
            float offset = i * (blockWidth / 16f); // فاصله بین فنس‌ها
            Vector3 fencePositionLane0 = block.transform.position + new Vector3(-blockWidth / 2f + offset, fenceHeight, -block.transform.position.z + GetStartOrEndLane(0));
            GameObject newFence0 = Instantiate(fencePrefab, fencePositionLane0, Quaternion.identity, block.transform);
            newFence0.transform.localScale = Vector3.one * fenceScale;
        }

        // فنس‌ها در انتهای لاین 4
        for (int i = 0; i < 18; i++)
        {
            float offset = i * (blockWidth / 16f); // فاصله بین فنس‌ها
            Vector3 fencePositionLane4 = block.transform.position + new Vector3(-blockWidth / 2f + offset, fenceHeight, -block.transform.position.z + GetStartOrEndLane(4));
            GameObject newFence4 = Instantiate(fencePrefab, fencePositionLane4, Quaternion.identity, block.transform);
            newFence4.transform.localScale = Vector3.one * fenceScale;
        }
    }


    Vector3 GetRandomPositionInLane(int lane)
    {
        float totalWidth = blockLength;
        float laneWidth;
        float laneStart;

        if (lane == 0 || lane == 4)
        {
            laneWidth = (totalWidth / 11f * 4f);
            laneStart = lane == 0 ? -totalWidth / 2f : totalWidth - laneWidth - totalWidth / 2f;
        }
        else
        {
            laneWidth = totalWidth / 11f;
            laneStart = (lane - 1) * laneWidth + (totalWidth / 11f * 4f) - totalWidth / 2f;
        }

        float randomZ = UnityEngine.Random.Range(laneStart+0.3f, laneStart-0.3f + laneWidth);
        if (lane == 1)
        {
            randomZ = -0.66f - 0.54f + 0.52f;
        }
        else if (lane == 2)
        {
            randomZ = -0.66f + 0.6f;
        }
        else if (lane == 3)
        {
            randomZ = -0.66f + 0.54f+0.66f;
        }
        float randomX = UnityEngine.Random.Range(-blockWidth / 2, blockWidth / 2);

        return new Vector3(randomX, 0, randomZ);
    }
    float GetStartOrEndLane(int lane)
    {
        float totalWidth = blockLength;
        float laneWidth;
        float laneStart;
        if (lane == 0)
        {
            laneWidth = (totalWidth / 11f * 4f);
            laneStart = lane == 0 ? -totalWidth / 2f : totalWidth - laneWidth - totalWidth / 2f;
            return laneStart + laneWidth;
        }
        else
        {
            laneWidth = (totalWidth / 11f * 4f);
            laneStart = lane == 0 ? -totalWidth / 2f : totalWidth - laneWidth - totalWidth / 2f;
            return laneStart ;
        }
    }
    void AddScore(int points)
    {
        score += points;
        Debug.Log("Score: " + score);
    }

}

using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Handlegame : MonoBehaviour
{
    
    Animator animator;
    public GameObject start;
    public GameObject end;
    public GameObject game;
    public TextMeshPro Record;
    public DynamicPath dynamicPath;
    public Cat cat;

    void Start()
    {
        dynamicPath.record = 0;
        SetParentActive(start, true);
        SetParentActive(end, false);
        SetParentActive(game, false);

    }
    void Update()
    {
        
    }
    public void StartGame()
    {
        dynamicPath.ClearCanvasContent(game);
        cat.Starting();
        dynamicPath.starting();
        
        SetParentActive(start, false);
        SetParentActive(game, true);
        SetParentActive(end, false);
    }
    public void endGame()
    {
        SetParentActive(start,true);
        SetParentActive(end,false);
        SetParentActive(game, false);
    }
    public void SetParentActive(GameObject parent, bool isActive)
    {
        // گرفتن تمام Rigidbody‌ها و Mesh Collider‌ها در زیرمجموعه
        Rigidbody[] rigidbodies = parent.GetComponentsInChildren<Rigidbody>();
        MeshCollider[] meshColliders = parent.GetComponentsInChildren<MeshCollider>();

        if (!isActive)
        {
            // زمانی که والد غیرفعال می‌شود
            foreach (var rb in rigidbodies)
            {
               
                if (rb.gameObject.CompareTag("Tree") || rb.gameObject.CompareTag("Rock"))
                {
                    rb.isKinematic = true; // همه Rigidbody‌ها را Kinematic کنید
                    rb.useGravity = true; // Gravity را غیرفعال کنید
                }
                //else
                //{
                //    rb.isKinematic = true; // دیگر Rigidbody‌ها را فعال کنید
                //    rb.useGravity = false; // Gravity برای آنها فعال باشد
                //}
            }

            //foreach (var collider in meshColliders)
            //{
            //    collider.convex = true; // تبدیل Mesh Collider به Convex
            //}
        }
        else
        {
            // زمانی که والد فعال می‌شود
            foreach (var rb in rigidbodies)
            {
                // بررسی اینکه آیا آبجکت مربوط به درخت یا سنگ است
                if (rb.gameObject.CompareTag("Tree") || rb.gameObject.CompareTag("Rock"))
                {
                    rb.isKinematic = true; // درخت‌ها و سنگ‌ها نیازی به Kinematic ندارند
                    rb.useGravity = true; // Gravity برای آنها غیرفعال باشد
                }
                //else
                //{
                //    rb.isKinematic = true; // دیگر Rigidbody‌ها را فعال کنید
                //    rb.useGravity = false; // Gravity برای آنها فعال باشد
                //}
            }

            //foreach (var collider in meshColliders)
            //{
            //    collider.convex = true; // مطمئن شوید که Mesh Collider‌ها همچنان Convex هستند
            //}
        }

        // فعال یا غیرفعال کردن والد
        parent.SetActive(isActive);
    }

}
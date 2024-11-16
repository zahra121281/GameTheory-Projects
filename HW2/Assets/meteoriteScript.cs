using UnityEngine;

public class meteoriteScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float moveSpeed = 5;
    // public float deadZone = -45;
 
    // Start is called before the first frame update
    void Start()
    {
        
    }
 
    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (Vector3.left *100* moveSpeed) * Time.deltaTime;
        if (transform.position.x <-45)
        {
            Destroy(gameObject);
        }

    }
    
}

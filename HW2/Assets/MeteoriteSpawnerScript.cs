using UnityEngine;

public class MeteoriteSpawnerScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject meteorite ;
    public float spawnRate = 2;
    private float timer = 0;
    public float heightOffset = 5000;
 
    // Start is called before the first frame update
    void Start()
    {
        spawnMeteorite();
    }
 
    // Update is called once per frame
    void Update()
    {
        if (timer < spawnRate)
        {
            timer = timer + Time.deltaTime;
        }
        else
        {
            spawnMeteorite();
            timer = 0;
        }
 
    }
 
    void spawnMeteorite()
    {
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;
 
        Instantiate(meteorite, new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0), transform.rotation);
    }
}

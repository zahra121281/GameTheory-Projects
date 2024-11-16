using UnityEngine;

public class RockspawnerScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject rock ;
    // public float spawnRate = 1;
    private float timer = 0;
 
    // Start is called before the first frame update
    void Start()
    {
        SpawnRock();
    }
 
    // Update is called once per frame
    void Update()
    {
        // if (timer < spawnRate)
        // {
        //     timer = timer + Time.deltaTime;
        // }
        // else
        // {
            SpawnRock();
        //     timer = 0;
        // }
 
    }
 
    void SpawnRock()
    {

 
        Instantiate(rock, transform.position, transform.rotation);
    }
}

using UnityEngine;

public class StartScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
public GameObject obj;
    public SpaceShipComponent spaceShipComponent;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            obj.SetActive(true);
            gameObject.SetActive(false);
            spaceShipComponent.playerScore = 0;
            spaceShipComponent.Speed = 1;

        }

    }
}

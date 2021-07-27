using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] SpriteRenderer sr;
    [SerializeField] float jumpForce = 10f;
    [SerializeField] Color[] colours;
    Dictionary<Color, string> dict;
    [SerializeField] string currentColour;
    [SerializeField] SceneLoader sceneLoader;
    [SerializeField] GameObject colourChangerPrefab;
    [SerializeField] GameObject circlePrefab;
    bool check = true;

    int numberOfCollisions = 0;
    void Start()
    {
        SetStartColour();
        Instantiate(colourChangerPrefab);
        Instantiate(circlePrefab);
        dict = new Dictionary<Color, string>();
        dict[colours[0]] = "Blue";
        dict[colours[1]] = "Yellow";
        dict[colours[2]] = "Pink";
        dict[colours[3]] = "Purple";
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            rb.velocity = Vector2.up * jumpForce;
        }
    }
    private void SetStartColour()
    {
        int rand = Random.Range(0, colours.Length);
        sr.color = colours[rand];
        switch (rand)
        {
            case 0:
                currentColour = "Blue";
                break;
            case 1:
                currentColour = "Yellow";
                break;
            case 2:
                currentColour = "Pink";
                break;
            case 3:
                currentColour = "Purple";
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string collisionName = collision.gameObject.tag;
        if (collisionName == "ColourChanger")
        {
            sr.color = collision.gameObject.GetComponent<SpriteRenderer>().color;
            currentColour = dict[sr.color];
            Vector2 currentChangePos = collision.gameObject.transform.position;
            Vector2 deltaY = new Vector2(0f, Random.Range(7f,10f));
            Vector2 newPos = currentChangePos + deltaY;
            Destroy(collision.gameObject);
            Instantiate(colourChangerPrefab,new Vector3(newPos.x,newPos.y,0),Quaternion.identity);
            check = true;

        }
        else if (collisionName != currentColour)
        {
            sceneLoader.LoadNextScene();
        }
        else
        {
            numberOfCollisions++;
            
            if(numberOfCollisions == 2 && check)
            {
                Vector2 currentCirclePos = collision.gameObject.GetComponentInParent<Transform>().position;
                Vector2 deltaY = new Vector2(0f, Random.Range(5f,7f));
                Vector2 newPos = currentCirclePos + deltaY;
                Instantiate(circlePrefab,new Vector3(newPos.x,newPos.y,0),Quaternion.identity);
                numberOfCollisions = 0;
                check = false;
            }
            if (numberOfCollisions > 2)
                numberOfCollisions = 0;
        }

    }
    private void OnBecameInvisible()
    {
        sceneLoader.LoadNextScene();
    }
}

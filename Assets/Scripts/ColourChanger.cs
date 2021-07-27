
using UnityEngine;

public class ColourChanger : MonoBehaviour
{
    [SerializeField] Color[] colours;
    [SerializeField] SpriteRenderer sr;
    void Start()
    {
        sr.color = colours[Random.Range(0, colours.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

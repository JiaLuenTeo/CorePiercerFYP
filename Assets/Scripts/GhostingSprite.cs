
using System.Collections;
using UnityEngine;



public class GhostingSprite : MonoBehaviour
{
    public GameObject trailPart;
    Vector3 offset;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            SpawnTrail();
          
        }
    }



    void SpawnTrail()
    {
        GameObject trailPart = new GameObject();
        SpriteRenderer trailPartRenderer = trailPart.AddComponent<SpriteRenderer>();
        trailPartRenderer.sprite = GetComponent<SpriteRenderer>().sprite;
        trailPart.transform.position = transform.position;
        trailPart.transform.rotation = transform.rotation;
        Destroy(trailPart, 0.5f); 
        StartCoroutine("FadeTrailPart", trailPartRenderer);  
    }

    IEnumerator FadeTrailPart(SpriteRenderer trailPartRenderer)
    {
        Color color = trailPartRenderer.color;
        color.a -= 0.5f;
        trailPartRenderer.color = color;
        yield return new WaitForEndOfFrame();
    }
}

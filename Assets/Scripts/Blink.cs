using UnityEngine;
using System.Collections;

public class Blink : MonoBehaviour {

    private Renderer renderer;
    public bool isBlinking;

    // Use this for initialization
    void Start()
    {
        renderer = this.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.B))
        //{
        //    StartBlinking(1f, 0.05f);
        //}
    }

    public void StartBlinking(float duration, float interval)
    {
        isBlinking = true;
        StartCoroutine(BlinkCoroutine(duration, interval));
    }

    private IEnumerator BlinkCoroutine(float duration, float interval)
    {
        int loopCount = (int)(duration / interval);

        for (int i = 0; i < loopCount; ++i)
        {
            if (i % 2 == 0)
            {
                // Turn off the renderer
                renderer.enabled = false;
            }
            else
            {
                // Turn on the renderer
                renderer.enabled = true;
            }

            yield return new WaitForSeconds(interval);
        }

        renderer.enabled = true;
        isBlinking = false;
    }
}

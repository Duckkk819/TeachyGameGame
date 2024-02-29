using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cord : MonoBehaviour
{
    [SerializeField] public LineRenderer lineRenderer;
    [SerializeField] float speed;

    private static Cord _instance = null;
    public static Cord Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void Launch(GameObject launchedFrom, Vector2 target)
    {
        StartCoroutine(LaunchCoroutine(launchedFrom, target));
    }

    public void Render(Vector2 startPos, Vector2 endPos, Color color)
    {
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);
    }

    private IEnumerator LaunchCoroutine(GameObject launchedFrom, Vector2 target)
    {
        Vector2 currentPos = launchedFrom.transform.position;

        while (currentPos != target)
        {
            Render(launchedFrom.transform.position, currentPos, Color.black);
            currentPos = Vector2.MoveTowards(currentPos, target, speed * Time.deltaTime);
            yield return null;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer.positionCount = 2;
        lineRenderer.useWorldSpace = true;
    }


}

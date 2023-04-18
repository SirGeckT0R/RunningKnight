using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [Range(-1f,1f)]
    [SerializeField] private float scrollSpeed= 0.5f;
    private float offset;
    private Renderer backgroundRenderer;

    private void Start()
    {
        backgroundRenderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        backgroundRenderer.material.mainTextureOffset += new Vector2(scrollSpeed * Time.deltaTime, 0f);
    }
}

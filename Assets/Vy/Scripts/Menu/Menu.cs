using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VyNS;

public class Menu : MonoBehaviour
{
    [SerializeField] private ObjectPool objectPool;
    [SerializeField] private RectTransform foreground;
    [SerializeField] private RectTransform background;
    [SerializeField] private float spawnRandomMinRange = 0.5f;
    [SerializeField] private float spawnRandomMaxRange = 1.5f;
    private Coroutine spawningCoroutine;

    public void Show()
    {
        StartSpawning();
    }

    public void Hide()
    {
        StopSpawning();
    }
    
    private void StartSpawning()
    {
        spawningCoroutine = StartCoroutine(Spawning());
    }

    public void OnPlayButtonPressed()
    {
        StopSpawning();
        GameController.Instance.SetState(GameController.Instance.LoadingState);
    }

    private IEnumerator Spawning()
    {
        while (true)
        {
            var bubble = GetBubble();
            bubble.transform.SetParent(Random.Range(0, 2) == 0 ? foreground : background);
            bubble.gameObject.SetActive(true);
            bubble.objectPool = objectPool;
            var delay = Random.Range(spawnRandomMinRange, spawnRandomMaxRange);
            yield return new WaitForSeconds(delay);
        }
    }
    
    private void StopSpawning()
    {
        if (spawningCoroutine != null)
        {
            StopCoroutine(spawningCoroutine);
            spawningCoroutine = null;
        }
    }

    private BubbleMovement GetBubble()
    {
        return objectPool.GetObject().GetComponent<BubbleMovement>();
    }
}

using UnityEngine;

public class Block : MonoBehaviour
{
    // configuration
    [SerializeField] AudioClip breakClip;
    [SerializeField] GameObject blockSparklesVfx;
    [SerializeField] int maxHits;
    [SerializeField] Sprite[] hitSprites;

    //cached references
    Level level;

    //state variables
    [SerializeField] int timesHit;

    private void Start()
    {
        CountBreakableBlocks();
    }

    private void CountBreakableBlocks()
    {
        level = FindObjectOfType<Level>();
        if (tag == "Breakable")
        {
            level.CountBlocks();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "Breakable")
        {
            HandleHit();
        }
    }

    private void HandleHit()
    {
        timesHit++;
        if (timesHit >= maxHits)
        {
            DestroyBlock();
        }
        else
        {
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite()
    {
        var spriteIndex = timesHit - 1;
        if (hitSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        else
        {
            Debug.LogError("Block sprie is missing from array " + gameObject.name);
        }
    }

    private void DestroyBlock()
    {
        PlayBlockDestroySfx();
        Destroy(gameObject);
        level.BlockDestroyed();
        TriggerSparklesVfx();
    }

    private void PlayBlockDestroySfx()
    {
        FindObjectOfType<GameSession>().AddToScore();
        AudioSource.PlayClipAtPoint(breakClip, Camera.main.transform.position);
    }

    private void TriggerSparklesVfx()
    {
        var sparkles = Instantiate(blockSparklesVfx, transform.position, transform.rotation);
        Destroy(sparkles, 2f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseCollider : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Died but why ??");
        Debug.Log(collision.gameObject.name);
        SceneManager.LoadScene("Game Over");
    }
}

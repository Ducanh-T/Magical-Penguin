using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    public LevelManager levelManager;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            levelManager.currentLevelMove += 3;
            UIManager.instance.UpdateTxtMaxMove(levelManager.currentLevelMove);
            Destroy(gameObject);
        }
    }
}

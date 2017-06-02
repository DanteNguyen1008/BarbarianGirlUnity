using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    [SerializeField]
    GameObject player;

    private bool isGameOver = false;

    public bool IsGameOver
    {
        get { return this.isGameOver; }
    }

    public GameObject Player
    {
        get { return this.player; }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad (gameObject);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayerHit(int currentHP)
    {
        if(currentHP > 0)
        {
            this.isGameOver = false;
        }
        else
        {
            this.isGameOver = true;
        }
    }
}

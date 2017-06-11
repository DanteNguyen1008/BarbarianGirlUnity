using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour {

    protected GameObject player;

    // Use this for initialization
    void Start()
    {
        this.player = GameManager.instance.Player;
        this.OnStart();
    }

    void Update()
    {
        if (isRotate())
        {
            this.transform.Rotate(0, 100 * Time.deltaTime, 0);
        }

        this.OnUpdate();
    }

    protected virtual void OnStart()
    {

    }

    protected virtual void OnUpdate()
    {

    }

    protected virtual void RegisterObject()
    {

    }

    protected virtual void UnRegisterObject()
    {

    }

    protected virtual bool isRotate()
    {
        return true;
    }
}

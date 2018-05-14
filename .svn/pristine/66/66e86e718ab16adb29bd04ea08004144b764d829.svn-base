using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class VPlayer : NetworkBehaviour {
    [SyncVar(hook = "OnChangeHealth")]
    public string info;
    Text txt;
	// Use this for initialization
	void Start () {
        txt = GetComponentInChildren<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        if (isServer)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                OnChangeHealth("shuihsuhuh");
                TakeDamage(2);
            }
        }
	}

    void OnChangeHealth(string info)
    {
        txt.text = info;  
    }

    [SyncVar]
   public  int health;

    [ClientRpc]
    void RpcDamage(int amount)
    {
        Debug.Log("Took damage:" + amount);
    }

    public void TakeDamage(int amount)
    {
        if (!isServer)
            return;

        health -= amount;
        RpcDamage(amount);
    }
}

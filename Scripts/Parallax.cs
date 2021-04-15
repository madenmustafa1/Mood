using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float speed;
    
    private Material mat;
    private float offset;
    private PlayerController player;
    private void Awake()
    {
        mat = GetComponent<Renderer>().material;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    
    void Update()
    {
        offset += Input.GetAxisRaw("Horizontal") * speed;
        if(player.leftClicked)
        {
            offset += -speed;

        }
       
        if (player.rightClicked)
        {
            offset += speed;

        }
        mat.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}

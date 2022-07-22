using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamAnim : MonoBehaviour
{
    private new Animation animation;
    void Start()
    {
        animation = GetComponent<Animation>();
    }
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        if (x != 0 || z != 0)
        {
            if (!animation.isPlaying) animation.Play();
        }
        

    }
}

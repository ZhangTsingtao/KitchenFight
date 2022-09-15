using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAnimation : MonoBehaviour
{
    private Animator anim;

    private bool animisJumping;
    private bool animisLanded;

    public PlayerMovementTutorial playerScript;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerScript.moveDirection != Vector3.zero)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

        if(Input.GetKey(KeyCode.Space) && playerScript.readyToJump > 1)
        {
            anim.SetBool("isJumping", true);
            animisJumping = true;
        }
        if (playerScript.grounded)
        {
            anim.SetBool("isJumping", false);
            anim.SetBool("isLanded", true);
            anim.SetBool("isAir", false);
            animisLanded = true;
            animisJumping = false;
        }
        else
        {
            anim.SetBool("isLanded", false);
            animisLanded = false;
            if (playerScript.rb.velocity.y < 0)
            {
                anim.SetBool("isAir", true);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField]
    private GameObject unity_chan;
    GameObject unityChan => unity_chan;

    GameObject GameObject => this.gameObject;
    Rigidbody Rigidbody => this.GetComponent<Rigidbody>();

    [SerializeField]
    private Animator player_animator;
    Animator playerAnimator => player_animator;

    public void PlayRun()
    {
        playerAnimator.Play("Running(loop)");
    }

    public void PlayStand()
    {
        playerAnimator.Play("Standing(loop)");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WolfVillageBattle.Interface;

public class EnemyView : MonoBehaviour, IEnemyView
{
    public GameObject GameObject => this.gameObject;
    public Rigidbody Rigidbody => this.GetComponent<Rigidbody>();
    public GameObject Body => throw new System.NotImplementedException();

    [SerializeField]
    private Animator animator;
    public Animator Animator => animator;

    public void PlayRun()
    {
        throw new System.NotImplementedException();
    }

    public void PlayStand()
    {
        throw new System.NotImplementedException();
    }

    public void PlayAttack()
    {
        throw new System.NotImplementedException();
    }

    public void PlayAttackSound()
    {
        throw new System.NotImplementedException();
    }

    public void PlayNotice()
    {
        animator.Play("Victory");
    }

    public void PlayOverlook()
    {
        animator.Play("SenseSomethingRPT");
    }

    public void PlayNoticeSound()
    {
        AudioManager.Instance.PlaySE("slime_found");
    }

    public void PlayOverlookSound()
    {
        throw new System.NotImplementedException();
    }
}

using UnityEngine;

public class PlayerMenuHeaderIconUI : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void SetOn()
    {
        _animator.SetBool("Select", true);
    }

    public void SetOff()
    {
        _animator.SetBool("Select", false);
    }

    public void Dispose()
    {
        
    }
}

using UnityEngine;

[RequireComponent(typeof(Entity))]
public class EntityAnimator : MonoBehaviour
{
    private Animator _animator;
    private Entity _entity;
    
    private static readonly int Die = Animator.StringToHash("Die");

    void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _entity = GetComponentInChildren<Entity>();

        _entity.OnDied += () => _animator.SetBool(Die, true);
    }
}

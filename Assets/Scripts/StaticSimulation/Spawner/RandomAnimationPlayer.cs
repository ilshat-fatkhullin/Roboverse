using UnityEngine;

namespace Assets.Scripts.StaticSimulation.Spawner
{
    [RequireComponent(typeof(Animator))]
    public class RandomAnimationPlayer : MonoBehaviour
    {
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            AnimationClip[] clips = _animator.runtimeAnimatorController.animationClips;
            _animator.SetFloat("Offset", Random.Range(0f, 1f));
            _animator.SetBool("Mirror", Random.Range(0, 2) == 0);
            _animator.Play(clips[Random.Range(0, clips.Length)].name);
        }
    }
}
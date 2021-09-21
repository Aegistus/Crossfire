using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Animator))]
public class AgentAnimation : MonoBehaviour
{
    public RuntimeAnimatorController defaultController;

    public event Action<AnimEvent> OnAnimationEvent;

    Agent agent;
    Animator anim;
    MultiDictionary<Type, int> animStates;

    private void Awake()
    {
        agent = GetComponentInParent<Agent>();
        agent.StateMachine.OnStateChange += UpdateAnimation;
        anim = GetComponent<Animator>();
        if (defaultController == null && anim.runtimeAnimatorController == null)
        {
            anim.runtimeAnimatorController = defaultController;
        }
        animStates = new MultiDictionary<Type, int>()
        {
            {typeof(Idling), Animator.StringToHash("Idling") },
            {typeof(Walking), Animator.StringToHash("Walking") },
            {typeof(InCoverIdling), Animator.StringToHash("In Cover Idling") },
            {typeof(Shooting), Animator.StringToHash("Shooting") },
            {typeof(Dying), Animator.StringToHash("Dying 01") },
            {typeof(Dying), Animator.StringToHash("Dying 02") },
            {typeof(Dying), Animator.StringToHash("Dying 03") },
            {typeof(Dying), Animator.StringToHash("Dying 04") },
        };
    }

    public void CallAnimationEvent(AnimEvent animEvent)
    {
        OnAnimationEvent?.Invoke(animEvent);
    }

    private void UpdateAnimation(State newState)
    {
        if (animStates.ContainsKey(newState.GetType()))
        {
            anim.speed = 1;
            anim.CrossFade(animStates[newState.GetType()], .1f);
        }
        else
        {
            Debug.LogWarning(agent.gameObject.name + ": No Animation Found For Current Agent State");
        }
    }

    //private void OnAnimatorMove()
    //{
    //    transform.parent.position += anim.deltaPosition;
    //}
}

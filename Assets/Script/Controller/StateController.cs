using Script.StatePlayer;
using UnityEngine;

namespace Script
{
    public abstract class StateController : MonoBehaviour
    {
        public virtual void ChangeState(IState newState){}

        public HurtStatePlayer hurtState = new HurtStatePlayer();
    }

}
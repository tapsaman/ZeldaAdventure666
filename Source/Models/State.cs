using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZA6.Managers;

namespace ZA6.Models
{
    public abstract class State
    {
        public StateMachine StateMachine;
        public virtual bool CanReEnter { get; protected set; } = true;
        public abstract void Enter(StateArgs args);
        public abstract void Update(GameTime gameTime);
        public abstract void Exit();
    }

    public class StateArgs {}
}
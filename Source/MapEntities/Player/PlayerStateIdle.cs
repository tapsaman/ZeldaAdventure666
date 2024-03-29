using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZA6.Managers;

namespace ZA6.Models
{
    public class PlayerStateIdle : PlayerState
    {
        public PlayerStateIdle(Player player) : base(player) {}

        public override void Enter(StateArgs _)
        {
            Player.AnimatedSprite.SetAnimation("Idle" + Player.Facing);
        }

        public override void Update(GameTime gameTime)
        {
            Player.DetermineInputVelocity();
            Player.DetermineHitInput();

            if (Player.Hitting)
            {
                StateMachine.TransitionTo("SwordHit");
            }
            else if (Player.Velocity.X != 0 || Player.Velocity.Y != 0)
            {
                StateMachine.TransitionTo("Walking");
            }
        }

        public override void Exit() {}
    } 
}
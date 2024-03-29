using Microsoft.Xna.Framework;

namespace ZA6.Models
{
    public class BariStateAttacking : CharacterState
    {
        private Enemy _enemy;
        private float _elapsedTime;
        private const float _TIME = 2f;

        public BariStateAttacking(Enemy enemy) : base(enemy)
        {
            _enemy = enemy;
        }

        public override void Enter(StateArgs _)
        {
            _enemy.AnimatedSprite.SetAnimation("Attacking");
            //_enemy.IsInvincible = true;
            _elapsedTime = 0;
        }

        public override void Update(GameTime gameTime)
        {
            _elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_elapsedTime > _TIME)
            {
                _enemy.StateMachine.TransitionTo("Default");
            }
        }

        public override void Exit() {}
    } 
}
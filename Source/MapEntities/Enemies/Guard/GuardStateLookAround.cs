using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TapsasEngine.Enums;
using TapsasEngine.Utilities;
using ZA6.Managers;

namespace ZA6.Models
{
    public class GuardStateLookAround : CharacterState
    {
        private Guard _guard;
        private float _elapsedTime;
        private const float _DIRECTION_LOOK_TIME = 0.8f;
        private Direction _lookDirection;
        private int _lookIter;

        public GuardStateLookAround(Guard guard) : base(guard)
        {
            _guard = guard;
        }

        public override void Enter(StateArgs _)
        {
            Character.Velocity = Vector2.Zero;
            _elapsedTime = 0;
            _lookIter = 0;
            _guard.Moving = false;
            _lookDirection = _guard.Facing.NextCounterclockwise();
            Character.AnimatedSprite.SetAnimation("Idle" + _guard.Facing + "Look" + _lookDirection);
        }
        public override void Update(GameTime gameTime)
        {
            if (_guard.DetectingPlayer(_lookDirection))
            {
                StateMachine.TransitionTo("NoticedPlayer");
            }
            else
            {
                _elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (_elapsedTime > _DIRECTION_LOOK_TIME)
                {
                    _elapsedTime = 0;
                    _lookIter++;

                    if (_lookIter == 1)
                    {
                        _lookDirection = _lookDirection.Next();
                        Character.AnimatedSprite.SetAnimation("Idle" + _lookDirection);
                    }
                    else if (_lookIter == 2)
                    {
                        _lookDirection = _lookDirection.Next();
                        Character.AnimatedSprite.SetAnimation("Idle" + _guard.Facing + "Look" + _lookDirection);
                    }
                    else if (_lookIter == 3)
                    {
                        _lookDirection = _lookDirection.NextCounterclockwise();
                        Character.AnimatedSprite.SetAnimation("Idle" + _lookDirection);
                    }
                    else
                    {
                        Character.Facing = Utility.RandomDirection();
                        StateMachine.TransitionTo("Default");
                    }
                }
            }
        }

        public override void Exit()
        {
            _guard.Moving = true;
        }
    } 
}
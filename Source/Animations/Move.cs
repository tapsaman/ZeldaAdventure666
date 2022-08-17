using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZA6.Animations
{
    public class Move : Animation
    {
        public Move(MapEntity target, Vector2 distance, float time)
        {
            Stages = new AnimationStage[]
            {
                new MoveStage(target, distance, time)
            };
        }

        private class MoveStage : AnimationStage
        {
            private float _time;
            private MapEntity _target;
            private Vector2 _distance;
            private Vector2 _startPosition;
            private Vector2 _endPosition;
            private float _elapsedTime = 0;
            
            public MoveStage(MapEntity target, Vector2 distance, float time)
            {
                _target = target;
                _distance = distance;
                _time = time;
            }
            public override void Enter()
            {
                _elapsedTime = 0;
                _startPosition = _target.Position;
                _endPosition =_startPosition + _distance;
            }
            public override void Update(GameTime gameTime)
            {
                _elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (_elapsedTime < _time)
                {
                    _target.Position = _startPosition + _distance * (_elapsedTime / _time);
                }
                else
                {
                    _target.Position = _endPosition;
                    IsDone = true;
                }
            }
            public override void Draw(SpriteBatch spriteBatch) {}
        }
    }
}
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameTestGame.Managers;

namespace MonoGameTestGame.Models
{
    public class GameStateDefault : RenderState
    {
        private ZeldaAdventure666 _game;

        public GameStateDefault(ZeldaAdventure666 game)
        {
            _game = game;
        }

        public override void Enter()
        {
            if (!StaticData.GameStarted)
            {
                _game.SceneManager.Start();
                StaticData.GameStarted = true;
            }
        }

        public override void Update(GameTime gameTime)
        {
            _game.SceneManager.Update(gameTime);
            _game.DialogManager.Update(gameTime);
            Music.Update(gameTime);

            _game.TitlePosition.X -= (float)gameTime.ElapsedGameTime.TotalSeconds * 15;

            if (Input.P1.IsPressed(Input.P1.Start))
                stateMachine.TransitionTo("MainMenu");
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Rendering.Start();

            _game.SceneManager.Draw(spriteBatch);
            _game.Hud.Draw(spriteBatch, _game.SceneManager.Player);
            
            BitmapFontRenderer.DrawString(spriteBatch, "zeldan seikkailut mikä mikä maassa vittu", _game.TitlePosition);
            
            Rendering.End(gameTime);
        }

        public override void Exit() {}
    } 
}
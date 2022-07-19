using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using MonoGameTestGame.Managers;
using MonoGameTestGame.Models;

namespace MonoGameTestGame
{
    public class SceneA2 : Scene
    {
        private Event _signEvent;
        private Song _song;

        public SceneA2(Player player): base(player) {}
        
        protected override void Load()
        {
            _song = StaticData.Content.Load<Song>("linktothepast-forest");
            TileMap = new MapA2();

            var signEventTrigger = new EventTrigger(TileMap.GetPosition(9, 4), 14, 14);
            _signEvent = new TextEvent(new Dialog("ZELDA'S TENT HOME"), signEventTrigger);
            signEventTrigger.Trigger += ReadSign;

            Add(signEventTrigger);
        }

        public override void Start()
        {
            MediaPlayer.Play(_song);
            MediaPlayer.Volume = 0.2f;
            MediaPlayer.IsRepeating = true;
            base.Start();
        }

        private void ReadSign()
        {
            EventManager.Load(_signEvent);
        }
    }
}
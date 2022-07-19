using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using TiledCS;

namespace MonoGameTestGame
{
    public class TestMap : TileMap
    {
        public override void Load()
        {
            _map = new TiledMap(StaticData.TiledProjectDirectory + "\\A1.tmx");
            _tileset = new TiledTileset(StaticData.TiledProjectDirectory + "\\linktothepast-tiles.tsx");
            _tilesetTexture = StaticData.TileTexture;
            _nextMaps = new Dictionary<Direction, MapCode>()
            {
                { Direction.Right, MapCode.B1 },
                { Direction.Down, MapCode.A2 }
            };
        }
    }
}
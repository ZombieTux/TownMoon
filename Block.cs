using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace TownMoon
{
    public abstract class Block
    {
        public string name;
        public Color tint;
        public Vector2 size;
        public bool collision;
        public Point position;

        public Block(string name, Vector2 size, Point position, Color tint, bool collision=false)
        {
            this.name = name;
            this.size = size;
            this.tint = tint;
            this.collision = collision;
            this.position = position;
        }


    }
}

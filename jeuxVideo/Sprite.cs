using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace jeuxVideo
{

    //Class a utilisé pour tout ce qui n'est pas annimé
    class Sprite
    {
        private Vector2 _direction;
        private Vector2 _position;
        private float _speed;
        private Texture2D _texture;
        private float _temp;

        public Sprite()
        {
        }

        public Vector2 Direction
        {
            get
            {
                return this._direction;
            }

            set
            {
                this._direction = value;
            }
        }

        public Vector2 Position
        {
            get
            {
                return this._position;
            }

            set
            {
                this._position = value;
            }
        }

        public float Speed
        {
            get
            {
                return this._speed;
            }

            set
            {
                this._speed = value;
            }
        }

        public Texture2D Texture
        {
            get
            {
                return this._texture;
            }

            set
            {
                this._texture = value;
            }
        }

        public float Temp
        {
            get
            {
                return _temp;
            }

            set
            {
                _temp = value;
            }
        }

        public void Initialize(Vector2 position, Vector2 direction, int vitesse)
        {
            this.Position = position;
            this.Direction = direction;
            this.Speed = vitesse;
            this.Temp = 0;
        }

        public void Update(GameTime gameTime)
        {
            this.Position += this.Direction * this.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public bool Contains(Point B)
        {
            Rectangle rectSprite = new Rectangle((int)this.Position.X, (int)this.Position.Y, this.Texture.Width, this.Texture.Height);
            return rectSprite.Contains(B);
        }
        public bool Intersects(Sprite autre)
        {
            Rectangle rectAutre = new Rectangle((int)autre.Position.X, (int)autre.Position.Y, autre.Texture.Width, autre.Texture.Height);
            Rectangle rectSprite = new Rectangle((int)this.Position.X, (int)this.Position.Y, this.Texture.Width, this.Texture.Height);

            return rectSprite.Intersects(rectAutre);
        }
        public bool Intersects(SpriteAnime autre, int correctionHitboxX, int correctionHitboxY)
        {
            Rectangle rectAutre = new Rectangle((int)autre.PositionPerso.X, (int)autre.PositionPerso.Y, autre.Perso.TextureRegion.Width-correctionHitboxX, autre.Perso.TextureRegion.Height-correctionHitboxY);
            Rectangle rectSprite = new Rectangle((int)this.Position.X, (int)this.Position.Y, this.Texture.Width, this.Texture.Height);

            return rectSprite.Intersects(rectAutre);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, Color.White);
        }

        
    }
}

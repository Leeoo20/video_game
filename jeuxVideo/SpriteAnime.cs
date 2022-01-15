using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;

using System.Text;

namespace jeuxVideo
{
    //Pour tout les perso animé qui bouge
    //Pour le déplacer juste besoin de _perso.Direction(new Vector(0,1)) -> pour descendre 
    class SpriteAnime
    {
        private Vector2 _positionPerso;
        private AnimatedSprite _perso;
        private int _vitessePerso;
        private SpriteSheet _texturePerso;
        private int _barreDeVie;
        private string _animation;
        private Vector2 _directionPerso;
        private bool _touchable;
        private float _temp;
      

        public SpriteAnime()
        {
        }

        public Vector2 PositionPerso
        {
            get
            {
                return this._positionPerso;
            }

            set
            {
                this._positionPerso = value;
            }
        }

        public AnimatedSprite Perso
        {
            get
            {
                return this._perso;
            }

            set
            {
                this._perso = value;
            }
        }

        public int VitessePerso
        {
            get
            {
                return this._vitessePerso;
            }

            set
            {
                this._vitessePerso = value;
            }
        }

        public SpriteSheet TexturePerso
        {
            get
            {
                return this._texturePerso;
            }

            set
            {
                this._texturePerso = value;
            }
        }

        public int BarreDeVie
        {
            get
            {
                return this._barreDeVie;
            }

            set
            {
                this._barreDeVie = value;
            }
        }

        public string Animation
        {
            get
            {
                return this._animation;
            }

            set
            {
                this._animation = value;
            }
        }

        public Vector2 DirectionPerso
        {
            get
            {
                return this._directionPerso;
            }

            set
            {
                this._directionPerso = value;
            }
        }

        public bool Touchable
        {
            get
            {
                return _touchable;
            }

            set
            {
                _touchable = value;
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

        public void Initialize(Vector2 positionPerso,int vitessePerso, int barreDeVie, string animation, Vector2 direction,bool touchable )
        {
            this.PositionPerso = positionPerso;
            this.VitessePerso = vitessePerso;
            this.BarreDeVie = barreDeVie;
            this.Animation = animation;
            this.DirectionPerso = direction;
            this.Touchable = true;
            this.Temp = 0;
        
        }

       public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Perso, this.PositionPerso);
        }

        public void Actualise(GameTime gameTime)
        {
            this.Perso.Play(this.Animation);
            this.Perso.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
        }
        public void ActualiseDeplacement(GameTime gameTime)
        {
            this.PositionPerso += this.DirectionPerso * this.VitessePerso * (float)gameTime.ElapsedGameTime.TotalSeconds;

        }
        public bool Contains(Point B)
        {
            Rectangle rectSprite = new Rectangle((int)this.PositionPerso.X, (int)this.PositionPerso.Y, this.Perso.TextureRegion.Width, this.Perso.TextureRegion.Height);
            return rectSprite.Contains(B);
        }

        public bool Intersects(SpriteAnime autre, int correctionX,int correctionY)
        {
            Rectangle rectAutre = new Rectangle((int)autre.PositionPerso.X, (int)autre.PositionPerso.Y, autre.Perso.TextureRegion.Width-correctionX, autre.Perso.TextureRegion.Height-correctionY);
            Rectangle rectSprite = new Rectangle((int)this.PositionPerso.X, (int)this.PositionPerso.Y, this.Perso.TextureRegion.Width, this.Perso.TextureRegion.Height);

            return rectSprite.Intersects(rectAutre);
        }

        public bool Intersects(Sprite autre, int correctionX, int correctionY)
        {
            Rectangle rectAutre = new Rectangle((int)autre.Position.X, (int)autre.Position.Y, autre.Texture.Width , autre.Texture.Height );
            Rectangle rectSprite = new Rectangle((int)this.PositionPerso.X, (int)this.PositionPerso.Y, this.Perso.TextureRegion.Width -correctionX, this.Perso.TextureRegion.Height -correctionY);

            return rectSprite.Intersects(rectAutre);
        }


        public void GetDamage(SpriteAnime vie, GameTime gameTime)
        {
            
                this.Touchable = false;
                this.BarreDeVie -= 1;
                 vie.Animation = $"vie{this.BarreDeVie}";
                vie.Actualise(gameTime);
            
        }


    }
}

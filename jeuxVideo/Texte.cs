using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

using System.Text;

namespace jeuxVideo
{
    class Texte
    {
        //Class a utilisé pour texte FIXE

        private SpriteFont _police;
        private Vector2 _position;
        private string _text;
        private Color _couleur;

        public Texte()
        {
        }

        public SpriteFont Police
        {
            get
            {
                return this._police;
            }

            set
            {
                this._police = value;
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

        public string Text
        {
            get
            {
                return this._text;
            }

            set
            {
                this._text = value;
            }
        }


        public Color Couleur
        {
            get
            {
                return this._couleur;
            }

            set
            {
                this._couleur = value;
            }
        }

     

        public void Initialize(Vector2 positon, string texte, Color couleur)
        {
            this.Position = positon;
            this.Text = texte;
            this.Couleur = couleur;
          
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(this.Police, this.Text, this.Position, this.Couleur);
        }
    }
}

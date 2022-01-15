using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Content;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.Text;


namespace jeuxVideo
{
    class EcranCommande : GameScreen
    {
        private Game1 _myGame;


        private Sprite _fondCommande;

        private Sprite _flecheRetour;

        private Sprite _texteGaucheDroite;
        private Sprite _toucheQCommande;
        private Sprite _toucheDCommande;

        private Sprite _texteSauter;
        private Sprite _commandeClavier;

        private Sprite _texteAttaquer;
        private Sprite _commandeSouris;





        public EcranCommande(Game1 game) : base(game)
        {
            _myGame = game;
        }

        public override void Initialize()
        {

            //FOND
            _fondCommande = new Sprite();
            _fondCommande.Initialize(new Vector2((Game.GraphicsDevice.Viewport.Width / 2 - 400), (Game.GraphicsDevice.Viewport.Height / 2) - 300), new Vector2(0, 0), 0);

            //FLECHE RETOUR
            _flecheRetour = new Sprite();
            _flecheRetour.Initialize(new Vector2((Game.GraphicsDevice.Viewport.Width / 2 - 400), (Game.GraphicsDevice.Viewport.Height / 2 - 300)), new Vector2(0, 0), 0);

            //TEXTE GAUCHE|DROITE
            _texteGaucheDroite = new Sprite();
            _texteGaucheDroite.Initialize(new Vector2((Game.GraphicsDevice.Viewport.Width / 2 - 400), (Game.GraphicsDevice.Viewport.Height / 2 - 190)), new Vector2(0, 0), 0);
            //TOUCHE CLAVIER POUR BOUGER
            _toucheQCommande = new Sprite();
            _toucheQCommande.Initialize(new Vector2((Game.GraphicsDevice.Viewport.Width / 2 - 120), (Game.GraphicsDevice.Viewport.Height / 2 - 200)), new Vector2(0, 0), 0);
            _toucheDCommande = new Sprite();
            _toucheDCommande.Initialize(new Vector2((Game.GraphicsDevice.Viewport.Width / 2 + 10), (Game.GraphicsDevice.Viewport.Height / 2 - 200)), new Vector2(0, 0), 0);


            //TEXTE SAUTER
            _texteSauter = new Sprite();
            _texteSauter.Initialize(new Vector2((Game.GraphicsDevice.Viewport.Width / 2 - 400), (Game.GraphicsDevice.Viewport.Height / 2 - 70)), new Vector2(0, 0), 0);
            //TOUCHE CLAVIER POUR SAUTER
            _commandeClavier = new Sprite();
            _commandeClavier.Initialize(new Vector2((Game.GraphicsDevice.Viewport.Width / 2 - 150), (Game.GraphicsDevice.Viewport.Height / 2 - 100)), new Vector2(0, 0), 0);

            //TEXTE ATTAQUER
            _texteAttaquer = new Sprite();
            _texteAttaquer.Initialize(new Vector2((Game.GraphicsDevice.Viewport.Width / 2 - 400), (Game.GraphicsDevice.Viewport.Height / 2 + 100)), new Vector2(0, 0), 0);
            //TOUCHE SOURIS POUR ATTAQUER
            _commandeSouris = new Sprite();
            _commandeSouris.Initialize(new Vector2((Game.GraphicsDevice.Viewport.Width / 2 - 130), (Game.GraphicsDevice.Viewport.Height / 2 + 30)), new Vector2(0, 0), 0);














            base.Initialize();
        }


        public override void LoadContent()
        {


            _fondCommande.Texture = Content.Load<Texture2D>("pageCommande/FondTuile");

            //FLECHE RETOUR
            _flecheRetour.Texture = Content.Load<Texture2D>("pageCommande/CommandeFlecheRetour");

            //TEXTE ET TOUCHE CLAVIER POUR BOUGER
            _texteGaucheDroite.Texture = Content.Load<Texture2D>("pageCommande/CommandeMouvement");
            _toucheQCommande.Texture = Content.Load<Texture2D>("pageCommande/toucheQclavier");
            _toucheDCommande.Texture = Content.Load<Texture2D>("pageCommande/toucheDclavier");

            //TEXTE ET TOUCHE CLAVIER POUR SAUTER
            _texteSauter.Texture = Content.Load<Texture2D>("pageCommande/CommandeSauter");
            _commandeClavier.Texture = Content.Load<Texture2D>("pageCommande/clavierCommande");

            //TEXTE ET TOUCHE SOURIS POUR ATTAQUER
            _texteAttaquer.Texture = Content.Load<Texture2D>("pageCommande/CommmandeAttaquer");
            _commandeSouris.Texture = Content.Load<Texture2D>("pageCommande/CliqueSouris");







            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();



            if (mouseState.LeftButton == ButtonState.Pressed && _flecheRetour.Contains(mouseState.Position))
            {
                _myGame.EcranAfficher = (int)EnumEcranAfficher.ACCEUIL;
            }




        }


        public override void Draw(GameTime gameTime)
        {
            _myGame.GraphicsDevice.Clear(Color.Red);
            _myGame.SpriteBatch.Begin();


            //FOND
            _fondCommande.Draw(_myGame.SpriteBatch);

            //FLECHE RETOUR
            _flecheRetour.Draw(_myGame.SpriteBatch);

            //TEXTE ET TOUCHE CLAVIER POUR BOUGER
            _texteGaucheDroite.Draw(_myGame.SpriteBatch);
            _toucheQCommande.Draw(_myGame.SpriteBatch);
            _toucheDCommande.Draw(_myGame.SpriteBatch);

            //TEXTE ET TOUCHE CLAVIER POUR SAUTER
            _texteSauter.Draw(_myGame.SpriteBatch);
            _commandeClavier.Draw(_myGame.SpriteBatch);

            //TEXTE ET TOUCHE SOURIS POUR ATTAQUER
            _texteAttaquer.Draw(_myGame.SpriteBatch);
            _commandeSouris.Draw(_myGame.SpriteBatch);


            _myGame.SpriteBatch.End();
        }


    }
}

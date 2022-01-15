using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using System;
using System.Collections.Generic;
using System.Text;

namespace jeuxVideo
{
    class EcranGagner : GameScreen
    {
        private Game1 _myGame;


        private Sprite _fondGagner;

        private Sprite _LogoGagner;

        private Sprite _boutonAcceuil;



        public EcranGagner(Game1 game) : base(game)
        {
            _myGame = game;
        }

        public override void Initialize()
        {

            //FOND
            _fondGagner = new Sprite();
            _fondGagner.Initialize(new Vector2((Game.GraphicsDevice.Viewport.Width / 2 - 400), (Game.GraphicsDevice.Viewport.Height / 2) - 300), new Vector2(0, 0), 0);

            //LOGO YOU WIN
            _LogoGagner = new Sprite();
            _LogoGagner.Initialize(new Vector2((GraphicsDevice.Viewport.Width / 2 - 100), (Game.GraphicsDevice.Viewport.Height / 2 - 230)), new Vector2(0, 0), 0);

            //BOUTON ACCEUIL
            _boutonAcceuil = new Sprite();
            _boutonAcceuil.Initialize(new Vector2(GraphicsDevice.Viewport.Width / 2 - 100, 400), new Vector2(0, 0), 0);


            base.Initialize();
        }


        public override void LoadContent()
        {

            //FOND
            _fondGagner.Texture = Content.Load<Texture2D>("EcranGagner/FondGagner");

            //LOGO YOU WIN
            _LogoGagner.Texture = Content.Load<Texture2D>("EcranGagner/LogoGagner");


            //BOUTON ACCEUIL
            _boutonAcceuil.Texture = Content.Load<Texture2D>("EcranGagner/BoutonAcceuil");



            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();


            if (mouseState.LeftButton == ButtonState.Pressed && _boutonAcceuil.Contains(mouseState.Position))
            {
                _myGame.EcranAfficher = (int)EnumEcranAfficher.ACCEUIL;
            }

        }


        public override void Draw(GameTime gameTime)
        {
            _myGame.GraphicsDevice.Clear(Color.DarkSalmon);
            _myGame.SpriteBatch.Begin();

            //FOND
            _fondGagner.Draw(_myGame.SpriteBatch);

            //LOGO YOU WIN
            _LogoGagner.Draw(_myGame.SpriteBatch);

            //BOUTON ACCUEIL
            _boutonAcceuil.Draw(_myGame.SpriteBatch);

            _myGame.SpriteBatch.End();
        }
    }
}

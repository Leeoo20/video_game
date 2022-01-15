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
    class EcranAcceuil : GameScreen

    {

        private Game1 _myGame;

        private SpriteAnime _fond;

        private Sprite _logo;

        private Sprite _boutonStart;

        private Sprite _boutonCommande;

        public float _tempAttente;

        public EcranAcceuil(Game1 game) : base(game)
        {
            _myGame = game;
        }

        public override void Initialize()
        {


            _fond = new SpriteAnime();
            _fond.Initialize(new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2), 0, 1000, "fondAnimation", new Vector2(0, 0), false);

            _logo = new Sprite();
            _logo.Initialize(new Vector2(GraphicsDevice.Viewport.Width / 2 - 240, 0), new Vector2(0, 0), 0);

            _boutonStart = new Sprite();
            _boutonStart.Initialize(new Vector2(GraphicsDevice.Viewport.Width / 2 - 100, 400), new Vector2(0, 0), 0);

            _boutonCommande = new Sprite();
            _boutonCommande.Initialize(new Vector2(GraphicsDevice.Viewport.Width / 2 - 100, 480), new Vector2(0, 0), 0);

            _tempAttente = 0;



            base.Initialize();
        }


        public override void LoadContent()
        {


            _fond.TexturePerso = Content.Load<SpriteSheet>("pageAcceuil/backGround.sf", new JsonContentLoader());
            _fond.Perso = new AnimatedSprite(_fond.TexturePerso);

            _logo.Texture = Content.Load<Texture2D>("pageAcceuil/LOGO");

            _boutonStart.Texture = Content.Load<Texture2D>("pageAcceuil/boutonStart");

            _boutonCommande.Texture = Content.Load<Texture2D>("pageAcceuil/BoutonCommande");


            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {

           
            MouseState mouseState = Mouse.GetState();
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            //TOUCHE DEPLACEMENT 
            //COMMANDE = N
            //ChoixNiveau = M

            if (_tempAttente < 1)
            {
                _tempAttente += deltaTime;
            }
            else{
                if (mouseState.LeftButton == ButtonState.Pressed && _boutonCommande.Contains(mouseState.Position))
                {
                    _myGame.EcranAfficher = (int)EnumEcranAfficher.COMMANDE;
                    
                }
                if (mouseState.LeftButton == ButtonState.Pressed && _boutonStart.Contains(mouseState.Position))
                {
                    _myGame.EcranAfficher = (int)EnumEcranAfficher.CHOIX_NIVEAU;
                }

            }


            _fond.ActualiseDeplacement(gameTime);
            _fond.Actualise(gameTime);

           

        }


        public override void Draw(GameTime gameTime)
        {
            _myGame.GraphicsDevice.Clear(Color.White);
            _myGame.SpriteBatch.Begin();
            //ECRIRE ICI

            _fond.Draw(_myGame.SpriteBatch);
            _logo.Draw(_myGame.SpriteBatch);
            _boutonStart.Draw(_myGame.SpriteBatch);
            _boutonCommande.Draw(_myGame.SpriteBatch);



            _myGame.SpriteBatch.End();
        }
    }



}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Content;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using System;


namespace jeuxVideo
{

    public enum SAUT { PEUT_SAUTER = 0, A_SAUTER = 1, SAUTE = 2 }

    public enum TIR {PEUT_TIRER = 0, TIRER =1, VALIDE_TIR =2, DEMANDE_TIR =3}

    public enum ETAT_BOOS { ATTENTE =0, ATTAQUE1=1, ATTAQUE2=2, ATTAQUE3=3}

    public enum ATTAQUE1 {INACTIF =0,DEBUT_ATTAQUE=1,ATTAQUE=2,REVIENS =3,RETOUR_INACTIF=4, FIN =5 }

    public enum ATTAQUE2 { INACTIF=0, DEBUT_ATTAQUE = 1, ATTAQUE=2 , ATTAQUE_IMPACT =3}

    public enum ATTAQUE3 { INACTIF=0,DEBUT_ATTAQUE =1, REJOINS_POSITION =2, ATTAQUE=3, REVIENS =4, RETOUR_INACTIF =5}

    public enum ETAT_PAUSE { CONTINUE = 0, PAUSE_DEMANDER =1, PAUSE = 2 , DEMANDE_REPRENDRE = 3 , REPRENDRE =4}

    class EcranCombat2: GameScreen
    {
        private Game1 _myGame;

        //GESTION PAUSE 

        private int _etatPause;
        private Sprite _boutonQuitter;
        private Sprite _boutonContinuer;
       

        //MAP
        private TiledMap _map;
        private TiledMapRenderer _mapRendu;

        //BOSS
        private SpriteAnime _boss;
        private int _etatBossAttaque1;
        private int _etatBossAttaque2;
        private int _prochaineAttaque;
        private int _etatBossAttaque3;
        private bool _bossAction;
        private int _etatBoss;
        private bool _etatEsquive;
        private float _tempsEsquive;
        private SoundEffect _sonLacherEngrenage;
        private SoundEffect _sonTelecommande;
        private SoundEffect _sonEnvole;
        private SpriteAnime _vieBoss;



        //TIRBOSS
        private Sprite[] _projectileBoss = new Sprite[10];
        private int[] _positionLacheProjectil = new int[10];
        private bool _positionTirer;

        //OISEAU BOSS

        private SpriteAnime _oiseauBoss;
    

        //perso
       
        private SpriteAnime _perso;
        private bool _sensPerso;
        private int _etatSaut;
        private float _positionYDebutSaut;
        private SoundEffect _sonTirFleche;
        private SoundEffect _sonSaut;
        private SpriteAnime _vie;
        private GameTime _gameTime;
        private bool _sensDefini;



        //sol
        private TiledMapTileLayer _sol;

        //physique 
        private Vector2 _physique;
        private int _vitessePhysique;



        //fleche

        private Fleche _fleche;

        public EcranCombat2(Game1 game) : base(game)
        {
            _myGame = game;
        }

        public override void Initialize()
        {

            //PAUSE 
            _etatPause = (int)ETAT_PAUSE.CONTINUE;
            _boutonQuitter = new Sprite();
            _boutonQuitter.Initialize(new Vector2(1000, 1000), new Vector2(0, 0), 0);
            _boutonContinuer = new Sprite();
            _boutonContinuer.Initialize(new Vector2(1000, 1000), new Vector2(0, 0), 0);



            //Boss
           
            _boss = new SpriteAnime();
            _boss.Initialize(new Vector2(GraphicsDevice.Viewport.Width - 150, GraphicsDevice.Viewport.Height - 300), 200, 20,"immobile", new Vector2(0, 0), true);
            _etatBoss = (int)ETAT_BOOS.ATTENTE;
            _etatBossAttaque1 = (int)ATTAQUE1.INACTIF;
            _etatBossAttaque2 = (int)ATTAQUE2.INACTIF;
            _etatBossAttaque3 = (int)ATTAQUE3.INACTIF;
            _bossAction = false;
            _prochaineAttaque = 0;

            //VIE BOSS
            _vieBoss = new SpriteAnime();
            _vieBoss.Initialize(new Vector2(Game.GraphicsDevice.Viewport.Width - 200, 45), 0, 0, "vieBoss20", new Vector2(0, 0), false);
           

            //esquive boss
            _etatEsquive = false;
            _tempsEsquive = 0;

            //BARRE DE VIE BOSS

           

            //PROJECTIL BOOSS 

         

            for (int i = 0; i < _projectileBoss.Length; i++)
            {
                _projectileBoss[i] = new Sprite();
                _projectileBoss[i].Initialize(new Vector2(1000, 1000), new Vector2(0, 1), 200);
            }


            _positionTirer = false;


            //oiseauBoss

            _oiseauBoss = new SpriteAnime();
            _oiseauBoss.Initialize(new Vector2(1000, 1000), 200, 1, "vole", new Vector2(0, 0), true);



            //Perso
           
            _perso = new SpriteAnime();
            _perso.Initialize(new Vector2(100, GraphicsDevice.Viewport.Height - 200), 200, 12, "allerADroite", new Vector2(0, 0), true);
            _sensPerso = false;
            _etatSaut = (int) SAUT.PEUT_SAUTER;
            _positionYDebutSaut = 0;

            //Bar de vie Perso

            _vie = new SpriteAnime();
           _vie.Initialize(new Vector2(51, 17), 0, 0, "vie12", new Vector2(0, 0), false);


            //fleche
            _fleche = new Fleche();
            for (int i =0;i<_fleche.Fleches.Length;i++)
            {
                _fleche.Fleches[i] = new Sprite();
                _fleche.Fleches[i].Initialize(new Vector2(20000, 20000), new Vector2(0, 0), 600);
                _fleche.SensDefinits[i] = false;
            }
            _fleche.FlecheATirer = 0;
            _fleche.EtatSouris = false;
            _fleche.EtatIntermediaire = false;
           




            //physique
            _physique = new Vector2(0, 1);
            _vitessePhysique = 2;

            base.Initialize();
        }


        public override void LoadContent()
        {


            //BOUTON QUITTER 
            _boutonQuitter.Texture = Content.Load<Texture2D>("Interface/boutonQuitter");
            _boutonContinuer.Texture = Content.Load<Texture2D>("Interface/boutonContinuer");

            //Boss
            _boss.TexturePerso = Content.Load<SpriteSheet>("EcranCombat2/boss.sf", new JsonContentLoader());
            _boss.Perso = new AnimatedSprite(_boss.TexturePerso);
            // de vie Boss
            _vieBoss.TexturePerso = Content.Load<SpriteSheet>("EcranCombat2/Boss/Vie/BarreVieBoss.sf", new JsonContentLoader());
            _vieBoss.Perso = new AnimatedSprite(_vieBoss.TexturePerso);

            


            //SonBoss
            _sonEnvole = Content.Load<SoundEffect>("EcranCombat2/Boss/bossEnvole");
            _sonLacherEngrenage = Content.Load<SoundEffect>("EcranCombat2/Boss/attaque1");
            _sonTelecommande = Content.Load<SoundEffect>("EcranCombat2/Boss/attaque2");
            //Projectile Boss
            foreach (Sprite projectil in _projectileBoss)
            {
                projectil.Texture = Content.Load<Texture2D>("EcranCombat2/projectilBoss");
            }

            //oiseauBoss
            _oiseauBoss.TexturePerso = Content.Load<SpriteSheet>("EcranCombat2/oiseau.sf", new JsonContentLoader());
            _oiseauBoss.Perso = new AnimatedSprite(_oiseauBoss.TexturePerso);


            //Perso
            _perso.TexturePerso = Content.Load<SpriteSheet>("EcranCombat2/perso.sf", new JsonContentLoader());
            _perso.Perso = new AnimatedSprite(_perso.TexturePerso);
            //BAR DE VIE PERSO
            _vie.TexturePerso = Content.Load<SpriteSheet>("joueur/barreDeVie/Vie.sf", new JsonContentLoader());
            _vie.Perso = new AnimatedSprite(_vie.TexturePerso);

            //Son PERSO
            _sonTirFleche = Content.Load<SoundEffect>("joueur/tirArc");
            _sonSaut = Content.Load<SoundEffect>("joueur/saut");

           

            //Fleche
            foreach (Sprite fleche in _fleche.Fleches)
            {
                fleche.Texture = Content.Load<Texture2D>("EcranCombat2/fleche");
            }

            //MAP
            _map = Content.Load<TiledMap>("EcranCombat2/map");
            _mapRendu = new TiledMapRenderer(GraphicsDevice, _map);

            //Sol
            _sol = _map.GetLayer<TiledMapTileLayer>("Sol");
 


            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            MouseState souris = Mouse.GetState();


            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

           
           

          
            //VARIABLE GLOBALE
            ushort txPerso = (ushort)(_perso.PositionPerso.X / _map.TileWidth);// Converti la posittion en pixel en case 
            ushort tyPerso = (ushort)(_perso.PositionPerso.Y / _map.TileHeight);


            ushort tyDessousPerso = (ushort)(tyPerso + 1);
            ushort txGauchePerso = (ushort)(txPerso - 1);
            ushort txDroitPerso = (ushort)(txPerso + 1);


            if (_etatPause ==(int) ETAT_PAUSE.CONTINUE)
            {
                //GESTION PAUSE 

                if (keyboardState.IsKeyDown(Keys.Escape))
                {
                    _etatPause = (int)ETAT_PAUSE.PAUSE_DEMANDER;
                }

                //GESTION DEPLACEMENT 

                _perso.DirectionPerso = new Vector2(0, 0);

                if (_sensPerso == false)
                {

                    _perso.Animation = "immobileDroite";
        
                }
                else
                {
                    _perso.Animation = "immobileGauche";
                }


                if (keyboardState.IsKeyDown(Keys.Q))
                {
                    _perso.DirectionPerso = new Vector2(-1, 0);
                    _perso.ActualiseDeplacement(gameTime);
                    _sensPerso = true;
                    _perso.Animation = "allerAGauche";
                }
                if (keyboardState.IsKeyDown(Keys.D))
                {
                    _perso.DirectionPerso = new Vector2(1, 0);
                    _perso.ActualiseDeplacement(gameTime);
                    _sensPerso = false;
                    _perso.Animation = "allerADroite";
                    
                }
               
                //SAUT
                if (keyboardState.IsKeyDown(Keys.Space) && IsCollision(txPerso, tyDessousPerso))
                {
                    
                    _positionYDebutSaut = _perso.PositionPerso.Y;
                    _sonSaut.Play();
                    _etatSaut = (int)SAUT.SAUTE;

                }



                if (_etatSaut == (int)SAUT.SAUTE)
                {
                  _perso.DirectionPerso = new Vector2(0, -1) ;
                 

                    _perso.ActualiseDeplacement(gameTime);
                }
                if (_etatSaut == (int)SAUT.SAUTE && _perso.PositionPerso.Y < _positionYDebutSaut - 120)
                {
                    _etatSaut = (int)SAUT.PEUT_SAUTER;
                }



                //FIN GESTION DEPLACEMENT

                //GESTION DE LA PHYSIQUE 

                if (!IsCollision(txPerso, tyDessousPerso))
                {
                    _perso.PositionPerso += _physique * _vitessePhysique;
                    _perso.ActualiseDeplacement(gameTime);


                }


                
                if (IsCollision(txGauchePerso, tyPerso))
                {
                    _perso.VitessePerso = -_perso.VitessePerso;
                    _perso.ActualiseDeplacement(gameTime);

                }
                if (IsCollision(txDroitPerso, tyPerso))
                {
                    _perso.VitessePerso = -_perso.VitessePerso;
                    _perso.ActualiseDeplacement(gameTime);


                }
                




                if (_perso.PositionPerso.X > GraphicsDevice.Viewport.Width - _perso.Perso.TextureRegion.Width / 2)
                {
                    _perso.DirectionPerso = new Vector2(-1, 0) * _vitessePhysique;
                    _perso.ActualiseDeplacement(gameTime);
                }
                if (_perso.PositionPerso.X < 0)
                {
                    _perso.DirectionPerso = new Vector2(1, 0) * _vitessePhysique;
                    _perso.ActualiseDeplacement(gameTime);
                }
                if (_perso.PositionPerso.Y < 0)
                {
                    _perso.DirectionPerso = new Vector2(0, 1) * _vitessePhysique;
                    _perso.ActualiseDeplacement(gameTime);
                }
                if (_perso.PositionPerso.Y > GraphicsDevice.Viewport.Height)
                {
                    _myGame.EcranAfficher = (int)EnumEcranAfficher.MORT;
                }
                //FIN GESTION PHYSIQUE 






                if (souris.LeftButton == ButtonState.Pressed && _fleche.EtatIntermediaire == false)
                {
                    _fleche.FlecheATirer++;

                    _fleche.EtatSouris = true;

                }
                if (_fleche.FlecheATirer > 4)
                {
                    _fleche.FlecheATirer = 0;
                }



                if (souris.LeftButton == ButtonState.Released && _fleche.EtatIntermediaire == true)
                {
                    _fleche.EtatIntermediaire = false;

                }

                if (_fleche.EtatSouris == true) //QUAND LE BOUTON EST CLIQUER ET QUE L'ANMATION ET PAS FINI
                {
                    _fleche.EtatIntermediaire = true;
                    if (_sensPerso == false)
                    {
                        _perso.Animation = "tireADroite";
                        _fleche.Fleches[_fleche.FlecheATirer].Position = _perso.PositionPerso;
                    }
                    else
                    {
                        _perso.Animation = "tireAGauche";
                        _fleche.Fleches[_fleche.FlecheATirer].Position =new Vector2(_perso.PositionPerso.X-40,_perso.PositionPerso.Y);
                    }

                   
                    _fleche.Fleches[_fleche.FlecheATirer].Temp += deltaTime;


                    if (_fleche.Fleches[_fleche.FlecheATirer].Temp > 0.8) //QUAND L'ANIMATION EST FINIS 
                    {
                        _fleche.EtatTirs[_fleche.FlecheATirer] = true;
                        _fleche.EtatSouris = false;
                        _sonTirFleche.Play();


                    }


                }



                for (int i = 0; i < _fleche.EtatTirs.Length; i++)
                {

                    if (_fleche.EtatTirs[i] == true)
                    {

                        _fleche.Fleches[i].Temp = 0;


                        if (_sensPerso == false && _fleche.SensDefinits[i] == false)
                        {
                            _fleche.Fleches[i].Direction = new Vector2(1, 0);
                            _fleche.SensDefinits[i] = true;
                            
                        }
                        else if (_fleche.SensDefinits[i] == false)
                        {
                            _fleche.Fleches[i].Direction = new Vector2(-1, 0);
                            _fleche.SensDefinits[i] = true;
                        }

                        _fleche.Fleches[i].Update(gameTime);


                        if (_fleche.Fleches[i].Position.X > GraphicsDevice.Viewport.Width)
                        {
                            _fleche.EtatTirs[i] = false;
                            _fleche.SensDefinits[i] = false;
                        }
                        else if (_fleche.Fleches[i].Position.X < 0)
                        {
                            _fleche.EtatTirs[i] = false;
                            _fleche.SensDefinits[i] = false;
                            _fleche.Fleches[i].Position = new Vector2(10000000, 100000);
                        }
                    }

                   
                    if (_fleche.Fleches[i].Intersects(_boss, 200, 175) && _fleche.Fleches[i].Position.X < GraphicsDevice.Viewport.Width)
                    {
                        int esquive = 0;

                        if (esquive == new Random().Next(0, 4) && _etatBoss == (int)ETAT_BOOS.ATTENTE)
                        {

                            _etatEsquive = true;

                        }

                        else
                        {
                            _boss.GetDamage(_vieBoss, gameTime);

                            _fleche.Fleches[i].Position = new Vector2(10000, 10000);

                        }



                    }
                    //esquive

                    if (_etatEsquive == true)
                    {

                        _tempsEsquive += deltaTime;
                        _boss.PositionPerso = new Vector2(GraphicsDevice.Viewport.Width - 150, GraphicsDevice.Viewport.Height - 400);
                        _boss.ActualiseDeplacement(gameTime);
                        if (_tempsEsquive >= 3)
                        {
                            _boss.PositionPerso = new Vector2(GraphicsDevice.Viewport.Width - 150, GraphicsDevice.Viewport.Height - 300);
                            _tempsEsquive = 0;
                            _etatEsquive = false;


                        }


                    }





                }












                //GESTION DES ATTAQUES


                if (_etatBoss == (int)ETAT_BOOS.ATTENTE)
                {


                    _boss.Temp += deltaTime;

                    if (_boss.Temp > 3)
                    {

                        _boss.Temp = 0;
                        _prochaineAttaque++;
                        if (_prochaineAttaque > (int)ETAT_BOOS.ATTAQUE3)
                        {
                            _prochaineAttaque = (int)ETAT_BOOS.ATTAQUE1;
                        }
                        _etatBoss = _prochaineAttaque;


                    }
                }






                //ATTAQUE NUMERO1


                if (_etatBoss == (int)ETAT_BOOS.ATTAQUE1 && _bossAction == false)
                {


                    _etatBossAttaque1 = (int)ATTAQUE1.DEBUT_ATTAQUE;
                    _sonEnvole.Play();
                    _bossAction = true;


                }
                if (_etatBossAttaque1 == (int)ATTAQUE1.DEBUT_ATTAQUE)
                {
                    _boss.DirectionPerso = new Vector2(0, -1);
                    _boss.ActualiseDeplacement(gameTime);
                    _boss.Animation = "allerAGauche";
                    if (_boss.PositionPerso.Y < 90)
                    {
                        _etatBossAttaque1 = (int)ATTAQUE1.ATTAQUE;
                    }
                }


                if (_etatBossAttaque1 == (int)ATTAQUE1.ATTAQUE)
                {

                    if (_positionTirer == false)
                    {
                        for (int i = 0; i < _positionLacheProjectil.Length; i++)
                        {

                            _positionLacheProjectil[i] = new Random().Next(0, 600);


                        }

                        _positionTirer = true;
                    }

                    _boss.DirectionPerso = new Vector2(-1, 0);
                    _boss.ActualiseDeplacement(gameTime);
                    _boss.Animation = "allerAGauche";


                    for (int i = 0; i < _positionLacheProjectil.Length; i++)
                    {
                        if (_boss.PositionPerso.X < _positionLacheProjectil[i] + 10 && _boss.PositionPerso.X > _positionLacheProjectil[i] - 10)
                        {

                            _projectileBoss[i].Position = _boss.PositionPerso;
                            _sonLacherEngrenage.Play();
                        }
                    }





                    if (_boss.PositionPerso.X < 0)
                    {
                        _etatBossAttaque1 = (int)ATTAQUE1.REVIENS;
                        _positionTirer = false;
                    }

                }

                if (_etatBossAttaque1 == (int)ATTAQUE1.REVIENS)
                {

                    _boss.DirectionPerso = new Vector2(1, 0);
                    _boss.ActualiseDeplacement(gameTime);
                    _boss.Animation = "allerADroite";

                    if (_boss.PositionPerso.X > GraphicsDevice.Viewport.Width - 150)
                    {
                        _etatBossAttaque1 = (int)ATTAQUE1.RETOUR_INACTIF;
                    }

                }
                if (_etatBossAttaque1 == (int)ATTAQUE1.RETOUR_INACTIF)
                {

                    _boss.DirectionPerso = new Vector2(0, 1);
                    _boss.ActualiseDeplacement(gameTime);
                    _boss.Animation = "allerADroite";

                    if (_boss.PositionPerso.Y > GraphicsDevice.Viewport.Height - 300)
                    {
                        _etatBossAttaque1 = (int)ATTAQUE1.INACTIF;

                    }

                }

                if (_etatBossAttaque1 == (int)ATTAQUE1.INACTIF)
                {
                    _boss.DirectionPerso = new Vector2(0, 0);
                    _boss.Animation = "immobile";
                    _boss.ActualiseDeplacement(gameTime);
                    _bossAction = false;
                    _etatBossAttaque1 = (int)ATTAQUE1.FIN;
                    _etatBoss = (int)ETAT_BOOS.ATTENTE;

                }



                foreach (Sprite projectil in _projectileBoss)
                {
                    ushort txProjectilBoss = (ushort)(projectil.Position.X / _map.TileWidth);
                    ushort tyProjectilBossDessous = (ushort)((ushort)(projectil.Position.Y / _map.TileHeight) + 1);
                    if (IsCollision(txProjectilBoss, tyProjectilBossDessous))
                    {
                        projectil.Position = new Vector2(1000, 1000);
                    }
                }

                //FIN ATTAQUE N1


                //ATTAQUE NUMERO 2


                if (_etatBoss == (int)ETAT_BOOS.ATTAQUE2 && _bossAction == false)
                {

                    _etatBossAttaque2 = (int)ATTAQUE2.DEBUT_ATTAQUE;
                    _bossAction = true;
                }

                if (_etatBossAttaque2 == (int)ATTAQUE2.DEBUT_ATTAQUE)
                {
                    _oiseauBoss.BarreDeVie = 1;
                    _oiseauBoss.PositionPerso = _boss.PositionPerso;
                    _sonTelecommande.Play();

                    _etatBossAttaque2 = (int)ATTAQUE2.ATTAQUE;

                }
                if (_etatBossAttaque2 == (int)ATTAQUE2.ATTAQUE)
                {
                    if (_perso.PositionPerso.X > _oiseauBoss.PositionPerso.X)
                    {


                        _oiseauBoss.DirectionPerso = new Vector2(1, 0);
                        _oiseauBoss.ActualiseDeplacement(gameTime);
                    }
                    if (_perso.PositionPerso.X < _oiseauBoss.PositionPerso.X)
                    {

                        _oiseauBoss.DirectionPerso = new Vector2(-1, 0);
                        _oiseauBoss.ActualiseDeplacement(gameTime);
                    }
                    if (_perso.PositionPerso.Y > _oiseauBoss.PositionPerso.Y)
                    {


                        _oiseauBoss.DirectionPerso = new Vector2(0, 1);
                        _oiseauBoss.ActualiseDeplacement(gameTime);
                    }
                    if (_perso.PositionPerso.Y < _oiseauBoss.PositionPerso.Y)
                    {
                        _oiseauBoss.DirectionPerso = new Vector2(0, -1);
                        _oiseauBoss.ActualiseDeplacement(gameTime);
                    }

                }
                //FIN ATTAQUE2


                //ATTAQUE3 

                if (_etatBoss == (int)ETAT_BOOS.ATTAQUE3 && _bossAction == false)
                {

                    _etatBossAttaque3 = (int)ATTAQUE3.DEBUT_ATTAQUE;
                    _bossAction = true;
                }


                if (_etatBossAttaque3 == (int)ATTAQUE3.DEBUT_ATTAQUE)
                {
                    _boss.DirectionPerso = new Vector2(0, -1);
                    _boss.ActualiseDeplacement(gameTime);

                    if ((_boss.PositionPerso.Y < -300))
                    {
                        _etatBossAttaque3 = (int)ATTAQUE3.REJOINS_POSITION;
                    }

                }

                if (_etatBossAttaque3 == (int)ATTAQUE3.REJOINS_POSITION)
                {
                    _boss.PositionPerso = new Vector2(_perso.PositionPerso.X, -300);
                    _etatBossAttaque3 = (int)ATTAQUE3.ATTAQUE;
                }
                if (_etatBossAttaque3 == (int)ATTAQUE3.ATTAQUE)
                {

                    _boss.Animation = "tombe";
                    _boss.DirectionPerso = new Vector2(0, 1);
                    _boss.ActualiseDeplacement(gameTime);

                    if (_boss.PositionPerso.Y > GraphicsDevice.Viewport.Height)
                    {
                        _etatBossAttaque3 = (int)ATTAQUE3.REVIENS;
                    }
                }

                if (_etatBossAttaque3 == (int)ATTAQUE3.REVIENS)
                {
                    _boss.PositionPerso = new Vector2(GraphicsDevice.Viewport.Width - 150, -200);
                    _etatBossAttaque3 = (int)ATTAQUE3.RETOUR_INACTIF;
                }

                if (_etatBossAttaque3 == (int)ATTAQUE3.RETOUR_INACTIF)
                {
                    _boss.Animation = "allerAGauche";
                    _boss.DirectionPerso = new Vector2(0, 1);
                    _boss.ActualiseDeplacement(gameTime);
                    if (_boss.PositionPerso.Y >= GraphicsDevice.Viewport.Height - 300)
                    {
                        _boss.Animation = "immobile";
                        _boss.DirectionPerso = new Vector2(0, 0);
                        _boss.ActualiseDeplacement(gameTime);
                        _etatBossAttaque3 = (int)ATTAQUE3.INACTIF;
                        _etatBoss = (int)ETAT_BOOS.ATTENTE;
                        _bossAction = false;
                    }
                }






                //GESTION BAR DE VIE

                //PROJECTILE BOSS
                foreach (Sprite projectil in _projectileBoss)
                {
                    if (_perso.Intersects(projectil, 80, 0) && _perso.Touchable == true)
                    {
                        _perso.GetDamage(_vie, gameTime);
                        projectil.Position = new Vector2(1000, 1000);
                        _perso.Touchable = false;


                    }
                }



                //OISEAUBOSS
                if (_oiseauBoss.Intersects(_perso, 29, 100) && _perso.Touchable == true)
                {
                    _oiseauBoss.PositionPerso = new Vector2(1000, 1000);
                    _perso.GetDamage(_vie, gameTime);
                    _perso.Touchable = false;
                    _etatBoss = (int)ETAT_BOOS.ATTENTE;
                    _etatBossAttaque2 = (int)ATTAQUE2.INACTIF;
                    _bossAction = false;
                }

                foreach (Sprite fleche in _fleche.Fleches)
                {
                    if (_oiseauBoss.Intersects(fleche, 0, 0))
                    {
                        _oiseauBoss.BarreDeVie--;
                        fleche.Position = new Vector2(2000, 2000);
                        _etatBoss = (int)ETAT_BOOS.ATTENTE;
                        _etatBossAttaque2 = (int)ATTAQUE2.INACTIF;
                        _bossAction = false;
                    }
                }


                if (_oiseauBoss.BarreDeVie <= 0)
                {
                    _oiseauBoss.PositionPerso = new Vector2(1000, 1000);

                }


                //BOSS/PERSO
                if (_perso.Intersects(_boss, 225, 200) && _perso.Touchable == true)
                {

                    _perso.GetDamage(_vie, gameTime);


                    //
                    _perso.Touchable = false;


                }




                //GESTION TOUCHABLE 
                if (_perso.Touchable == false)
                {

                    _perso.Temp += deltaTime;

                }

                if (_perso.Temp > 3)
                {
                    _perso.Touchable = true;
                    _perso.Temp = 0;
                }

                //GAGNER PERDU

                if (_boss.BarreDeVie <= 0)
                {
                    _myGame.EcranAfficher = (int)EnumEcranAfficher.GAGNER;
                }
                if (_perso.BarreDeVie <= 0)
                {
                    _myGame.EcranAfficher = (int)EnumEcranAfficher.MORT;
                }
                //


                //GESTION DE LA FLECHE

                foreach (Sprite projectile in _projectileBoss)
                {
                    projectile.Update(gameTime);
                }



                _boss.Actualise(gameTime);
                _perso.Actualise(gameTime);
                _oiseauBoss.Actualise(gameTime);





            }
            else
            {
                if (keyboardState.IsKeyUp(Keys.Escape) && _etatPause == (int)ETAT_PAUSE.PAUSE_DEMANDER)
                {
               
                    _etatPause = (int)ETAT_PAUSE.PAUSE;
                    
                }
                if(_etatPause ==(int)ETAT_PAUSE.PAUSE)
                {
                    _boutonQuitter.Position = new Vector2(GraphicsDevice.Viewport.Width / 2 - _boutonQuitter.Texture.Width / 2, GraphicsDevice.Viewport.Height / 2 - _boutonQuitter.Texture.Height / 2 + 200);
                    _boutonContinuer.Position = new Vector2(GraphicsDevice.Viewport.Width / 2 - _boutonQuitter.Texture.Width / 2, GraphicsDevice.Viewport.Height / 2 - _boutonQuitter.Texture.Height / 2 + 100);

                    if(souris.LeftButton == ButtonState.Pressed && _boutonQuitter.Contains(souris.Position))
                    {
                        _myGame.EcranAfficher = (int)EnumEcranAfficher.ACCEUIL;
                    }
                    if (keyboardState.IsKeyDown(Keys.Escape) || (souris.LeftButton == ButtonState.Pressed && _boutonContinuer.Contains(souris.Position)))
                    {
                        _boutonQuitter.Position = new Vector2(1000,1000);
                        _boutonContinuer.Position = new Vector2(1000,1000);
                        _etatPause = (int)ETAT_PAUSE.DEMANDE_REPRENDRE;
                    }
                }

                if (_etatPause == (int)ETAT_PAUSE.DEMANDE_REPRENDRE)
                {
                    
                
                    _etatPause = (int)ETAT_PAUSE.REPRENDRE;
                   
                }
                if(keyboardState.IsKeyUp(Keys.Escape) && _etatPause == (int)ETAT_PAUSE.REPRENDRE)
                {
                   
                    _etatPause = (int)ETAT_PAUSE.CONTINUE;
                    
                }
            }
           
        }


        public override void Draw(GameTime gameTime)
        {
            _myGame.GraphicsDevice.Clear(Color.Violet);
            _myGame.SpriteBatch.Begin();
            //ECRIRE ICI
            _mapRendu.Draw();

            foreach(Sprite fleche in _fleche.Fleches)
            {
                fleche.Draw(_myGame.SpriteBatch);
            }
            foreach (Sprite projectil in _projectileBoss)
            {
                projectil.Draw(_myGame.SpriteBatch);
            }
            _oiseauBoss.Draw(_myGame.SpriteBatch);
            _perso.Draw(_myGame.SpriteBatch);
            _boss.Draw(_myGame.SpriteBatch);
            //VIE PERSONNAGE 
            _vie.Draw(_myGame.SpriteBatch);
            _vieBoss.Draw(_myGame.SpriteBatch);
            _boutonQuitter.Draw(_myGame.SpriteBatch);
            _boutonContinuer.Draw(_myGame.SpriteBatch);
            _myGame.SpriteBatch.End();



        }
    
       
            private bool IsCollision(ushort x, ushort y)
        {
            // définition de tile qui peut être null (?)
            TiledMapTile? tile;
            if (_sol.TryGetTile(x, y, out tile) == false)
                return false;
            if (!tile.Value.IsBlank)
                return true;
            return false;
        }
      

    }


   
}

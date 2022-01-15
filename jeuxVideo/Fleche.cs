using System;
using System.Collections.Generic;
using System.Text;

namespace jeuxVideo
{
    class Fleche
    {
        private Sprite[] _fleches = new Sprite[5];
        private bool[] _etatTirs = new bool[5];
        private bool _etatIntermediaire;
        private int _flecheATirer;
        private bool _etatSouris;
        private bool[] _sensDefinits = new bool[5];

        public bool[] EtatTirs
        {
            get
            {
                return _etatTirs;
            }

            set
            {
                _etatTirs = value;
            }
        }

        public bool EtatIntermediaire
        {
            get
            {
                return _etatIntermediaire;
            }

            set
            {
                _etatIntermediaire = value;
            }
        }

        public int FlecheATirer
        {
            get
            {
                return _flecheATirer;
            }

            set
            {
                _flecheATirer = value;
            }
        }

        public bool EtatSouris
        {
            get
            {
                return _etatSouris;
            }

            set
            {
                _etatSouris = value;
            }
        }

        public bool[] SensDefinits
        {
            get
            {
                return _sensDefinits;
            }

            set
            {
                _sensDefinits = value;
            }
        }

        internal Sprite[] Fleches
        {
            get
            {
                return _fleches;
            }

            set
            {
                _fleches = value;
            }
        }
    }
}

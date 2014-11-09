using System;
using System.Collections.Generic;

using AntMe.Deutsch;

// Füge hier hinter AntMe.Spieler einen Punkt und deinen Namen ohne Leerzeichen
// ein! Zum Beispiel "AntMe.Spieler.WolfgangGallo".
namespace AntMe.Spieler
{
    
    public abstract class AbstrakteKonkreteMeise
    {
        
        public abstract void Wartet();

        public abstract void WirdMüde();

        public abstract void Sieht(Zucker zucker);

        public abstract void Sieht(Obst obst);

        public abstract void ZielErreicht(Zucker zucker);

        public abstract void ZielErreicht(Obst obst);

        public abstract void RiechtFreund(Markierung markierung);

        public abstract void SiehtFreund(Ameise ameise);

        public abstract void SiehtVerbündeten(Ameise ameise);

        public abstract void SiehtFeind(Wanze wanze);

        public abstract void SiehtFeind(Ameise ameise);

        public abstract void WirdAngegriffen(Wanze wanze);

        public abstract void WirdAngegriffen(Ameise ameise);

        public abstract void IstGestorben(Todesart todesart);

        public abstract void Tick();

    }
}
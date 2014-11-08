using System;
using System.Collections.Generic;

using AntMe.Deutsch;


// Füge hier hinter AntMe.Spieler einen Punkt und deinen Namen ohne Leerzeichen
// ein! Zum Beispiel "AntMe.Spieler.WolfgangGallo".
namespace AntMe.Spieler
{

    // Das Spieler-Attribut erlaubt das Festlegen des Volk-Names und von Vor-
    // und Nachname des Spielers. Der Volk-Name muß zugewiesen werden, sonst wird
    // das Volk nicht gefunden.
    [Spieler(
        Volkname = "Meine erste K#-Ameise",
        Vorname = "",
        Nachname = ""
    )]

    // Das Typ-Attribut erlaubt das Ändern der Ameisen-Eigenschaften. Um den Typ
    // zu aktivieren muß ein Name zugewiesen und dieser Name in der Methode 
    // BestimmeTyp zurückgegeben werden. Das Attribut kann kopiert und mit
    // verschiedenen Namen mehrfach verwendet werden.
    // Eine genauere Beschreibung gibts in Lektion 6 des Ameisen-Tutorials.
    [Kaste(
        Name = "KKI",
        GeschwindigkeitModifikator = -1,
        DrehgeschwindigkeitModifikator = -1,
        LastModifikator = -1,
        ReichweiteModifikator = -1,
        SichtweiteModifikator = 0,
        EnergieModifikator = 2,
        AngriffModifikator = 2
    )]

    [Kaste(
        Name = "Standard",
        GeschwindigkeitModifikator = 2,
        DrehgeschwindigkeitModifikator = 0,
        LastModifikator = 2,
        ReichweiteModifikator = -1,
        SichtweiteModifikator = -1,
        EnergieModifikator = -1,
        AngriffModifikator = -1
    )]

    public class MeineBasisAmeise : Basisameise
    {
        private AbstrakteKonkreteMeise meise;
        public override string BestimmeKaste(Dictionary<string, int> anzahl)
        {
            if ((anzahl["Standard"] + anzahl["KKI"]) % 5 == 0)
            {
                meise = new KKIAmeise(this);
                return "KKI";
            }
            meise = new MeineAmeise(this);
            return "Standard";
        }

        public override void Wartet()
        {
            meise.Wartet();
        }

        public override void WirdMüde()
        {
            meise.WirdMüde();
        }

        public override void Sieht(Zucker zucker)
        {
            meise.Sieht(zucker);
        }

        public override void Sieht(Obst obst)
        {
            meise.Sieht(obst);
        }

        public override void ZielErreicht(Zucker zucker)
        {
            meise.ZielErreicht(zucker);
        }

        public override void ZielErreicht(Obst obst)
        {
            meise.ZielErreicht(obst);
        }

        public override void RiechtFreund(Markierung markierung)
        {
            meise.RiechtFreund(markierung);
        }

        public override void SiehtFreund(Ameise ameise)
        {
            meise.SiehtFreund(ameise);
        }

        public override void SiehtVerbündeten(Ameise ameise)
        {
            meise.SiehtVerbündeten(ameise);
        }

        public override void SiehtFeind(Wanze wanze)
        {
            meise.SiehtFeind(wanze);
        }

        public override void SiehtFeind(Ameise ameise)
        {
            meise.SiehtFeind(ameise);
        }

        public override void WirdAngegriffen(Wanze wanze)
        {
            meise.WirdAngegriffen(wanze);
        }

        public override void WirdAngegriffen(Ameise ameise)
        {
            meise.WirdAngegriffen(ameise);
        }

        public override void IstGestorben(Todesart todesart)
        {
            meise.IstGestorben(todesart);
        }

        public override void Tick()
        {
            meise.Tick();
        }
    }
}
using System;
using System.Collections.Generic;

using AntMe.Deutsch;

// Füge hier hinter AntMe.Spieler einen Punkt und deinen Namen ohne Leerzeichen
// ein! Zum Beispiel "AntMe.Spieler.WolfgangGallo".
namespace AntMe.Spieler
{

    public class SpäherAmeise : AbstrakteKonkreteMeise
    {

        private MeineBasisAmeise masterMeise;
        private int kleinerRadius = 20;
        private int großerRadius = 150;
        private int feindRadius = 100;
        private int FundStelle;
        private int tickCounter = 0;
        private String state = "suche";

        public SpäherAmeise(MeineBasisAmeise masterMeise)
        {
            this.masterMeise = masterMeise;
        }

        #region Fortbewegung

        /// <summary>
        /// Wird wiederholt aufgerufen, wenn der die Ameise nicht weiss wo sie
        /// hingehen soll.
        /// </summary>
        public override void Wartet()
        {
            if (state == "suche")
            {
                masterMeise.DreheUmWinkel(Zufall.Zahl(-32, 32));
                masterMeise.GeheGeradeaus(20);
            }
            else if (state == "wegZumBau")
            {
                masterMeise.GeheZuBau();
            }
        }

        /// <summary>
        /// Wird einmal aufgerufen, wenn die Ameise ein Drittel ihrer maximalen
        /// Reichweite überschritten hat.
        /// </summary>
        public override void WirdMüde()
        {
        }

        #endregion

        #region Nahrung

        /// <summary>
        /// Wird wiederholt aufgerufen, wenn die Ameise mindestens einen
        /// Zuckerhaufen sieht.
        /// </summary>
        /// <param name="zucker">Der nächstgelegene Zuckerhaufen.</param>
        public override void Sieht(Zucker zucker)
        {
            // @TODO masterMeise.SprüheMarkierung(); Damit alle anderen Späher weglaufen
            if (state == "suche")
            {
                state = "gefunden";
                masterMeise.GeheZuZiel(zucker);
            }
        }

        /// <summary>
        /// Wird wiederholt aufgerufen, wenn die Ameise mindstens ein
        /// Obststück sieht.
        /// </summary>
        /// <param name="obst">Das nächstgelegene Obststück.</param>
        public override void Sieht(Obst obst)
        {
        }

        /// <summary>
        /// Wird einmal aufgerufen, wenn di e Ameise einen Zuckerhaufen als Ziel
        /// hat und bei diesem ankommt.
        /// </summary>
        /// <param name="zucker">Der Zuckerhaufen.</param>
        public override void ZielErreicht(Zucker zucker)
        {
            state = "angekommen";
            masterMeise.GeheZuBau();
        }

        /// <summary>
        /// Wird einmal aufgerufen, wenn die Ameise ein Obststück als Ziel hat und
        /// bei diesem ankommt.
        /// </summary>
        /// <param name="obst">Das Obstück.</param>
        public override void ZielErreicht(Obst obst)
        {
        }

        #endregion

        #region Kommunikation

        /// <summary>
        /// Wird einmal aufgerufen, wenn die Ameise eine Markierung des selben
        /// Volkes riecht. Einmal gerochene Markierungen werden nicht erneut
        /// gerochen.
        /// </summary>
        /// <param name="markierung">Die nächste neue Markierung.</param>
        public override void RiechtFreund(Markierung markierung)
        {
        }

        /// <summary>
        /// Wird wiederholt aufgerufen, wenn die Ameise mindstens eine Ameise des
        /// selben Volkes sieht.
        /// </summary>
        /// <param name="ameise">Die nächstgelegene befreundete Ameise.</param>
        public override void SiehtFreund(Ameise ameise)
        {
        }

        /// <summary>
        /// Wird aufgerufen, wenn die Ameise eine befreundete Ameise eines anderen Teams trifft.
        /// </summary>
        /// <param name="ameise"></param>
        public override void SiehtVerbündeten(Ameise ameise)
        {
        }

        #endregion

        #region Kampf

        /// <summary>
        /// Wird wiederholt aufgerufen, wenn die Ameise mindestens eine Wanze
        /// sieht.
        /// </summary>
        /// <param name="wanze">Die nächstgelegene Wanze.</param>
        public override void SiehtFeind(Wanze wanze)
        {
        }

        /// <summary>
        /// Wird wiederholt aufgerufen, wenn die Ameise mindestens eine Ameise eines
        /// anderen Volkes sieht.
        /// </summary>
        /// <param name="ameise">Die nächstgelegen feindliche Ameise.</param>
        public override void SiehtFeind(Ameise ameise)
        {
        }

        /// <summary>
        /// Wird wiederholt aufgerufen, wenn die Ameise von einer Wanze angegriffen
        /// wird.
        /// </summary>
        /// <param name="wanze">Die angreifende Wanze.</param>
        public override void WirdAngegriffen(Wanze wanze)
        {
        }

        /// <summary>
        /// Wird wiederholt aufgerufen in der die Ameise von einer Ameise eines
        /// anderen Volkes Ameise angegriffen wird.
        /// </summary>
        /// <param name="ameise">Die angreifende feindliche Ameise.</param>
        public override void WirdAngegriffen(Ameise ameise)
        {
        }

        #endregion

        #region Sonstiges

        /// <summary>
        /// Wird einmal aufgerufen, wenn die Ameise gestorben ist.
        /// </summary>
        /// <param name="todesart">Die Todesart der Ameise</param>
        public override void IstGestorben(Todesart todesart)
        {
        }

        /// <summary>
        /// Wird unabhängig von äußeren Umständen in jeder Runde aufgerufen.
        /// </summary>
        public override void Tick()
        {
            int z = masterMeise.RestWinkel;

            if (state == "angekommen")
            {
                state = "wegZumBau";
                tickCounter = 0;
            }
            else if (state == "wegZumBau")
            {
                tickCounter++;
                if (tickCounter == 30)
                {
                    FundStelle = masterMeise.Richtung;
                }
                if (masterMeise.EntfernungZuBau < 10 && masterMeise.EntfernungZuBau >= 1)
                {
                    masterMeise.SprüheMarkierung(FundStelle + 720, 100);
                }
                else if (masterMeise.EntfernungZuBau < 1)
                {
                    state = "suche";
                    masterMeise.BleibStehen();
                }
            }
        }

        #endregion

    }
}
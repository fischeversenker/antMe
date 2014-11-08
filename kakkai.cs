using System;
using System.Collections.Generic;

using AntMe.Deutsch;

namespace AntMe.Spieler
{

    public class KKIAmeise : AbstrakteKonkreteMeise
    {
        
        private MeineBasisAmeise masterMeise;
        private int kleinerRadius = 50;
        private int gro�erRadius = 100;

        public KKIAmeise(MeineBasisAmeise masterMeise) {
            this.masterMeise = masterMeise;
        }

        #region Fortbewegung

        /// <summary>
        /// Wird wiederholt aufgerufen, wenn der die Ameise nicht weiss wo sie
        /// hingehen soll.
        /// </summary>
        public override void Wartet()
        {
                masterMeise.DreheUmWinkel(Zufall.Zahl(-20, 20));
                masterMeise.GeheGeradeaus(20);
        }

        /// <summary>
        /// Wird einmal aufgerufen, wenn die Ameise ein Drittel ihrer maximalen
        /// Reichweite �berschritten hat.
        /// </summary>
        public override void WirdM�de()
        {
        }

        #endregion

        #region Nahrung

        /// <summary>
        /// Wird wiederholt aufgerufen, wenn die Ameise mindestens einen
        /// Zuckerhaufen sieht.
        /// </summary>
        /// <param name="zucker">Der n�chstgelegene Zuckerhaufen.</param>
        public override void Sieht(Zucker zucker)
        {
            Spielobjekt z = masterMeise.Ziel;
            masterMeise.Spr�heMarkierung(Koordinate.BestimmeRichtung(masterMeise,zucker), gro�erRadius);
            if (z != null)
            {
                masterMeise.GeheZuZiel(z);
            }
            else
            {
                masterMeise.GeheGeradeaus(20);
            }
        }

        /// <summary>
        /// Wird wiederholt aufgerufen, wenn die Ameise mindstens ein
        /// Obstst�ck sieht.
        /// </summary>
        /// <param name="obst">Das n�chstgelegene Obstst�ck.</param>
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
        }

        /// <summary>
        /// Wird einmal aufgerufen, wenn die Ameise ein Obstst�ck als Ziel hat und
        /// bei diesem ankommt.
        /// </summary>
        /// <param name="obst">Das Obst�ck.</param>
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
        /// <param name="markierung">Die n�chste neue Markierung.</param>
        public override void RiechtFreund(Markierung markierung)
        {
            if (!(masterMeise.Ziel is Wanze) && !(masterMeise.Ziel is Bau))
            {
                if (markierung.Information >= 360 && markierung.Information < 720)
                {
                    if (Koordinate.BestimmeEntfernung(masterMeise, markierung) < 50 && markierung.Information != 360)
                    {
                        masterMeise.DreheInRichtung(markierung.Information-360);
                        masterMeise.GeheGeradeaus(20);
                    }
                    else
                    {
                        masterMeise.Spr�heMarkierung(Koordinate.BestimmeRichtung(masterMeise, markierung)+360, kleinerRadius);
                        masterMeise.GeheZuZiel(markierung);
                    }
                }
                else if(markierung.Information != 0)
                {
                    masterMeise.DreheInRichtung(markierung.Information - 180);
                    masterMeise.GeheGeradeaus(20);
                }
                else
                {
                    masterMeise.GeheGeradeaus(50);
                }
            }
        }

        /// <summary>
        /// Wird wiederholt aufgerufen, wenn die Ameise mindstens eine Ameise des
        /// selben Volkes sieht.
        /// </summary>
        /// <param name="ameise">Die n�chstgelegene befreundete Ameise.</param>
        public override void SiehtFreund(Ameise ameise)
        {
        }

        /// <summary>
        /// Wird aufgerufen, wenn die Ameise eine befreundete Ameise eines anderen Teams trifft.
        /// </summary>
        /// <param name="ameise"></param>
        public override void SiehtVerb�ndeten(Ameise ameise)
        {
        }

        #endregion

        #region Kampf

        /// <summary>
        /// Wird wiederholt aufgerufen, wenn die Ameise mindestens eine Wanze
        /// sieht.
        /// </summary>
        /// <param name="wanze">Die n�chstgelegene Wanze.</param>
        public override void SiehtFeind(Wanze wanze)
        {
            masterMeise.Spr�heMarkierung(360, gro�erRadius);
            if (masterMeise.AktuelleEnergie >= masterMeise.MaximaleEnergie * 3 / 5)
            {
                masterMeise.GreifeAn(wanze);
            }
            else
            {
                masterMeise.GeheZuBau();
            }
        }

        /// <summary>
        /// Wird wiederholt aufgerufen, wenn die Ameise mindestens eine Ameise eines
        /// anderen Volkes sieht.
        /// </summary>
        /// <param name="ameise">Die n�chstgelegen feindliche Ameise.</param>
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
            masterMeise.GreifeAn(wanze);
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
            masterMeise.Spr�heMarkierung(360, gro�erRadius);
        }

        /// <summary>
        /// Wird unabh�ngig von �u�eren Umst�nden in jeder Runde aufgerufen.
        /// </summary>
        public override void Tick()
        {
            if ((masterMeise.Reichweite - masterMeise.Zur�ckgelegteStrecke - 50 < masterMeise.EntfernungZuBau) || masterMeise.AktuelleEnergie < masterMeise.MaximaleEnergie * 4 / 5)
            {
                masterMeise.GeheZuBau();
            }
        }

        #endregion

    }
}
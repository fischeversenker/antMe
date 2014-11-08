using System;
using System.Collections.Generic;

using AntMe.Deutsch;

// F�ge hier hinter AntMe.Spieler einen Punkt und deinen Namen ohne Leerzeichen
// ein! Zum Beispiel "AntMe.Spieler.WolfgangGallo".
namespace AntMe.Spieler
{

    public class MeineAmeise : AbstrakteKonkreteMeise
	{

        private MeineBasisAmeise masterMeise;
        private int kleinerRadius = 20;
        private int gro�erRadius = 150;
        private int feindRadius = 100;
 

        public MeineAmeise(MeineBasisAmeise masterMeise) {
            this.masterMeise = masterMeise;
        }

		#region Fortbewegung

		/// <summary>
		/// Wird wiederholt aufgerufen, wenn der die Ameise nicht weiss wo sie
		/// hingehen soll.
		/// </summary>
		public override void Wartet()
        {
            masterMeise.DreheUmWinkel(Zufall.Zahl(-10, 10));
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
            if (masterMeise.AktuelleLast / masterMeise.MaximaleLast < 0.8)
            {
                masterMeise.GeheZuZiel(zucker);
            }
            else
            {
                masterMeise.GeheZuBau();
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
            masterMeise.Spr�heMarkierung(0, gro�erRadius);
            masterMeise.Nimm(zucker);
            masterMeise.GeheZuBau();
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
            
            // Koordinate.BestimmeRichtung gibt immer einen Wert kleiner 359 aus.
            // Ich habe Werte kleiner 360 den Sammlern zugeteilt, Werte zwischen 360 und 719 f�r Krieger, 720 - 1079 vielleicht Sp�her oder so?
            if (markierung.Information < 360 && !(masterMeise.Ziel is Zucker) && !(masterMeise.Ziel is Bau) && !(masterMeise.Ziel is Markierung))
            {
                if (Koordinate.BestimmeEntfernung(masterMeise, markierung) < 50 && markierung.Information != 0)
                {
                    masterMeise.DreheInRichtung(markierung.Information);
                    masterMeise.GeheGeradeaus(20);
                }
                else
                {
                    masterMeise.Spr�heMarkierung(Koordinate.BestimmeRichtung(masterMeise, markierung), gro�erRadius);
                    masterMeise.GeheZuZiel(markierung);
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
            masterMeise.Spr�heMarkierung(Koordinate.BestimmeRichtung(masterMeise, wanze)+360, feindRadius);
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
            masterMeise.Spr�heMarkierung(360, feindRadius);
		}

		/// <summary>
		/// Wird unabh�ngig von �u�eren Umst�nden in jeder Runde aufgerufen.
		/// </summary>
		public override void Tick()
		{
            if (masterMeise.Reichweite - masterMeise.Zur�ckgelegteStrecke - 50 < masterMeise.EntfernungZuBau)
            {
                masterMeise.GeheZuBau();
            }
            else if (masterMeise.AktuelleLast > 0 && masterMeise.Ziel is Bau)
            {
                if (Koordinate.BestimmeEntfernung(masterMeise, masterMeise.Ziel) < 100)
                {
                    masterMeise.Spr�heMarkierung(Koordinate.BestimmeRichtung(masterMeise.Ziel, masterMeise), 100 - Koordinate.BestimmeEntfernung(masterMeise.Ziel, masterMeise));
                }
                else
                {
                    masterMeise.Spr�heMarkierung(Koordinate.BestimmeRichtung(masterMeise.Ziel, masterMeise), kleinerRadius);
                }
            }
		}

		#endregion
		 
	}
}
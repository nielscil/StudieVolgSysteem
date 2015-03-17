using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using WebMatrix.Data;
using WebMatrix.WebData;

/// <summary>
/// Afspraken uitwisselen met database
/// </summary>
public class Afspraak
{
    public int afspraakID { get; set; }
    public string opmerking { get; set; }
    public bool afgevinkt { get; set; }
    public int GesprekID { get; set; }
    /// <summary>
    /// Afspraak aanmaken
    /// </summary>
    /// <param name="AfspraakID">afspraakid</param>
    /// <param name="Opmerking">opmerking</param>
    /// <param name="Afgevinkt">afgevinkt</param>
    /// <param name="gesprekID">gesprekid van evaluatie</param>
    public Afspraak(int AfspraakID, string Opmerking, bool Afgevinkt, int gesprekID)
    {
        this.afspraakID = AfspraakID;
        this.opmerking = Opmerking;
        this.afgevinkt = Afgevinkt;
        this.GesprekID = gesprekID;
    }
    /// <summary>
    /// Afspraak maken
    /// </summary>
    /// <param name="Opmerking">opmerking</param>
    /// <param name="Afgevinkt">afgevinkt</param>
    /// <param name="gesprekID">gesprekid van evaluatie</param>
    public Afspraak(string Opmerking, bool Afgevinkt, int gesprekID)
    {
        this.opmerking = Opmerking;
        this.afgevinkt = Afgevinkt;
        this.GesprekID = gesprekID;
    }
    /// <summary>
    /// Afspraak toevoegen aan database
    /// </summary>
    /// <param name="Opmerking">opmerking</param>
    /// <param name="gesprekID">gesprekid van evaluatie</param>
    public static void AddAfspraak(string Opmerking, int gesprekID)
    {
        Database db = Database.Open(Constants.DBName);
        db.Execute("INSERT INTO Afspraken(Opmerking,Afgevinkt,gesprekID) VALUES (@0,@1,@2)", Opmerking, false, gesprekID);
        db.Close();
    }
    /// <summary>
    /// telt aantal afspraken in database
    /// </summary>
    /// <returns>aantal afspraken</returns>
    public static int Count()
    {
        Database db = Database.Open(Constants.DBName);
        int count = db.QueryValue("SELECT COUNT(afspraakid) FROM afspraken;");
        db.Close();
        return count;
    }
}
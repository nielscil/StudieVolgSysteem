using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using WebMatrix.Data;
using WebMatrix.WebData;

/// <summary>
/// Evaluatie uitwisselen met database
/// </summary>
public class Evaluatie
{
    public int GesprekID { get; set; }
    public string Besproken { get; set; }
    public DateTime DatumVolgendeGesprek { get; set; }
    public int AgendaID { get; set; }
    public string LocatieVolgendeGesprek { get; set; }

    /// <summary>
    /// Maakt evaluatie aan
    /// </summary>
    /// <param name="gesprekID">gesprekid</param>
    /// <param name="besproken">besproken</param>
    /// <param name="datumVolgendeGesprek">datum volgende gesprek</param>
    /// <param name="agendaID">agendaid</param>
    /// <param name="locatieVolgendeGesprek">locatie volgende gesprek</param>
    public Evaluatie(int gesprekID, string besproken, DateTime datumVolgendeGesprek, int agendaID, string locatieVolgendeGesprek)
    {
        this.GesprekID = gesprekID;
        this.Besproken = besproken;
        this.DatumVolgendeGesprek = datumVolgendeGesprek;
        this.AgendaID = agendaID;
        this.LocatieVolgendeGesprek = locatieVolgendeGesprek;
    }

    /// <summary>
    /// Maakt evaluatie aan
    /// </summary>
    /// <param name="besproken">besproken</param>
    /// <param name="datumVolgendeGesprek">datum volgende gesprek</param>
    /// <param name="agendaID">agendaid</param>
    /// <param name="locatieVolgendeGesprek">locatie volgende gesprek</param>
    public Evaluatie(string besproken, DateTime datumVolgendeGesprek, int agendaID, string locatieVolgendeGesprek)
    {
        this.Besproken = besproken;
        this.DatumVolgendeGesprek = datumVolgendeGesprek;
        this.AgendaID = agendaID;
        this.LocatieVolgendeGesprek = locatieVolgendeGesprek;
    }

    /// <summary>
    /// Voegt evaluatie toe aan database
    /// </summary>
    /// <param name="besproken">besproken</param>
    /// <param name="datumVolgendeGesprek">datum volgende gesprek</param>
    /// <param name="agendaID">agendaid</param>
    /// <param name="locatieVolgendeGesprek">locatie volgende gesprek</param>
    /// <returns>gesprekid van aangemaakte evaluatie</returns>
    public static int AddEvaluatie(string besproken, DateTime datumVolgendeGesprek, int agendaID, string locatieVolgendeGesprek)
    {
        Database db = Database.Open(Constants.DBName);
        db.Execute("INSERT INTO Evaluatie(besproken,agendaid) VALUES (@0,@1)", besproken, agendaID);
        //add volgende agenda afspraak
        var id = db.GetLastInsertId();
        db.Close();
        if (id == null)
            return -1;
        else
            return (int)id;
    }
    /// <summary>
    /// Voegt evaluatie toe aan database
    /// </summary>
    /// <param name="besproken">besproken</param>
    /// <param name="agendaID">agendaid</param>
    /// <returns>gesprekid van aangemaakte evaluatie</returns>
    public static int AddEvaluatie(string besproken, int agendaID)
    {
        Database db = Database.Open(Constants.DBName);
        db.Execute("INSERT INTO Evaluatie(besproken,agendaid) VALUES (@0,@1)", besproken, agendaID);
        var id = db.GetLastInsertId();
        db.Close();
        if (id == null)
            return -1;
        else
            return (int)id;
    }
    /// <summary>
    /// Checkt of een agenda afspraak al een evaluatie heeft
    /// </summary>
    /// <param name="agendaid">agendaid</param>
    /// <returns>true als er al een evaluatie is, anders false</returns>
    public static bool HasEvaluatie(int agendaid)
    {
        Database db = Database.Open(Constants.DBName);
        var row = db.QuerySingle("SELECT * FROM Evaluatie WHERE agendaid=@0", agendaid);
        if (row != null)
            return true;
        else
            return false;
    }

    /// <summary>
    /// Telt aantal evaluaties
    /// </summary>
    /// <returns>aantal evaluaties</returns>
    public static int Count()
    {
        Database db = Database.Open(Constants.DBName);
        int count = db.QueryValue("SELECT COUNT(gesprekid) FROM evaluatie;");
        db.Close();
        return count;
    }
}
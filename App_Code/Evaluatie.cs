using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using WebMatrix.Data;
using WebMatrix.WebData;

/// <summary>
/// Summary description for Evaluatie
/// </summary>
public class Evaluatie
{
    public int GesprekID { get; set; }
    public string Besproken { get; set; }
    public DateTime DatumVolgendeGesprek { get; set; }
    public int AgendaID { get; set; }
    public string LocatieVolgendeGesprek { get; set; }
    public Evaluatie(int gesprekID, string besproken, DateTime datumVolgendeGesprek, int agendaID, string locatieVolgendeGesprek)
    {
        this.GesprekID = gesprekID;
        this.Besproken = besproken;
        this.DatumVolgendeGesprek = datumVolgendeGesprek;
        this.AgendaID = agendaID;
        this.LocatieVolgendeGesprek = locatieVolgendeGesprek;
    }
    public Evaluatie(string besproken, DateTime datumVolgendeGesprek, int agendaID, string locatieVolgendeGesprek)
    {
        this.Besproken = besproken;
        this.DatumVolgendeGesprek = datumVolgendeGesprek;
        this.AgendaID = agendaID;
        this.LocatieVolgendeGesprek = locatieVolgendeGesprek;
    }
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
    public static int Count()
    {
        Database db = Database.Open(Constants.DBName);
        int count = db.QueryValue("SELECT COUNT(gesprekid) FROM evaluatie;");
        db.Close();
        return count;
    }
}
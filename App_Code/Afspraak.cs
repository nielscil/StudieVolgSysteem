using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using WebMatrix.Data;
using WebMatrix.WebData;

/// <summary>
/// Summary description for Afspraak
/// </summary>
public class Afspraak
{
    public int afspraakID { get; set; }
    public string opmerking { get; set; }
    public bool afgevinkt { get; set; }
    public int GesprekID { get; set; }
    public Afspraak(int AfspraakID, string Opmerking, bool Afgevinkt, int gesprekID)
    {
        this.afspraakID = AfspraakID;
        this.opmerking = Opmerking;
        this.afgevinkt = Afgevinkt;
        this.GesprekID = gesprekID;
    }
    public Afspraak(string Opmerking, bool Afgevinkt, int gesprekID)
    {
        this.opmerking = Opmerking;
        this.afgevinkt = Afgevinkt;
        this.GesprekID = gesprekID;
    }
    public static void AddAfspraak(string Opmerking, int gesprekID)
    {
        Database db = Database.Open(Constants.DBName);
        db.Execute("INSERT INTO Afspraken(Opmerking,Afgevinkt,gesprekID) VALUES (@0,@1,@2)", Opmerking, false, gesprekID);
        db.Close();
    }

    public static int Count()
    {
        Database db = Database.Open(Constants.DBName);
        int count = db.QueryValue("SELECT COUNT(afspraakid) FROM afspraken;");
        db.Close();
        return count;
    }
}
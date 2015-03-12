using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using WebMatrix.Data;
using WebMatrix.WebData;

/// <summary>
/// Summary description for Agenda
/// </summary>
public class Agenda
{
    int AfspaakID { get; set; }
    int StudentID { get; set; }
    int SLBerID { get; set; }
    DateTime Datum { get; set; }
    string Locatie { get; set; }
    bool Definitief { get; set; }
    int Tijdsduur { get; set; }

    public Agenda(int afspraakid, int studentid,int slberid,DateTime datum,string locatie,bool definitief,int tijdsduur)
    {
        this.AfspaakID = afspraakid;
        this.StudentID = studentid;
        this.SLBerID = slberid;
        this.Datum = datum;
        this.Locatie = locatie;
        this.Definitief = definitief;
        this.Tijdsduur = tijdsduur;
    }

    public Agenda(int studentid, int slberid, DateTime datum,string locatie, bool definitief, int tijdsduur)
    {
        this.StudentID = studentid;
        this.SLBerID = slberid;
        this.Datum = datum;
        this.Locatie = locatie;
        this.Definitief = definitief;
        this.Tijdsduur = tijdsduur;
    }

    public Agenda(int afspraakid, int studentid, int slberid, DateTime datum,string locatie)
    {
        this.AfspaakID = afspraakid;
        this.StudentID = studentid;
        this.SLBerID = slberid;
        this.Datum = datum;
        this.Locatie = locatie;
    }

    public Agenda(int studentid, int slberid, DateTime datum, string locatie)
    {
        this.StudentID = studentid;
        this.SLBerID = slberid;
        this.Datum = datum;
        this.Locatie = locatie;
    }

    public static int AddAgenda(int studentid, int slberid, DateTime datum, string locatie)
    {
        Database db = Database.Open(Constants.DBName);
        db.Execute("INSERT INTO agenda (studentid,slberid,datum,locatie) VALUES(@0,@1,@2,@3)", studentid, slberid, datum, locatie);
        var id = db.GetLastInsertId();
        db.Close();
        if (id == null)
            return -1;
        else
            return (int)id;
    }

    public static void AddAgenda(int studentid, int slberid, DateTime datum, string locatie, bool definitief, int tijdsduur)
    {
        Database db = Database.Open(Constants.DBName);
        db.Execute("INSERT INTO agenda (studentid,slberid,datum,locatie,definitief,tijdsduur) VALUES(@0,@1,@2,@3,@4,@5)", studentid, slberid, datum, locatie,definitief,tijdsduur);
        var id = db.GetLastInsertId();
        db.Close();
        if (id == null)
            return -1;
        else
            return (int)id;
    }

    public static List<Agenda> GetAgendas(int slberid)
    {
        Database db = Database.Open(Constants.DBName);
        var rows = db.Query("SELECT * FROM agenda WHERE slberid=@0 ORDER BY datum;",slberid);
        List<Agenda> list = new List<Agenda>();
        foreach(var r in rows)
        {
            bool def = r.Definitief ?? false;
            if(def)
            {
                Agenda a = new Agenda(r.AfspraakID, r.StudentID, r.SLBerID, r.Datum, r.locatie, r.Definitief, r.Tijdsduur);
                list.Add(a);
            }
            
        }
        return list;
    }
}

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
    public long afspraakID { get; set; }
    public string opmerking { get; set; }
    public bool afgevinkt { get; set; }
    public int GesprekID { get; set; }
    public DateTime Deathline {get;set;}
    /// <summary>
    /// Afspraak aanmaken
    /// </summary>
    /// <param name="AfspraakID">afspraakid</param>
    /// <param name="Opmerking">opmerking</param>
    /// <param name="Afgevinkt">afgevinkt</param>
    /// <param name="Deathline">Deathline</param>
    /// <param name="gesprekID">gesprekid van evaluatie</param>
    public Afspraak(long AfspraakID, string Opmerking, bool Afgevinkt, int gesprekID,DateTime Deathline)
    {
        this.afspraakID = AfspraakID;
        this.opmerking = Opmerking;
        this.afgevinkt = Afgevinkt;
        this.GesprekID = gesprekID;
        this.Deathline = Deathline;
    }
    /// <summary>
    /// Afspraak maken
    /// </summary>
    /// <param name="Opmerking">opmerking</param>
    /// <param name="Afgevinkt">afgevinkt</param>
    /// <param name="Deathline">Deathline</param>
    /// <param name="gesprekID">gesprekid van evaluatie</param>
    public Afspraak(string Opmerking, bool Afgevinkt, int gesprekID,DateTime Deathline)
    {
        this.opmerking = Opmerking;
        this.afgevinkt = Afgevinkt;
        this.GesprekID = gesprekID;
        this.Deathline = Deathline;
    }
    /// <summary>
    /// Afspraak toevoegen aan database
    /// </summary>
    /// <param name="Opmerking">opmerking</param>
    /// <param name="Deathline">Deathline</param>
    /// <param name="gesprekID">gesprekid van evaluatie</param>
    public static void AddAfspraak(string Opmerking, int gesprekID,DateTime deathline)
    {
        Database db = Database.Open(Constants.DBName);
        db.Execute("INSERT INTO Afspraken(Opmerking,Afgevinkt,gesprekID,deathline) VALUES (@0,@1,@2,@3)", Opmerking, false, gesprekID,deathline);
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
    /// <summary>
    /// update de afspraak
    /// </summary>
    /// <param name="agendaid">agendaid</param>
    /// <param name="locatie">locatie</param>
    /// <param name="definitief">definitief</param>
    /// <param name="tijdsduur">tijdsduur</param>
    public static void UpdateAgenda(int agendaid,string locatie, bool definitief,int tijdsduur)
    {
        Database db = Database.Open(Constants.DBName);
        db.Execute("Update agenda Set locatie=@0, definitief=@1 ,tijdsduur=@2 WHERE afspraakid=@3",locatie, definitief, tijdsduur, agendaid);
        db.Close();
    }

    /// <summary>
    /// update de overeenkomst
    /// </summary>
    /// <param name="afspraakid">id van overeenkomst</param>
    /// <param name="check">afgevinkt</param>
    public static void UpdateAfspraak(int afspraakid, bool check) 
    {
        Database db = Database.Open(Constants.DBName);
        db.Execute("UPDATE afspraken SET afgevinkt=@0 WHERE afspraakid=@1", check, afspraakid);
        db.Close();
    }

    /// <summary>
    /// haalt lijst met overeenkomsten op bij bepaalde afspraak
    /// </summary>
    /// <param name="gesprekid">id van overeenkomst</param>
    /// <returns>lijst met overeenkomsten</returns>
    public static List<Afspraak> GetAfspraken(int gesprekid)
    {
        Database db = Database.Open(Constants.DBName);
        var rows = db.Query("SELECT * FROM afspraken WHERE gesprekid=@0", gesprekid);
        db.Close();
        List<Afspraak> list = new List<Afspraak>();
        foreach(var row in rows)
        {
            list.Add(new Afspraak(row.afspraakID, row.opmerking, row.afgevinkt, row.GesprekID, row.Deathline));
        }
        return list;
    }

    /// <summary>
    /// Haalt lijst van overeenkomsten op bij een slb'er
    /// </summary>
    /// <param name="slberid">slb'erid</param>
    /// <returns>lijst met overeenkomsten</returns>
    public static List<Afspraak> GetAfspraakSLB(int slberid)
    {
        List<Agenda> agendas = Agenda.GetAgendas(slberid);
        List<Afspraak> afspraken = new List<Afspraak>();
        foreach(Agenda a in agendas)
        {
            try
            {
                afspraken.AddRange(Afspraak.GetAfspraken(Evaluatie.GetEvaluatie(a.AfspaakID).GesprekID));
            }
            catch
            {
                
            }
        }
        afspraken.Sort((x, y) => DateTime.Compare(x.Deathline, y.Deathline));
        return afspraken;
    }

    /// <summary>
    /// haalt overeenkomsten op bij student
    /// </summary>
    /// <param name="studentID">studentid</param>
    /// <returns>lijst met overeenkomsten</returns>
    public static List<Afspraak> GetAfsprakenStudent(int studentID)
    {
        List<Agenda> agendas = Agenda.GetAgendas(Student.GetStudent(studentID).SLBerID,studentID);
        List<Afspraak> afspraken = new List<Afspraak>();
        foreach(Agenda a in agendas)
        {
            try
            {
                int test = Evaluatie.GetEvaluatie(a.AfspaakID).GesprekID;
                afspraken.AddRange(GetAfspraken(test));
            }
            catch
            {
                
            }
        }       
        return afspraken;
    }

    /// <summary>
    /// Verwijderd een overeenkomst
    /// </summary>
    /// <param name="afspraakid">id van overeenkomst</param>
    public static void DeleteAfspraak(long afspraakid)
    {
        Database db = Database.Open(Constants.DBName);
        db.Execute("DELETE FROM afspraken WHERE afspraakid=@0", afspraakid);
        db.Close();
    }
}
/// <summary>
/// hulpklasse voor overeenkomsten
/// </summary>
public class HulpAfspraak
{
    public string Opmerking {get;set;}
    public DateTime Deathline {get;set;}

    public HulpAfspraak(string opmerking,DateTime deathline)
    {
        this.Opmerking = opmerking;
        this.Deathline = deathline;
    }
}
﻿using System;
using System.Globalization;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using WebMatrix.Data;
using WebMatrix.WebData;

/// <summary>
/// Agenda afspraken uitwisselen met database
/// </summary>
public class Agenda
{
    public int AfspaakID { get; set; }
    public int StudentID { get; set; }
    public int SLBerID { get; set; }
    public DateTime Datum { get; set; }
    public int Begin {get;set;}
    public string Locatie { get; set; }
    public bool Definitief { get; set; }
    public int Tijdsduur { get; set; }

    /// <summary>
    /// Agenda afspraak aanmaken
    /// </summary>
    /// <param name="afspraakid">afspraakid</param>
    /// <param name="studentid">studentid</param>
    /// <param name="slberid">slberid</param>
    /// <param name="datum">datum</param>
    /// <param name="locatie">locatie</param>
    /// <param name="definitief">afspraak definitief</param>
    /// <param name="begin">begintijd</param>
    /// <param name="tijdsduur">lengte van afspraak</param>
    public Agenda(int afspraakid, int studentid,int slberid,DateTime datum,string locatie,bool definitief,int begin,int tijdsduur)
    {
        this.AfspaakID = afspraakid;
        this.StudentID = studentid;
        this.SLBerID = slberid;
        this.Datum = datum;
        this.Locatie = locatie;
        this.Definitief = definitief;
        this.Begin = begin;
        this.Tijdsduur = tijdsduur;
    }
    /// <summary>
    /// Agenda afspraak aanmaken
    /// </summary>
    /// <param name="studentid">studentid</param>
    /// <param name="slberid">slberid</param>
    /// <param name="datum">datum</param>
    /// <param name="locatie">locatie</param>
    /// <param name="definitief">afspraak definitief</param>
    /// <param name="begin">begintijd</param>
    /// <param name="tijdsduur">lengte van afspraak</param>
    public Agenda(int studentid, int slberid, DateTime datum,string locatie, bool definitief,int begin, int tijdsduur)
    {
        this.StudentID = studentid;
        this.SLBerID = slberid;
        this.Datum = datum;
        this.Locatie = locatie;
        this.Definitief = definitief;
        this.Begin = begin;
        this.Tijdsduur = tijdsduur;
    }
    /// <summary>
    /// Agenda afspraak aanmaken
    /// </summary>
    /// <param name="afspraakid">afspraakid</param>
    /// <param name="studentid">studentid</param>
    /// <param name="slberid">slberid</param>
    /// <param name="datum">datum</param>
    /// <param name="locatie">locatie</param>
    /// <param name="begin">begintijd</param>
    public Agenda(int afspraakid, int studentid, int slberid, DateTime datum,int begin,string locatie)
    {
        this.AfspaakID = afspraakid;
        this.StudentID = studentid;
        this.SLBerID = slberid;
        this.Datum = datum;
        this.Begin = begin;
        this.Locatie = locatie;
    }
    /// <summary>
    /// Agenda afspraak aanmaken
    /// </summary>
    /// <param name="studentid">studentid</param>
    /// <param name="slberid">slberid</param>
    /// <param name="datum">datum</param>
    /// <param name="locatie">locatie</param>
    /// <param name="begin">begintijd</param>
    public Agenda(int studentid, int slberid, DateTime datum, string locatie,int begin)
    {
        this.StudentID = studentid;
        this.SLBerID = slberid;
        this.Datum = datum;
        this.Locatie = locatie;
        this.Begin = begin;
    }
    /// <summary>
    /// Voegt afspraak toe aan database
    /// </summary>
    /// <param name="studentid">studentid</param>
    /// <param name="slberid">slberid</param>
    /// <param name="datum">datum</param>
    /// <param name="locatie">locatie</param>
    /// <param name="begin">begin</param>
    /// <returns>agendaid van de toegevoegde afspraak</returns>
    public static int AddAgenda(int studentid, int slberid, DateTime datum,int begin,string locatie)
    {
        Database db = Database.Open(Constants.DBName);
        db.Execute("INSERT INTO agenda (studentid,slberid,datum, [begin],locatie ) VALUES(@0,@1,@2,@3,@4)", studentid, slberid, datum,begin,locatie);
        var id = db.GetLastInsertId();
        db.Close();
        if (id == null)
            return -1;
        else
            return (int)id;
    }
    /// <summary>
    /// Voegt afspraak toe aan database
    /// </summary>
    /// <param name="studentid">studentid</param>
    /// <param name="slberid">slberid</param>
    /// <param name="datum">datum</param>
    /// <param name="locatie">locatie</param>
    /// <param name="definitief">afspraak definitief</param>
    /// <param name="begin">begintijd</param>
    /// <param name="tijdsduur">lengte van afspraak</param>
    /// <returns>agendaid van de toegevoegde afspraak</returns>
    public static int AddAgenda(int studentid, int slberid, DateTime datum, string locatie, bool definitief,int begin, int tijdsduur)
    {
        Database db = Database.Open(Constants.DBName);
        db.Execute("INSERT INTO agenda (studentid,slberid,datum,locatie,definitief,tijdsduur, [begin] ) VALUES(@0,@1,@2,@3,@4,@5,@6)", studentid, slberid, datum, locatie,definitief,tijdsduur,begin);
        var id = db.GetLastInsertId();
        db.Close();
        if (id == null)
            return -1;
        else
            return (int)id;
    }

    /// <summary>
    /// update afspraak
    /// </summary>
    /// <param name="agendaid">afspraakid</param>
    /// <param name="locatie">locatie</param>
    /// <param name="definitief">definitief</param>
    /// <param name="tijdsduur">lengte van afspraak</param>
    public static void UpdateAgenda(int agendaid,string locatie, bool definitief,int tijdsduur)
    {
        Database db = Database.Open(Constants.DBName);
        db.Execute("Update agenda Set locatie=@0, definitief=@1 ,tijdsduur=@2 WHERE afspraakid=@3",locatie, definitief, tijdsduur, agendaid);
        db.Close();
    }

    /// <summary>
    /// verwijderd de afspraak
    /// </summary>
    /// <param name="agendaid">afspraakid</param>
    public static void DeleteAgenda(int agendaid)
    {
        Database db = Database.Open(Constants.DBName);
        db.Execute("DELETE FROM agenda WHERE afspraakid=@0",agendaid);
        db.Close();
    }

    /// <summary>
    /// Haal afspraken op voor specifieke slber
    /// </summary>
    /// <param name="slberid">slberid</param>
    /// <returns>lijst met afspraken</returns>
    public static List<Agenda> GetAgendas(int slberid)
    {
        Database db = Database.Open(Constants.DBName);
        var rows = db.Query("SELECT * FROM agenda WHERE slberid=@0 ORDER BY datum;",slberid);
        db.Close();
        List<Agenda> list = new List<Agenda>();
        foreach(var r in rows)
        {
            bool def = r.Definitief ?? false;
            if(def)
            {
                Agenda a = new Agenda(r.AfspraakID, r.StudentID, r.SLBerID, r.Datum, r.locatie, r.Definitief,r.Begin, r.Tijdsduur);
                list.Add(a);
            }
            else
            {
                Agenda a = new Agenda(r.AfspraakID, r.StudentID, r.SLBerID, r.Datum, r.Begin, r.locatie);
                list.Add(a);
            }
            
        }
        return list;
    }

    /// <summary>
    /// haalt laatste afspraak voor een student op
    /// </summary>
    /// <param name="studentid">studentid</param>
    /// <returns>afspraak</returns>
    public static Agenda GetLastestAgenda(int studentid)
    {
        Database db = Database.Open(Constants.DBName);
        var r = db.QuerySingle("SELECT top 1 * FROM agenda WHERE afspraakid IN (SELECT afspraakid FROM agenda WHERE studentid=@0 AND (definitief = 1 OR definitief IS NOT NULL)) ORDER BY datum DESC", studentid);
        db.Close();
        return new Agenda(r.AfspraakID, r.StudentID, r.SLBerID, r.Datum, r.locatie, r.Definitief, r.Begin, r.Tijdsduur);
    }

    /// <summary>
    /// Zoekt uit of een student een afspraak nodig heeft
    /// </summary>
    /// <param name="studentid">studentid</param>
    /// <param name="maanden">aantal maanden wanneer een afspraak ingepland moet worden</param>
    /// <returns>true wanneer er een afspraak aangevraagd moet worden, anders false</returns>
    public static bool NeedsAppointment(int studentid,int maanden)
    {
        Database db = Database.Open(Constants.DBName);
        var row = db.QuerySingle("SELECT top 1 * FROM agenda WHERE afspraakid IN (SELECT afspraakid FROM agenda WHERE studentid=@0 AND (definitief = 1 OR definitief IS NOT NULL)) ORDER BY datum DESC",studentid);
        db.Close();
        if(row != null)
        {
            DateTime datum = row.Datum;
            if(datum.AddMonths(maanden).Date < DateTime.Now.Date)
            {
                return true;
            }
            else
            return false; 
        }
        else
            return true;
    }

    /// <summary>
    /// haalt lijst van afspraken op voor een bepaald student
    /// </summary>
    /// <param name="slberid">slberid</param>
    /// <param name="studentid">studentid</param>
    /// <returns>lijst van afspraken</returns>
    public static List<Agenda> GetAgendas(int slberid,int studentid)
    {
        Database db = Database.Open(Constants.DBName);
        var rows = db.Query("SELECT * FROM agenda WHERE slberid=@0 AND studentid=@1 ORDER BY datum;", slberid,studentid);
        db.Close();
        List<Agenda> list = new List<Agenda>();
        foreach (var r in rows)
        {
            bool def = r.Definitief ?? false;
            if (def)
            {
                Agenda a = new Agenda(r.AfspraakID, r.StudentID, r.SLBerID, r.Datum, r.locatie, r.Definitief, r.Begin, r.Tijdsduur);
                list.Add(a);
            }
            else
            {
                Agenda a = new Agenda(r.AfspraakID, r.StudentID, r.SLBerID, r.Datum, r.Begin,r.locatie);
                list.Add(a);
            }

        }
        return list;
    }

    
    /// <summary>
    /// Haal lijst van afspraken op voor specieke slber en dag
    /// </summary>
    /// <param name="slberid">slberid</param>
    /// <param name="day">dag</param>
    /// <returns>lijst met afspraken</returns>
    public static List<Agenda> GetAgendaByDay(int slberid,DateTime day)
    {
        List<Agenda> raw = GetAgendas(slberid);
        List<Agenda> returnlist = new List<Agenda>();
        foreach(Agenda a in raw)
        {
            if(a.Datum.Date == day.Date)
            {
                returnlist.Add(a);
            }
        }
        return returnlist;
    }

    /// <summary>
    /// Haal specifieke afspraak op
    /// </summary>
    /// <param name="agendaid">agendaid</param>
    /// <returns>afspraak</returns>
    public static Agenda GetAgenda(int agendaid)
    {
        Database db = Database.Open(Constants.DBName);
        var r = db.QuerySingle("SELECT * FROM agenda WHERE afspraakid=@0;", agendaid);
        db.Close();
        if (r == null)
            throw new Exception("niet gevonden");
         bool def = r.Definitief ?? false;
         if(def)
         {
             return new Agenda(r.AfspraakID, r.StudentID, r.SLBerID, r.Datum, r.locatie, r.Definitief, r.Begin, r.Tijdsduur);
         }
         else
         {
             return new Agenda(r.AfspraakID, r.StudentID, r.SLBerID, r.Datum, r.Begin, r.locatie);
         }
        
    }

    /// <summary>
    /// haalt specifieke dag op in een array
    /// </summary>
    /// <param name="slberid">slberid</param>
    /// <param name="day">dag</param>
    /// <returns>array van intergers</returns>
    public static int[] GetAgendaDay(int slberid, DateTime day)
    {
        List<Agenda> afsprakenDag = Agenda.GetAgendaByDay(slberid, day);
        List<Rooster> rooster = Rooster.GetRoosterDay(slberid, day);
        int[] lijst = new int[32];
        foreach (Rooster r in rooster)
        {
            int bg = r.Begin;
            int length = r.Tijdsduur;
            for (int i = bg; i < bg + length; i++)
            {
                lijst[i] = -1;
            }
        }
        foreach (Agenda a in afsprakenDag)
        {
            int bg = a.Begin;
            int length = a.Tijdsduur;
            if (length == 0)
            {
                length = 1;
            }
            for (int i = bg; i < bg + length; i++)
            {
                lijst[i] = a.AfspaakID;
            }
        }
        return lijst;
    }

    /// <summary>
    /// Haalt eerste dag van de week op
    /// </summary>
    /// <param name="year">jaar</param>
    /// <param name="weekOfYear">weeknr</param>
    /// <returns>eerste dag van de week</returns>
    public static DateTime FirstDateOfWeek(int year, int weekOfYear)
    {
        DateTime jan1 = new DateTime(year, 1, 1);
        int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;
        DateTime firstThursday = jan1.AddDays(daysOffset);
        var cal = CultureInfo.CurrentCulture.Calendar;
        int firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

        var weekNum = weekOfYear;
        if (firstWeek <= 1)
        {
            weekNum -= 1;
        }
        var result = firstThursday.AddDays(weekNum * 7);
        return result.AddDays(-3);
    }
}

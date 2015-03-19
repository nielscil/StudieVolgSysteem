﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using WebMatrix.Data;
using WebMatrix.WebData;
using DDay.iCal;

/// <summary>
/// Summary description for Rooster
/// </summary>
public class Rooster
{
    public long RoosterID { get; set; }
    public int SLBerID { get; set; }
    public int Begin { get; set; }
    public int Tijdsduur { get; set; }
    public DateTime Datum { get; set; }

    public Rooster(long roosterID,int slberid,int begin,int tijdsduur,DateTime datum)
    {
        this.RoosterID = roosterID;
        this.SLBerID = slberid;
        this.Begin = begin;
        this.Tijdsduur = tijdsduur;
        this.Datum = datum;
    }

    public Rooster(int slberid, int begin, int tijdsduur,DateTime datum)
    {
        this.SLBerID = slberid;
        this.Begin = begin;
        this.Tijdsduur = tijdsduur;
        this.Datum = datum;
    }

    public static List<Rooster> GetRooster(int slberid)
    {
        Database db = Database.Open(Constants.DBName);
        var rows = db.Query("SELECT * FROM rooster WHERE slberid=@0", slberid);
        db.Close();
        List<Rooster> rooster = new List<Rooster>();
        foreach(var row in rows)
        {
            Rooster r = new Rooster(row.RoosterID, row.SLBerID, row.Begin, row.Tijdsduur,row.Datum);
            rooster.Add(r);
        }
        return rooster;
    }
    public static List<Rooster> GetRoosterDay(int slberid, DateTime day)
    {
        List<Rooster> raw = GetRooster(slberid);
        List<Rooster> niew = new List<Rooster>();
        foreach(Rooster r in raw)
        {
            if(r.Datum.Date == day.Date)
            {
                niew.Add(r);
            }
        }
        return niew;
    }

    public static void UpdateRooster(int slberid, string url)
    {
        DeleteRooster(slberid);
        Database db = Database.Open(Constants.DBName);
        IICalendarCollection kalender = iCalendar.LoadFromUri(new Uri(url));
        TimeSpan begin = new TimeSpan(8,0,0);
        TimeSpan eind = new TimeSpan(16,0,0);
        foreach(IEvent ev in kalender[0].Events)
        {
            if(ev.Start.TimeOfDay > begin && ev.Start.TimeOfDay < eind)
            {
                DateTime datum = DateTime.Parse(ev.Start.ToString());
                TimeSpan start = ev.Start.TimeOfDay;
                TimeSpan stop = ev.End.TimeOfDay;
                TimeSpan verschil = stop - start;
                int beginUur = start.Hours - 8;
                int beginMin = start.Minutes;
                int beginNr = beginUur * 4 + beginMin / 15;
                int tijdsDuurUur = verschil.Hours;
                int tijdsDuurMin = verschil.Minutes;
                int tijdsDuur = tijdsDuurUur * 4 + tijdsDuurMin / 15;
                if(tijdsDuur + beginNr > 32)
                {
                    tijdsDuur = 32 - beginNr;
                }
                db.Execute("INSERT INTO rooster (slberid, [Begin], tijdsduur, Datum) VALUES(@0,@1,@2,@3)", slberid, beginNr, tijdsDuur,datum);
            }
        }
        db.Close();
    }
    private static void DeleteRooster(int slberid)
    {
        Database db = Database.Open(Constants.DBName);
        db.Execute("DELETE FROM rooster WHERE slberid=@0", slberid);
        db.Close();
    }
}

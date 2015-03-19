﻿using System;
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

    public static void DeleteAfspraak(long afspraakid)
    {
        Database db = Database.Open(Constants.DBName);
        db.Execute("DELETE FROM afspraken WHERE afspraakid=@0", afspraakid);
        db.Close();
    }
}

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
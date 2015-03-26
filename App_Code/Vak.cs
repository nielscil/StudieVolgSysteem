using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using WebMatrix.Data;
using WebMatrix.WebData;

/// <summary>
/// Vakken uitwisselen met database
/// </summary>
public class Vak
{
    public int VakID { get; set; }
    public string Naam { get; set; }
    public int EC {get;set;}

    /// <summary>
    /// Maakt vak aan
    /// </summary>
    /// <param name="vakid">vakid</param>
    /// <param name="naam">naam</param>
	public Vak(int vakid,string naam,int ec)
	{
        this.VakID = vakid;
        this.Naam = naam;
        this.EC = ec;
	}

    /// <summary>
    /// Maakt vak aan
    /// </summary>
    /// <param name="naam">naam</param>
    public Vak(string naam,int ec)
    {
        this.Naam = naam;
        this.EC = ec;
    }

    /// <summary>
    /// Haalt lijst van vakken op
    /// </summary>
    /// <returns>lijst van vakken</returns>
    public static List<Vak> GetSubjects()
    {
        Database db = Database.Open(Constants.DBName);
        var rows = db.Query("SELECT * FROM vakken ORDER BY naam;");
        db.Close();
        List<Vak> list = new List<Vak>();
        foreach(var r in rows)
        {
            Vak v = new Vak(r.VakID, r.Naam,r.EC);
            list.Add(v);
        }
        return list;
    }

    /// <summary>
    /// Haalt lijst van vakken op specifiek voor een opleiding
    /// </summary>
    /// <param name="opleidingid">opleidingid</param>
    /// <returns>lijst van vakken</returns>
    public static List<Vak> GetSubjects(int opleidingid)
    {
        Database db = Database.Open(Constants.DBName);
        var rows = db.Query("SELECT * FROM vakken WHERE vakid in(SELECT vakid FROM opl_vak WHERE opleidingid=@0) ORDER BY naam;",opleidingid);
        db.Close();
        List<Vak> list = new List<Vak>();
        foreach (var r in rows)
        {
            Vak v = new Vak(r.VakID, r.Naam,r.EC);
            list.Add(v);
        }
        return list;
    }

    /// <summary>
    /// Haalt opleidingen op bij een specifiek vak
    /// </summary>
    /// <param name="vakid">vakid</param>
    /// <returns>lijst van opleidingen</returns>
    public static List<Opleiding> GetStudies(int vakid)
    {
        Database db = Database.Open(Constants.DBName);
        var rows = db.Query("SELECT * FROM opleidingen WHERE opleidingid in (SELECT opleidingid FROM opl_vak WHERE vakid=@0) ORDER BY naam;",vakid);
        db.Close();
        List<Opleiding> list = new List<Opleiding>();
        foreach(var r in rows)
        {
            Opleiding o = Opleiding.GetStudy(r.OpleidingID);
            list.Add(o);
        }
        return list;
    }

    /// <summary>
    /// Haalt vak op
    /// </summary>
    /// <param name="vakid">vakid</param>
    /// <returns>vak</returns>
    public static Vak GetSubject(int vakid)
    {
        Database db = Database.Open(Constants.DBName);
        var row = db.QuerySingle("SELECT * FROM vakken WHERE vakid=@0;",vakid);
        db.Close();
        Vak v = new Vak(row.VakID, row.Naam,row.EC);
        return v;
    }

    /// <summary>
    /// Voegt nieuw vak toe
    /// </summary>
    /// <param name="naam">naam</param>
    /// <returns>id van toegevoegde vak</returns>
    public static int AddSubject(string naam,int ec)
    {
        Database db = Database.Open(Constants.DBName);
        db.Execute("INSERT INTO vakken (naam,ec) VALUES(@0,@1)", naam, ec);
        var row = db.GetLastInsertId();
        db.Close();
        if (row == null)
            return -1;
        else
            return (int)row;
    }

    /// <summary>
    /// Update vak
    /// </summary>
    /// <param name="vakid">vakid</param>
    /// <param name="naam">naam</param>
    public static void UpdateSubject(int vakid,string naam,int ec)
    {
        Database db = Database.Open(Constants.DBName);
        db.Execute("UPDATE vakken SET naam=@0, ec=@1 WHERE vakid=@2", naam, ec,vakid);
        db.Close();
    }

    /// <summary>
    /// Telt aantal vakken
    /// </summary>
    /// <returns>aantal vakken</returns>
    public static int Count()
    {
        Database db = Database.Open(Constants.DBName);
        int count = db.QueryValue("SELECT COUNT(vakid) FROM vakken;");
        db.Close();
        return count;
    }

    /// <summary>
    /// Verwijderd specifiek vak
    /// </summary>
    /// <param name="vakid">vakid</param>
    public static void DelSubject(int vakid)
    {
        Database db = Database.Open(Constants.DBName);
        db.Execute("DELETE FROM opl_vak WHERE vakid=@0", vakid);
        db.Execute("DELETE FROM vakken WHERE vakid=@0", vakid);
        db.Close();
    }

    /// <summary>
    /// Zoekt naar vakken met een specifieke zoekterm
    /// </summary>
    /// <param name="input">zoekterm</param>
    /// <returns>lijst van vakken</returns>
    public static List<Vak> SearchSubject(string input)
    {
        Database db = Database.Open(Constants.DBName);
        List<Vak> lijst = new List<Vak>();
        input = "%" + input + "%";
        var rows = db.Query("SELECT * FROM vakken WHERE naam LIKE @0 ORDER BY naam;", input);
        foreach (var r in rows)
        {
            Vak g = new Vak(r.VakID,r.Naam,r.EC);
            lijst.Add(g);
        }
        db.Close();
        return lijst;
    }
}
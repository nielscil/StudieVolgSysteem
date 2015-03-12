using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using WebMatrix.Data;
using WebMatrix.WebData;

/// <summary>
/// Summary description for Opleiding
/// </summary>
public class Opleiding
{
    public int OpleidingID { get; set; }
    public string Naam { get; set; }
    public List<Vak> Vakken { get; set; }

	public Opleiding(int opleidingid,string naam,List<Vak> vakken)
	{
        this.OpleidingID = opleidingid;
        this.Naam = naam;
        this.Vakken = vakken;
	}

    public Opleiding(string naam, List<Vak> vakken)
    {
        this.Naam = naam;
        this.Vakken = vakken;
    }

    public static List<Opleiding> GetStudies()
    {
        Database db = Database.Open(Constants.DBName);
        var rows = db.Query("SELECT * FROM opleidingen ORDER BY naam;");
        db.Close();
        List<Opleiding> list = new List<Opleiding>();
        foreach (var r in rows)
        {
            List<Vak> vakken = Vak.GetSubjects(r.OpleidingID);
            Opleiding v = new Opleiding(r.OpleidingID, r.Naam, vakken);
            list.Add(v);
        }
        return list;
    }

    public static Opleiding GetStudy(int opleidingid)
    {
        Database db = Database.Open(Constants.DBName);
        var row = db.QuerySingle("SELECT * FROM opleidingen WHERE opleidingid=@0;", opleidingid);
        db.Close();
        List<Vak> vakken = Vak.GetSubjects(opleidingid);
        Opleiding v = new Opleiding(row.OpleidingID, row.Naam, vakken);
        return v;
    }

    public static int AddStudy(string naam)
    {
        Database db = Database.Open(Constants.DBName);
        db.Execute("INSERT INTO opleidingen (Naam) VALUES(@0)", naam);
        var id = db.GetLastInsertId();
        db.Close();
        if (id == null)
            return -1;
        else
            return (int)id;
    }

    public static void AddSubject(int opleidingid, int vakid)
    {
        Database db = Database.Open(Constants.DBName);
        db.Execute("INSERT INTO opl_vak (opleidingid,vakid) VALUES(@0,@1)", opleidingid,vakid);
        db.Close();
    }

    public static void DelSubject(int opleidingid, int vakid)
    {
        Database db = Database.Open(Constants.DBName);
        db.Execute("DELETE FROM opl_vak WHERE opleidingid=@0 AND vakid=@1", opleidingid, vakid);
        db.Close();
    }

    public static void DelStudy(int opleidingid)
    {
        Database db = Database.Open(Constants.DBName);
        db.Execute("DELETE FROM opl_vak WHERE opleidingid=@0", opleidingid);
        db.Execute("DELETE FROM opleidingen WHERE opleidingid=@0", opleidingid);
        db.Close();
    }

    public static void UpdateStudy(int opleidingid, string naam)
    {
        Database db = Database.Open(Constants.DBName);
        db.Execute("UPDATE opleidingen SET naam=@0 WHERE opleidingid=@1", naam, opleidingid);
        db.Close();
    }

    public static int Count()
    {
        Database db = Database.Open(Constants.DBName);
        int count = db.QueryValue("SELECT COUNT(opleidingid) FROM opleidingen;");
        db.Close();
        return count;
    }

    public static List<Opleiding> SearchStudy(string input)
    {
        Database db = Database.Open(Constants.DBName);
        List<Opleiding> lijst = new List<Opleiding>();
        input += "%";
        var rows = db.Query("SELECT * FROM opleidingen WHERE naam LIKE @0 ORDER BY naam;", input);
        foreach (var r in rows)
        {
            List<Vak> vakken = Vak.GetSubjects(r.OpleidingID);
            Opleiding g = new Opleiding(r.OpleidingID,r.Naam,vakken);
            lijst.Add(g);
        }
        db.Close();
        return lijst;
    }
}
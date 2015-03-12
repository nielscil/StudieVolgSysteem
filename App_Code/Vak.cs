using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using WebMatrix.Data;
using WebMatrix.WebData;

public class Vak
{
    public int VakID { get; set; }
    public string Naam { get; set; }
	public Vak(int vakid,string naam)
	{
        this.VakID = vakid;
        this.Naam = naam;
	}

    public Vak(string naam)
    {
        this.Naam = naam;
    }

    public static List<Vak> GetSubjects()
    {
        Database db = Database.Open(Constants.DBName);
        var rows = db.Query("SELECT * FROM vakken ORDER BY naam;");
        db.Close();
        List<Vak> list = new List<Vak>();
        foreach(var r in rows)
        {
            Vak v = new Vak(r.VakID, r.Naam);
            list.Add(v);
        }
        return list;
    }

    public static List<Vak> GetSubjects(int opleidingid)
    {
        Database db = Database.Open(Constants.DBName);
        var rows = db.Query("SELECT * FROM vakken WHERE vakid in(SELECT vakid FROM opl_vak WHERE opleidingid=@0) ORDER BY naam;",opleidingid);
        db.Close();
        List<Vak> list = new List<Vak>();
        foreach (var r in rows)
        {
            Vak v = new Vak(r.VakID, r.Naam);
            list.Add(v);
        }
        return list;
    }

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

    public static Vak GetSubject(int vakid)
    {
        Database db = Database.Open(Constants.DBName);
        var row = db.QuerySingle("SELECT * FROM vakken WHERE vakid=@0;",vakid);
        db.Close();
        Vak v = new Vak(row.VakID, row.Naam);
        return v;
    }

    public static int AddSubject(string naam)
    {
        Database db = Database.Open(Constants.DBName);
        db.Execute("INSERT INTO vakken (naam) VALUES(@0)", naam);
        var row = db.GetLastInsertId();
        db.Close();
        if (row == null)
            return -1;
        else
            return (int)row;
    }

    public static void UpdateSubject(int vakid,string naam)
    {
        Database db = Database.Open(Constants.DBName);
        db.Execute("UPDATE vakken SET naam=@0 WHERE vakid=@1", naam,vakid);
        db.Close();
    }

    public static int Count()
    {
        Database db = Database.Open(Constants.DBName);
        int count = db.QueryValue("SELECT COUNT(vakid) FROM vakken;");
        db.Close();
        return count;
    }

    public static void DelSubject(int vakid)
    {
        Database db = Database.Open(Constants.DBName);
        db.Execute("DELETE FROM opl_vak WHERE vakid=@0", vakid);
        db.Execute("DELETE FROM vakken WHERE vakid=@0", vakid);
        db.Close();
    }

    public static List<Vak> SearchSubject(string input)
    {
        Database db = Database.Open(Constants.DBName);
        List<Vak> lijst = new List<Vak>();
        input = "%" + input + "%";
        var rows = db.Query("SELECT * FROM vakken WHERE naam LIKE @0 ORDER BY naam;", input);
        foreach (var r in rows)
        {
            Vak g = new Vak(r.VakID,r.Naam);
            lijst.Add(g);
        }
        db.Close();
        return lijst;
    }
}
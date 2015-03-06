using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using WebMatrix.Data;
using WebMatrix.WebData;

public class Cijfer
{
    public int CijferID {get;set;}
    public int StudentID {get;set;}
    public int VakID {get;set;}
    public int Cijfertje {get;set;}

    public Cijfer(int cijferid,int studentid,int vakid,int cijfer)
    {
        this.CijferID = cijferid;
        this.StudentID = studentid;
        this.VakID = vakid;
        this.Cijfertje = cijfer;
    }

    public Cijfer(int studentid,int vakid,int cijfer)
    {
        this.StudentID = studentid;
        this.VakID = vakid;
        this.Cijfertje = cijfer;
    }

    public static List<Cijfer> GetGradesFromStudent(int studentid)
    {
        Database db = Database.Open(Constants.DBName);
        var rows = db.Query("SELECT * FROM cijfers WHERE studentid=@0",studentid);
        db.Close();
        List<Cijfer> list = new List<Cijfer>();
        foreach(var r in rows)
        {
            Cijfer c = new Cijfer(r.CijferID,r.StudentID,r.VakID,r.Cijfer);
            list.Add(c);
        }
        return list;

    }
}

public class Algoritme
{
    enum Prioriteit {Laag=1, Gemiddeld, Hoog};
    public Algoritme(){}

    //Berekent de prioriteit.
    public string ber_Prioriteit(int studID)
    {
        int prioProcent = 100;
        int count = Cijfer.GetGradesFromStudent(studID).Count;

        //Berekend het prioProcent van de student.
        foreach(Cijfer c in Cijfer.GetGradesFromStudent(studID))
        {
            switch(c.Cijfertje)
            {
                case 5: case 4: 
                prioProcent = prioProcent - (100 / count);
                        break;

                case 3: case 2: case 1: 
                prioProcent = prioProcent - (100 / count);
                        break;
            }
        }

        if (prioProcent >= 75) {return "Laag";}
        else if (prioProcent <= 75 && prioProcent >= 50) {return "Gemiddeld";}
        else {return "Hoog";}
    }
}


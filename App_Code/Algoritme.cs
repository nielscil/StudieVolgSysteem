using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using WebMatrix.Data;
using WebMatrix.WebData;

/// <summary>
/// Algoritme van status student
/// </summary>
public class Algoritme
{
    enum Prioriteit {Laag=1, Gemiddeld, Hoog};
    public Algoritme(){}

    //Berekent de prioriteit.

    /// <summary>
    /// berekend de status van de student
    /// </summary>
    /// <param name="studID">studentid</param>
    /// <returns>status als integer</returns>
    public static int ber_Prioriteit(int studID)
    {
        int prioLevel = 100;
        int count = Cijfer.GetGradesFromStudent(studID).Count;
        
        //Berekend het prioProcent van de student
        // haalt het cijfer op van de student
        foreach (Cijfer c in Cijfer.GetGradesFromStudent(studID))
        {
            // kijkt of het cijfer lager is als 6
            // haalt het vak op
            Vak vak = Vak.GetSubject(c.VakID);
            if(c.Cijfertje <6)
            {
            // ziet hoeveel ec's het vak waard is en haalt het bijbehoorende aantal prioprocent weg
            if(vak.EC == 1)
                    prioLevel = prioLevel - 5;

            if(vak.EC == 2)
                    prioLevel = prioLevel - 10;

            if(vak.EC == 3)
                    prioLevel = prioLevel -  15;

            if(vak.EC > 3 )
                    prioLevel = prioLevel -  20;
                    }
            }
        return prioLevel;
    }
}   


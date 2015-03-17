using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using WebMatrix.Data;
using WebMatrix.WebData;

public class Algoritme
{
    enum Prioriteit {Laag=1, Gemiddeld, Hoog};
    public Algoritme(){}

    //Berekent de prioriteit.
    public static int ber_Prioriteit(int studID)
    {
        int prioProcent = 100;
        int count = Cijfer.GetGradesFromStudent(studID).Count;

        //Berekend het prioProcent van de student.
        foreach(Cijfer c in Cijfer.GetGradesFromStudent(studID))
        {
            prioProcent = prioProcent - (100 / count) * (5 - c.Cijfertje + 1);
            switch(c.Cijfertje)
            {
                case 5: case 4: 
                prioProcent = prioProcent - (100 / count);
                        break;

                case 3: case 2: case 1: 
                prioProcent = prioProcent - (100 / count)*2;
                        break;
            }
        }
        return prioProcent;
    }
}


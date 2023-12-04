using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _4_gewinnt_20._11._2023
{
    public partial class Form1 : Form
    {
        Label[,] Feld = new Label[7, 6];
        bool red = true;                                                                   //die Farbe rot fängt an
        int[] hoehe = { 5, 5, 5, 5, 5, 5, 5 };
        int moves = 42;                                                                    //unentschieden ingsgesamt 42 Züge
        public Form1()
        {
            InitializeComponent();                                                          // damit wir nicht jeden einzelnen Spielstein programmieren müssen bearbeiten wir diesen befehl
            var path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddEllipse(0, 0, 100, 100);


            int index = 2;
            this.BackColor = Color.Blue;                                                    //hintergrund auf blau gesetzt
            for (int X = 0; X < 7; X++)
            {
                for (int Y = 0; Y < 6; Y++)
                {
                    Feld[X, Y] = new System.Windows.Forms.Label();
                    Feld[X, Y].BackColor = Color.White;
                    Feld[X, Y].AutoSize = false;                                            // damit man die größe bestimmen kann
                    Feld[X, Y].Location = new System.Drawing.Point(X * 100, Y * 100);
                    Feld[X, Y].Name = "label1" + (index++);                                 // index erhöht jedes mal
                    Feld[X, Y].Size = new System.Drawing.Size(100, 100);                    // größe des spielsteins
                    Feld[X, Y].TabIndex = index;
                    Feld[X, Y].Region = new Region(path);
                    Feld[X, Y].Text = "";
                    Feld[X, Y].AccessibleDescription = "";
                    Feld[X, Y].Click += new System.EventHandler(this.label1_Click);
                    this.Controls.Add(Feld[X, Y]);
                }
            }
        }
        private bool InReihe(int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4)
        {
            if (Feld[x1, y1].AccessibleDescription == Feld[x2, y2].AccessibleDescription &&    // Zeile 46 bis 49 vergleicht man die einzelnen Zeilen für die gewinnabfrage miteinander
                Feld[x2, y2].AccessibleDescription == Feld[x3, y3].AccessibleDescription &&     //accessibledescription ist ein vergleich zwischen...
                Feld[x3, y3].AccessibleDescription == Feld[x4, y4].AccessibleDescription &&
                Feld[x1, y1].AccessibleDescription != "")
                return true;
            return false;
        }


        private bool Diagonale1()                               //überprüfung von oben links nach unten rechts
        {
            for (int x = 0; x < 4; x++)
            {
                for (int y = 5; y >= 3; y--)
                {
                    if (InReihe(x, y, x + 1, y - 1, x + 2, y - 2, x + 3, y - 3))
                        return true;
                }
            }
            return false;
        }
        private bool Diagonale2()                               //überprüfung von unten links nach oben rechts
        {
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y <= 2; y++)
                {
                    if (InReihe(x, y, x + 1, y + 1, x + 2, y + 2, x + 3, y + 3))
                        return true;
                }
            }

            return false;
        }
        private bool Gewonnen(int X, int Y)                     //sagen grad wo wir gesetzt haben damit man nicht alles durchschauen muss
        {

            for (int i = 0; i < 4; i++)                             // 4 möglichkeiten horizontal zu gewinnen
                if (InReihe(i, Y, i + 1, Y, i + 2, Y, i + 3, Y))
                    return true;                                    //horizontale abfrage 

            for (int i = 0; i < 3; i++)
                if (InReihe(X, i, X, i + 1, X, i + 2, X, i + 3))      // 3 möglichkeiten vertikal zu gewinnen
                    return true;                                    //vertikale abfrage 

            if (Diagonale1()) return true;
            if (Diagonale2()) return true;

            return false;



        }
        private void Reset(string s)
        {
            MessageBox.Show(s);
            red = true;                                 //rot beginnt
            for (int i = 0; i < 7; i++)
                hoehe[i] = 5;
            foreach(Label L in Feld)                    
            {
                L.BackColor = Color.White;
                L.AccessibleDescription = "";
            }
            moves = 42;
        }
            
            
             private void label1_Click(object sender, EventArgs e)
             {
                int X = (sender as Label).Location.X / 100;                                         //hier berechnen wir welchen Stein wir setzten                                                 
                int Y = hoehe[X]--; 
                if (Y >= 0)  
                {
                    Feld[X,Y].BackColor = (red ? Color.Red : Color.Yellow);
                    Feld[X, Y].AccessibleDescription = (red ? "red" : "yellow");                    //if else absfrage
                    moves--;            
                     if (Gewonnen(X, Y)) Reset((red ? "ROT" : "GELB") + " HAT GEWONNEN!!");         // speil nach gewinn zurücksetzen
                    else if (moves <= 0) Reset("Unentschieden");
                    else red = red ? false : true;                                                  //nach jedem Spielzug Spielerwechsel bzw Farbe
                }
             }
           
             
            
             






        private void Form1_Load(object sender, EventArgs e)
        {

        }


        
    }
}




using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DaneZPliku
{
    public class Regula
    {
        public List<Deskryptor> deskryptor = new List<Deskryptor>();
        public string decyzja;
        public int support;

        public Regula(string decyzja, Deskryptor desk)
        {
            this.decyzja = decyzja;
            this.deskryptor.Add(desk);
        }
        public bool czyObiektSpelniaRegule(string[] obiekt)
        {
            foreach (var desk in this.deskryptor)
            {
                if (obiekt[desk.nrAtrybutu] != desk.wartosc)
                    return false;
            }

            return true;
        }
        public bool czyRegulaSprzeczna(string[][] obiekty)
        {
            foreach (var obiekt in obiekty)
            {
                if (czyObiektSpelniaRegule(obiekt) && this.decyzja != obiekt.Last())
                    return false;
            }
            return true;
        }

        public List<int> generujPokjrycie(string[][] obiekty, List<int> maska)
        {
            int tmp = 0;
            foreach (var obiekt in obiekty)
            {

                if (czyObiektSpelniaRegule(obiekt))
                    maska.Remove(tmp);
                tmp++;
            }
            return maska;
        }
        public int SupportReguly(string[][] obiekty)
        {
            support = 0;
            foreach (var ob in obiekty)
            {
                if (czyObiektSpelniaRegule(ob))
                    support++;
            }
            return support;
        }

        public override string ToString()
        {
            string wynik = string.Empty;
            string r = "";
            if (deskryptor.Count() != 1)
            {
                for (int i = 0; i < deskryptor.Count; i++)
                {
                    int nrAtr = deskryptor.ElementAt(i).nrAtrybutu+1;
                    string wartAtr = deskryptor.ElementAt(i).wartosc;

                    r += "(a" + nrAtr + "=" + wartAtr + ")";
                    if (!(i == deskryptor.Count - 1))
                    {
                        r += "/" + @"\";
                    }
                    if (i == deskryptor.Count - 1)
                    {
                        r += "=>(d=" + decyzja + ")";
                    }
                }
                if (support > 1)
                    wynik += r + "[" + support + "]" + "\r\n";
                else
                    wynik += r + "\r\n";
                r = "";
            }
            else
            {
                foreach (var desk in deskryptor)
                {
                    int nrAtr = desk.nrAtrybutu + 1;
                    string wartAtr = desk.wartosc;
                    if (support > 1)
                        wynik += "(a" + nrAtr + "=" + wartAtr + ")" + "=>(d=" + decyzja + ")" + "[" + support + "]" + "\r\n";
                    else
                        wynik += "(a" + nrAtr + "=" + wartAtr + ")" + "=>(d=" + decyzja + ")" + "\r\n";
                }
            }

            return wynik;
        }

    }

}

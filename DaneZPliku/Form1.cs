using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DaneZPliku;
using Kw.Combinatorics;

namespace DaneZPlikuOkienko
{
    public partial class DaneZPliku : Form
    {
        private string[][] systemDecyzyjny;
        public int ostatni; 
        public DaneZPliku()
        {
            InitializeComponent();
        }

        private void btnWybierzPlik_Click(object sender, EventArgs e)
        {
            DialogResult wynikWyboruPliku = ofd.ShowDialog(); // wybieramy plik
            if (wynikWyboruPliku != DialogResult.OK)
                return;

            tbSciezkaDoSystemuDecyzyjnego.Text = ofd.FileName;
            string trescPliku = System.IO.File.ReadAllText(ofd.FileName); // wczytujemy treść pliku do zmiennej
            string[] wiersze = trescPliku.Trim().Split(new char[] { '\n' }); // treść pliku dzielimy wg znaku końca linii, dzięki czemu otrzymamy każdy wiersz w oddzielnej komórce tablicy
            systemDecyzyjny = new string[wiersze.Length][];   // Tworzymy zmienną, która będzie przechowywała wczytane dane. Tablica będzie miała tyle wierszy ile wierszy było z wczytanego poliku

            for (int i = 0; i < wiersze.Length; i++)
            {
                string wiersz = wiersze[i].Trim();     // przypisuję i-ty element tablicy do zmiennej wiersz
                string[] cyfry = wiersz.Split(new char[] { ' ' });   // dzielimy wiersz po znaku spacji, dzięki czemu otrzymamy tablicę cyfry, w której każda oddzielna komórka to czyfra z wiersza
                ostatni = cyfry.Length - 1;
                systemDecyzyjny[i] = new string[cyfry.Length];    // Do tablicy w której będą dane finalne dokładamy wiersz w postaci tablicy integerów tak długą jak długa jest tablica cyfry, czyli tyle ile było cyfr w jednym wierszu
                for (int j = 0; j < cyfry.Length; j++)
                {
                    string cyfra = cyfry[j].Trim(); // przypisuję j-tą cyfrę do zmiennej cyfra
                    systemDecyzyjny[i][j] = cyfra;  
                }
            }

            tbSystemDecyzyjny.Text = TablicaDoString(systemDecyzyjny);
        }

        public string TablicaDoString<T>(T[][] tab)
        {
            string wynik = "";
            for (int i = 0; i < tab.Length; i++)
            {
                for (int j = 0; j < tab[i].Length; j++)
                {
                    wynik += tab[i][j].ToString() + " ";
                }
                wynik = wynik.Trim() + Environment.NewLine;
            }

            return wynik;
        }


        public double StringToDouble(string liczba)
        {
            double wynik; liczba = liczba.Trim();
            if (!double.TryParse(liczba.Replace(',', '.'), out wynik) && !double.TryParse(liczba.Replace('.', ','), out wynik))
                throw new Exception("Nie udało się skonwertować liczby do double");

            return wynik;
        }


        public int StringToInt(string liczba)
        {
            int wynik;
            if (!int.TryParse(liczba.Trim(), out wynik))
                throw new Exception("Nie udało się skonwertować liczby do int");

            return wynik;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            // Tablica z wczytanymi danymi dostępna poniżej
            // this.systemDecyzyjny;

            // 
            // Przykład konwersji string to double 
            string sLiczbaDouble = "1.5";
            double dsLiczbaDouble = StringToDouble(sLiczbaDouble);

            // Przykład konwersji string to int 
            string sLiczbaInt = "1";
            int iLiczbaInt = StringToInt(sLiczbaInt);

            /****************** Miejsce na rozwiązanie *********************************/




            //string wynik = String.Join(",", koceptFinal(koncept(systemDecyzyjny, "1"), systemDecyzyjny));

            
            
            Deskryptor desk = new Deskryptor();
            Dictionary<string, int>slownik = new Dictionary<string, int>();
            List<Regula> reguly= new List<Regula>();
            List<Deskryptor> pDeskryptory = new List<Deskryptor>();
            string[][] test;
            foreach (var decyzje in fUnkialnosc(fKolumna(systemDecyzyjny, ostatni)))
            {
                List<int> maska1 = new List<int>();
                List<int> maska2 = new List<int>();
                foreach (var m in maska(konceptFinall(decyzje)))
                {
                    maska1.Add(m);
                }               
                          
                string[][] tb;
                do
                {
                    pDeskryptory.Clear();
                    if (maska1.Count() != konceptFinall(decyzje).Count())
                    {
                        tb=konceptBezPok(konceptFinall(decyzje),maska1);
                        desk = fMaxCalosc(konceptBezPok(konceptFinall(decyzje), maska1), pDeskryptory);
                    }
                    else
                        desk = fMaxCalosc(konceptFinall(decyzje), pDeskryptory);
                    Regula r = new Regula(decyzje, desk);
                    

                    do
                    {
                        if (!r.czyRegulaSprzeczna(systemDecyzyjny))
                        {
 
                            pDeskryptory.Add(desk);
                            desk = fMaxCalosc(konceptPoPokryciu(konceptFinall(decyzje), maska2), pDeskryptory);
                            r = dodajDeskryptor(r, desk);
                            maska2.Clear();
                            foreach (var m in maska(konceptFinall(decyzje)))
                            {
                                maska2.Add(m);
                            }
                            maska2 = r.generujPokjrycie(konceptFinall(decyzje), maska2);
                            if (r.czyRegulaSprzeczna(systemDecyzyjny))
                            {
                                r.SupportReguly(systemDecyzyjny);
                                reguly.Add(r);                             
                                maska1 = r.generujPokjrycie(konceptFinall(decyzje),  maska1);
                            }
                        }
                        else
                        {
                            r.SupportReguly(systemDecyzyjny);
                            reguly.Add(r);
                            maska1 = r.generujPokjrycie(konceptFinall(decyzje), maska1);
                            
                        }


                    } while (!r.czyRegulaSprzeczna(systemDecyzyjny));
                    
                } while (maska1.Count() != 0);
            }
            foreach (var reg in reguly)
            {
                wynik.Text += reg.ToString();
            }

            /****************** Koniec miejsca na rozwiązanie ********************************/
        }
        
        public string[] fKolumna(string[][] tab, int nrKolumny)
        {
            string[] kolumna= new string[tab.Length];
            for (int i = 0; i < tab.Length; i++)
            {
                kolumna[i] = tab[i][nrKolumny];
            }
            return kolumna;
        }
        public string[] fUnkialnosc(string[] tab)
        {
            List<string> lista = new List<string>();
            lista.Add(tab[0]);
            for (int i = 0; i < tab.Length; i++)
            {
                if (!lista.Contains(tab[i]))
                    lista.Add(tab[i]);
            }
            return lista.ToArray();
        }
        public string[] fKolumnaJ(string[][] tab,int nrKolumny,int nrKolumnyWarunkowj, string wartoscWarunku)
        {
            List<string> lista = new List<string>();
            for (int i = 0; i < tab.Length; i++)
            {
                string[] wiersz = tab[i];
                if (wiersz[nrKolumnyWarunkowj]==wartoscWarunku)
                    lista.Add(wiersz[nrKolumny]);              
            }
            return lista.ToArray();
        }
        
        public string[][] koncept(string[][] wczytaneDane, string dec)
        {

            string[][] tablica = new string[ostatni][];
            int pom = 0;
            for (int i = 0; i < ostatni; i++)
            {

                tablica[pom] = fKolumnaJ(wczytaneDane, i,ostatni, dec);
                pom++;

            }       
            return tablica;

        }
        public string[][] konceptFinall(string decyzja)
        {
            string[][] tab = new string[koncept(systemDecyzyjny, decyzja)[0].Length][];
            int dlugosc = koncept(systemDecyzyjny, decyzja)[0].Length;
            for (int i = 0; i < dlugosc; i++)
            {
                tab[i] = fKolumna(koncept(systemDecyzyjny, decyzja), i);
            }
            return tab;
        }
        
              
        public Dictionary<T, int> fCzestosc<T>(T[] tab)
        {
            Dictionary<T, int> czestosc = new Dictionary<T, int>();
            czestosc.Add(tab[0], 1);
            for (int i = 1; i < tab.Length; i++)
            {
                if (czestosc.ContainsKey(tab[i]))
                    czestosc[tab[i]]++;
                else
                    czestosc.Add(tab[i], 1);
            }
            return czestosc;
        }
        public Deskryptor fmax(Dictionary<string,int> czestosc,int nrAtr)
        {
            Deskryptor desk = new Deskryptor();
            desk.nrAtrybutu = nrAtr;
            desk.wartosc = czestosc.First().Key;
            desk.czestosc =czestosc.First().Value;
            foreach (var cz in czestosc)
            {
                if (desk.czestosc < cz.Value)
                {
                    desk.czestosc = cz.Value;
                    desk.wartosc = cz.Key;
                }
                   
            }
            return desk;
        }
        public Deskryptor fMaxCalosc(string[][] tab,List<Deskryptor> deskP)
        {
            Dictionary<string, int> slownik = new Dictionary<string, int>();
            Deskryptor desk = new Deskryptor();
            desk.czestosc = 0;
            int nrAtr = 0;
            List<int> pom = new List<int>();
            foreach (var t in deskP)
            {
                pom.Add(t.nrAtrybutu);
            }
                for (int i = 0; i < tab[0].Length; i++)
                {
                    nrAtr = i;
                    slownik = fCzestosc(fKolumna(tab, i));
                      if (desk.czestosc < fmax(slownik, nrAtr).czestosc)
                        {
                            if (deskP.Count != 0)
                            {                                                                 
                                    if (!pom.Contains(fmax(slownik, nrAtr).nrAtrybutu ) )
                                    {
                                        desk.czestosc = fmax(slownik, nrAtr).czestosc;
                                        desk.wartosc = fmax(slownik, nrAtr).wartosc;
                                        desk.nrAtrybutu = fmax(slownik, nrAtr).nrAtrybutu;
                                    }                               
                            }
                            else
                            {
                                desk.czestosc = fmax(slownik, nrAtr).czestosc;
                                desk.wartosc = fmax(slownik, nrAtr).wartosc;
                                desk.nrAtrybutu = fmax(slownik, nrAtr).nrAtrybutu;
                            }

                        }                                          
                }               
            

            return desk;
        }
       

        public List<int> maska(string[][] system)
        {
            List<int> lista = new List<int>();
            for (int i = 0; i < system.Length; i++)
            {
                lista.Add(i);
            }
            return lista;
        }
        

        public string[][] konceptPoPokryciu(string[][] koncept,List<int> maska)
        {
            int wilkosc = (koncept.Length - maska.Count());
            string[][] tab = new string[wilkosc][];
            int pom = 0;
            for (int i = 0; i <koncept.Length ; i++)
            {
                if(!maska.Contains(i))
                {
                    tab[pom] = koncept[i];
                    pom++;
                }               
            }
            return tab;
        }

        public string[][] konceptBezPok(string[][] koncept, List<int> maska)
        {
            int wilkosc = maska.Count();
            string[][] tab = new string[wilkosc][];
            int pom = 0;
            for (int i = 0; i < koncept.Length; i++)
            {
                if (maska.Contains(i))
                {
                    tab[pom] = koncept[i];
                    pom++;
                }
            }
            return tab;
        }
        public Regula dodajDeskryptor(Regula regula,Deskryptor desk)
        {
            regula.deskryptor.Add(desk);
            return regula;
        }
        
    }
     
}

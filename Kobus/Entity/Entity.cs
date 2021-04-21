using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using Kobus.Substructure;

namespace Kobus.Entity
{
    public class Entity
    {
        public DoubleDirectionalList VoyageList(int n)
        {
            DoubleDirectionalList Voyage = new DoubleDirectionalList();
            string FileName = Globals.Globals.FileBus + "/Voyage/" + Globals.Globals.VoyageList[n].ToString();
            StreamReader sr = new StreamReader(FileName);
            string FileChar = sr.ReadLine();
            sr.Close();
            string data = "";
            for (int i = 0; i < FileChar.Length; i++)
            {
                if (FileChar[i] == ';')
                {
                    Voyage.Add(data);
                    data = "";
                }
                else
                {
                    data = data + FileChar[i];
                }
            }
            if (n != 0 && Globals.Globals.filecontrol[n] != "History" && Globals.Globals.filecontrol[n] != "Null")
            {
                DateTime now = DateTime.Now;
                IFormatProvider culture = new CultureInfo("tr-TR", true);
                DateTime date = DateTime.ParseExact(Voyage.Data(2).ToString(), "dd.MM.yyyy", culture);
                DateTime time = DateTime.ParseExact(Voyage.Data(3).ToString(), "HH:mm", CultureInfo.InvariantCulture);
                TimeSpan ts = date - now;
                if (ts.Days < 0 )
                {
                    VoyageHistory(n);
                }
                else if( ts.Days == 0 && ts.Hours < 0)
                {
                    ts = time.Subtract(now);
                    if(ts.Minutes < 0)
                    {
                        VoyageHistory(n);
                    }
                }
            }
            return Voyage;
        }
        public void VoyageAdd(DoubleDirectionalList VoyageList, string Capacity)
        {
            string FileName = Globals.Globals.FileBus + "/Voyage/Voyage" + Convert.ToInt32(Globals.Globals.Length) + ".txt";
            FileStream fs = new FileStream(FileName, FileMode.Create, FileAccess.Write);
            fs.Close();
            StreamWriter FileWrite = File.AppendText(FileName);
            Node temp = VoyageList.top;
            while (temp != null)
            {
                FileWrite.Write(temp.Data + ";");
                temp = temp.Next;
            }
            FileWrite.Close();
            FileName = Globals.Globals.FileBus + "/Voyage/VoyageListLength.txt";
            FileWrite = File.AppendText(FileName);
            FileWrite.WriteLine("Voyage" + Convert.ToInt32(Globals.Globals.Length) + ".txt");
            FileWrite.Close();
            FileName = Globals.Globals.FileBus + "/Voyage/VoyageListLengthPosition.txt";
            FileWrite = File.AppendText(FileName);
            FileWrite.WriteLine("Active");
            FileWrite.Close();
            FileName = (Globals.Globals.FileBus + "/Ticket/Ticket" + Globals.Globals.Length + ".txt");
            string[] FileData = new string[Convert.ToInt32(Capacity)];
            for (int i = 0; i < Convert.ToInt32(Capacity); i++)
            {
                FileData[i] = "Null";
            }
            File.WriteAllLines(FileName, FileData);
            FileWrite.Close();
            Log(DateTime.Now + " - " + Globals.Globals.Length + " Numaralı Sefer Eklenmiştir");
        }
        public void VoyageEdit(DoubleDirectionalList VoyageList, int n)
        {
            string FileName = Globals.Globals.FileBus + "/Voyage/" + Globals.Globals.VoyageList[n].ToString();
            string Data = "";
            Node temp = VoyageList.top;
            while (temp != null)
            {
                Data = Data + temp.Data + ";";
                temp = temp.Next;
            }
            string[] FileEdit = { Data };
            File.WriteAllLines(FileName, FileEdit);
            Log(DateTime.Now + " - " + n + " Numaralı Sefer Düzenlenmiştir");
        }
        public void VoyageDelete(int data)
        {
            string FileName = Globals.Globals.FileBus + "/Voyage/VoyageListLengthPosition.txt";
            string[] FileEdit = File.ReadAllLines(FileName);
            FileEdit[data] = "Null";
            File.WriteAllLines(FileName, FileEdit);
            Log(DateTime.Now + " - " + data + " Numaralı Sefer Silinmiştir");
        }
        public void VoyageBackToDeleted(int data)
        {
            string FileName = Globals.Globals.FileBus + "/Voyage/VoyageListLengthPosition.txt";
            string[] FileEdit = File.ReadAllLines(FileName);
            FileEdit[data] = "Active";
            File.WriteAllLines(FileName, FileEdit);
            Log(DateTime.Now + " - " + data + " Numaralı Silinen Sefer Geri Alınmıştır.");
        }
        public void VoyageHistory(int data)
        {
            string FileName = Globals.Globals.FileBus + "/Voyage/VoyageListLengthPosition.txt";
            string[] FileEdit = File.ReadAllLines(FileName);
            FileEdit[data] = "History";
            File.WriteAllLines(FileName, FileEdit);
            Log(DateTime.Now + " - " + data + " Numaralı Sefer Geçmiş Seferlere Eklenmiştir");
        }
        public DoubleDirectionalList TicketList(int n)
        {
            DoubleDirectionalList Ticket = new DoubleDirectionalList();

            string FileChar = Globals.Globals.Capacity[n];
            if (FileChar == "Null")
            {
                Ticket.Add(FileChar);
                Ticket.Add(FileChar);
                Ticket.Add(FileChar);
                return Ticket;
            }
            else
            {
                string data = "";
                FileChar.ToCharArray();
                foreach (var item in FileChar)

                    if (item == ';')
                    {
                        Ticket.Add(data);
                        data = "";
                    }
                    else
                    {
                        data = data + item;
                    }
                return Ticket;
            }
        }
        public void TicketBuy(string koltuk, string musteri, string cinsiyet, string durum)
        {
            string FileName = (Globals.Globals.FileBus + "/Ticket/Ticket" + Globals.Globals.Data + ".txt");
            string[] TicketFile = File.ReadAllLines(FileName);
            TicketFile[Convert.ToInt32(koltuk)] = durum + ";" + musteri + ";" + cinsiyet + ";";
            File.WriteAllLines(FileName, TicketFile);
            Log(DateTime.Now + " - " + Globals.Globals.Data + " Numaralı Seferin " + koltuk + "Numaralı Koltuk" + durum + "alındı");
        }
        public void Log(string Value)
        {
            string FileName = Globals.Globals.FileBus + "log.txt";
            StreamWriter FileWrite = File.AppendText(FileName);
            FileWrite.WriteLine(Value);
            FileWrite.Close();
        }
        public void Days(string Date, DoubleDirectionalList voyage, string ticket)
        {
            string[] Data = new string[9];
            DoubleDirectionalList list = VoyageList(0);
            Data[0] = "----------------------------------------------------------------";
            Data[1] = "Sefer Bilgileri";
            for (int i = 0; i < voyage.Count(); i++)
            {
                Data[2] = Data[2] + list.Data(i).ToString() + "->" + voyage.Data(i).ToString() + ";";
            }
            Data[5] = "Koltuk Bilgileri";
            Data[6] = "Numara->Durum";
            for (int i = 0; i < Convert.ToInt32(ticket); i++)
            {
                Data[7] = Data[7] + i + "->Boş;";
            }
            Data[8] = "----------------------------------------------------------------";
            string FileName = Globals.Globals.FileBus + "/Days/" + Date + ".txt";
            StreamWriter FileWrite = File.AppendText(FileName);
            for (int i = 0; i < Data.Length; i++)
            {
                FileWrite.WriteLine(Data[i]);
            }
            FileWrite.Close();
            Log(DateTime.Now + " - " + list.Data(0).ToString() + " Numaralı Sefer gg.aa.yyyy.txt dosyasına eklenmiştir.");
        }
        public void TicketClose(string[] List)
        {
            string FileName = (Globals.Globals.FileBus + "/Ticket/Ticket" + Globals.Globals.Data + ".txt");
            File.WriteAllLines(FileName, List);
            Log(DateTime.Now + " - " + Globals.Globals.Data + " Numaralı Bilet Satışı iptal edilmiştir.");
        }
        public int VoyageTotalBudget(int data)
        {
            string FileName = (Globals.Globals.FileBus + "/Ticket/Ticket" + data + ".txt");
            string[] TotalBudget = File.ReadAllLines(FileName);
            int data1 = 0;
            foreach (var item in TotalBudget)
            {
                if (item != "Null")
                {
                    data1 = data1 + 1;
                }
            }
            return data1;
        }

    }
}
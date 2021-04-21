using Kobus.Substructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Kobus.Globals
{
    public class Globals
    {
        public static string FileBus;
        public static int Length;
        public static string[] VoyageList;
        public static string[] filecontrol;
        public static int Data;
        public static string[] Capacity;
        public static DoubleDirectionalList TicketList;
        public static void FileControl()
        {
            filecontrol = File.ReadAllLines(Globals.FileBus + "/Voyage/VoyageListLengthPosition.txt");
        }
        public static void VoyageListLength()
        {
            VoyageList = File.ReadAllLines(FileBus + "/Voyage/VoyageListLength.txt");
            Length = VoyageList.Length;
        }
        public static void VoyageCapacity(int data)
        {
            Capacity = File.ReadAllLines(FileBus + "/Ticket/Ticket" + data + ".txt");
        }
        public static string DATEHTML(DateTime date)
        {
            string DateHTML;
            if (date.Month < 10 && date.Day < 10)
            {
                DateHTML = date.Year + "-0" + date.Month + "-0" + date.Day;
            }
            else if (date.Month < 10)
            {
                DateHTML = date.Year + "-0" + date.Month + "-" + date.Day;
            }
            else if (date.Day < 10)
            {
                DateHTML = date.Year + "-" + date.Month + "-0" + date.Day;
            }
            else
            {
                DateHTML = date.Year + "-" + date.Month + "-" + date.Day;
            }
            return DateHTML;
        }
        public static string DATEFILE(DateTime date)
        {
            string DateFILE;
            if (date.Month < 10 && date.Day < 10)
            {
                DateFILE = "0" + date.Day + ".0" + date.Month + "." + date.Year;
            }
            else if (date.Month < 10)
            {
                DateFILE = +date.Day + ".0" + date.Month + "." + date.Year;
            }
            else if (date.Day < 10)
            {
                DateFILE = "0" + date.Day + "." + date.Month + "." + date.Year;
            }
            else
            {
                DateFILE = +date.Day + "." + date.Month + "." + date.Year;
            }
            return DateFILE;
        }
    }
}
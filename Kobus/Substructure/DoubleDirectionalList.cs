using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kobus.Substructure
{
    // Çift Yönlü Liste Yapısı Kaynak: https://sanalkurs.net/c-ta-bagli-liste-ile-koleksiyon-olusturmak-10660.html
    public class Node
    {
        public object Data;
        public Node Previous;
        public Node Next;
        public Node(object Data)
        {
            this.Data = Data;
        }
    }
    public class DoubleDirectionalList
    {
        public Node top;
        public Node end;
        //Liste Uzunluğu 
        public int Count()
        {
            int n = 0;
            Node temp = top;
            while (temp != null)
            {
                n++;
                temp = temp.Next;
            }
            return n;
        }
        //Listeye Eleman Ekleme(sona ekler)
        public void Add(object data)
        {
            Node node = new Node(data);
            if (top == null)
            {
                top = node;
                end = node;
                end.Next = null;
            }
            else
            {
                node.Previous = end;
                end.Next = node;
                end = node;
                end.Next = null;
            }
        }
        //Listeyi Temizler
        public void Clear()
        {
            top = null;
            end = null;
        }
        //Listenin n.elemanını çağırır
        public object Data(int data)
        {
            Node temp = top;
            for (int i = 0; i < data; i++)
            {
                temp = temp.Next;
            }
            return temp.Data;
        }
        //Listenin istenilen elemanını siler(sıralaması ile)
        public void Remove(object data)
        {
            Node temp = top;
            if (temp == null)
            {
               //LİSTE BOŞ
            }
            else
            {
                while (temp != null)
                {
                    if (temp.Data == data)
                    { break; }
                    temp = temp.Next;
                }
                if (temp == null)
                {
                    //SİLİNECEK ELEMAN LİSTEDE YOK
                }
                else
                {
                    if (temp == top)
                    {
                        if (top == end)
                        {
                            top = null;
                            end = null;
                        }
                        else
                        {
                            top = end.Next;
                            top.Previous = null;
                        }
                    }
                    else if (temp == end)
                    {
                        end = end.Previous;
                        end.Next = null;
                    }
                    else
                    {
                        temp.Previous.Next = temp.Next;
                        temp.Next.Previous = temp.Previous;
                    }
                }
            }
        }
        //Listenin istenilen elemanının siler(değer ile)
        public void RemoveAt(int order)
        {
            Node temp = top;
            int count = Count();
            if (count <= order)
            {
                //HATA
            }
            else
            {
                for (int i = 0; i < order; i++)
                {
                    temp = temp.Next;
                }
                if (temp == top)
                {
                    if (top == end)
                    {
                        top = null;
                        end = null;
                    }
                    else
                    {
                        top = top.Next;
                        top.Previous = null;
                    }
                }
                else if (temp == end)
                {
                    end = end.Previous;
                    top.Next = null;
                }
                else
                {
                    temp.Previous.Next = temp.Next;
                    temp.Next.Previous = temp.Previous;

                }
            }
        }
    }
}
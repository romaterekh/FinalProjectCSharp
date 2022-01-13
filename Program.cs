using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace FinalTest
{
    [Serializable]
    public abstract class Telephone
    {
        public string name;
        public string firm;
        public string price;
        [XmlAttribute]
        public string Name
        {
            get { return name; }
        }
        [XmlAttribute]
        public string Firm
        {
            get { return firm; }
        }
        [XmlAttribute]
        public string Price
        {
            get { return price; }
        }
        public Telephone(string name, string firm, string price)
        {
            this.name = name;
            this.firm = firm;
            this.price = price;
        }
        public abstract void Print();
        public Telephone()
        {

        }
    }
    public class MobTelephone : Telephone
    {
        private string color;
        private string memory;
        [XmlAttribute]
        public string Color
        {
            get { return color; }
        }
        [XmlAttribute]
        public string Memory
        {
            get { return memory; }
        }
        public MobTelephone(string name, string firm, string price, string color, string memory) : base(name,firm,price)
        {
            this.color = color;
            this.memory = memory;
        }
        public override void Print()
        {
            Console.WriteLine($"Name: {name} Firm: {firm} Price: {price} Color: {color} Memory: {memory}");
        }
    }
    public class RadioTelephone : Telephone
    {
        private string radius;
        private string avtovidpovidach;
        [XmlAttribute]
        public string Radius
        {
            get { return radius; }
        }
        [XmlAttribute]
        public string Avtovidpovidach
        {
            get { return avtovidpovidach; }
        }
        public RadioTelephone(string name, string firm, string price, string radius, string avtovidpovidach) : base(name, firm, price)
        {
            this.radius = radius;
            this.avtovidpovidach = avtovidpovidach;
        }
        public override void Print()
        {
            Console.WriteLine($"Name: {name} Firm: {firm} Price: {price} Radius: {radius} Avtovidpovidach: {avtovidpovidach}");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (StreamReader sr1 = new StreamReader(@"D:\phone1.txt"))
                using (StreamReader sr2 = new StreamReader(@"D:\phone2.txt"))
                using (StreamWriter sw = new StreamWriter(@"D:\phone.txt"))
                {
                    string phone1 = sr1.ReadToEnd();
                    string phone2 = sr2.ReadToEnd();

                    string[] words1 = phone1.Split(new char[] { ' ' });
                    string[] words2 = phone2.Split(new char[] { ' ' });

                    List<Telephone> telephones = new List<Telephone>();
                    telephones.Add(new MobTelephone(words1[1], words1[3], words1[5], words1[7], words1[9]));
                    telephones.Add(new RadioTelephone(words2[1], words2[3], words2[5], words2[7], words2[9]));

                    //xml
                    //Telephone[] phone = new Telephone(words2[1], words2[3], words2[5]);
                    //XmlSerializer xmlser = new XmlSerializer(typeof(Telephone[]));
                    //Stream serialStream = new FileStream("Phone.xml", FileMode.Create);
                    //xmlser.Serialize(serialStream, phone);
                    //serialStream.Close();
                    //serialStream = new FileStream("Phone.xml", FileMode.Open);
                    //Telephone[] phonexml = xmlser.Deserialize(serialStream) as Telephone[];
                    //serialStream.Close();
                    //Console.WriteLine();
                    //foreach (Telephone p in phonexml)
                    //{
                    //    Console.WriteLine("XML:", p.Name, p.Firm, p.Price);
                    //}

                    Console.WriteLine("Phones:");
                    foreach (var telephone in telephones)
                    {
                        telephone.Print();
                    }

                    int p1 = Convert.ToInt32(words1[5]);
                    int p2 = Convert.ToInt32(words2[5]);

                    int[] prices = { p1, p2 };
                    Array.Sort(prices);
                    sw.WriteLine($"Sort by price:");
                    foreach (var i in prices)
                    {
                        sw.WriteLine(i);
                    }

                    sw.WriteLine($"Total price: \n{p1 + p2}");

                    if (words1[9] == "true" || words2[9] == "true")
                    {
                        if (words1[9] == "true")
                        {
                            sw.WriteLine($"Radiotelephones with avtovidpovidach:");
                            foreach (string w1 in words1)
                                sw.Write($"{w1} ");
                        }
                        if (words2[9] == "true")
                        {
                            sw.WriteLine($"Radiotelephones with avtovidpovidach:");
                            foreach (string w2 in words2)
                                sw.Write($"{w2} ");
                        }
                    }
                    else
                        sw.WriteLine($"Radiotelephones with avtovidpovidach:\nNone");

                    Console.WriteLine("The file has been filled!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

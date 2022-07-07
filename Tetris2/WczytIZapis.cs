using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Tetris2
{
    static class WczytIZapis
    {
        static XmlDocument db = new XmlDocument();
        static string xmlDB_path = "../../Zapis.xml";

        public static int Save(int[,] mapa)
        {
            db.Load(xmlDB_path);
            XmlNodeList list = db.SelectNodes("/zapisy/zapis[@id=0]");
            XmlNode node = list[0];

            for (int i = 1; i < 27; i++)
            {
                string linia = "";
                for (int j = 0; j < 15; j++)
                {
                    linia = linia + mapa[j, i - 1].ToString() + " ";
                }
                node["linia_" + i].InnerText = linia;
            }
            db.Save(xmlDB_path);
            return 0;
        }
        public static int[,] Load()
        {
            db.Load(xmlDB_path);
            XmlNodeList list = db.SelectNodes("/zapisy/zapis[@id=0]");
            XmlNode node = list[0];
            int[,] mapa = new int[15, 27];
            for (int i = 1; i < 27; i++)
            {
                string linia = node["linia_" + i.ToString()].InnerText;
                for (int j = 0; j < 15; j++)
                {
                    mapa[j, i] = int.Parse(linia.Substring(j * 2, 1));
                }
            }
            return mapa;
        }
    }
}

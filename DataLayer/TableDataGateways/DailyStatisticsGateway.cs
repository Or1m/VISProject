using DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace DataLayer.TableDataGateways
{
    public class DailyStatisticsGateway
    {
        private static readonly object lockObj = new object();
        private static DailyStatisticsGateway instance;

        public static DailyStatisticsGateway Instance {
            get {
                lock (lockObj)
                {
                    return instance ?? (instance = new DailyStatisticsGateway());
                }
            }
        }

        private DailyStatisticsGateway() { }


        public bool Save(DailyStatisticsDTO dto, out string msgErr)
        {
            msgErr = string.Empty;

            string postfix = dto.Date.Date.Day.ToString() + "." + dto.Date.Date.Month.ToString() + "." + dto.Date.Date.Year.ToString() + ".xml";

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(DailyStatisticsDTO));
            using (FileStream stream = new FileStream(Properties.Settings.Default.XMLPath + postfix, FileMode.Create))
            {
                try
                {
                    xmlSerializer.Serialize(stream, dto);
                }
                catch (Exception e)
                {
                    msgErr = e.Message;
                    return false;
                }
            }

            return true;
        }

        public bool Load(out DailyStatisticsDTO dto, out string msgErr)
        {
            msgErr = string.Empty;
            dto = null;

            if (!File.Exists(Properties.Settings.Default.XMLPath))
                return true;

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(DailyStatisticsDTO));
            using (FileStream stream = new FileStream(Properties.Settings.Default.XMLPath, FileMode.Open))
            {
                try
                {
                    dto = (DailyStatisticsDTO)xmlSerializer.Deserialize(stream);
                }
                catch (Exception e)
                {
                    msgErr = e.Message;
                    return false;
                }
            }
            return true;
        }
    }
}
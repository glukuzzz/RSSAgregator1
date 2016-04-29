using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Syndication;
using System.Xml;
using System.IO;


namespace ConsoleApplication2
{
    class DataSource
    {
        public string name { get; set; }
        public string inputUri { get; set; }
        public SyndicationFeed feed { get; set; }
        public DateTimeOffset date { get; set; }

        public DataSource(string name, string inputUri)
        {
            this.name = name;
            this.inputUri = inputUri;
            feed = new SyndicationFeed();
            date = new DateTimeOffset();
            
        }
    }

    
    class Syndi
    {
        List<DataSource> datasource { get; set; } = new List<DataSource>();


        public void AddResoure(string Name, string InputUri)
        {
            datasource.Add(new DataSource(Name, InputUri));
        }

        DataSource ModifyFeed(DataSource datasource)
        {
            try
            {
                
                Rss20FeedFormatter formatter = new Rss20FeedFormatter();
                formatter.ReadFrom(XmlReader.Create(datasource.inputUri));
                SyndicationFeed feed = formatter.Feed;

                DateTimeOffset itemdate = datasource.date;
                DateTimeOffset maxitemdate = datasource.date;
                datasource.feed.Title = feed.Title;
                datasource.feed.Description = feed.Description;

                List<SyndicationItem> items = new List<SyndicationItem>();
                items.AddRange(datasource.feed.Items);
                foreach (SyndicationItem item in feed.Items)
                {
                    if (item.PublishDate > itemdate) items.Add(item);
                    if (item.PublishDate > maxitemdate) maxitemdate = item.PublishDate;

                    
                }

                //SindicationIntem - это и есть новость
                datasource.feed.Items = items;
                datasource.date = maxitemdate;
            }
            catch { }
            
            return datasource;
        }

        DataSource MakeRssXml(DataSource datasource)
        {
            var formatter = new Rss20FeedFormatter(datasource.feed, true);
            XmlWriter writer = XmlWriter.Create("data5.xml");
            //if (Directory.Exists(datasource.name) != false) Directory.CreateDirectory(datasource.name);
            formatter.WriteTo(writer);
            datasource.feed = new SyndicationFeed();
            datasource.date = new DateTimeOffset();
            return datasource;
        }

       public void BuildData()
        {
            int datacount = datasource.Count();
            for (int i = 0; i < datacount; ++i )
            {
                datasource[i] = ModifyFeed(datasource[i]);
            }

            for (int i = 0; i < datacount; ++i)
            {
                datasource[i] = MakeRssXml(datasource[i]);
            }
        }
    }
}

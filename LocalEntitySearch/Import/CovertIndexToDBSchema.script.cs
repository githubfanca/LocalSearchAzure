

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Web;
using System.Xml.XPath;
using System.IO;
using System.Text.RegularExpressions;

public class Utils
{

    public static string separator = "|";
    public static string[] separators = separator.Split(' ');

    public static string GetEntityId(String RawEntity)
    {
        return String.Format("{0}_{1}", StringQuery(RawEntity, @"/Entity/FeedEntityProcessing/FeedID"),
                             StringQuery(RawEntity, @"/Entity/FeedEntityProcessing/ExternalID"));
    }

    public static bool IsHighConfidentAndFromBingGeocoder(String RawEntity)
    {
        Regex regex =
            new Regex(
                "<LatLong>.+?<LatLongConfidenceLevel>High</LatLongConfidenceLevel>.+?<Source>BingGeocoder</Source>.+?</LatLong>",
                RegexOptions.IgnoreCase);
        return regex.IsMatch(RawEntity);
    }

    public static string ParseFeedId(String id)
    {
        return id.Substring(0, id.IndexOf("_") > 0 ? id.IndexOf("_") : 0);
    }

    public static string ExtractExternalId(String xml)
    {
        String[] feedIds =
            StringQuery(xml, "/EntityIndex/Content/Entity/FeedEntityProcessing/MatchedFeedEntity/FeedEntity/@FeedID").
                Split(separators, StringSplitOptions.RemoveEmptyEntries);
        String[] externalIDs =
            StringQuery(xml, "/EntityIndex/Content/Entity/FeedEntityProcessing/MatchedFeedEntity/FeedEntity/@ExternalID").
                Split(separators, StringSplitOptions.RemoveEmptyEntries);

        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < feedIds.Length; i++)
        {
            sb.Append(feedIds[i] + "," + (externalIDs.Length > i ? externalIDs[i] : "") + "|");
        }
        return sb.ToString();
    }

    public static string ExtractYpId(String xml)
    {
        return
            "YN" + StringQuery(xml, "/EntityIndex/@masterId");
    }

    public static string ExtractId(String xml)
    {
        return
            StringQuery(xml, "/EntityIndex/Content/Entity/Identifiers/Identifier");
    }

    public static string ExtractMeta(String xml, string name)
    {
        return HttpUtility.HtmlDecode(
            StringQuery(xml, string.Format("/EntityIndex/Document/Index/PhraseStream[@name=\'{0}\']/Phrase", name)));
    }


    public static string ExtractExternalIdFromentityId(string id)
    {
        return id.Substring(id.LastIndexOf("-") + 1);
    }
    public static string StringQuery(string XML, string query, string args)
    {
        bool debug = false;
        string separator = "|";
        string result = "";

        int k = XML.IndexOf("xmlns");
        if (k >= 0)
        {
            int t = XML.IndexOf("\"", k + 1);
            t = XML.IndexOf("\"", t + 1);
            XML = XML.Substring(0, k - 1) + XML.Substring(t + 1);
        }

        //Console.WriteLine(String.Format("[{0}]", XML));

        foreach (string arg in args.Split(new char[] { ' ' }))
        {
            if (arg == "-debug")
            {
                debug = true;
            }
        }
        try
        {
            XPathNavigator nav;
            XPathDocument docNav;
            XPathNodeIterator nodeIter;
            StringReader sr = new StringReader(XML);
            XmlReader xmlr = XmlReader.Create(sr);
            docNav = new XPathDocument(xmlr);
            nav = docNav.CreateNavigator();
            nodeIter = nav.Select(query);
            bool first = true;
            foreach (XPathNavigator navi in nodeIter)
            {
                if (!first)
                {
                    result += separator;
                }
                //Console.WriteLine(String.Format("({0})", navi.Value));
                result += navi.Value;
                first = false;
            }
        }
        catch (Exception e)
        {
            if (debug)
            {
                result = e.Message;
            }
        }
        return result;
    }

    public static string StringQuery(string XML, string query)
    {
        return StringQuery(XML, query, "");
    }
}


using System.Collections.Generic;
using System.Xml.Serialization;

namespace Fic.XTB.PowerBiEmbedder.Model
{
    [XmlRoot("form")]
    public class FormModel
    {
        [XmlArray("tabs")]
        [XmlArrayItem("tab")]
        public List<FormTab> Tabs { get; set; }

        [XmlAttribute("showImage")]
        public bool ShowImage { get; set; }
    }

    public class FormTab
    {
        [XmlArray("labels")]
        [XmlArrayItem("label")]
        public List<FormTabLabel> Labels { get; set; }

        [XmlArray("columns")]
        [XmlArrayItem("column")]
        public List<FormTabColumn> Columns { get; set; }

        [XmlAttribute("id")]
        public string Id { get; set; }
    }

    public class FormTabLabel
    {
        [XmlAttribute("description")]
        public string Description { get; set; }

        [XmlAttribute("languagecode")]
        public ushort LanguageCode { get; set; }
    }

    public class FormTabColumn
    {
        [XmlArray("sections")]
        [XmlArrayItem("section")]
        public List<FormTabColumnSection> Sections { get; set; }
    }

    public class FormTabColumnSection
    {
        [XmlArray("labels")]
        [XmlArrayItem("label")]
        public List<FormTabColumnSectionLabel> Labels { get; set; }
        [XmlArray("rows")]
        [XmlArrayItem("row")]
        public List<Row> Rows { get; set; }

        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlAttribute("showlabel")]
        public bool ShowLabel { get; set; }
    }

    public class FormTabColumnSectionLabel
    {
        [XmlAttribute("description")]
        public string Description { get; set; }
        [XmlAttribute("languagecode")]
        public ushort LanguageCode { get; set; }
    }

    public class Row {
        [XmlElement("cell")]
        public List<Cell> Cells { get; set; }
    }

    public class Cell
    {
        [XmlAttribute("id")]
        public string Id { get; set; }
        [XmlAttribute("rowspan")]
        public string RowSpan { get; set; }
        [XmlElement("control")]
        public Control Control { get; set; }
    }

    public class Control {
        [XmlAttribute("id")]
        public string Id { get; set; }
        [XmlAttribute("classid")]
        public string ClassId { get; set; }
        [XmlElement("parameters")]
        public Parameters Parameters { get; set; }
    }

    public class Parameters {
        [XmlElement("PowerBIGroupId")]
        public string PowerBIGroupId { get; set; }
        [XmlElement("PowerBIReportId")]
        public string PowerBIReportId { get; set; }
        [XmlElement("TileUrl")]
        public string TileUrl { get; set; }
        [XmlElement("PowerBIFilter")]
        public string PowerBIFilter { get; set; }
    }
}


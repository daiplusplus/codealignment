using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace CMcG.CodeAlignment.Business
{
    using Settings = Properties.Settings;

    public class CodeAlignmentOptions : INotifyPropertyChanged
    {
        private Settings m_settings = Settings.Default;

        public CodeAlignmentOptions()
        {
            this.Reload();
        }

        void Reload()
        {
            this.Shortcuts               = KeyShortcut.Get(this.m_settings.Shortcuts).ToList();
            this.XmlTypes                = this.m_settings.XmlTypes.Cast<String>().ToArray();
            this.ScopeSelectorLineValues = this.m_settings.ScopeSelectorLineValues;
            this.ScopeSelectorLineEnds   = this.m_settings.ScopeSelectorLineEnds;
            this.UseIdeTabSettings       = this.m_settings.UseIdeTabSettings;
        }

        public List<KeyShortcut> Shortcuts { get; set; }
        public String[]          XmlTypes  { get; private set; }

        public String ScopeSelectorRegex
        {
            get
            {
                String values = this.ToOrRegex(this.ScopeSelectorLineValues, @"^\s*({0}|)\s*$");
                String ends   = this.ToOrRegex(this.ScopeSelectorLineEnds,   @"({0})\s*$");

                return ends == null ? values : String.Format("({0}|{1})", values, ends);
            }
        }

        private String ToOrRegex(String input, String format)
        {
            List<String> items = input
                .Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                .Select(x => Regex.Escape(x))
				.ToList();

			if (items.Count == 0) return null;

			String regexOr = String.Join(separator: "|", items);

            return String.Format(format, regexOr);
        }

        public String ScopeSelectorLineValues { get; set; }
        public String ScopeSelectorLineEnds   { get; set; }
        public Boolean UseIdeTabSettings       { get; set; }

        public String XmlTypesString
        {
            get { return String.Join(separator: "\r\n", this.XmlTypes); }
            set
            {
                this.XmlTypes = value
					.Split('\n')
                    .Select(x => x.Trim().ToLower())
					.ToArray();
            }
        }

        public KeyShortcut GetShortcut(Key key, String language = null)
        {
            return this.Shortcuts
                .Where(x =>
                    x.Value == key
                    &&
                    (x.Language == null || x.Language == language)
                )
                .OrderBy(x => x.Language == language)
                .FirstOrDefault();
        }

        public void ResetShortcuts()
        {
            String xml     = (String)Settings.Default.Properties["Shortcuts"].DefaultValue;
            this.Shortcuts = KeyShortcut.Get(xml).ToList();

            this.FirePropertyChanged("Shortcuts");
        }

        private static readonly XmlSerializer _stringArraySerializer = new XmlSerializer(typeof(String[]));

        public void ResetSelectorTypes()
        {
            String xml                   = (String)Settings.Default.Properties["XmlTypes"].DefaultValue;
            this.XmlTypes                = (String[])_stringArraySerializer.Deserialize(new StringReader(xml));
            this.ScopeSelectorLineValues = (String)Settings.Default.Properties["ScopeSelectorLineValues"].DefaultValue;
            this.ScopeSelectorLineEnds   = (String)Settings.Default.Properties["ScopeSelectorLineEnds"]  .DefaultValue;

            this.FirePropertyChanged("XmlTypes", "ScopeSelectorLineValues", "ScopeSelectorLineEnds");
        }

        public void Save()
        {
            this.m_settings.Shortcuts = KeyShortcut.Serialize(this.Shortcuts.ToArray());
            this.m_settings.XmlTypes.Clear();
            this.m_settings.XmlTypes.AddRange(this.XmlTypes);

            this.m_settings.ScopeSelectorLineValues = this.ScopeSelectorLineValues;
            this.m_settings.ScopeSelectorLineEnds   = this.ScopeSelectorLineEnds;
            this.m_settings.UseIdeTabSettings       = this.UseIdeTabSettings;
            this.m_settings.Save();
        }

        public void SaveAs(String filename)
        {
            XElement node = new XElement("Settings");

            foreach (SettingsPropertyValue prop in this.m_settings.PropertyValues)
            {
                if (!prop.UsingDefaultValue)
                {
                    node.Add(new XElement(prop.Name) { Value = (String)prop.SerializedValue });
                }
            }

            node.Save(filename);
        }

        public void LoadFrom(String filename)
        {
            XElement node = XElement.Load(filename);
            foreach (XElement setting in node.Elements())
            {
                String key = setting.Name.ToString();
                SettingsPropertyValue prop = this.m_settings.PropertyValues[key];
                prop.SerializedValue = setting.Value;
                prop.Deserialized    = false;
            }

            this.m_settings.Save();
            this.Reload();
        }

        private void FirePropertyChanged(params String[] propertyNames)
        {
            PropertyChangedEventHandler del = this.PropertyChanged;
            if (del is null) return;

            foreach (String propertyName in propertyNames)
            {
                del.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
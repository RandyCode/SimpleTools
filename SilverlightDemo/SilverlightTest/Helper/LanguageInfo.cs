using Matrix.Silverlight.Forms.Extends;
using System;
using System.Collections.Generic;

namespace SilverlightTest.Helper
{
    public class LanguageInfo
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }

        private static List<LanguageInfo> _languages;

        public static List<LanguageInfo> Languages
        {
            get
            {
                if (_languages == null)
                {
                    _languages = new List<LanguageInfo> 
                    { 
                     new LanguageInfo{ DisplayName="繁體中文",Name="zh-Hant"},                   
                     new LanguageInfo{ DisplayName="English",Name="en-US"},
                     new LanguageInfo{ DisplayName="简体中文",Name="zh-Hans"}
                    };
                }
                return _languages;
            }
        }

        private static LanguageInfo _currentLanguage;

        public static LanguageInfo CurrentLanguage
        {
            get
            {
                if (_currentLanguage == null)
                {
                    _currentLanguage = Languages[1];
                }
                return LanguageInfo._currentLanguage;
            }
            set
            {
                LanguageInfo._currentLanguage = value;
            }
        }

    }
}

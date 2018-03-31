using CommonUtils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WindowsFormsApp.Models
{
    public class SearchResult
    {
        private static int x = 1;
        private string _moduleName;
        private Match _match;
        private FileInfo _fileInfo;
        private int _keyIndex;

        public int Id { get; private set; }
        public int Line { get; set; }
        public FileInfo FileInfo
        {
            get => _fileInfo;
            set
            {
                _fileInfo = value;
                _moduleName = _fileInfo.Name.Split('.')[0].ToLowerFirstCharacter();
            }
        }
        public string Text
        {
            get => _match.Value;
        }
        public string FileName
        {
            get => FileInfo.FullName;
        }
        public string Resource
        {
            get
            {
                return Text.GenerateResource(_moduleName);
            }
        }
        public string Key
        {
            get
            {
                return Text.GenerateKey(_moduleName);
            }
        }
        public string ReplacementString
        {
            get
            {
                string result;
                string format = @"{0}CommonUtils.getApplicationResource(""{1}""){2}";
                result = string.Format(format,
                            Regex.Match(Text, @"^"" ").Success ? @""" "" + " : string.Empty,
                            Key,
                            Regex.Match(Text, @" ""$").Success ? @" + "" """ : string.Empty

                    );

                return result;
            }
        }

        public SearchResult(int line, Match match, FileInfo fileInfo, int keyIndex = 0)
        {
            Id = x++;
            Line = line;
            _match = match;
            FileInfo = fileInfo;
            _keyIndex = keyIndex;
        }

        public string GetCmdOpenVisualCode()
        {
            string format = "-g {0}:{1}:{2}";

            return string.Format(format, FileInfo.FullName, Line, _match.Index);
        }
    }
}

using CommandCraft_App.Business_Logic.DataTypes;
using CommandCraft_App.Business_Logic.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CommandCraft_App.Business_Logic.Activities
{
    class ExtractBuildingName
    {
        //inputs
        MyString htmlMain;

        //outputs
        MyString buildingName;

        public void SetInput(string _htmlMain)
        {
            htmlMain = new MyString(_htmlMain);
        }

        public void SetOutput(MyString _buildingName)
        {
            buildingName = _buildingName;
        }


        public void Process()
        {
            //Finding full match for building name
            var match = RegexConfig.FindBuildingName.Match(htmlMain.Value).Value;
            //Cutting out strings surrounding building name
            var result = match.Replace(@"<h1 id=""content-title"" itemprop=""name"">", "").Replace(@"</h1>", "");
           
            buildingName.Value = result;
        }

    }
}

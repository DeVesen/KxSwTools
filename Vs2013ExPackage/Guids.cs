// Guids.cs
// MUST match guids.h
using System;

namespace KARDEXSoftwareGmbH.Vs2013ExPackage
{
    static class GuidList
    {
        public const string guidVs2013ExPackagePkgString = "C568AFAB-BD85-4D61-B6C5-038DBC76D32E";
        public static readonly Guid guidVs2013ExPackagePkgSet = new Guid(guidVs2013ExPackagePkgString);


        public const string guidGroup_MenuElementsString = "A360580D-C903-4C30-807E-87FE6DEFF96E";
        public static readonly Guid guidGroup_MenuElements = new Guid(guidGroup_MenuElementsString);


        public const string guidGroup_SourceCommentsString = "E6FDD5E9-B7A1-4461-ACE0-3B94984403E2";
        public static readonly Guid guidGroup_SourceComments = new Guid(guidGroup_SourceCommentsString);


        public const string guidGroup_GPStartUPString = "5897C192-ACBE-4045-A35A-D34F53139D9A";
        public static readonly Guid guidGroup_GPStartUP = new Guid(guidGroup_GPStartUPString);
        public const string guidGroup_GPStartProjInstString = "FA2EEAB7-3335-4613-A611-A0F45DDAD020";
        public static readonly Guid guidGroup_GPStartProjInst = new Guid(guidGroup_GPStartProjInstString);


        public const string guidGroup_GPSpecialDocuString = "B4A6ED41-58E6-4F2E-AABF-88067C5BBD6E";
        public static readonly Guid guidGroup_GPSpecialDocu = new Guid(guidGroup_GPSpecialDocuString);


        public const string guidGroup_GPSpecialActionsString = "BD6DB39D-A100-4651-A8B8-2AF5AD4A1F8C";
        public static readonly Guid guidGroup_GPSpecialActions = new Guid(guidGroup_GPSpecialActionsString);


        public const string guidGroup_GPDatabaseActions01String = "ED49BACF-DA8A-4C72-A399-B372FA976068";
        public static readonly Guid guidGroup_GPDatabaseActions01 = new Guid(guidGroup_GPDatabaseActions01String);
        public const string guidGroup_GPDatabaseActions02String = "6103AC14-93AD-479B-9C9D-597BB7D45C3D";
        public static readonly Guid guidGroup_GPDatabaseActions02 = new Guid(guidGroup_GPDatabaseActions02String);
        public const string guidGroup_GPDatabaseActions03String = "90F47311-B470-42D9-BA1F-0A5A7285B932";
        public static readonly Guid guidGroup_GPDatabaseActions03 = new Guid(guidGroup_GPDatabaseActions03String);


        public const string guidGroup_GPDeployActions01String = "662CB501-3DDA-4F65-BE38-55C148A2165C";
        public static readonly Guid guidGroup_GPDeployActions01 = new Guid(guidGroup_GPDeployActions01String);



        public static bool GuidIsKnown(Guid? guid)
        {
            bool _result = false;

            _result = _result || guid == GuidList.guidVs2013ExPackagePkgSet;

            _result = _result || guid == GuidList.guidGroup_SourceComments;

            _result = _result || guid == GuidList.guidGroup_GPStartUP;
            _result = _result || guid == GuidList.guidGroup_GPStartProjInst; 

            _result = _result || guid == GuidList.guidGroup_GPSpecialDocu;

            _result = _result || guid == GuidList.guidGroup_GPSpecialActions;

            _result = _result || guid == GuidList.guidGroup_GPDatabaseActions01;
            _result = _result || guid == GuidList.guidGroup_GPDatabaseActions02;
            _result = _result || guid == GuidList.guidGroup_GPDatabaseActions03;

            _result = _result || guid == GuidList.guidGroup_GPDeployActions01;

            return _result;
        }
        public static string Guid2String(Guid? guid)
        {
            if (guid == GuidList.guidVs2013ExPackagePkgSet)
            {
                return "Extention Package";
            }

            else if (guid == GuidList.guidGroup_SourceComments)
            {
                return "Source Comments";
            }

            else if (guid == GuidList.guidGroup_GPStartUP)
            {
                return "GP Start UP";
            }
            else if (guid == GuidList.guidGroup_GPStartProjInst)
            {
                return "GP Start Instance";
            }

            else if (guid == GuidList.guidGroup_GPSpecialDocu)
            {
                return "GP Special Docu";
            }

            else if (guid == GuidList.guidGroup_GPSpecialActions)
            {
                return "GP Special Actions";
            }

            else if (guid == GuidList.guidGroup_GPDatabaseActions01)
            {
                return "GP DB Action 01";
            }
            else if (guid == GuidList.guidGroup_GPDatabaseActions02)
            {
                return "GP DB Action 02";
            }
            else if (guid == GuidList.guidGroup_GPDatabaseActions03)
            {
                return "GP DB Action 03";
            }

            else if (guid == GuidList.guidGroup_GPDeployActions01)
            {
                return "GP Deploy Actions 01";
            }

            return "<N/A>";
        }
    };
}
using KARDEXSoftwareGmbH.Vs2013ExPackage.FrameworkExtention;
using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;

namespace KARDEXSoftwareGmbH.Vs2013ExPackage.MenuComandHelpers
{
    public abstract class EnvGpDbBaseDev
    {
        public class TableRefInfo
        {
            public string TableName;
            public string[] ReferentUpper;
            public int SteppsToTop { get; set; }

            public override string ToString()
            {
                return string.Format("TN: '{0}' | SU: '{1}'", this.TableName, this.SteppsToTop);
            }
        };
        

        private readonly EnvGpSysFile m_gpSysFile;


        public abstract string ProviderInvariantName { get; }

        public string ConnectionString { get { return this.m_gpSysFile.DbConString; } }


        protected EnvGpDbBaseDev(EnvDTE80.DTE2 envDte = null)
        {
            this.m_gpSysFile = EnvGpSysFile.CreateInst(envDte);
        }



        public static bool IsValid(EnvDTE80.DTE2 envDte = null)
        {
            envDte = envDte ?? Package.GetGlobalService(typeof(EnvDTE.DTE)) as EnvDTE80.DTE2;

            var _result = EnvGpDev.IsValid(envDte);
            _result = _result && EnvGpSysFile.IsValid(envDte);

            return _result;
        }



        public virtual bool IsConnectAble()
        {
            if (string.IsNullOrEmpty(this.ProviderInvariantName))
                return false;
            if (string.IsNullOrEmpty(this.ConnectionString))
                return false;

            return true;
        }



        public virtual void Execute(params string[] sqlCommands)
        {
            if (!this.IsConnectAble())
                return;

            var _dbProvFac = DbProviderFactories.GetFactory(this.ProviderInvariantName);

            using (var _dbCon = _dbProvFac.CreateConnection())
            {
                _dbCon.ConnectionString = this.ConnectionString;

                _dbCon.Open();

                using (var _command = _dbProvFac.CreateCommand())
                {
                    foreach (var _sqlCommand in sqlCommands)
                    {
                        _command.CommandText = _sqlCommand;
                        _command.Connection = _dbCon;
                        _command.ExecuteNonQuery();
                    }
                }

                _dbCon.Close();
            }
        }

        public virtual DataTable Select(string sqlCommands)
        {
            DataTable _resultTable;

            if (!this.IsConnectAble())
                return null;

            var _dbProvFac = DbProviderFactories.GetFactory(this.ProviderInvariantName);
            DbProviderFactories.GetFactoryClasses();

            using (var _connection = _dbProvFac.CreateConnection())
            {
                _connection.ConnectionString = this.ConnectionString;

                var _command = _dbProvFac.CreateCommand();
                _command.CommandType = CommandType.Text;
                _command.Connection = _connection;
                _command.CommandText = sqlCommands;

                var _adapter = _dbProvFac.CreateDataAdapter();
                _adapter.SelectCommand = _command;

                var _builder = _dbProvFac.CreateCommandBuilder();
                _builder.DataAdapter = _adapter;

                _resultTable = new DataTable();

                _adapter.Fill(_resultTable);
            }

            return _resultTable;
        }

        public virtual void Update(DataTable dataTable, string sqlCommand)
        {
            if (!this.IsConnectAble())
                return;

            var _dbProvFac = DbProviderFactories.GetFactory(this.ProviderInvariantName);
            DbProviderFactories.GetFactoryClasses();

            using (var _connection = _dbProvFac.CreateConnection())
            {
                _connection.ConnectionString = this.ConnectionString;

                var _command = _dbProvFac.CreateCommand();
                _command.CommandType = CommandType.Text;
                _command.Connection = _connection;
                _command.CommandText = sqlCommand;

                var _adapter = _dbProvFac.CreateDataAdapter();
                _adapter.SelectCommand = _command;

                var _builder = _dbProvFac.CreateCommandBuilder();
                _builder.DataAdapter = _adapter;

                var _resultTable = new DataTable();

                _adapter.Fill(_resultTable);

                //Leeren
                _resultTable.Rows.Clear();
                _resultTable.AcceptChanges();

                //Übertragen
                foreach(DataRow _sourceRow in dataTable.Rows)
                {
                    var _destRow = _resultTable.NewRow();

                    foreach (DataColumn _destColumn in _resultTable.Columns)
                    {
                        if (dataTable.Columns.Contains(_destColumn.ColumnName))
                        {
                            _destRow[_destColumn.ColumnName] = _sourceRow[_destColumn.ColumnName];
                        }
                    }

                    _resultTable.Rows.Add(_destRow);
                }

                //Aktuallisieren
                _adapter.Update(_resultTable);
            }
        }

        public virtual void ClearAllTables()
        {
            var _doRetry = true;
            var _retry = 20;
            var _tables = (this.SelectSchema() ?? new TableRefInfo[0]).ToList();

            for (var _try = 0; _try < _retry && _doRetry; _try++)
            {
                _doRetry = false;
                foreach (var _tableItem in _tables)
                {
                    try
                    {
                        this.Execute("DELETE " + _tableItem.TableName);
                    }
                    catch// (Exception ex)
                    {
                        _doRetry = true;
                    }
                }
            }
        }



        public virtual void BackUp_Create(string fileName)
        {
            var _tables = (this.SelectSchema() ?? new TableRefInfo[0]).ToList();

            var _dataSet = new DataSet();
            foreach(var _tableInfo in _tables)
            {
                var _selectedDt = this.Select("SELECT * FROM " + _tableInfo.TableName);
                _selectedDt.TableName = _tableInfo.TableName;
                _dataSet.Tables.Add(_selectedDt);
            }
            _dataSet.WriteXml(fileName);

            this.Dispose_DataSet(ref _dataSet);
        }
        public virtual void BackUp_Restore(string fileName)
        {
            if (!File.Exists(fileName))
                return;

            var _tables = (this.SelectSchema() ?? new TableRefInfo[0]).OrderBy(p => p.SteppsToTop).ToList();
            if (_tables.Count <= 0) return;

            var _dataSet = new DataSet();
            _dataSet.ReadXml(fileName);

            if (_dataSet.Tables.Count <= 0) return;

            this.ClearAllTables();

            var _tablesToCheck = from _p in _tables
                where _dataSet.Tables.Contains(_p.TableName)
                orderby _p.SteppsToTop
                select _dataSet.Tables[_p.TableName];

            foreach (var _tableInfo in _tablesToCheck)
            {
                this.Update(_tableInfo, "SELECT * FROM " + _tableInfo.TableName);
            }
        }


        public abstract TableRefInfo[] SelectSchema();



        protected void Dispose_DataTable(ref DataTable table)
        {
            if (table != null)
            {
                var _dataset = table.DataSet;
                if (_dataset != null)
                {
                    string _relationName;
                    DataTable _childTable;
                    for (var _relationIndex = 0; _relationIndex < _dataset.Relations.Count; _relationIndex++)
                    {
                        if (_dataset.Relations[_relationIndex].ParentTable == table || _dataset.Relations[_relationIndex].ChildTable == table)
                        {
                            _relationName = _dataset.Relations[_relationIndex].RelationName;
                            _childTable = _dataset.Relations[_relationIndex].ChildTable;
                            for (var _childIndex = 0; _childIndex < _childTable.Constraints.Count; _childIndex++)
                                if (_childTable.Constraints[_childIndex].ConstraintName == _relationName && _childTable.Constraints[_childIndex].GetType() == typeof(ForeignKeyConstraint))
                                    _childTable.Constraints.RemoveAt(_childIndex);

                            _dataset.Relations.RemoveAt(_relationIndex);
                        }
                    }

                    table.DataSet.Tables.Remove(table);
                    if (_dataset.Tables.Count <= 0)
                    {
                        _dataset.Tables.Clear();
                        _dataset.Dispose();
                    }
                }

                table.Clear();
                table.Dispose();
                table = null;
            }
        }
        protected void Dispose_DataSet(ref DataSet dataSet)
        {
            if (dataSet != null)
            {
                dataSet.Reset();
                dataSet.Relations.Clear();
                dataSet.Clear();

                foreach (DataTable _table in dataSet.Tables)
                {
                    _table.Dispose();
                }

                dataSet.Tables.Clear();
                dataSet.Dispose();
                dataSet = null;
            }
        }
    }




    public class EnvGpDbSqlDev : EnvGpDbBaseDev
    {
        public override string ProviderInvariantName
        {
            get
            {
                return "System.Data.SqlClient";
            }
        }



        public EnvGpDbSqlDev(EnvDTE80.DTE2 envDte = null)
            : base(envDte)
        {
        }



        public override TableRefInfo[] SelectSchema()
        {
            var _result = new List<TableRefInfo>();

            var _sqlCommand = string.Empty;
            _sqlCommand += "select ";
            _sqlCommand += "	INFORMATION_SCHEMA.TABLES.TABLE_NAME, ";
            _sqlCommand += "	SubSql.destTable ";
            _sqlCommand += "from ";
            _sqlCommand += "	INFORMATION_SCHEMA.TABLES ";
            _sqlCommand += "	left join  ";
            _sqlCommand += "		( ";
            _sqlCommand += "			SELECT ";
            _sqlCommand += "				fk.name 'FKName', ";
            _sqlCommand += "				tp.name 'sourceTable', ";
            _sqlCommand += "				cp.name 'sourceColumn', ";
            _sqlCommand += "				cp.column_id 'sourceColumnId', ";
            _sqlCommand += "				tr.name 'destTable', ";
            _sqlCommand += "				cr.name 'destColumn', ";
            _sqlCommand += "				cr.column_id 'destColumnId' ";
            _sqlCommand += "			FROM  ";
            _sqlCommand += "				sys.foreign_keys fk ";
            _sqlCommand += "				INNER JOIN sys.tables tp ON fk.parent_object_id = tp.object_id ";
            _sqlCommand += "				INNER JOIN sys.tables tr ON fk.referenced_object_id = tr.object_id ";
            _sqlCommand += "				INNER JOIN sys.foreign_key_columns fkc ON fkc.constraint_object_id = fk.object_id ";
            _sqlCommand += "				INNER JOIN sys.columns cp ON fkc.parent_column_id = cp.column_id AND fkc.parent_object_id = cp.object_id ";
            _sqlCommand += "				INNER JOIN sys.columns cr ON fkc.referenced_column_id = cr.column_id AND fkc.referenced_object_id = cr.object_id ";
            _sqlCommand += "		) as SubSql on SubSql.sourceTable = INFORMATION_SCHEMA.TABLES.TABLE_NAME ";
            _sqlCommand += "WHERE ";
            _sqlCommand += "	INFORMATION_SCHEMA.TABLES.TABLE_TYPE = 'BASE TABLE' ";

            var _tbl = this.Select(_sqlCommand);

            if (_tbl != null && _tbl.Rows.Count > 0)
            {
                _result = (from _p in _tbl.Rows.Cast<DataRow>().ToArray()
                           group _p by _p["TABLE_NAME"].ToString() into _g
                           select new TableRefInfo() { TableName = _g.Key, ReferentUpper = this.SelectSchema_ReferentUpper(_g.Key, _tbl.Rows.Cast<DataRow>().ToArray()) }).ToList();

                foreach (var _element in _result)
                {
                    _element.SteppsToTop = this.SelectSchema_SteppsToTop(_element, _result.ToArray());
                }

                _result = _result.OrderByDescending(p => p.SteppsToTop).ThenBy(p => p.TableName).ToList();
            }

            this.Dispose_DataTable(ref _tbl);

            return _result.ToArray();
        }

        private string[] SelectSchema_ReferentUpper(string tableName, params DataRow[] rows)
        {
            var _result = (from _p in rows
                                where _p["TABLE_NAME"].ToString() == tableName && _p["destTable"].ToString() != tableName
                                select _p["destTable"].ToString()).ToArray();

            _result = (from _p in _result
                       where !string.IsNullOrEmpty(_p) && !string.IsNullOrEmpty(_p.Trim())
                       select _p).ToArray();

            return _result;
        }

        private int SelectSchema_SteppsToTop(TableRefInfo tableTo, params TableRefInfo[] allElements)
        {
            var _result = 0;
            foreach (var _upperItem in allElements.Where(p => (tableTo.ReferentUpper ?? new string[0]).Contains(p.TableName)))
            {
                var _upper = this.SelectSchema_SteppsToTop(_upperItem, allElements);
                _result = Math.Max(_result, _upper + 1);
            }

            return _result;
        }
    }




    public class EnvGpSysFile
    {
        public EnvDTE80.DTE2 EnvDte { get; private set; }
        private readonly XmlDocument m_xmlDoc = new XmlDocument();
        private readonly FileInfo m_gpOemSupportDll;


        public string DbConString
        {
            get
            {
                var _result = this.GetSetting("m_databaseConnectionString");

                _result = this.Decrypt(_result);

                return _result;
            }
        }



        private string GetSetting(string key)
        {
            if (this.m_xmlDoc.DocumentElement == null) return null;
            if (this.m_xmlDoc.DocumentElement.ChildNodes[0].SelectSingleNode(key) == null) return null;

            // ReSharper disable once PossibleNullReferenceException
            return this.m_xmlDoc.DocumentElement.ChildNodes[0].SelectSingleNode(key) != null ? this.m_xmlDoc.DocumentElement.ChildNodes[0].SelectSingleNode(key).InnerText : null;
        }

        public string Decrypt(string encryptedText)
        {
            var _assembly = Assembly.LoadFrom(this.m_gpOemSupportDll.FullName);
            var _type = _assembly.GetType("GP.OEMSupport.Support");
            var _propertyInfo = _type.GetProperty("Instance");
            var _methodInfo = _type.GetMethod("ConvertFrom");
            var _support = _propertyInfo.GetValue(null);
            var _decryptedText = (string)_methodInfo.Invoke(_support, new object[] { encryptedText });

            return _decryptedText;
        }


        private EnvGpSysFile(EnvDTE80.DTE2 envDte, string sysSettXml)
        {
            this.EnvDte = envDte;
            this.m_xmlDoc.Load(sysSettXml);

            this.m_gpOemSupportDll = new FileInfo(PathHelper.Combine(EnvSolutionDev.GetDirectory(envDte), "Lib\\GP.OEMSupport.dll"));
        }
        public static EnvGpSysFile CreateInst(EnvDTE80.DTE2 envDte = null)
        {
            envDte = envDte ?? Package.GetGlobalService(typeof(EnvDTE.DTE)) as EnvDTE80.DTE2;

            var _sysSettXml = PathHelper.Combine(EnvSolutionDev.GetDirectory(envDte), "bin\\SystemSettings.xml");

            return new EnvGpSysFile(envDte, _sysSettXml);
        }

        public static bool IsValid(EnvDTE80.DTE2 envDte = null)
        {
            envDte = envDte ?? Package.GetGlobalService(typeof(EnvDTE.DTE)) as EnvDTE80.DTE2;
            PathHelper.Combine(EnvSolutionDev.GetDirectory(envDte), "bin\\SystemSettings.xml");

            var _result = EnvSolutionDev.IsValid(envDte);
            _result = _result && EnvSolutionDev.GetDirectory(envDte).Exists;
            _result = _result && File.Exists(PathHelper.Combine(EnvSolutionDev.GetDirectory(envDte), "bin\\SystemSettings.xml"));

            return _result;
        }
    }
}

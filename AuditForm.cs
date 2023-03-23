using DatabaseUtils.Model.Database;
using DatabaseUtils.Model.General;
using DatabaseUtils.Model;
using DatabaseUtils.Service;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using DatabaseUtils.Utils;
using System.Threading;
using System.Collections.Generic;

namespace DatabaseUtils
{
    public partial class AuditForm : Form
    {
        private BindingList<Database> _databases;
        private BindingList<Database> _targetDatabases;
        private BindingList<DatabaseTable> _tables;

        private readonly string _chooseDbString = "--Choose Database--";
        private readonly string _newDbString = "--New Database--";

        private string _logs = "";

        public AuditForm()
        {
            InitializeComponent();
        }

        private void AuditForm_Load(object sender, EventArgs e)
        {
            DbConnectionString dbConnectionString = new DbConnectionString(GlobalValues.DbConnectionString);
            var result = DatabaseService.GetDataBases(new Query<Database>
            {
                Conn = dbConnectionString
            });

            if (result.Result)
            {
                result.Response.Insert(0, new Database
                {
                    Id = 0,
                    Name = _chooseDbString
                });
                _databases = new BindingList<Database>(result.Response);

                var targetDbs = result.Response.Select(x => new Database
                {
                    Id = x.Id,
                    Name = x.Name,
                }).ToList();

                targetDbs.Insert(1, new Database
                {
                    Id = 10000,
                    Name = _newDbString
                });

                _targetDatabases = new BindingList<Database>(targetDbs);

            }

            bwLogs.WorkerReportsProgress = true;
            bwLogs.WorkerSupportsCancellation = true;
        }

        private void AuditForm_Activated(object sender, EventArgs e)
        {
            cboDbNames.DataSource = _databases;
            cboTargetDb.DataSource = _targetDatabases;
        }

        private void AuditForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnDbGetTables_Click(object sender, EventArgs e)
        {
            RefreshTables(); 
        }

        private void RefreshTables()
        {
            if (_tables != null)
                _tables.Clear();



            var database = cboDbNames.SelectedItem as Database;

            if (database == null || database.Name == _chooseDbString)
            {
                return;
            }


            DbConnectionString dbConnectionString = new DbConnectionString(GlobalValues.DbConnectionString);
            dbConnectionString.InitialCatalog = database.Name;
            var result = DatabaseService.GetDataBaseTables(new Query<Database>
            {
                Conn = dbConnectionString
            });


            if (result.Result)
            {
                _tables = new BindingList<DatabaseTable>(result.Response.OrderBy(x => x.Name).ToList());

                clbDbTables.DataSource = _tables;

                clbDbTables.DisplayMember = "Name";
                clbDbTables.ValueMember = "Name";
            }
        }

        private void btnDBCheckAllTables_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < clbDbTables.Items.Count; i++)
            {
                clbDbTables.SetItemChecked(i, true);
            }
        }

        private void btnDBCheckNoTables_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < clbDbTables.Items.Count; i++)
            {
                clbDbTables.SetItemChecked(i, false);
            }
        }

        private void btnCreateAudit_Click(object sender, EventArgs e)
        {
            _logs = "";

            string targetDb = "";
            string tableSuffix = txtSuffix.Text;
            // --------------------------------------------------------------------------------
            var sourceDb = cboDbNames.SelectedItem as Database;
            var selectedTargetDb = cboTargetDb.SelectedItem as Database;
            var newDb = selectedTargetDb != null && selectedTargetDb.Name == _newDbString;
            if (newDb)
            {
                targetDb = txtDbName.Text;

                if (targetDb.IsEmpty())
                {
                    MessageBox.Show("New Db name is not defined. Please write DB Name.", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                targetDb = selectedTargetDb.Name == _chooseDbString ? string.Empty : selectedTargetDb.Name;
            }

            if (targetDb.IsEmpty())
            {
                MessageBox.Show("Target Db is not defined. Please Check target DB.", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var checkedTables = clbDbTables.CheckedItems;

            if (checkedTables.Count < 1)
            {
                MessageBox.Show("At least one table has to be checked! ", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!bwLogs.IsBusy)
                bwLogs.RunWorkerAsync();

            DbConnectionString dbConnectionString = new DbConnectionString(GlobalValues.DbConnectionString);

            // Generate New db if __New__
            // if new we have to check if same db exists before  
            if (newDb)
            {
                
                var result = DatabaseService.GetDataBases(new Query<Database>
                {
                    Conn = dbConnectionString
                });

                if (result.Result)
                {
                    if (result.Response.Any(x => x.Name.Equals(targetDb, StringComparison.OrdinalIgnoreCase)))
                    {
                        MessageBox.Show("There is already database with the same name!", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // Create Database -------------------------------------------------------------
                //CREATE DATABASE testDB; 
                Log($"Create Database {targetDb}");
                var createDbResult = DatabaseService.CreateDatabase(new Query<Database>
                {
                    Conn = dbConnectionString,
                    Parameter = new Database { Name = targetDb }
                });

                if (createDbResult.Result)
                {
                    Log($"{targetDb} Database created successfully!");
                }
                else
                {
                    Log($"{targetDb} Database was not created! Error: {createDbResult.ErrorMessage}");
                    StopCleanAndReturn();
                }
            }
            // For each table ------------------------------------------------------------------
            Log($"Checking Tables");
            var targetTables = new List<DatabaseTable>();
            if (!newDb)
            { 
                dbConnectionString.InitialCatalog = targetDb;
                var result = DatabaseService.GetDataBaseTables(new Query<Database>
                {
                    Conn = dbConnectionString
                });

                if (!result.Result)
                {
                    Log($"Could not get {targetDb} database tables!");
                    StopCleanAndReturn();
                }

                targetTables = result.Response;
            }


            foreach (var checkedTable in checkedTables)
            {
                var sourceTable = checkedTable as DatabaseTable;

                var logPrefix = "\t";

                Log($"Checking table {sourceTable.Name}!");
                // Get table columns from source -----------------------------------------------------------  
                dbConnectionString.InitialCatalog = sourceDb.Name;
                var columnsResult = DatabaseService.GetDataBaseTableColumns(new Query<DatabaseTable>
                {
                    Conn = dbConnectionString,
                    Parameter = new DatabaseTable
                    {
                        Name = sourceTable.Name
                    }
                });

                if (!columnsResult.Result)
                {
                    Log($"{logPrefix} Could not get {sourceTable.Name} database table columns! Error: {columnsResult.ErrorMessage}");
                    StopCleanAndReturn();
                }

                var tableColumns = columnsResult.Response;

                string logTableName = sourceTable.Name + tableSuffix;

                var targetTable = targetTables.FirstOrDefault(t => t.Name.InsensitiveEqual(logTableName));

                if (targetTable == null)
                {
                    Log($"{logPrefix} Creating log table {logTableName} on target Db {targetDb} ");

                    string createTableCommandText = $" USE {targetDb}; ";
                    createTableCommandText += $"CREATE TABLE {logTableName} ( ";
                    createTableCommandText += "AuditId BIGINT IDENTITY(1,1) NOT NULL, [AuditAction] [CHAR](1) NOT NULL, [AuditDate] [datetime] NOT NULL, [AuditUser] [NVARCHAR](50) NULL, [AuditApp] [NVARCHAR](128) NULL";
                    for (int i = 0; i < tableColumns.Count; i++)
                    {
                        var tableColumn = tableColumns[i];

                        createTableCommandText += ", ";
                        createTableCommandText += "[" + tableColumn.Name + "] " + tableColumn.DataType + GetColumnLengthPhrase(tableColumn);
                    }
                    createTableCommandText += @" CONSTRAINT [PK_audit_" + logTableName + @"] PRIMARY KEY CLUSTERED 
                            (
	                            [AuditId] ASC
                            )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
                            ) ON [PRIMARY]
 
                            ;";
                    //TODO: Check if PK constraint is existed before 

                    // Execute
                    dbConnectionString.InitialCatalog = targetDb;
                    var executeResult = DatabaseService.ExecuteSql(new Query<DatabaseCommand>
                    {
                        Conn = dbConnectionString,
                        Parameter = new DatabaseCommand
                        {
                            Command = createTableCommandText
                        }
                    });

                    if (!executeResult.Result)
                    {

                        Log($"{logPrefix} Could not create {logTableName} database table! Error: {executeResult.ErrorMessage}");
                        StopCleanAndReturn();
                    }
                    else
                    {
                        Log($"{logPrefix} {logTableName} database table created!");
                    }
                }
                else
                {
                    dbConnectionString.InitialCatalog = targetDb;
                    var targetColumnsResult = DatabaseService.GetDataBaseTableColumns(new Query<DatabaseTable>
                    {
                        Conn = dbConnectionString,
                        Parameter = new DatabaseTable
                        {
                            Name = logTableName
                        }
                    });

                    if (!targetColumnsResult.Result)
                    {
                        Log($"{logPrefix} Could not get target {logTableName} database table columns! Error: {targetColumnsResult.ErrorMessage}");
                        StopCleanAndReturn();
                    }

                    var targetColumns = targetColumnsResult.Response;

                    // Check for  missed columns  
                    var missedColumns = tableColumns.Where(x => !targetColumns.Any(y => y.Name.InsensitiveEqual(x.Name))).ToList();
                    if (missedColumns.Any())
                    {
                        string command = $" USE {targetDb}; ";
                        foreach (var missedColumn in missedColumns)
                        {
                            command += $" ALTER TABLE {logTableName} ADD {missedColumn.Name} {missedColumn.DataType}{GetColumnLengthPhrase(missedColumn)};" + Environment.NewLine;
                        }


                        // Execute
                        dbConnectionString.InitialCatalog = targetDb;
                        var executeResult = DatabaseService.ExecuteSql(new Query<DatabaseCommand>
                        {
                            Conn = dbConnectionString,
                            Parameter = new DatabaseCommand
                            {
                                Command = command
                            }
                        });

                        if (!executeResult.Result)
                        {

                            Log($"{logPrefix} Could not add {logTableName} missed columnss! Error: {executeResult.ErrorMessage}");
                            StopCleanAndReturn();
                        }
                        else
                        {
                            Log($"{logPrefix} {logTableName} database table missed columns added!");
                        }

                    }
                } 

                //Log($"Get source table  {sourceTable.Name} columns ");
                //// Check if it exists on Target (if new db created no need) ------------------------
                //Log($"Check if it exists on Target (if new db created no need)");
                //// Create new table if not exists OR Check if missed columns created and add them --
                //Log($"Create new table if not exists OR Check if missed columns created and add them");
                // Drop triggers and recreate them ------------------------------------------------- 
                //Log($"Drop triggers and recreate them");

                string columsScript = "";

                for (int i = 0; i < tableColumns.Count; i++)
                {
                    var tableColumn = tableColumns[i];

                    columsScript += ",";

                    columsScript += "[" + tableColumn.Name + "]" + Environment.NewLine;
                }


                string dropAllTriiggersScript = @"
                        USE " + sourceDb.Name + @";
                        DROP TRIGGER IF EXISTS TR_" + sourceTable.Name + @"_INSERT;
                        DROP TRIGGER IF EXISTS TR_" + sourceTable.Name + @"_UPDATE;
                        DROP TRIGGER IF EXISTS TR_" + sourceTable.Name + @"_DELETE;  ";

                // Execute
                dbConnectionString.InitialCatalog = sourceDb.Name;
                var executeResultDrop = DatabaseService.ExecuteSql(new Query<DatabaseCommand>
                {
                    Conn = dbConnectionString,
                    Parameter = new DatabaseCommand
                    {
                        Command = dropAllTriiggersScript
                    }
                });

                if (!executeResultDrop.Result)
                {

                    Log($"Could not drop {sourceTable.Name} triggers! Error: {executeResultDrop.ErrorMessage}");
                    StopCleanAndReturn();
                }
                else
                {
                    Log($"{logPrefix} {sourceTable.Name} triggers dropped!");
                }

                dbConnectionString.InitialCatalog = sourceDb.Name;

                string createInsertTriggerScript = @" 
                        CREATE TRIGGER TR_" + sourceTable.Name + @"_INSERT
                            ON  " + sourceTable.Name + @" 
                            FOR INSERT
                        AS 
                        BEGIN 
	                        SET NOCOUNT ON;

	                        INSERT INTO "  + targetDb + @".[dbo]." + logTableName + @"
                                    ([AuditAction]
                                    ,[AuditDate]
                                    ,[AuditUser]
                                    ,[AuditApp]
                                    " + columsScript + @")
		                        SELECT 'I'
                                    ,GETDATE()
                                    ,'System'
                                    ,'' 
                                    " + columsScript + @"
		                        FROM INSERTED  
                        END 

                ";


                var executeResultInsert = DatabaseService.ExecuteSql(new Query<DatabaseCommand>
                {
                    Conn = dbConnectionString,
                    Parameter = new DatabaseCommand
                    {
                        Command = createInsertTriggerScript
                    }
                });

                if (!executeResultInsert.Result)
                {

                    Log($"{logPrefix} Could not create {sourceTable.Name} Insert trigger! Error: {executeResultInsert.ErrorMessage}");
                    StopCleanAndReturn();
                }
                else
                {
                    Log($"{logPrefix} {sourceTable.Name} Insert trigger created!");
                }

                string createUpdateTriggerScript = @" 
                        
                        CREATE TRIGGER TR_" + sourceTable.Name + @"_UPDATE
                            ON  " + sourceTable.Name + @" 
                            FOR UPDATE
                        AS 
                        BEGIN 
	                        SET NOCOUNT ON;

	                        INSERT INTO " + targetDb + @".[dbo]." + logTableName + @"
                                    ([AuditAction]
                                    ,[AuditDate]
                                    ,[AuditUser]
                                    ,[AuditApp]
                                    " + columsScript + @")
		                        SELECT 'U'
                                    ,GETDATE()
                                    ,'System'
                                    ,'' 
                                    " + columsScript + @"
		                        FROM INSERTED  
                        END 

                ";


                var executeResultUpdate = DatabaseService.ExecuteSql(new Query<DatabaseCommand>
                {
                    Conn = dbConnectionString,
                    Parameter = new DatabaseCommand
                    {
                        Command = createUpdateTriggerScript
                    }
                });

                if (!executeResultUpdate.Result)
                {

                    Log($"{logPrefix} Could not create {sourceTable.Name} Update trigger! Error: {executeResultUpdate.ErrorMessage}");
                    StopCleanAndReturn();
                }
                else
                {
                    Log($"{logPrefix} {sourceTable.Name} Update trigger created!");
                }


                string createDeleteTriggerScript = @" 
                        
                        CREATE TRIGGER TR_" + sourceTable.Name + @"_DELETE
                            ON  "  + sourceTable.Name + @" 
                            FOR DELETE
                        AS 
                        BEGIN 
	                        SET NOCOUNT ON;

	                        INSERT INTO " + targetDb + @".[dbo]." + logTableName + @"
                                    ([AuditAction]
                                    ,[AuditDate]
                                    ,[AuditUser]
                                    ,[AuditApp]
                                    " + columsScript + @")
		                        SELECT 'D'
                                    ,GETDATE()
                                    ,'System'
                                    ,'' 
                                    " + columsScript + @"
		                        FROM DELETED  
                        END 

                ";


                var executeResultDelete = DatabaseService.ExecuteSql(new Query<DatabaseCommand>
                {
                    Conn = dbConnectionString,
                    Parameter = new DatabaseCommand
                    {
                        Command = createDeleteTriggerScript
                    }
                });

                if (!executeResultDelete.Result)
                {

                    Log($"{logPrefix} Could not create {sourceTable.Name} Delete trigger! Error: {executeResultDelete.ErrorMessage}");
                    StopCleanAndReturn();
                }
                else
                {
                    Log($"{logPrefix} {sourceTable.Name} Delete trigger created!");
                }

                // ---------------------------------------------------------------------------------
            }







            StopCleanAndReturn();
        }

        private string GetColumnLengthPhrase(DatabaseColumn tableColumn)
        {
            return (tableColumn.Length > 0 ? $"({tableColumn.Length})" : (tableColumn.Length < 0 ? $"(MAX)" : ""));
        }

        private void StopCleanAndReturn()
        {
            Thread.Sleep(2000);

            if (bwLogs.IsBusy)
                bwLogs.CancelAsync();

            txtLogs.Text = _logs;

            return;
        }

        private void cboTargetDb_SelectedValueChanged(object sender, EventArgs e)
        {
            var selectedTargetDb = cboTargetDb.SelectedItem as Database;

            var dbControlsVisibility = selectedTargetDb != null && selectedTargetDb.Name == _newDbString;

            lblDbName.Visible = dbControlsVisibility;
            txtDbName.Visible = dbControlsVisibility;
        }
        private void Log(string line)
        {
            _logs += line + Environment.NewLine;
            txtLogs.Text = _logs;
        }

        private void bwLogs_DoWork(object sender, DoWorkEventArgs e)
        {
            bwLogs.ReportProgress(0, _logs);
        }

        private void bwLogs_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //txtLogs.Text = e.UserState as string;
            //txtLogs.Text += "........." + Environment.NewLine;
        }

        private void cboDbNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshTables();
        }
    }
}

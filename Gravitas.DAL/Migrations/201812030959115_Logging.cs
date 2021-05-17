namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Logging : DbMigration
    {
        public override void Up()
        {
            Sql("CREATE TABLE [dbo].[Logging]( " +
                "[Logging_guid][uniqueidentifier] ROWGUIDCOL  NOT NULL," +
                "[entered_date][datetime] NULL, "+
                     "[log_application][varchar](200) NULL, " +
                     "[log_date][varchar](100) NULL, " +
                     "[log_level][varchar](100) NULL, " +
                     "[log_logger][varchar](8000) NULL, " +
                     "[log_message][varchar](8000) NULL, " +
                     "[log_machine_name][varchar](8000) NULL, " +
                     "[log_user_name][varchar](8000) NULL, " +
                     "[log_call_site][varchar](8000) NULL, " +
                     "[log_thread][varchar](100) NULL, " +
                     "[log_exception][varchar](8000) NULL, " +
                     "[log_stacktrace][varchar](8000) NULL, " +
                     "CONSTRAINT[PK_Logging] PRIMARY KEY CLUSTERED(" +
                     "[Logging_guid] ASC  " +
                     ")WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]  " +
                     ") ON[PRIMARY] ");

            Sql("ALTER TABLE[dbo].[Logging] ADD CONSTRAINT[DF_Logging_Logging_guid]  DEFAULT(newid()) FOR[Logging_guid]");

            Sql("ALTER TABLE [dbo].[Logging] ADD  CONSTRAINT [DF_Logging_entered_date]  DEFAULT (getdate()) FOR [entered_date]");
        }

        public override void Down()
        {
            Sql("DROP TABLE[dbo].[Logging]");
        }
    }
}

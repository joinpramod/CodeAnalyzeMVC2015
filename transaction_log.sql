USE codeanalyze;
GO
-- Truncate the log by changing the database recovery model to SIMPLE.
ALTER DATABASE codeanalyze
SET RECOVERY SIMPLE;
GO
-- Shrink the truncated log file to 1 MB.
DBCC SHRINKFILE (codeanalyze_log, 1);
GO
-- Reset the database recovery model.
ALTER DATABASE codeanalyze
SET RECOVERY FULL;
GO
# Migration: Open terminal from DataAccess
dotnet ef migrations add InitialMigration --project ../DataAccess --startup-project ../RealTimeChat
dotnet ef database update --project ../DataAccess --startup-project ../RealTimeChat

# Drop database
SELECT pg_terminate_backend(pg_stat_activity.pid)
FROM pg_stat_activity
WHERE pg_stat_activity.datname = 'RealTime'
  AND pid <> pg_backend_pid();

DROP DATABASE "RealTime"

CREATE EXTENSION "uuid-ossp";
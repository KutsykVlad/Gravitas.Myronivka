CREATE TRIGGER [opd].trgOpdScaleOpDataHistory
    ON [mhp].[opd].[ScaleOpData]
    AFTER INSERT, UPDATE, DELETE
    AS
    DECLARE @LogType AS CHAR(1) =
        CASE
            WHEN EXISTS (SELECT 1 FROM inserted) AND EXISTS (SELECT 1 FROM DELETED) THEN 'u'
            WHEN EXISTS (SELECT 1 FROM inserted) AND NOT EXISTS (SELECT 1 FROM DELETED) THEN 'i'
            WHEN EXISTS (SELECT 1 FROM deleted) AND NOT EXISTS (SELECT 1 FROM INSERTED) THEN 'd'
            ELSE NULL
        END;
IF (@LogType IN ('i', 'u'))
    BEGIN
        INSERT INTO [mhp].opd.ScaleOpDataHistory
        (
            [ScaleOpDataId],
            [LogType],
            [TypeId],
			[TruckWeightDateTime],
			[TruckWeightValue],
			[TruckWeightIsAccepted],
			[TrailerWeightDateTime],
			[TrailerWeightValue],
			[TrailerWeightIsAccepted],
			[StateId],
			[NodeId],
			[TicketId],
			[TicketContainerId],
			[CheckInDateTime],
			[CheckOutDateTime],
			[TrailerAvailable],
			[GuardPresence]
        )
        SELECT
            [Id] AS [ScaleOpDataId],
            @LogType,
            [TypeId],
			[TruckWeightDateTime],
			[TruckWeightValue],
			[TruckWeightIsAccepted],
			[TrailerWeightDateTime],
			[TrailerWeightValue],
			[TrailerWeightIsAccepted],
			[StateId],
			[NodeId],
			[TicketId],
			[TicketContainerId],
			[CheckInDateTime],
			[CheckOutDateTime],
			[TrailerAvailable],
			[GuardPresence]
            FROM inserted;
    END;
IF (@LogType = 'd')
    BEGIN
        INSERT INTO [mhp].opd.ScaleOpDataHistory
        (
            [ScaleOpDataId],
            [LogType],
            [TypeId],
			[TruckWeightDateTime],
			[TruckWeightValue],
			[TruckWeightIsAccepted],
			[TrailerWeightDateTime],
			[TrailerWeightValue],
			[TrailerWeightIsAccepted],
			[StateId],
			[NodeId],
			[TicketId],
			[TicketContainerId],
			[CheckInDateTime],
			[CheckOutDateTime],
			[TrailerAvailable],
			[GuardPresence]
        )
        SELECT
            [Id] AS [ScaleOpDataId],
            @LogType,
            [TypeId],
			[TruckWeightDateTime],
			[TruckWeightValue],
			[TruckWeightIsAccepted],
			[TrailerWeightDateTime],
			[TrailerWeightValue],
			[TrailerWeightIsAccepted],
			[StateId],
			[NodeId],
			[TicketId],
			[TicketContainerId],
			[CheckInDateTime],
			[CheckOutDateTime],
			[TrailerAvailable],
			[GuardPresence]
            FROM deleted;
    END;
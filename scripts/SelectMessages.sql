SELECT TOP (1000) 
    [Id],
    [Expires],
    [Headers],
    [Body],
    cast([Body] as varchar(max)) as [BodyString]
FROM [NServiceBus-4.x].[dbo].[NSB.SqlServer.Consumer] WITH (READPAST);

SELECT TOP (1000) 
    [RowVersion],
    cast([Body] as varchar(max)) as [BodyString]
FROM [NServiceBus-4.x].[dbo].[audit] WITH (READPAST)
ORDER BY RowVersion DESC;
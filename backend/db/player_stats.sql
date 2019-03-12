SELECT s.Name, *
  FROM [dbo].[SkaterStatistics] sk 
  inner join [dbo].[Seasons] s ON s.Id = sk.SeasonId
  where sk.PlayerId = 1
  AND s.Name Like '%Regular%'
  order by s.Name

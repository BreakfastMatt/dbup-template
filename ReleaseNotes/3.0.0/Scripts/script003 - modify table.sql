BEGIN TRAN

SELECT * FROM [dbo].[ActivityTrackerDropdown]
WHERE Configuration IS NUll

DELETE FROM [dbo].[ActivityTrackerDropdown]
WHERE Configuration IS NUll

SELECT * FROM [dbo].[ActivityTrackerDropdown]
WHERE Configuration IS NUll

PRINT  'Success'
COMMIT
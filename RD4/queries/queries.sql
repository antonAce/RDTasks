-- 1) �������� ������ ���� ���������� �������� � ����������� ����������� �� ������ �� ���

SELECT [position_name], [ammout_of_positions]
FROM [EmployeePositions] JOIN
(SELECT [position_id], COUNT([employee_id]) AS [ammout_of_positions]
FROM [TasksToEmployees]
GROUP BY [position_id]) AS [AmmountOfPositions]
ON [AmmountOfPositions].[position_id] = [EmployeePositions].[position_id];

-- 2) ���������� ������ ���������� ��������, �� ������� ��� �����������

SELECT [position_name] FROM
(SELECT [position_name], COUNT([employee_id]) AS [ammout_of_positions]
FROM [EmployeePositions]
LEFT JOIN [TasksToEmployees] ON [EmployeePositions].[position_id] = [TasksToEmployees].[position_id]
GROUP BY [position_name]) AS [EmpAmmountOfPositions]
WHERE [ammout_of_positions] = 0;


-- 3) �������� ������ �������� � ���������, ������� ����������� ������ ��������� �������� �� �������

SELECT [project_name], [position_name], [ammout_of_employees] FROM
(SELECT [project_id], [position_id], COUNT([employee_id]) AS [ammout_of_employees] FROM [Tasks]
JOIN [TasksToEmployees] ON [Tasks].[task_id] = [TasksToEmployees].[task_id]
GROUP BY [project_id], [position_id]) AS [ProjectsPositionsEmployees]
JOIN [EmployeePositions] ON [EmployeePositions].[position_id] = [ProjectsPositionsEmployees].[position_id]
JOIN [Projects] ON [Projects].[project_id] = [ProjectsPositionsEmployees].[project_id]
ORDER BY [project_name], [position_name];


-- 4) ��������� �� ������ �������, ����� � ������� ���������� ����� ���������� �� ������� ����������

SELECT [project_name], [avg_tasks] FROM
(SELECT [project_id], AVG([tasks_count]) AS [avg_tasks] FROM
(SELECT [project_id], [employee_id], COUNT([TasksToEmployees].[task_id]) AS [tasks_count] FROM [Tasks]
JOIN [TasksToEmployees] ON [Tasks].[task_id] = [TasksToEmployees].[task_id]
GROUP BY [project_id], [employee_id]) AS [ProjectsEmployeesCount]
GROUP BY [project_id]) AS [ProjectToAvg]
JOIN [Projects] ON [ProjectToAvg].[project_id] = [Projects].[project_id];

-- 5) ���������� ������������ ���������� ������� �������

-- ������� ������ �� ����
SELECT DATEDIFF(DAY, [project_start_date], [project_end_date]) AS [days_spent_for_projects] FROM [Projects];

-- ������� �� ���� + �� �����
SELECT CONCAT('Years:', [total_day_spent] / 365, ', Days:', [total_day_spent] % 365) AS [days_spent_for_projects] FROM
(SELECT DATEDIFF(DAY, [project_start_date], [project_end_date]) AS [total_day_spent] FROM [Projects]) AS [DaysSpent];

-- 6) ���������� ����������� � ����������� ����������� ���������� �����

SELECT [employee_firstname], [employee_lastname] FROM
(SELECT [employee_id] FROM
(
-- Query of taking min ammount of tasks as one scalar for ammount result
SELECT MIN([ammount_of_tasks]) AS [min_of_tasks] FROM
(
-- Query of taking ammount of tasks with state not equal 'Closed'
SELECT [employee_id], COUNT([TaskState].[state_name]) AS [ammount_of_tasks] FROM [Tasks]
JOIN [TasksToEmployees] ON [Tasks].[task_id] = [TasksToEmployees].[task_id]
JOIN [TaskState] ON [Tasks].[state_id] = [TaskState].[state_id]
WHERE [TaskState].[state_name] != 'Closed'
GROUP BY [employee_id]
) AS [StatesOfTasksForEmployees]
-----------------------------------------------------------------
) AS [MinAmmountOfTasks]
-------------------------------------------------------------------------

JOIN

(
-- Dublicating the ammout query upper and joinig with min ammout table to get emplyees with min ammount of tasks
SELECT [employee_id], COUNT([TaskState].[state_name]) AS [ammount_of_tasks] FROM [Tasks]
JOIN [TasksToEmployees] ON [Tasks].[task_id] = [TasksToEmployees].[task_id]
JOIN [TaskState] ON [Tasks].[state_id] = [TaskState].[state_id]
WHERE [TaskState].[state_name] != 'Closed'
GROUP BY [employee_id]
) AS [StatesOfTasksForEmployees]
--------------------------------------------------------------------------
ON [MinAmmountOfTasks].[min_of_tasks] = [StatesOfTasksForEmployees].[ammount_of_tasks]
) AS [EmployeesWithMinNotClosedTasks]
JOIN [Employees] ON [Employees].[employee_id] = [EmployeesWithMinNotClosedTasks].[employee_id];

-- 7) ���������� ����������� � ������������ ����������� ���������� �����, ������� ������� ��� �����

SELECT [employee_firstname], [employee_lastname] FROM
(SELECT [employee_id] FROM
(
SELECT MAX([ammount_of_tasks]) AS [min_of_tasks] FROM
(
SELECT [employee_id], COUNT([TaskState].[state_name]) AS [ammount_of_tasks] FROM [Tasks]
JOIN [TasksToEmployees] ON [Tasks].[task_id] = [TasksToEmployees].[task_id]
JOIN [TaskState] ON [Tasks].[state_id] = [TaskState].[state_id]
WHERE [state_name] != 'Closed'
-- Here is right line of code below, but it returns an empty set (03.07.19), cuz all tasks still don't reach their deadline.
AND [task_deadline] < GETDATE()
-- So below is test query clause
--AND [task_deadline] < convert(datetime2, '22-07-2019 10:25:00', 105)
GROUP BY [employee_id]
) AS [StatesOfTasksForEmployees]
-----------------------------------------------------------------
) AS [MinAmmountOfTasks]
-------------------------------------------------------------------------

JOIN

(
SELECT [employee_id], COUNT([TaskState].[state_name]) AS [ammount_of_tasks] FROM [Tasks]
JOIN [TasksToEmployees] ON [Tasks].[task_id] = [TasksToEmployees].[task_id]
JOIN [TaskState] ON [Tasks].[state_id] = [TaskState].[state_id]
WHERE [state_name] != 'Closed'
-- Here is right line of code below, but it returns an empty set (03.07.19), cuz all tasks still don't reach their deadline.
AND [task_deadline] < GETDATE()
-- So below is test query clause
--AND [task_deadline] < convert(datetime2, '22-07-2019 10:25:00', 105)
GROUP BY [employee_id]
) AS [StatesOfTasksForEmployees]
ON [MinAmmountOfTasks].[min_of_tasks] = [StatesOfTasksForEmployees].[ammount_of_tasks]
) AS [EmployeesWithMinNotClosedTasks]
JOIN [Employees] ON [Employees].[employee_id] = [EmployeesWithMinNotClosedTasks].[employee_id];

-- 8) �������� ������� ���������� ����� �� 5 ����

UPDATE [Tasks]
SET [task_deadline] = DATEADD(DAY, DATEDIFF(DAY, 0, [task_deadline]), 5)
FROM [Tasks]
JOIN [TaskState] ON [Tasks].[state_id] = [TaskState].[state_id]
WHERE [state_name] != 'Closed';

-- 9) ��������� �� ������ ������� ���������� �����, � ������� ��� �� ����������

SELECT [project_name], [ammount_of_not_started_tasks] FROM
(SELECT [Projects].[project_id], COUNT([TaskStateInProjectFiltered].[state_id]) AS [ammount_of_not_started_tasks] FROM
(SELECT [TaskStateInProject].[project_id], [TaskStateInProject].[state_id] FROM
(SELECT [Projects].[project_id], [Tasks].[state_id] FROM [Projects]
JOIN [Tasks] ON [Projects].[project_id] = [Tasks].[project_id]) AS [TaskStateInProject]
JOIN [TaskState] ON [TaskStateInProject].[state_id] = [TaskState].[state_id]
-- Did you mean tasks that are not started yet are ''
WHERE [TaskState].[state_name] = 'Open') AS [TaskStateInProjectFiltered]
RIGHT JOIN [Projects] ON [TaskStateInProjectFiltered].[project_id] = [Projects].[project_id]
GROUP BY [Projects].[project_id]) AS [AmmountOfNotStartedTasksInProject]
JOIN [Projects] ON [AmmountOfNotStartedTasksInProject].[project_id] = [Projects].[project_id]
ORDER BY [ammount_of_not_started_tasks] DESC;

-- 10) ��������� ������� � ��������� ������, ��� ������� ��� ������ ������� � ������ ����� �������� �������� �������� ������ �������, �������� ���������

UPDATE [Projects]
SET [state_id] = 1
FROM
(SELECT [project_id] FROM (
-- Query gets ids of projects with all closed tasks by comparing to the ammount of closed tasks to the ammount of all tasks
-- And then gets the set of all projects where ammount of closed tasks equals to ammount of all tasks
SELECT [TasksPerProject].[project_id], [ammount_of_tasks], [ammount_of_closed_tasks] FROM
(SELECT [project_id], COUNT([state_id]) AS [ammount_of_tasks] FROM [Tasks]
GROUP BY [project_id]) AS [TasksPerProject]

LEFT JOIN -- Performed the left join cuz COUNT(all_tasks_proj_ids) > COUNT(closed_tasks_proj_ids) always, so we won't lose data

(SELECT [project_id], COUNT([state_name]) AS [ammount_of_closed_tasks] FROM [Tasks]
JOIN [TaskState] ON [Tasks].[state_id] = [TaskState].[state_id]
WHERE [state_name] = 'Closed'
GROUP BY [project_id]) AS [ClosedTasksPerProject]

ON [TasksPerProject].[project_id] = [ClosedTasksPerProject].[project_id]
) AS [Tasks_ammount_comparing] 
WHERE [ammount_of_tasks] = [ammount_of_closed_tasks]) AS [IdsOfClosedProjects]
JOIN [Projects] ON [IdsOfClosedProjects].[project_id] = [Projects].[project_id]
JOIN [ProjectState] ON [Projects].[state_id] = [ProjectState].[state_id]

WHERE [Projects].[project_id] = [IdsOfClosedProjects].[project_id];


-- 11) �������� �� ���� ��������, ����� ���������� �� ������� �� ����� ���������� �����

SELECT [project_name], [employee_firstname], [employee_lastname] FROM
(SELECT [project_id], [employee_id] FROM
(
-- Taking all set of tasks
SELECT [task_id] FROM [Tasks]

EXCEPT
-- And substract from general set tasks with not closed state
SELECT [task_id] FROM [Tasks]
JOIN [TaskState] ON [Tasks].[state_id] = [TaskState].[state_id]
WHERE [state_name] != 'Closed') AS [TableWithNoIdsOfNotClosedTasks]
JOIN [TasksToEmployees] ON [TableWithNoIdsOfNotClosedTasks].[task_id] = [TasksToEmployees].[task_id]
JOIN [Tasks] ON [Tasks].[task_id] = [TableWithNoIdsOfNotClosedTasks].[task_id]) AS [ProjectEmployeeIds]
JOIN [Employees] ON [ProjectEmployeeIds].[employee_id] = [Employees].[employee_id]
JOIN [Projects] ON [ProjectEmployeeIds].[project_id] = [Projects].[project_id]
ORDER BY [project_name];

-- 12) �������� ������ (�� ��������) ������� ��������� �� ���������� � ����������� ����������� ����������� �� �����


GO

-- Because the relation type between Tasks and Employees is 'Many-many', the only way to give a task is add column in TaskToEmployees
-- And that is why we should check if the combination we want to add doesn't already exist, and we can't do it via a single query
-- so the only option here is do it via procedure

CREATE PROCEDURE AddTaskToEmployeeWithMinTasks
@task_name VARCHAR(250)
AS
BEGIN
	DECLARE @employee_id_with_min_tasks INT;
	DECLARE @employee_id_with_min_tasks_position INT;
	DECLARE @queried_task_id INT;
	DECLARE @position_id INT;
	DECLARE @does_employee_has_this_task INT;

	SELECT @employee_id_with_min_tasks = (SELECT TOP(1) [employee_id] FROM [TasksToEmployees]
	GROUP BY [employee_id]
	ORDER BY COUNT([task_id]));

	SELECT @queried_task_id = (SELECT TOP(1) [task_id] FROM [Tasks] WHERE [task_name] = @task_name);
	SELECT @position_id = (SELECT TOP(1) [position_id] FROM [TasksToEmployees] WHERE [employee_id] = @employee_id_with_min_tasks);

	SELECT @does_employee_has_this_task = (SELECT COUNT([employee_id]) FROM [TasksToEmployees]
	JOIN [Tasks] ON [TasksToEmployees].[task_id] = [Tasks].[task_id]
	WHERE [task_name] = @task_name
	AND [employee_id] = @employee_id_with_min_tasks);

	IF @does_employee_has_this_task <> 0
	BEGIN
		INSERT INTO [TasksToEmployees] (employee_id, task_id, position_id)
		VALUES (@employee_id_with_min_tasks, @queried_task_id, @position_id);
	END
END;

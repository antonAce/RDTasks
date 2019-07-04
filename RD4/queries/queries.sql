-- 1) Получить список всех должностей компании с количеством сотрудников на каждой из них

SELECT [position_name], [ammout_of_positions]
FROM [EmployeePositions] JOIN
(SELECT [position_id], COUNT([employee_id]) AS [ammout_of_positions]
FROM [TasksToEmployees]
GROUP BY [position_id]) AS [AmmountOfPositions]
ON [AmmountOfPositions].[position_id] = [EmployeePositions].[position_id];

-- 2) Определить список должностей компании, на которых нет сотрудников

SELECT [position_name] FROM
(SELECT [position_name], COUNT([employee_id]) AS [ammout_of_positions]
FROM [EmployeePositions]
LEFT JOIN [TasksToEmployees] ON [EmployeePositions].[position_id] = [TasksToEmployees].[position_id]
GROUP BY [position_name]) AS [EmpAmmountOfPositions]
WHERE [ammout_of_positions] = 0;


-- 3) Получить список проектов с указанием, сколько сотрудников каждой должности работает на проекте

SELECT [project_name], [position_name], [ammout_of_employees] FROM
(SELECT [project_id], [position_id], COUNT([employee_id]) AS [ammout_of_employees] FROM [Tasks]
JOIN [TasksToEmployees] ON [Tasks].[task_id] = [TasksToEmployees].[task_id]
GROUP BY [project_id], [position_id]) AS [ProjectsPositionsEmployees]
JOIN [EmployeePositions] ON [EmployeePositions].[position_id] = [ProjectsPositionsEmployees].[position_id]
JOIN [Projects] ON [Projects].[project_id] = [ProjectsPositionsEmployees].[project_id]
ORDER BY [project_name], [position_name];


-- 4) Посчитать на каждом проекте, какое в среднем количество задач приходится на каждого сотрудника

SELECT [project_name], [avg_tasks] FROM
(SELECT [project_id], AVG([tasks_count]) AS [avg_tasks] FROM
(SELECT [project_id], [employee_id], COUNT([TasksToEmployees].[task_id]) AS [tasks_count] FROM [Tasks]
JOIN [TasksToEmployees] ON [Tasks].[task_id] = [TasksToEmployees].[task_id]
GROUP BY [project_id], [employee_id]) AS [ProjectsEmployeesCount]
GROUP BY [project_id]) AS [ProjectToAvg]
JOIN [Projects] ON [ProjectToAvg].[project_id] = [Projects].[project_id];

-- 5) Подсчитать длительность выполнения каждого проекта

-- Подсчет только по дням
SELECT DATEDIFF(DAY, [project_start_date], [project_end_date]) FROM [Projects];

-- Подсчет по дням + по годам
SELECT CONCAT('Years:', [total_day_spent] / 365, ', Days:', [total_day_spent] % 365) FROM
(SELECT DATEDIFF(DAY, [project_start_date], [project_end_date]) AS [total_day_spent] FROM [Projects]) AS [DaysSpent];

-- 6) Определить сотрудников с минимальным количеством незакрытых задач

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

-- 7) Определить сотрудников с максимальным количеством незакрытых задач, дедлайн которых уже истек

SELECT [employee_firstname], [employee_lastname] FROM
(SELECT [employee_id] FROM
(
SELECT MAX([ammount_of_tasks]) AS [min_of_tasks] FROM
(
SELECT [employee_id], COUNT([TaskState].[state_name]) AS [ammount_of_tasks] FROM [Tasks]
JOIN [TasksToEmployees] ON [Tasks].[task_id] = [TasksToEmployees].[task_id]
JOIN [TaskState] ON [Tasks].[state_id] = [TaskState].[state_id]
WHERE [state_name] != 'Closed'
-- Here is right line of code below, but it returns an empty set, cuz all tasks still don't reach their deadline.
AND [task_deadline] < GETDATE()
-- So below is test query constraint
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
-- Here is right line of code below, but it returns an empty set, cuz all tasks still don't reach their deadline.
AND [task_deadline] < GETDATE()
-- So below is test query constraint
--AND [task_deadline] < convert(datetime2, '22-07-2019 10:25:00', 105)
GROUP BY [employee_id]
) AS [StatesOfTasksForEmployees]
ON [MinAmmountOfTasks].[min_of_tasks] = [StatesOfTasksForEmployees].[ammount_of_tasks]
) AS [EmployeesWithMinNotClosedTasks]
JOIN [Employees] ON [Employees].[employee_id] = [EmployeesWithMinNotClosedTasks].[employee_id];

-- 8) Продлить дедлайн незакрытых задач на 5 дней



-- 9) Посчитать на каждом проекте количество задач, к которым еще не приступили

SELECT [project_name], [ammount_of_not_started_tasks] FROM
(SELECT [Projects].[project_id], COUNT([TaskStateInProjectFiltered].[state_id]) AS [ammount_of_not_started_tasks] FROM
(SELECT [TaskStateInProject].[project_id], [TaskStateInProject].[state_id] FROM
(SELECT [Projects].[project_id], [Tasks].[state_id] FROM [Projects]
JOIN [Tasks] ON [Projects].[project_id] = [Tasks].[project_id]) AS [TaskStateInProject]
JOIN [TaskState] ON [TaskStateInProject].[state_id] = [TaskState].[state_id]
WHERE [TaskState].[state_name] = 'Open') AS [TaskStateInProjectFiltered]
RIGHT JOIN [Projects] ON [TaskStateInProjectFiltered].[project_id] = [Projects].[project_id]
GROUP BY [Projects].[project_id]) AS [AmmountOfNotStartedTasksInProject]
JOIN [Projects] ON [AmmountOfNotStartedTasksInProject].[project_id] = [Projects].[project_id]
ORDER BY [ammount_of_not_started_tasks] DESC;

-- 10) Перевести проекты в состояние закрыт, для которых все задачи закрыты и задать время закрытия временем закрытия задачи проекта, принятой последней

-- 11) Выяснить по всем проектам, какие сотрудники на проекте не имеют незакрытых задач



-- 12) Заданную задачу (по названию) проекта перевести на сотрудника с минимальным количеством выполняемых им задач
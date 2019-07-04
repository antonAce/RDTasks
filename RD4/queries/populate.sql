-- 'TaskState' sample populating

INSERT INTO [TaskState] ([state_id],[state_name])
VALUES (0, 'Open')

INSERT INTO [TaskState] ([state_id],[state_name])
VALUES (1, 'Done')

INSERT INTO [TaskState] ([state_id],[state_name])
VALUES (2, 'Redo')

INSERT INTO [TaskState] ([state_id],[state_name])
VALUES (3, 'Closed')

-- 'ProjectState' sample populating

INSERT INTO [ProjectState] ([state_id],[state_name])
VALUES (0, 'Open')

INSERT INTO [ProjectState] ([state_id],[state_name])
VALUES (1, 'Closed')

-- 'EmployeePositions' sample populating

INSERT INTO [EmployeePositions] ([position_id],[position_name])
VALUES (0, 'C# Backend Web Developer')

INSERT INTO [EmployeePositions] ([position_id],[position_name])
VALUES (1, 'Angular Frontend Web Developer')

INSERT INTO [EmployeePositions] ([position_id],[position_name])
VALUES (2, 'Embedded C++ Developer')

INSERT INTO [EmployeePositions] ([position_id],[position_name])
VALUES (3, 'Project Manager')

INSERT INTO [EmployeePositions] ([position_id],[position_name])
VALUES (4, 'Business Analyst')

INSERT INTO [EmployeePositions] ([position_id],[position_name])
VALUES (5, 'HR')

INSERT INTO [EmployeePositions] ([position_id],[position_name])
VALUES (6, 'Data Scientists')

INSERT INTO [EmployeePositions] ([position_id],[position_name])
VALUES (7, 'Data Engineer')

INSERT INTO [EmployeePositions] ([position_id],[position_name])
VALUES (8, 'Web UI Designer')

INSERT INTO [EmployeePositions] ([position_id],[position_name])
VALUES (9, 'Security Tester')

INSERT INTO [EmployeePositions] ([position_id],[position_name])
VALUES (10, 'C# Unity Game Developer')

-- 'Projects' sample populating

INSERT INTO [Projects] ([project_id], [project_name], [project_start_date], [project_end_date], [state_id])
VALUES (0, 'Online photo-album for EPAM', convert(datetime2, '01-06-2019 12:00:00', 105), convert(datetime2, '01-08-2019 00:00:00', 105), 0)

INSERT INTO [Projects] ([project_id], [project_name], [project_start_date], [project_end_date], [state_id])
VALUES (1, 'Fortnite 2', convert(datetime2, '12-12-2018 00:00:00', 105), convert(datetime2, '01-01-2021 00:00:00', 105), 0)

INSERT INTO [Projects] ([project_id], [project_name], [project_start_date], [project_end_date], [state_id])
VALUES (2, 'PI Blog: the IT Blog', convert(datetime2, '06-05-2017 16:45:00', 105), convert(datetime2, '06-11-2018 00:00:00', 105), 1)

INSERT INTO [Projects] ([project_id], [project_name], [project_start_date], [project_end_date], [state_id])
VALUES (3, 'BDJE: The Music Game', convert(datetime2, '11-12-2016 10:25:00', 105), convert(datetime2, '06-03-2017 10:25:00', 105), 1)

INSERT INTO [Projects] ([project_id], [project_name], [project_start_date], [project_end_date], [state_id])
VALUES (4, 'Tesla Roadster Project', convert(datetime2, '12-01-2016 12:00:00', 105), convert(datetime2, '01-01-2022 12:00:00', 105), 0)

-- 'Tasks' sample populating

INSERT INTO [Tasks] ([task_id], [task_name], [task_description], [task_deadline], [project_id], [state_id])
VALUES (0, 'Name the project', 'Create a brand-new name for the blog and check if this name is unique', convert(datetime2, '05-07-2019 12:00:00', 105), 0, 2)

INSERT INTO [Tasks] ([task_id], [task_name], [task_description], [task_deadline], [project_id], [state_id])
VALUES (1, 'Make DB structure', 'Create a physical model of database and populate it with sample data', convert(datetime2, '10-07-2019 12:00:00', 105), 0, 0)

INSERT INTO [Tasks] ([task_id], [task_name], [task_description], [task_deadline], [project_id], [state_id])
VALUES (2, 'Set up the environment', 'Install all required software and read all documentation', convert(datetime2, '24-06-2019 11:00:00', 105), 0, 3)

INSERT INTO [Tasks] ([task_id], [task_name], [task_description], [task_deadline], [project_id], [state_id])
VALUES (3, 'Create a Telegram channel', NULL, convert(datetime2, '08-05-2017 16:45:00', 105), 2, 3)

INSERT INTO [Tasks] ([task_id], [task_name], [task_description], [task_deadline], [project_id], [state_id])
VALUES (4, 'UI/UX Design of the site', NULL, convert(datetime2, '01-08-2019 12:00:00', 105), 2, 0)

INSERT INTO [Tasks] ([task_id], [task_name], [task_description], [task_deadline], [project_id], [state_id])
VALUES (5, 'Adding roadster to the website', 'Adding new column with images and characteristics in a cars section of the site', convert(datetime2, '15-01-2017 00:00:00', 105), 4, 3)

-- 'Employees' sample populating

INSERT INTO [Employees] ([employee_id], [employee_firstname], [employee_lastname])
VALUES (0, 'Elon', 'Musk')

INSERT INTO [Employees] ([employee_id], [employee_firstname], [employee_lastname])
VALUES (1, 'Kevin', 'Mitnic')

INSERT INTO [Employees] ([employee_id], [employee_firstname], [employee_lastname])
VALUES (2, 'Evgeniy', 'Morozuk')

INSERT INTO [Employees] ([employee_id], [employee_firstname], [employee_lastname])
VALUES (3, 'Andrew', 'Kondratuk')

INSERT INTO [Employees] ([employee_id], [employee_firstname], [employee_lastname])
VALUES (4, 'Anton', 'Kozyriev')

INSERT INTO [Employees] ([employee_id], [employee_firstname], [employee_lastname])
VALUES (5, 'Toby', 'Fox')

INSERT INTO [Employees] ([employee_id], [employee_firstname], [employee_lastname])
VALUES (6, 'Pavel', 'Borysenko')

-- 'TasksToEmployees' sample populating

INSERT INTO [TasksToEmployees] ([task_id], [employee_id], [position_id])
VALUES (0, 4, 0)

INSERT INTO [TasksToEmployees] ([task_id], [employee_id], [position_id])
VALUES (1, 4, 3)

INSERT INTO [TasksToEmployees] ([task_id], [employee_id], [position_id])
VALUES (2, 4, 0)

INSERT INTO [TasksToEmployees] ([task_id], [employee_id], [position_id])
VALUES (3, 2, 1)

INSERT INTO [TasksToEmployees] ([task_id], [employee_id], [position_id])
VALUES (3, 3, 6)

INSERT INTO [TasksToEmployees] ([task_id], [employee_id], [position_id])
VALUES (3, 4, 3)

INSERT INTO [TasksToEmployees] ([task_id], [employee_id], [position_id])
VALUES (4, 4, 0)

INSERT INTO [TasksToEmployees] ([task_id], [employee_id], [position_id])
VALUES (4, 2, 1)

INSERT INTO [TasksToEmployees] ([task_id], [employee_id], [position_id])
VALUES (5, 0, 3)

INSERT INTO [TasksToEmployees] ([task_id], [employee_id], [position_id])
VALUES (5, 4, 0)
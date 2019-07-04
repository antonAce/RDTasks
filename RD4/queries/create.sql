-- 'TaskState' table

CREATE TABLE [TaskState] (
	[state_id] INT NOT NULL,
	[state_name] VARCHAR(250) NOT NULL
)

ALTER TABLE [TaskState] ADD CONSTRAINT [TaskStatePK] PRIMARY KEY CLUSTERED ([state_id] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON);

-- 'ProjectState' table

CREATE TABLE [ProjectState] (
	[state_id] INT NOT NULL,
	[state_name] VARCHAR(250) NOT NULL
)

ALTER TABLE [ProjectState] ADD CONSTRAINT [ProjectStatePK] PRIMARY KEY CLUSTERED ([state_id] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON);

-- 'EmployeePositions' table

CREATE TABLE [EmployeePositions] (
	[position_id] INT NOT NULL,
	[position_name] VARCHAR(250) NOT NULL
)

ALTER TABLE [EmployeePositions] ADD CONSTRAINT [EmployeePositionsPK] PRIMARY KEY CLUSTERED ([position_id] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON);

-- 'Projects' table

CREATE TABLE [Projects] (
	[project_id] INT NOT NULL,
	[project_name] VARCHAR(250) NOT NULL,
	[project_start_date] DATETIME2(7) NULL,
	[project_end_date] DATETIME2(7) NULL,
	[state_id] INT NOT NULL
)

ALTER TABLE [Projects] ADD CONSTRAINT [ProjectsPK] PRIMARY KEY CLUSTERED ([project_id] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON);
ALTER TABLE [Projects] ADD CONSTRAINT [ProjectsToStateFK] FOREIGN KEY ([state_id]) REFERENCES [ProjectState]([state_id])

-- 'Tasks' table

CREATE TABLE [Tasks] (
	[task_id] INT NOT NULL,
	[task_name] VARCHAR(250) NOT NULL,
	[task_description] VARCHAR(2000) NULL,
	[task_deadline] DATETIME2(7) NOT NULL,
	[project_id] INT NOT NULL,
	[state_id] INT NOT NULL
)

ALTER TABLE [Tasks] ADD CONSTRAINT [TasksPK] PRIMARY KEY CLUSTERED ([task_id] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON);
ALTER TABLE [Tasks] ADD CONSTRAINT [TasksProjectsFK] FOREIGN KEY ([project_id]) REFERENCES [Projects]([project_id])
ALTER TABLE [Tasks] ADD CONSTRAINT [TasksTaskStateFK] FOREIGN KEY ([state_id]) REFERENCES [TaskState]([state_id])

-- 'Employees' table

CREATE TABLE [Employees] (
	[employee_id] INT NOT NULL,
	[employee_firstname] VARCHAR(250) NOT NULL,
	[employee_lastname] VARCHAR(250) NOT NULL
)

ALTER TABLE [Employees] ADD CONSTRAINT [EmployeesPK] PRIMARY KEY CLUSTERED ([employee_id] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON);

-- 'TasksToEmployees' table

CREATE TABLE [TasksToEmployees](
	[task_id] INT NOT NULL,
	[employee_id] INT NOT NULL,
	[position_id] INT NOT NULL
)

ALTER TABLE [TasksToEmployees] ADD CONSTRAINT [TasksToEmployeesPK] PRIMARY KEY CLUSTERED ([task_id] ASC, [employee_id] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON);

ALTER TABLE [TasksToEmployees] ADD CONSTRAINT [TasksToEmployeesToPositionsFK] FOREIGN KEY([position_id]) REFERENCES [EmployeePositions]([position_id])
ALTER TABLE [TasksToEmployees] ADD CONSTRAINT [TableToEmployeesFK] FOREIGN KEY([employee_id]) REFERENCES [Employees]([employee_id])
ALTER TABLE [TasksToEmployees] ADD CONSTRAINT [TableToTasksFK] FOREIGN KEY([task_id]) REFERENCES [Tasks]([task_id])
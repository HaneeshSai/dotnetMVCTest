Create database Exammanage

use Exammanage


-- Create Adminlogin Table
CREATE TABLE Adminlogin (
    AdminId INT PRIMARY KEY,
    AdminName VARCHAR(50) NOT NULL,
    Password VARCHAR(50) NOT NULL
);

-- Create Cours Table
CREATE TABLE Courses (
    courseid INT PRIMARY KEY  IDENTITY(1,1),
    coursename VARCHAR(100) NOT NULL,
    Descriptions Varchar(100)
);

-- Create Studentdetail Table
CREATE TABLE Studentdetails (
    studentid INT PRIMARY KEY,
    fullname VARCHAR(100) NOT NULL,
    username VARCHAR(50) NOT NULL,
    password VARCHAR(50) NOT NULL,
    location VARCHAR(100),
    email VARCHAR(100),
    mobile VARCHAR(15)
);

-- Create Exam Table
CREATE TABLE Exams (
    Examid INT PRIMARY KEY,
    courseid INT,
    Examdate DATE,
    Duration VARCHAR(20),
    FOREIGN KEY (courseid) REFERENCES Courses(courseid)
);

-- Create Question Table
CREATE TABLE Questions (
    QuestionID INT PRIMARY KEY  IDENTITY(1,1),
    Examid INT,
    Courseid INT,
    QuestionText TEXT NOT NULL,
    OptionA VARCHAR(255),
    OptionB VARCHAR(255),
    OptionC VARCHAR(255),
    OptionD VARCHAR(255),
    CorrectOption CHAR(1),
    FOREIGN KEY (Examid) REFERENCES Exams(Examid)
);

-- Create StudentAnswer Table
CREATE TABLE StudentAnswers (
    StudentAnswerId INT PRIMARY KEY IDENTITY(1,1),
    studentid INT,
    QuestionID INT,
    SelectedAnswer CHAR(1),
    Examid INT,
    FOREIGN KEY (studentid) REFERENCES Studentdetails(studentid),
    FOREIGN KEY (QuestionID) REFERENCES Questions(QuestionID),
    FOREIGN KEY (Examid) REFERENCES Exams(Examid)
);

select @@VERSION

-- Create StudentExam Table
CREATE TABLE StudentExamApplications (
    Studentappid INT PRIMARY KEY IDENTITY(1,1),
    Examid INT,
    studentid INT,
    applieddate DATE,
    coursename VARCHAR(100),
    FOREIGN KEY (Examid) REFERENCES Exams(Examid),
    FOREIGN KEY (studentid) REFERENCES Studentdetails(studentid)
);

-- Create StudentMark Table
CREATE TABLE StudentMarks (
    id INT PRIMARY KEY  IDENTITY(1,1),
    studentid INT,
    Examid INT,
    marks INT,
    studentname VARCHAR(100),
    FOREIGN KEY (studentid) REFERENCES Studentdetails(studentid),
    FOREIGN KEY (Examid) REFERENCES Exams(Examid)
);

-- Insert data into Adminlogin Table
INSERT INTO Adminlogin (AdminId, AdminName, Password) VALUES
(1, 'admin', 'admin123'),
(2, 'manager', 'manager123');

-- Insert data into Cours Table
INSERT INTO Courses (courseid, coursename, Descriptions) VALUES
(1, 'Physics', 'Introduction to Physics'),
(2, 'Chemistry', 'Basics of Chemistry'),
(3, 'Mathematics', 'Advanced Mathematics');

-- Insert data into Studentdetail Table
INSERT INTO Studentdetails (studentid, fullname, username, password, location, email, mobile) VALUES
(1, 'Alice Smith', 'alice123', 'password1', 'California', 'alice@example.com', '1234567890'),
(2, 'Bob Johnson', 'bobj', 'password2', 'Texas', 'bob@example.com', '0987654321'),
(3, 'Carol Williams', 'carolw', 'password3', 'New York', 'carol@example.com', '1122334455');

-- Insert data into Exam Table
INSERT INTO Exams (Examid, courseid, Examdate, Duration) VALUES
(1, 1, '2024-10-15', '2 hours'),
(2, 2, '2024-10-20', '3 hours'),
(3, 3, '2024-10-25', '1.5 hours');

-- Insert data into Question Table
INSERT INTO Questions (QuestionID, Examid, Courseid, QuestionText, OptionA, OptionB, OptionC, OptionD, CorrectOption) VALUES
(1, 1, 1, 'What is the speed of light?', '299,792 km/s', '300,000 km/s', '150,000 km/s', '100,000 km/s', 'A'),
(2, 2, 2, 'What is the chemical symbol for water?', 'O2', 'H2O', 'CO2', 'H2O2', 'B'),
(3, 3, 3, 'What is 2 + 2?', '3', '4', '5', '6', 'B');

-- Insert data into StudentAnswer Table
INSERT INTO StudentAnswers (StudentAnswerId, studentid, QuestionID, SelectedAnswer, Examid) VALUES
(1, 1, 1, 'A', 1),
(2, 2, 2, 'B', 2),
(3, 3, 3, 'B', 3);

-- Insert data into StudentExam Table
INSERT INTO StudentExamApplications (Studentappid, Examid, studentid, applieddate, coursename) VALUES
(1, 1, 1, '2024-10-10', 'Physics'),
(2, 2, 2, '2024-10-15', 'Chemistry'),
(3, 3, 3, '2024-10-20', 'Mathematics');

-- Insert data into StudentMark Table
INSERT INTO StudentMarks (id, studentid, Examid, marks, studentname) VALUES
(1, 1, 1, 90, 'Alice Smith'),
(2, 2, 2, 85, 'Bob Johnson'),
(3, 3, 3, 95, 'Carol Williams');

-- Drop StudentMark Table (no dependencies)
DROP TABLE IF EXISTS StudentMarks;

-- Drop StudentExam Table (no dependencies)
DROP TABLE IF EXISTS StudentExamApplications;

-- Drop StudentAnswer Table (no dependencies)
DROP TABLE IF EXISTS StudentAnswers;

-- Drop Question Table (depends on Exam and Cours)
DROP TABLE IF EXISTS Questions;

-- Drop Exam Table (depends on Cours)
DROP TABLE IF EXISTS Exam;

-- Drop Studentdetail Table (no dependencies)
DROP TABLE IF EXISTS Studentdetail;

-- Drop Cours Table (no dependencies)
DROP TABLE IF EXISTS Courses;

-- Drop Adminlogin Table (no dependencies)
DROP TABLE IF EXISTS Adminlogin;



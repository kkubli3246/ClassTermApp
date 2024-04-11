using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SQLite;
using Terms.Models;

namespace Terms.Services
{
    public static class DatabaseService
    {
        private static SQLiteAsyncConnection _db;
        private static SQLiteConnection _dbConnection;
        public static async Task Init()
        {
            if (_db != null) //Don't create db if it already exists
            {
                return;
            }

            //Get absolute path to the database file
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "Terms.db");

            _db = new SQLiteAsyncConnection(databasePath);


            //@@@@@@Switched these
            await _db.CreateTableAsync<Course>();
            await _db.CreateTableAsync<Term>();
        }

        #region Term Methods
        public static async Task AddTerm(string name, DateTime startTermDate, DateTime endTermDate)
        {
            await Init();
            var term = new Term()
            {
                termName = name,
                start = startTermDate,
                end = endTermDate
            };

            await _db.InsertAsync(term);

            var id = term.termId; //Returns the TermId
        }

        public static async Task RemoveTerm(int id)
        {
            await Init();

            await _db.DeleteAsync<Term>(id);
        }

        public static async Task<IEnumerable<Term>> GetTerms()
        {
            await Init();

            var terms = await _db.Table<Term>().ToListAsync();
            return terms;
        }

        public static async Task UpdateTerm(int id, string name, DateTime startTermDate, DateTime endTermDate)
        {
            await Init();

            var termQuery = await _db.Table<Term>()
                .Where(i => i.termId == id)
                .FirstOrDefaultAsync();

            if (termQuery != null)
            {
                termQuery.termName = name;
                termQuery.start = startTermDate;
                termQuery.end = endTermDate;

                await _db.UpdateAsync(termQuery);
            }
        }
        #endregion

        #region Course Methods
        public static async Task AddCourse(int termId, string courseName, string instructorName, string instructorPhone, string instructorEmail, string status, DateTime startDate, DateTime endDate,
            string noteDetails, string paName, string oaName, DateTime oaStart, DateTime oaEnd, DateTime paStart, DateTime paEnd)
        {
            await Init();
            var course = new Course
            {
                termID = termId,
                CourseName = courseName,
                instructorName = instructorName,
                instructorPhone = instructorPhone,
                instructorEmail = instructorEmail,
                Status = status,
                start = startDate,
                end = endDate,
                paName = paName,
                oaName = oaName,
                noteDetails = noteDetails,
                oaStart = oaStart,
                oaEnd = oaEnd,
                paStart = paStart,
                paEnd = paEnd
            };


            await _db.InsertAsync(course);

            var id = course.courseId; //Returns the CourseId
        }
        public static async Task RemoveCourse(int id)
        {
            await Init();

            await _db.DeleteAsync<Course>(id);
        }

        public static async Task<IEnumerable<Course>> GetCourses(int termID)
        {
            await Init();

            var courses = await _db.Table<Course>().Where(i => i.termID == termID).ToListAsync();

            return courses;
        }

        public static async Task<IEnumerable<Course>> GetCourses()
        {
            await Init();

            var courses = await _db.Table<Course>().ToListAsync();
            
            return courses;
        }

        public static async Task UpdateCourse(int id, string courseName, string instructorName, string instructorPhone, string instructorEmail, string status, DateTime startDate, DateTime endDate,
            string noteDetails, string paName, string oaName, DateTime oaStart, DateTime oaEnd, DateTime paStart, DateTime paEnd)
        {
            await Init();

            var courseQuery = await _db.Table<Course>()
                .Where(i => i.courseId == id)
                .FirstOrDefaultAsync();

            if(courseQuery != null) 
            {
                courseQuery.CourseName = courseName;
                courseQuery.instructorName = instructorName;
                courseQuery .instructorPhone = instructorPhone;
                courseQuery.instructorEmail = instructorEmail;
                courseQuery.Status = status;
                courseQuery.start = startDate;
                courseQuery.end = endDate;
                courseQuery.noteDetails = noteDetails;
                courseQuery.paName = paName;
                courseQuery.oaName = oaName;
                courseQuery.oaStart = oaStart;
                courseQuery.oaEnd = oaEnd;
                courseQuery.paStart = paStart;
                courseQuery.paEnd = paEnd;

                await _db.UpdateAsync(courseQuery);
            }
        }
        #endregion

        #region User Methods
        public static async Task AddUser(string username, string password)
        {
            await Init();
            var user = new UserInfo()
            {
                UserName = username,
                Password = password
            };

            await _db.InsertAsync(user);

            var id = user.UserId; //Returns the UserId
        }

        public static async Task RemoveUser(int id)
        {
            await Init();

            await _db.DeleteAsync<UserInfo>(id);
        }

        public static async Task<IEnumerable<UserInfo>> GetUser()
        {
            await Init();

            var users = await _db.Table<UserInfo>().ToListAsync();
            return users;
        }

        public static async Task UpdateUser(int id, string username, string password)
        {
            await Init();

            var userQuery = await _db.Table<UserInfo>()
                .Where(i => i.UserId == id)
                .FirstOrDefaultAsync();

            if (userQuery != null)
            {
                userQuery.UserName = username;
                userQuery.Password = password;

                await _db.UpdateAsync(userQuery);
            }
        }
        #endregion

        #region DemoData

        public static async void LoadSampleData()
        {
            await Init();

            Term term = new Term
            {
                termName = "Term 1",
                start = DateTime.Today.Date,
                end = DateTime.Now.AddMonths(1)
            };


            await _db.InsertAsync(term);
                       

            Course course = new Course
            {
                termID = term.termId,
                termName = term.termName,
                instructorName = "Anika Patel",
                instructorPhone = "555-123-4567",
                instructorEmail = "anika.patel@strimeuniversity.edu",
                CourseName = "English 101",
                Status = "In Progress",
                start = DateTime.Now.Date,
                end = DateTime.Now.AddMonths(1),
                paName = "Performance Assessment 1",
                oaName = "Objective Assessment 1",
                paStart = DateTime.Now.Date,
                paEnd = DateTime.Now.AddMonths(1),
                oaStart = DateTime.Now.Date,
                oaEnd = DateTime.Now.AddMonths(1),
                startNotification = true,
                endNotification = false,
                noteDetails = "Test"
            };

            await _db.InsertAsync(course);
        }

        public static async Task ClearSampleData()
        {
            await Init();

            await _db.DropTableAsync<Course>();
            await _db.DropTableAsync<Term>();
            _db = null;
            _dbConnection = null;
        }

        public static async void LoadSampleDataSql()
        {
            await Init();

            Term term = new Term
            {
                termName = "Term 1",
                start = DateTime.Today.Date,
                end = DateTime.Now.AddMonths(1)
            };

            await _db.InsertAsync(term);

            Course course = new Course
            {
                termID = term.termId,
                termName = term.termName,
                instructorName = "Anika Patel",
                instructorPhone = "555-123-4567",
                instructorEmail = "anika.patel@strimeuniversity.edu",
                CourseName = "English 101",
                Status = "In Progress",
                start = DateTime.Now.Date,
                end = DateTime.Now.AddMonths(1),
                paName = "Performance Assessment 1",
                oaName = "Objective Assessment 1",
                paStart = DateTime.Now.Date,
                paEnd = DateTime.Now.AddMonths(1),
                oaStart = DateTime.Now.Date,
                oaEnd = DateTime.Now.AddMonths(1),
                startNotification = true,
                endNotification = false,
                noteDetails = "Test"
            };

            await _db.InsertAsync(course);

            Course course2 = new Course
            {
                termID = term.termId,
                termName = term.termName,
                instructorName = "Anika Patel",
                instructorPhone = "555-123-4567",
                instructorEmail = "anika.patel@strimeuniversity.edu",
                CourseName = "Science 101",
                Status = "Completed",
                start = DateTime.Today.AddMonths(-1),
                end = DateTime.Now.Date,
                paName = "Performance Assessment 2",
                oaName = "Objective Assessment 2",
                paStart = DateTime.Now.Date,
                paEnd = DateTime.Now.AddMonths(1),
                oaStart = DateTime.Now.Date,
                oaEnd = DateTime.Now.AddMonths(1),
                startNotification = true,
                endNotification = false,
                noteDetails = "Test"
            };

            await _db.InsertAsync(course2);
        }

        #endregion

        #region Count Determinations

        public static async Task<int> GetCourseCountAsync(int selectedTermId)
        {
            //TODO getting a course count from a table

            //int courseCount = await _db.ExecuteScalarAsync<int>("Select Count(*) from Course where TermId = '" + selectedTermId + "'"); //Works
            //int courseCount = await _db.ExecuteScalarAsync<int>($"Select Count(*) from Course where TermId = '{selectedTermId}'"); //Works
            int courseCount = await _db.ExecuteScalarAsync<int>($"Select Count(*) from Course where termId = ?", selectedTermId); //Works
            //int courseCount = await _db.ExecuteScalarAsync<int>("Select Count(*) from Course");
            //var objectiveCount = await _conn.QueryAsync<Assessment>($"Select Type From Assessments Where Course = '{_course.Id}' And Type = 'Objective'");
            //var performanceCount = await _conn.QueryAsync<Assessment>($"Select Type From Assessments Where Course = '{_course.Id}' And Type = 'Performance'");

            return courseCount;
        }


        #endregion

        #region Looping through table records

        public static async void LoopingTermTable()
        {
            // Change the SELECT statement to return only those record that have notifications indicated
            // Create Notification on start of program

            await Init();

            var allTermRecords = _dbConnection.Query<Term>("SELECT * FROM Term");

            foreach (var termRecord in allTermRecords)
            {
                Console.WriteLine("Name " + termRecord.termName);
            }
        }

        public static async Task<List<Term>> GetNotifyTermsAsync()
        {
            await Init();
            var records = _dbConnection.Query<Term>("SELECT * FROM Term");

            return records;
        }

        public static async Task<IEnumerable<Term>> GetNotificationTerms()
        {
            //Change the SELECT statment to return only those records that have notifications indicated
            //Create Notification on start of program
            //Not used for this program but code is in place to show how it might work
            //See Dashboard.xaml.cs constructor

            await Init();
            var allTermRecords = _dbConnection.Query<Term>("SELECT * FROM Term");

            return allTermRecords;
        }

        #endregion

    }
}
